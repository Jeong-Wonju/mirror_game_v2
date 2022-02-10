//==========================================================
// File : Writing.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 06월 29일
// Unity Connect : 
// Remarks 
// -  전체 쪽지 창 UI 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Writing : MonoBehaviour
{

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

    public void OnWritingChatClick()
    {
        Debug.Log("OnWritingChatClick()");
        GameManager_Talk.Instance.m_WholeChatWindow.gameObject.SetActive(true);
    }
}
