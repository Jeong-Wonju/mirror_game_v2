//==========================================================
// File : FadeInOutEnum.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 19일
// Unity Connect : 
// Remarks 
// -  패이드 인/아웃에 필요한 열거형을 관리합니다.
//==========================================================
using UnityEngine;
using System.Collections;


namespace Aing
{
    //=============================================================================
    //	ENUM	FADEINOUT_ENUMS
    //!	@brief	페이드 인/아웃 플래그(Flag)들을 참조하기 위함입니다.
    //=============================================================================
    public class FadeInOutEnum { };

    //=============================================================================
    //	ENUM	FADEINOUT_ID
    //!	@brief  페이드 인/아웃 효과 ID 입니다.
    //=============================================================================
    public enum FADEINOUT_ID : int
    {
        FIOI_START = 0,         // 00 : 시작
        FIOI_NONE = FIOI_START, // 00 : 진행이 없다는 뜻입니다.
        FIOI_NEXT,              // 01 : 다음 진행으로 넘깁니다.
        FIOI_CLEAR,             // 02 : 사라지게 합니다. 
        FIOI_BLACK,             // 03 : 서서히 검게 합니다.
        FIOI_TOTAL,             // 06 : 총 합계입니다.
    }
};

