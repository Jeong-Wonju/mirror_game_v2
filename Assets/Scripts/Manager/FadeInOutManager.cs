//==========================================================
// File : FadeInOutManager.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 19일
// Unity Connect : 
// Remarks 
// -   씬 장면을 희미해지게 만드는 효과 매니저
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aing;

public class FadeInOutManager : MonoSingletonFactory<FadeInOutManager>
{
    #region
    private float       m_fFadeSpeed = 0.0f;     // 화면에 검정색 페이드 속도를 나타냅니다. ( 0.0f ~ 1.0f )
    //private bool        m_bFadeInOutComplete = false;
    #endregion

    public FadeInOut m_FadeInOut = null;
    public float fFadeSpeed { get { return m_fFadeSpeed; } set { m_fFadeSpeed = value; } }
    public FADEINOUT_ID eCurrentFIOID { get { return m_FadeInOut.eCurrentFIOID; } set { m_FadeInOut.eCurrentFIOID = value; } }
    //public bool bFadeInOutComplete { get { return m_bFadeInOutComplete; } set { m_bFadeInOutComplete = value; } }

    // 임시 테스트 차후에 삭제
    public float fR = 0.0f;
    public float fG = 0.0f;
    public float fB = 0.0f;
    public float fA = 0.0f;

    void Start ()
    {
        // 자동으로 테스트 할때 주석풀면된다. 
        //m_fFadeSpeed = 0.5f;
        //m_FadeInOut.eCurrentFIOID = FADEINOUT_ID.FIOI_CLEAR;
    }

    // Update is called once per frame
    void Update ()
    {
        switch (m_FadeInOut.eCurrentFIOID)
        {
            case FADEINOUT_ID.FIOI_NONE: { ProcessFadeInOutSceneScriptNone(); break; }
            case FADEINOUT_ID.FIOI_CLEAR: { ProcessFadeInOutSceneScriptCheckShowTimeClear(); break; }
            case FADEINOUT_ID.FIOI_BLACK: { ProcessFadeInOutSceneScriptCheckShowTimeBlack(); break; }
            case FADEINOUT_ID.FIOI_NEXT: { ProcessFadeInOutSceneScriptNext(); break; }
            default:
                {
                    break;
                }
        }
    }

    private void ProcessFadeInOutSceneScriptNone()  // 00 : 진행이 없다는 뜻입니다.
    {

    }

    private void ProcessFadeInOutSceneScriptNext()   // 01 : 다음 진행으로 넘깁니다.
    {

    }

    private void ProcessFadeInOutSceneScriptCheckShowTimeClear() // 02 : 사라지게 합니다. 
    {
        // 1.투명질감을 바꿉니다. - FadeToClear 
        if (m_FadeInOut.m_Image != null)
        {
            // 1-1. 희미해지는 과정입니다.
            m_FadeInOut.m_Image.color = Color.Lerp(m_FadeInOut.m_Image.color, Color.clear, m_fFadeSpeed * Time.deltaTime);

            fR = m_FadeInOut.m_Image.color.r;
            fG = m_FadeInOut.m_Image.color.g;
            fB = m_FadeInOut.m_Image.color.b;
            fA = m_FadeInOut.m_Image.color.a;

            if (m_FadeInOut.m_Image.color.a <= 0.1f)
            {
                // 1-1-1. 0.05까지 희미해지면 이미지를 지우고, 이미지를 사용하지 않겠다고 설정합니다. 
                m_FadeInOut.m_Image.color = Color.clear;

                SetChangeFadeInOutSceneScript(FADEINOUT_ID.FIOI_NONE, 0.0f);
            }
        }

    }

    private void ProcessFadeInOutSceneScriptCheckShowTimeBlack()    // 03 : 서서히 검게 합니다.
    {
        // 1. 다시 서서히 검게 변화합니다.
        if (m_FadeInOut.m_Image != null)
        {
            m_FadeInOut.m_Image.color = Color.Lerp(m_FadeInOut.m_Image.color, Color.black, m_fFadeSpeed * Time.deltaTime);

            fR = m_FadeInOut.m_Image.color.r;
            fG = m_FadeInOut.m_Image.color.g;
            fB = m_FadeInOut.m_Image.color.b;
            fA = m_FadeInOut.m_Image.color.a;

            // 1-1. 검게 되는 과정입니다. 
            if (m_FadeInOut.m_Image.color.a >= 0.9f)
            {
                // 1-1-1. 더 이상 진행할 것이 없으면 상태값을 변경합니다.
                m_FadeInOut.m_Image.color = Color.black;

                SetChangeFadeInOutSceneScript(FADEINOUT_ID.FIOI_NONE, 0.0f);
            }
        }
    }

    public void SetChangeFadeInOutSceneScript(FADEINOUT_ID eFadeInOutID, float fFadeInOutSpeed)
    {
        m_fFadeSpeed = fFadeInOutSpeed;
        m_FadeInOut.ePrevFIOID = m_FadeInOut.eCurrentFIOID;
        m_FadeInOut.eCurrentFIOID = eFadeInOutID;
    }

    public bool IsFadeInOutSceneScriptPrev(FADEINOUT_ID eFadeInOutID)
    {
        if (eFadeInOutID == m_FadeInOut.ePrevFIOID) return true;
        return false;
    }

    public bool IsFadeInOutSceneScriptCurrent(FADEINOUT_ID eFadeInOutID)
    {
        if (eFadeInOutID == m_FadeInOut.eCurrentFIOID) return true;
        return false;
    }
}
