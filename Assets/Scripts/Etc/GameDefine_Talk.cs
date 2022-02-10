//==========================================================
// File : GameDefine.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 03일
// Unity Connect : 
// Remarks 
// -  명령에 필요한 열거형을 관리합니다. 
//==========================================================
using System.Collections;
using UnityEngine;

namespace GameDefine_Talk
{
    //=============================================================================
    //	ENUM	COMMAND_ENUMS
    //!	@brief	명령 플래그(Flag)들을 참조하기 위함입니다.
    //=============================================================================
    public class GameDefine { };  // GameDefine

    //=============================================================================
    //	ENUM	LOWER_CONTROLS_TYPE
    //!	@brief  명령 하단 컨트롤 기능 타입 들입니다.
    //=============================================================================
    public enum LOWER_CONTROLS_TYPE : int
    {
        LCT_START = 0,          // 00 : 시작
        LCT_NONE = LCT_START,   // 00 : 없다
        LCT_WHOLE_NOTE,         // 01 : 전체쪽지
        LCT_INDIVIDUAL_NOTE,    // 02 : 개인쪽지
        LCT_EVENT_NOTE,         // 03 : 이벤트쪽지
        LCT_DIALOGUE_ROOM,      // 04 : 대화방 
        LCT_PROFILE,            // 05 : 프로필 
        LCT_CASH,               // 06 : 캐쉬
        LCT_INFORMATION,        // 07 : 이용 안내
        LCT_SETTING,            // 08 : 설정
        LCT_OTHER,              // 09 : 기타(다른)
        LCT_TOTAL,              // 10 : 총 합계
    };

    //=============================================================================
    //	ENUM	MESSAGE_CONTROLS_TYPE
    //!	@brief  메시지 컨트롤 기능 타입 들입니다.
    //=============================================================================
    public enum MESSAGE_CONTROLS_TYPE : int
    {
        MCT_START = 0,          // 00 : 시작              
        MCT_NONE = MCT_START,   // 00 : 없다
        MCT_NOTE,               // 01 : 쪽지
        MCT_NOTE_SETTING,       // 02 : 쪽지 환경          
        MCT_TOTAL,              // 03 : 총 합계
    };

    //=============================================================================
    //	ENUM	MESSAGE_GROUP_CONTROL_TYPE
    //!	@brief  메시지 그룹 컨트롤 타입 들입니다.
    //=============================================================================
    public enum MESSAGE_GROUP_CONTROL_TYPE : int
    {
        MGCY_START = 0,         // 00 : 시작 
        MGCY_NONE = MGCY_START, // 00 : 없다
        MGCY_TOP_LEFT,          // 01 : 상단 왼쪽
        MGCY_TOP_RIGHT,         // 02 : 상단 오른쪽
        MGCY_BOTTOM_LEFT,       // 03 : 하단 왼쪽
        MGCY_BOTTOM_RIGHT,      // 04 : 하단 오른쪽
        MGCY_TOTAL,             // 05 : 총 합계
    };

    //=============================================================================
    //	ENUM	LAYOUT_CONTROL_TYPE
    //!	@brief  레이아웃 컨트롤 타입 들입니다.
    //=============================================================================
    public enum LAYOUT_CONTROL_TYPE : int
    {
        LCT_START = 0,          // 00 : 시작 
        LCT_NONE = LCT_START,   // 00 : 없다
        LCT_TOP,                // 01 : 상단 
        LCT_BOTTOM,             // 02 : 하단 
        LCT_TOTAL,              // 03 : 총 합계 
    };

    //=============================================================================
    //	ENUM	RANDOM_GAME_TYPE
    //!	@brief  랜덤 게임 타입 들입니다.
    //=============================================================================
    public enum RANDOM_GAME_TYPE : int
    {
        RTG_START = 0,          // 00 : 시작
        RTG_NONE = RTG_START,   // 00 : 없다
        RTG_LOTTO,              // 01 : 로또
        RTG_TOTAL,              // 02 : 총 합계
    };

    //=============================================================================
    //	ENUM	DATA_ASSET_TYPE
    //!	@brief  데이타 어셋 타입 들입니다.
    //=============================================================================
    public enum SCRIPTABLE_OBJECT_TYPE : int
    {
        SOT_START = 0,          // 00 : 시작
        SOT_NONE = SOT_START,   // 01 : 없다
        SOT_LOTTO,              // 02 : 로또 테이블
        SOT_TOTAL,              // 03 : 총 합계
    };

    //=============================================================================
    //	ENUM	GAME_SCENE_TYPE
    //!	@brief  씬 타입
    //=============================================================================
    public enum GAME_SCENE_TYPE : int
    {
        GST_START = 0,          // 00 : 시작
        GST_NONE = GST_START,   // 00 : 없다
        GST_LOGO,               // 01 : 게임로고입니다.
        GST_INGAME,             // 02 : 인 게임입니다..
        GST_TOTAL,              // 03 : 총 합계입니다.
    };

}
