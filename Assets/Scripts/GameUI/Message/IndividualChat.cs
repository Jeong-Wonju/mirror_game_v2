//==========================================================
// File : IndividualChat.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 25일
// Unity Connect : 
// Remarks 
// -  개인 쪽지 창 UI 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameDefine_Talk;
    
public class IndividualChat : MonoBehaviour
{
    public Image m_PushImage = null;

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}

    public void OnIndividualChatClick()
    {
        if (LOWER_CONTROLS_TYPE.LCT_INDIVIDUAL_NOTE != GameManager_Talk.Instance.m_LowerControlsTypes)
        {
            Debug.Log("OnIndividualChatClick() - 성공");
            GameManager_Talk.Instance.RefreshIndividualChat();
            return;
        }

        Debug.Log("OnIndividualChatClick()- 이미 개인쪽지창입니다.");
    }
}
