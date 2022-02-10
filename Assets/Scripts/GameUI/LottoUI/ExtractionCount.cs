//==========================================================
// File : ExtractionCount.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 14일
// Unity Connect : 
// Remarks 
// -   로또 생성 카운트 UI 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtractionCount : MonoBehaviour
{
    private int         m_ExtractionCount   = 0;

    public LeftButton   m_LeftButton        = null;
    public RightButton  m_RightButton       = null;
    public Text         m_Text              = null;


    public int nExtractionCount { set { m_ExtractionCount = value; } get { return m_ExtractionCount; } }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if( null != m_Text )
        {
            m_Text.text = m_ExtractionCount.ToString();
        }
	}
}
