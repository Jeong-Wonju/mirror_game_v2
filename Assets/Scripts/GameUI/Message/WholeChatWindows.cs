//==========================================================
// File : WholeChatWindows.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 26일
// Unity Connect : 
// Remarks 
// -  전체 창 쓰기 UI 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WholeChatWindows : MonoBehaviour
{
    public Text m_TitleText = null;
    public Image m_AvatarImage = null;
    public Text m_InputText = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSendClick()
    {
        // 2019-07-22
        //NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendChat(0,  UserInfo.Instance.m_strUserID.text, m_InputText.text);
        System.TimeSpan ts = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0);
        int nTotalSeconds = (int)ts.TotalSeconds;
        NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendWholeChatReq(0, UserInfo.Instance.m_strUserID.text, m_InputText.text, nTotalSeconds, 100 );

        Debug.Log("WholeChatWindows PacketSender SendNoteInfo OK");
        gameObject.SetActive(false);

    }

    // 2019-05-07
    public void OnPickImagePress()
    {
        NativeToolkit.PickImage();
    }

    public void OnCancelClick()
    {
        // 창 닫기 
        gameObject.SetActive(false);
    }
}
