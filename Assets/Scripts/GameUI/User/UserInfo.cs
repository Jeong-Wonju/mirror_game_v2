//==========================================================
// File : UserInfo.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 22일
// Unity Connect : 
// Remarks 
// -  유저 정보
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoSingletonFactory<UserInfo>
{
    public Text m_GoldText;
    public Text m_LevelText;
    public Text m_strUserID;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
