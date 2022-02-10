//==========================================================
// File : WholeChat.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 25일
// Unity Connect : 
// Remarks 
// -  전체 쪽지 창 UI 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.UI;
using GameDefine_Talk;

public class WholeChat : MonoBehaviour
{
    public Image m_PushImage = null;
    public int m_nPushValue = 0;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

    public void OnWholeChatClick()
    {
        if (LOWER_CONTROLS_TYPE.LCT_WHOLE_NOTE != GameManager_Talk.Instance.m_LowerControlsTypes)
        {   
            Debug.Log("OnWholeChatClick() - 성공");
            GameManager_Talk.Instance.RefreshWholeChat();
            return;
        }

        Debug.Log("OnWholeChatClick() - 이미 전체쪽지창입니다.");
    }

    public void OnWritingWholeChatClick()
    {
        Debug.Log("OnWritingWholeChatClick()");
        GameManager_Talk.Instance.m_WholeChatWindow.gameObject.SetActive(true);
    }

    // 2019-07-01
    public void SourcePushImageChange( int nPushValue = 0 )
    {
        ++m_nPushValue;

        string strPath = "Atlases/TextureImage/Lotto/" + m_nPushValue;// + ".png";
        if (null != m_PushImage)
        {
            m_PushImage.sprite = Resources.Load(strPath, typeof(Sprite)) as Sprite;
        }
    }
}
