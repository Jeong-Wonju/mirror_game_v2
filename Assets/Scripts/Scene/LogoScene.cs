//==========================================================
// File : LogoScene.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 19일
// Unity Connect : 
// Remarks 
// -   씬 장면 로고 입니다.
//     (오브젝트를 쉽게 접근하기 위해서 스크립트를 만들었다.)
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aing;
using GameDefine_Talk;

public class LogoScene : MonoBehaviour
{
    private bool m_bDataLoad = false;

    public void SetActive(bool bActive = false)
    {
        gameObject.SetActive(bActive);
    }

    public void Awake()
    {
        SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {   
        FadeInOutManager.Instance.eCurrentFIOID = FADEINOUT_ID.FIOI_CLEAR;
        FadeInOutManager.Instance.fFadeSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update ()
    {
        float fFadeSpeed = FadeInOutManager.Instance.fFadeSpeed;
        FADEINOUT_ID eType = FadeInOutManager.Instance.eCurrentFIOID;
        if( (0.0f >= fFadeSpeed) && (FADEINOUT_ID.FIOI_NONE == eType) )
        {
            // 데이타 로드 
            if( false == m_bDataLoad)
            {
                DataAssetManager.Instance.DataAssetLoad();
                m_bDataLoad = true;
            }
            else if( true == DataAssetManager.Instance.bDataLoadComplete )
            {
                SetActive(false);
                GameManager_Talk.Instance.m_GameSceneType = GAME_SCENE_TYPE.GST_INGAME;
            }
        }

    }
}
