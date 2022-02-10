//==========================================================
// File : MonoSingletonFactory.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 17일
// Unity Connect : 
// Remarks 
// -  특정 클래스에 대해 인스턴스 하나가 생성되고, 어디서든지
//    해당 인스턴스에 접근할 수 있도록 하기 위한 패턴입니다.
//==========================================================
using UnityEngine;
using System.Collections.Generic;

public class MonoSingletonFactory<T> : MonoBehaviour where T : class
{
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            // 값이 NULL 이면
            if (m_Instance == null)
            {
                // Hierarchy 창에 있는 오브젝트에 붙은 컴포넌트를 찾아서 반환을 해줍니다.                   
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
            }

            return m_Instance;
        }
    }


    protected virtual void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = Instance;

            // 유니티는 씬 전환할때 모든 객체를 해제 시킵니다. 
            // 이때 씬 전환 할때 해제하기 싫은 객체가 있으면 밑에 함수처럼 선언하면됩니다.            
            DontDestroyOnLoad(gameObject);
        }
    }
}