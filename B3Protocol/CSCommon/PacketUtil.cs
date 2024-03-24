using B3.Protocol;
using Bass.Net;
using Google.FlatBuffers;
using System;

namespace B3.CSCommon
{
    public static class PacketUtil
    {
        // 수신한 패킷으로부터 FlatBuffer 메시지를 만듭니다
        public static T GetData<T>(this Packet pPacket) where T : IFlatbufferObject, new()
        {
            if (null == pPacket)
                return default;

            T pRet = new T();
            ByteBuffer bb = new ByteBuffer(pPacket.Binary, Packet.PACKET_DATA_OFFSET);
            try
            {
                pRet.__init(bb.GetInt(bb.Position) + bb.Position, bb);
            }
            catch (Exception)
            {
                return default;
            }

            return pRet;
        }

        // FlatBuffer 메시지 버퍼로부터 패킷을 만듭니다.
        public static Packet SetData(this FlatBufferBuilder fbb, EPacketProtocol protocol)
        {
            var pData = fbb.SizedByteArray();
            if (pData.Length > Packet.MAX_PACKET_DATA_SIZE)
                return null;    // 패킷 사이즈 오버...

            Packet pPacket = new Packet((int)protocol, pData, pData.Length);
            return pPacket;
        }



    }
}
