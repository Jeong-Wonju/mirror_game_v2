//==========================================================
// File : InGameScene.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 19일
// Unity Connect : 
// Remarks 
// -   씬 장면 인 게임 입니다.
//     (오브젝트를 쉽게 접근하기 위해서 스크립트를 만들었다.)
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aing;

public class InGameScene : MonoBehaviour
{
    //public string ip;

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
        if (true != GameManager_Talk.Instance.m_bClient)
        {
            // 서버 호출 
            string ip = "catchone13.iptime.org";

            /*if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                ip = "catchone13.iptime.org";  //본섭

                if (true == GameManager_Talk.Instance.m_bIPHouse)        
                {
                    ip = "192.168.0.37";    // 동생집                   
                }
                else if (true == GameManager_Talk.Instance.m_bIPMyHouse)
                {
                    ip = "192.168.0.8"; // 우리집
                }
                else if (true == GameManager_Talk.Instance.m_bIPlLbrary)
                {
                    ip = "192.168.5.170"; // 도서관
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                ip = "catchone13.iptime.org";  //본섭

                if (true == GameManager_Talk.Instance.m_bIPHouse)
                {
                    ip = "192.168.0.37";    // 동생집
                    //ip = "127.0.0.1";    // 동생집
                }
                else if (true == GameManager_Talk.Instance.m_bIPMyHouse)
                {
                    ip = "192.168.0.8"; // 우리집
                }
                else if (true == GameManager_Talk.Instance.m_bIPlLbrary)
                {
                    ip = "192.168.5.170"; // 도서관
                }
            }*/
            Debug.Log("ip : " + ip);
            NetworkManager_Talk.Instance.ConnectionServer(ip, 18002, 0); // 서버접속 시도
        }

        //NativeToolkit.StartLocation();
    }
	
	// Update is called once per frame
	void Update () {}
}
