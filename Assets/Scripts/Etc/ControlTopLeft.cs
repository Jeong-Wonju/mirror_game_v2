using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTopLeft : ControlBase
{
    public void Click()
    {
        GameManager_Talk.Instance.m_IndividualChatWindow.gameObject.SetActive(true);
        GameManager_Talk.Instance.m_strOtherID = strUserID;
        GameManager_Talk.Instance.m_nOtherDataIndex = nDataIndex;
        Debug.Log("ControlTopLeft Click : 쪽지기능입니다.");
    }
}
