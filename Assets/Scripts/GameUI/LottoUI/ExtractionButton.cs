//==========================================================
// File : ExtractionButton.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 14일
// Unity Connect : 
// Remarks 
// -   로또 생성 버튼 UI
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionButton : MonoBehaviour
{
    public ExtractionCount m_ExtractionCount = null;
    public LottoUI lottoui = null;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnClick()
    {
        // 로또 생성한다. 
        lottoui.LottoRandomExtraction();
    }
}
