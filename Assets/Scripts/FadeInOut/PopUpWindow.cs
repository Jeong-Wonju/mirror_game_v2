using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindow : MonoBehaviour
{
    public InputWindow m_InputWindow = null;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnCancelClick()
    {
        // 창 닫기 
        this.gameObject.SetActive(false);
    }

    public void OnSendClick()
    {
        // 쪽지(개인) 서버로 전송 보내기 
        NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendChat(1, "krmrsin01","노트북 이용자를 위한 좌석이오니 노트북 비 이용자 분들은 일반 열람석을 이용해주시기 바랍니다,");
        Debug.Log("PopUpWindow PacketSender SendNoteInfo OK");

    }
}
