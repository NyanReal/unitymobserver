using B3.Protocol;

namespace B3.CSCommon
{
    public static class PacketExtension
    {
        public static string StringMessage(this CSMoveReq msg)
        {
            return $"C => S CSMoveReq Pos ({msg.X}, {msg.Y}, {msg.Z}) Rotation ({msg.Rotation}) ";
        }

        public static string StringMessage(this SCMoveRes msg)
        {
            return $"S => C CSMoveRes User ({msg.UserID}) Pos ({msg.X}, {msg.Y}, {msg.Z}) Rotation ({msg.Rotation}) ";
        }


        public static string StringMessage(this SCUserInfoNotify msg)
        {
            return $"S => C SCUserInfoNotify My UserID ({msg.MyUserID})";
        }

        public static string StringMessage(this SCLeaveNotify msg)
        {
            return $"S => C SCLeaveNotify User ({msg.UserID}) Leave";
        }

    }
}
