//==========================================================
// File : FadeInOut.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 19일
// Unity Connect : 
// Remarks 
// -  패이드 인/아웃에 필요한 열거형을 관리합니다.
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aing;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    // 진행사항을 Inspector에서 봅니다. ( 수정은 불가 )
    #region 
    [SerializeField]
    private FADEINOUT_ID m_ePrevFIOID;     // 이전 효과 상태 ID 입니다.
    [SerializeField]
    private FADEINOUT_ID m_eCurrentFIOID;  // 현재 효과 상태 ID 입니다.
    #endregion


    public Image m_Image;   
    public FADEINOUT_ID ePrevFIOID { get { return m_ePrevFIOID; } set { m_ePrevFIOID = value; } }
    public FADEINOUT_ID eCurrentFIOID { get { return m_eCurrentFIOID; } set { m_eCurrentFIOID = value; } }

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}
}
