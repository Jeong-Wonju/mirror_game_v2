//==========================================================
// File : Event.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 04일
// Unity Connect : 
// Remarks 
// -  이벤트 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : ControlBase
{
    public void OnClick()
    {
        Debug.Log("Event : " + "이벤트기능으로 넘어갑니다.");
    }
}
