//==========================================================
// File : GroupImage.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 03일
// Unity Connect : 
// Remarks 
// -  메시지 컨트롤 관리 기능 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDefine_Talk;

public class GroupImage : MonoBehaviour
{
    public ControlTopLeft           m_ControlTopLeft;           // 쪽지 기능      
    public ControlTopRight          m_ControlTopRight;          // 환경 설정
    public ControlBottomLeft        m_ControlBottomLeft;        // 추가 예정
    public ControlBottomRight       m_ControlBottomRight;       // 추가 예정

    // Use this for initialization
    void Start (){}
	
	// Update is called once per frame
	void Update (){}

    public void SetGroupMessageGroupControlActive(MESSAGE_GROUP_CONTROL_TYPE eMGCType = MESSAGE_GROUP_CONTROL_TYPE.MGCY_NONE, bool bActive = false  )
    {
        switch (eMGCType)
        {
            case MESSAGE_GROUP_CONTROL_TYPE.MGCY_NONE: { MessageGroupControlAllActive(bActive);  break; }
            case MESSAGE_GROUP_CONTROL_TYPE.MGCY_TOP_LEFT: { MessageGroupControlTopLeftActive(bActive); break; }              
            case MESSAGE_GROUP_CONTROL_TYPE.MGCY_TOP_RIGHT: { MessageGroupControlTopRightActive(bActive); break; }
            case MESSAGE_GROUP_CONTROL_TYPE.MGCY_BOTTOM_LEFT: { MessageGroupControlBottomLeftActive(bActive);  break; }
            case MESSAGE_GROUP_CONTROL_TYPE.MGCY_BOTTOM_RIGHT: { MessageGroupControlBottomRightActvie(bActive);  break; }
            default:
                {
                    break;
                }
        }
    }

    private void MessageGroupControlAllActive( bool bActive = false )
    {
        m_ControlTopLeft.gameObject.SetActive(bActive);
        m_ControlTopRight.gameObject.SetActive(bActive);
        m_ControlBottomLeft.gameObject.SetActive(bActive);
        m_ControlBottomRight.gameObject.SetActive(bActive);
    }

    private void MessageGroupControlTopLeftActive( bool bActive = false )
    {
        m_ControlTopLeft.gameObject.SetActive(bActive);
    }

    private void MessageGroupControlTopRightActive( bool bActive = false )
    {
        m_ControlTopRight.gameObject.SetActive(bActive);
    }

    private void MessageGroupControlBottomLeftActive(bool bActive = false)
    {
        m_ControlBottomLeft.gameObject.SetActive(bActive);
    }

    private void MessageGroupControlBottomRightActvie(bool bActive = false)
    {
        m_ControlBottomRight.gameObject.SetActive(bActive);
    }
}
