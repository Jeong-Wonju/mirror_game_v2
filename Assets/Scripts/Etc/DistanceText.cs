//==========================================================
// File : DistanceText.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 05일
// Unity Connect : 
// Remarks 
// -   거리 정보
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceText : MonoBehaviour
{
    private string m_strDistance = "";   // 이름 

	// Use this for initialization
	void Start (){}
	
	// Update is called once per frame
	void Update (){}

    public void SetDistanceInfo( string strDistance = "" )
    {
        m_strDistance = strDistance;
    }
}
