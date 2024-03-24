using B3.CSCommon;
using B3.Protocol;
using Bass.Net;
using Google.FlatBuffers;
using System.Collections.Generic;

namespace B3.Server
{
    public class PacketDispatcher
    {
        private B3Server mServer;

        private List<Packet> mWorkList = new List<Packet>();    // 작업용
        private List<Packet> mRecvList = new List<Packet>();
        private object mRecvLock = new object();

        private FlatBufferBuilder mFbb = new FlatBufferBuilder(Packet.MAX_PACKET_DATA_SIZE);
        private FlatBufferBuilder fbb => mFbb;


        public PacketDispatcher(B3Server server)
        {
            mServer = server;   // DI
        }

        /// <summary>
        /// 수신한 패킷을 처리하기 위해 저장합니다.
        /// </summary>
        /// <param name="pPacket"></param>
        public void Push(Packet pPacket)
        {
            if (null == pPacket)
                return;

            lock (mRecvLock)
                mRecvList.Add(pPacket);
        }



        /// <summary>
        /// 수신한 패킷을 처리합니다. <br/>
        /// </summary>
        /// <returns>처리한 패킷이 있는지 여부</returns>
        public bool DispatchPacket()
        {

            lock (mRecvLock)
                (mWorkList, mRecvList) = (mRecvList, mWorkList);

            if (mWorkList.Count == 0)
                return false;   // 작업할 패킷이 없음...

            bool bRet = false;
            foreach (var pPacket in mWorkList)
            {
                bRet = _DispatchPacket(pPacket);
                if (false == bRet)
                {
                    // Dispatch Packet Failure!!!
                }
            }

            mWorkList.Clear();

            return true;
        }


        #region Dispatch Packet List

        private bool _DispatchPacket(Packet pPacket)
        {
            if (null == pPacket)
                return false;

            bool bRet = false;
            fbb.Clear();    // 미리 앞에서 해주기.

            int sessionIndex = pPacket.SenderIndex;
            EPacketProtocol protocol = (EPacketProtocol)pPacket.Protocol;

            switch (protocol)
            {
                //case EPacketProtocol.CS_Ping:
                //    bRet = _OnCSPing(pPacket.SessionIndex, pPacket.GetData<CSPing>());
                //    break;
                //case EPacketProtocol.CS_Pong:
                //    bRet = _OnCSPong(pPacket.SessionIndex, pPacket.GetData<CSPong>());
                //    break;
                case EPacketProtocol.CS_MoveReq:
                    bRet = _OnCSMoveReq(sessionIndex, pPacket.GetData<CSMoveReq>());
                    break;
                default:
                    break;
            }


            return bRet;
        }




        //private bool _OnCSPing(int sessionIndex, CSPing msg)
        //{
        //    long nowtick = DateTime.UtcNow.Ticks;
        //    var ret = SCPang.CreateSCPang(fbb, msg.Tick, nowtick);
        //    fbb.Finish(ret.Value);

        //    SessionManager.Instance.SetPingTime(sessionIndex, nowtick);

        //    bool bRet = mServer.SendPacket(sessionIndex, fbb.SetData(EPacketProtocol.SC_Pang));
        //    return bRet;
        //}

        //private bool _OnCSPong(int sessionIndex, CSPong msg)
        //{



        //    return true;
        //}

        private bool _OnCSMoveReq(int sessionIndex, CSMoveReq msg)
        {
            var ret = SCMoveRes.CreateSCMoveRes(fbb,
                (short)sessionIndex,
                msg.X,
                msg.Y,
                msg.Z,
                msg.Rotation);

            fbb.Finish(ret.Value);
            bool bRet = mServer.SendToAll(fbb.SetData(EPacketProtocol.SC_MoveRes));
            return bRet;
        }





        #endregion

    }
}
