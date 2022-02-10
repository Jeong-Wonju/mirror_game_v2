//==========================================================
// File : LoadingBar.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 18일
// Unity Connect : 
// Remarks 
// -   데이터 관리 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    #region
    private int m_nLoadCount;
    private int m_nMaxLoadCount;
    private bool m_bLoadComplete;
    private bool m_bProcess;
    #endregion

    public int nLoadCount { get { return m_nLoadCount; } set { m_nLoadCount = value; } }
    public int nMaxLoadCount { get { return m_nMaxLoadCount; } set { m_nMaxLoadCount = value; } }
    public bool bLoadComplete { get { return m_bLoadComplete; } set { m_bLoadComplete = value; } }
    public bool bProcess { get { return m_bProcess; } }

    public Slider slider;
    public Text text;

    public void SetActive(bool bActive = false)
    {
        if (null != slider)
        {
            slider.gameObject.SetActive(bActive);
        }
    }

    private void Awake()
    {
        slider.gameObject.SetActive(true);
        text.text = "Loading.....0%";
    }

    public void LoadingStart(int nMaxLoadingCount = 0)
    {
        slider.gameObject.SetActive(true);
        m_bLoadComplete = false;
        m_nMaxLoadCount = nMaxLoadingCount;
        m_bProcess = true;
        text.text = "Loading.....0%";
    }

    public void LoadingUpdate()
    {
        m_nLoadCount++;
        //++m_nLoadCount;
    }

    public void LoadingEnd()
    {
        slider.gameObject.SetActive(true);
        m_nMaxLoadCount = 0;
        m_bLoadComplete = true;
        m_bProcess = false;
    }


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if( true == m_bProcess)
        {
            //float progress = Mathf.Clamp01( m_nLoadCount / 0.9f );
            //slider.value = progress;
            //text.text = progress * 100.0f + "%";

            float progress = Mathf.Clamp01( m_nLoadCount / m_nMaxLoadCount);
            slider.value = progress;
            text.text = m_nLoadCount + "/" + m_nMaxLoadCount;

            // 100프로이면 비활성화시킨다.
            if( 1.0f <= slider.value )
            {
                LoadingEnd();
            }
        }
    }
}
