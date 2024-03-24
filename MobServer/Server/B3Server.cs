using B3.CSCommon;
using B3.Protocol;
using Bass;
using Bass.Net;
using Bass.Net.Server;
using Google.FlatBuffers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace B3.Server
{
    public class B3Server : Singleton<B3Server>
    {
        private ServerSocket mServer = new ServerSocket();
        private bool mIsStarted = false;
        //private NetworkEvent mEvent = new NetworkEvent();

        private PacketDispatcher mPacketDispatcher;

        private List<int> mDisconnectSessionList = new List<int>();
        private object mDisconnectLock = new object();
        

        private long mLastCalcTick = 0;
        private Thread? mThread = null;




        public B3Server()
        {
            mPacketDispatcher = new PacketDispatcher(this);

            mServer.OnConnected = _OnConnected;
            mServer.OnDisconnected = _OnDisconnected;
            mServer.OnPacketReceived = _OnReceived;

        }

        public void Start()
        {
            if (mIsStarted)
                return;

            mServer.StartServer(CommonData.SERVER_PORT);

            mIsStarted = true;

            if (null != mThread)
                mThread = null;

            mThread = new Thread(_Run);
            mThread.Start();
        }



        #region Network Event Delegate

        private bool _OnReceived(Packet packet)
        {
            mPacketDispatcher.Push(packet);
            return true;
        }

        private bool _OnDisconnected(int sessionIndex, string peerip)
        {
            SessionManager.Instance.Disconnect(sessionIndex);

            Console.WriteLine($"Client [{sessionIndex}]({peerip}) disconnected.");
            lock (mDisconnectLock)
                mDisconnectSessionList.Add(sessionIndex);
            return true;
        }

        private bool _OnConnected(int sessionIndex, string ip)
        {
            SessionManager.Instance.Connect(sessionIndex);

            Console.WriteLine($"Client [{sessionIndex}] connected!! ({ip})");

            var fbb = new FlatBufferBuilder(1);
            var msg = SCUserInfoNotify.CreateSCUserInfoNotify(fbb, (short)sessionIndex);
            fbb.Finish(msg.Value);
            SendPacket(sessionIndex, fbb.SetData(EPacketProtocol.SC_UserInfoNotify));
            return true;
        }

        #endregion

        #region for DI applied Objects

        public bool SendPacket(int SessionIndex, Packet packet)
        {
            return mServer.SendToClient(SessionIndex, packet);
        }

        public bool SendToAll(Packet packet)
        {
            return mServer.SendToAll(packet);
        }

        //public bool SendToAllWithoutMe(Packet packet, int sessionIndex)
        //{
        //    return mServer.SendToAllWithoutMe(packet, sessionIndex);
        //}

        #endregion



        private void _Run()
        {
            mLastCalcTick = Clock.TickMS();

            while (true)
            {
                if (false == mPacketDispatcher.DispatchPacket())
                    Thread.Sleep(0);

                _DisconnectNotifyProcess();
                _MainLogic();
            }
        }


        private void _MainLogic()
        {
            if (mLastCalcTick + CommonData.SERVER_LOGIC_TIME_MS > Clock.TickMS())
                return; // 아직 연산할 시간이 아님
            mLastCalcTick = Clock.TickMS();

            // 아래 메인 로직 계산할꺼 있으면 할것

        }

        private void _DisconnectNotifyProcess()
        {
            List<int> workList = new List<int>();

            lock (mDisconnectLock)
                (workList, mDisconnectSessionList) = (mDisconnectSessionList, workList);
            var fbb = new FlatBufferBuilder(1);

            foreach (var sessionIndex in workList)
            {
                fbb.Clear();
                var msg = SCLeaveNotify.CreateSCLeaveNotify(fbb, (short)sessionIndex);
                fbb.Finish(msg.Value);
                mServer.SendToAll(fbb.SetData(EPacketProtocol.SC_LeaveNotify));
            }
        }




    }
}
