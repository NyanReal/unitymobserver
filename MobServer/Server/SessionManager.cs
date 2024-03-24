using System.Collections.Generic;

namespace B3.Server
{
    public class SessionManager : Singleton<SessionManager>
    {
        // <SessionIndex, PingTick>
        private Dictionary<int, long> mSessionList = new Dictionary<int, long>();
        private object mLock = new object();    


        public void Connect(int sessionIndex)
        {
            lock(mLock)
            {
                if(false == mSessionList.ContainsKey(sessionIndex))
                    mSessionList.Add(sessionIndex, 0);
            }
        }

        public void Disconnect(int sessionIndex)
        {
            lock(mLock)
                mSessionList.Remove(sessionIndex);
        }

        public bool SetPingTime(int sessionIndex, long tick)
        {
            lock(mLock)
            {
                if (false == mSessionList.ContainsKey(sessionIndex))
                    return false;

                mSessionList[sessionIndex] = tick;
            }
            return true;
        }


        public long GetLastPingTick(int sessionIndex)
        {
            lock (mLock)
            {
                if (false == mSessionList.ContainsKey(sessionIndex))
                    return -1;

                return mSessionList[sessionIndex];
            }
        }



    }
}
