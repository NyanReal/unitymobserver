namespace B3.CSCommon
{
    /// <summary>
    /// 클라이언트 / 서버간 공통으로 사용할 수치.
    /// 보통은 엑셀파일이나 SQLite DB 파일로 로드해서 사용하겠지만서도...
    /// </summary>
    public class CommonData
    {
        public const int SERVER_PORT = 23333;   // 서버 수신 / 클라 접속 포트

        public const float MOVE_SPEED = 3.0f;   // 이동 속도. 초속.

        public const int MOVE_TICK_DELAY_MS = 200;  // 패킷 전송 딜레이 ms


        // 서버전용
        public const int SERVER_LOGIC_TIME_MS = 50; // 서버 연산 1프레임 시간


    }
}
