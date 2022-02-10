//==========================================================
// File : LottoEvent.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 22일
// Unity Connect : 
// Remarks 
// -   로또 이벤트 UI 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LottoEvent : MonoBehaviour
{
    private bool m_bLottoWindowActive = false;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

    public void OnLottoEventClick()
    {
        StartCoroutine(OnLottoClick());
        Debug.Log("LottoEvent.... OnLottoEventClick");
    }

    IEnumerator OnLottoClick()
    {
        m_bLottoWindowActive = !m_bLottoWindowActive;
        GameManager_Talk.Instance.m_LottoUI.m_LottoWindows.gameObject.SetActive(m_bLottoWindowActive);
        yield return null;
    }
}
