//==========================================================
// File : UpperUI.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 30일
// Unity Connect : 
// Remarks 
// -   위(상단) 관리 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpperUI : MonoBehaviour
{
    public Text m_AingTalkName; // 하단 컨트롤 제목 

    public void SetGameObjectActive(bool bActive = false)
    {
        gameObject.SetActive(bActive);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
