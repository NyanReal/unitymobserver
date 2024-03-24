using Bass;
using System.Collections.Generic;

namespace B3.User
{
    public class UserManager : Singleton<UserManager>
    {
        // <sessionIndex, UserData>
        private Dictionary<int, UserData> mUserList = new Dictionary<int, UserData>();




    }
}
