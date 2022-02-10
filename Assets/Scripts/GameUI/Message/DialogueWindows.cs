//==========================================================
// File : DialogueWindows.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 30일
// Unity Connect : 
// Remarks 
// -  대화 창  UI 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindows : MonoBehaviour
{
    public Text m_OtherIDText = null;
    public Text m_InputText = null;


    public void SetActive(bool bActive = false, string strOtherName ="")
    {
        gameObject.SetActive(bActive);
        m_OtherIDText.text = strOtherName;
    }

    public void OnDialogueWindowsClick()
    {
        Debug.Log("OnDialogueWindowsClick()");

        // 2019-07-04
        GameManager_Talk.Instance.RefreshIndividualChat();
        GameManager_Talk.Instance.m_GameUpperUI.gameObject.SetActive(true);
        GameManager_Talk.Instance.m_GameObjectScroller.gameObject.SetActive(true);
        SetActive(false);
    }

    public void OnDialogueMessageSendClick()
    {
        string strOtherNick = GameManager_Talk.Instance.m_strOtherID;
        NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendChat(2, strOtherNick, m_InputText.text);
        Debug.Log("OnDialogueMessageSendClick()");
    }

    // Use this for initialization
    void Start ()
    {
        // 선택한 유저
        //m_OtherIDText.text = GameManager_Talk.Instance.m_strOtherID;

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
