//==========================================================
// File : LeftButton.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 14일
// Unity Connect : 
// Remarks 
// -   로또 생성 카운트 왼쪽 UI 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LeftButton : MonoBehaviour
{
    private int             m_nLeftCount        = 0;
    public ExtractionCount  m_ExtractionCont    = null;
    public Button           m_Button            = null;
    public bool             m_bOnClick          = false;

    public int nLeftCount { set { m_nLeftCount = value; } get { return m_nLeftCount; } }

    // Use this for initialization
    void Start ()
    {
        //if( null != m_Button )
        //      {
        //          // 이미지를 비활성화/활성화를 시킨다. 
        //          m_Button.interactable = false;
        //      }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //// 마우스
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if ( true == EventSystem.current.IsPointerOverGameObject() )
        //    {
        //        ++m_nLeftCount;
        //        m_ExtractionCont.nExtractionCount -= m_nLeftCount;
        //        Debug.Log("LeftButton Count : " + "(" + m_nLeftCount + ")");
        //    }

        //    //UnityEngine.GUIUtility:ProcessEvent(
        //}

        ////터치
        //if (Input.touchCount > 0)
        //{
        //    if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //    {
        //        //터치 처리
        //        Debug.Log("LeftButton touchCount Ok");
        //    }
        //}

    }

    public void OnClick()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // 마우스
            ++m_nLeftCount;
            m_ExtractionCont.nExtractionCount -= m_nLeftCount;
            Debug.Log("LeftButton Count : " + "(" + m_nLeftCount + ")");
        }
        else
        {
            ++m_nLeftCount;
            m_ExtractionCont.nExtractionCount -= m_nLeftCount;
            Debug.Log("LeftButton else : " + "(" + m_nLeftCount + ")");
        }
    }
}
