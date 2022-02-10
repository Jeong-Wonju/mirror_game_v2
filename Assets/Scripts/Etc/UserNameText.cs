//==========================================================
// File : UserNameText.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 05일
// Unity Connect : 
// Remarks 
// -   유저 정보 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserNameText : MonoBehaviour
{
    public Text m_Text = null;

    private string m_strUserName = "";   // 이름 
    private string m_strSex = "";   // 성별
    private string m_strAge = "";   // 나이   

    public string strUserName { get { return m_strUserName;} }
    public string strSex { get { return m_strSex; } }
    public string strAge { get { return m_strAge; } }


    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void SetUserNameInfo( string strUserName = "", string strSex = "", string strAge = "" )
    {
        m_strUserName   = strUserName;
        m_strSex        = strSex;
        m_strAge        = strAge;

        m_Text.text = m_strUserName;
    }
}
