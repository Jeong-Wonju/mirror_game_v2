using UnityEngine;
using System.Collections;

public class Define
{
    public const int MAX_NICKLEN = 20;
    public const int MAX_MSGLEN = 128;
    public const int MAX_FIDLEN = 256;
}

public enum _PTCODE
{
    GENERAL_FAILURE = 1,

    ADMIN_MSG,          // 공지 

    DISCONNECT_NOTI,    // 접속 종료 알림

    LOGOUT_REQ,         // 프로그램 종료
    LOGOUT_ANS,         // 
    LOGOUT_NOTI,        // 

    ENTRANCE_REQ,       // 입장
    ENTRANCE_ANS,       // 
    ENTRANCE_NOTI,      // 

    EXIT_REQ,           // 퇴장 예약/취소 요청 
    EXIT_ANS,
    EXIT_NOTI,          // 퇴장 예약/취소 상황을 알림

    WORLDENTER_ANS,     // 월드에 입장
    WORLDLEAVE_REQ,     // 월드에서 퇴장 요청
    WORLDLEAVE_ANS,     // 월드에서 퇴장 알림

    CHAT_MSG,           // 채팅 

    SYSTEM_MSG,         // 시스템 정보 알림

    ACCEPT_ANS,         // 접속 성공

    MEMBERJOIN_REQ,     // 회원가입 
    MEMBERJOIN_ANS,
    REMEMBERJOIN_ANS,   // 잘못된 회원 알림

    LOGIN_REQ,
    LOGIN_ANS,
    LOGIN_NOTI,

    FID_REQ,
    PUSH_ANS,

    USERINFO_REQ,
    USERINFO_ANS,

    WHOLE_CHAT_REQ,     // 전체 채팅방 
    WHOLE_CHAT_ANS,     // 전체 요청 받은 유저에게 전달 

    CHAT_REQ,           // 상대방에게 첫번째 채팅 , 캐쉬 소모
    CHAT_ANS,           // 요청 받은 유저에게 메시지 전달

    CHATUSER_REQ,       // 채팅한 유저 목록
    CHATUSER_ANS,

    CHATLIST_REQ,       // 대화 목록
    CHATLIST_ANS,
};

public class Protocol : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
