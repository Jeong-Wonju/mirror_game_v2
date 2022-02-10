//==========================================================
// File : IndividualChatWindows.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 22일
// Unity Connect : 
// Remarks 
// -  쪽지 창 UI 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndividualChatWindows : MonoBehaviour
{
    public Text m_TitleText = null;
    public Text m_ContentText = null;
    public Text m_InputText = null;

	// Use this for initialization
	void Start ()
    {
        // 창 제목 
        m_TitleText.text = "쪽지 보내기";

        // 내용
        m_ContentText.text = "보낼 메시지를 적어주세요\n" + "잔여 골드[" + UserInfo.Instance.m_GoldText.text + "]\n" +
            "쪽지는 건당 100Gold를 사용하여 보내게 됩니다.";
    }
	
	// Update is called once per frame
	void Update () {}

    public void OnCancelClick()
    {
        // 창 닫기 
        gameObject.SetActive(false);
    }

    public void OnSendClick()
    {
        // 쪽지(개인) 서버로 전송 보내기 
        //NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendChat("krmrsin0", "노트북 이용자를 위한 좌석이오니 노트북 비 이용자 분들은 일반 열람석을 이용해주시기 바랍니다,");
        NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendChat(1, GameManager_Talk.Instance.m_strOtherID, m_InputText.text);

        Debug.Log("IndividualChatWindows PacketSender SendNoteInfo OK");
        gameObject.SetActive(false);

    }
}
