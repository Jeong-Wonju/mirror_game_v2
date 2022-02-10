//==========================================================
// File : RightButton.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 14일
// Unity Connect : 
// Remarks 
// -   로또 생성 카운트 오른쪽 UI 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RightButton : MonoBehaviour, IPointerEnterHandler

{
    private int             m_nRightCount       = 0;
    public ExtractionCount  m_ExtractionCont    = null;
    public Button           m_Button            = null;
    public bool             m_bOnClick          = false;
    public RectTransform    m_rtTransform       = null;

    public int nRightCount { set { m_nRightCount = value; } get { return m_nRightCount; } }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (!EventSystem.current.IsPointerOverGameObject())
        //{
        //    // 마우스
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        ++m_nRightCount;
        //        m_ExtractionCont.nExtractionCount += m_nRightCount;
        //        Debug.Log("RightButton Count : " + "(" + m_nRightCount + ")");
        //    }

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

        //if( true == m_bOnClick )
        //{
        //    // 마우스
        //    ++m_nRightCount;
        //    m_ExtractionCont.nExtractionCount += m_nRightCount;
        //    Debug.Log("RightButton Count : " + "(" + m_nRightCount + ")");
        //    m_bOnClick = false;
        //}
    }

    //IEnumerator LottoExtraction()
    //{
    //    yield return WaitForSeconds(0.1f);
    //}

    public void OnClick()
    {
        //// 로또 생성한다. 
        //++m_nRightCount;
        //m_ExtractionCont.nExtractionCount += m_nRightCount;
        //Debug.Log("RightButton Count : " + "(" + m_nRightCount + ")");

        //m_bOnClick = true;


        //if (!EventSystem.current.IsPointerOverGameObject())
        //{
        //    // 마우스
        //    ++m_nRightCount;
        //    m_ExtractionCont.nExtractionCount += m_nRightCount;
        //    Debug.Log("RightButton Count : " + "(" + m_nRightCount + ")");
        //}
        //else
        //{
        //++m_nRightCount;
        //m_ExtractionCont.nExtractionCount += m_nRightCount;
        //Debug.Log("RightButton OnClick : " + "(" + m_nRightCount + ")");
        //}
    }

    public void OnPointerEnter( PointerEventData eventData )
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle( m_rtTransform, eventData.position, eventData.enterEventCamera, out pos ))
        {
            //++m_nRightCount;
            //m_ExtractionCont.nExtractionCount += m_nRightCount;
            Debug.Log("OnPointerEnter Ok : " + "(" + m_nRightCount + ")( " + pos.x + "," + pos.y + ")");
        }
    }

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Vector2 pos;
    //    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rtTransform, eventData.position, eventData.enterEventCamera, out pos))
    //    {
    //        ++m_nRightCount;
    //        m_ExtractionCont.nExtractionCount += m_nRightCount;
    //        Debug.Log("OnPointerExit Ok : " + "(" + m_nRightCount + ")( " + pos.x + "," + pos.y + ")");
    //    }
    //}

}
