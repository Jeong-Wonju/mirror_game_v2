using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using GameDefine_Talk;


public class DialogueCellView : EnhancedScrollerCellView
{
    /// <summary>
    /// A reference to the UI Text element to display the cell data
    /// </summary>
    public Text someTextText;
    public RectTransform m_RectTransform;
    public Image m_UserAvatarImage;
    public GroupImage m_GroupImage;
    public DistanceText m_DistanceText;
    public UserNameText m_UserNameText;
    public SexAgeText m_SexAgeText;
    public Button m_Button;

    // 오른 컨트롤 기능들......
    public ControlTopLeft m_ControlTopLeft;
    public ControlTopRight m_ControlTopRight;
    public ControlBottomLeft m_ControlBottomLeft;
    public ControlBottomRight m_ControlBottomRight;

    /// <summary>
    /// The space around the text label so that we
    /// aren't up against the edges of the cell
    /// </summary>
    public RectOffset textBuffer;

    /// <summary>
    /// Public reference to the index of the data
    /// </summary>
    public int DataIndex { get; private set; }

    void Start() { }
    void Update() { }

    /// <summary>
    /// This function just takes the Demo data and displays it
    /// </summary>
    /// <param name="data"></param>
    public void SetData(int dataIndex, DialogueRoomData data)
    {
        m_ControlTopLeft.strUserID = data.strUserName;
        m_ControlTopLeft.nDataIndex = dataIndex;

        m_ControlTopRight.strUserID = data.strUserName;
        m_ControlTopRight.nDataIndex = dataIndex;

        m_ControlBottomLeft.strUserID = data.strUserName;
        m_ControlBottomLeft.nDataIndex = dataIndex;

        m_ControlBottomRight.strUserID = data.strUserName;
        m_ControlBottomRight.nDataIndex = dataIndex;

        // link the data to the cell view
        DataIndex = dataIndex;

        //someTextText.text = "( " + data.strUserName + " )\n" + data.someText;
        someTextText.text = data.strTiem + "\n" + data.someText;

        m_DistanceText.GetComponent<Text>().text = data.strDistance;
        m_DistanceText.gameObject.SetActive(false); // 사용안함

        m_UserNameText.SetUserNameInfo(data.strUserName, data.strSex, data.strAge);
        m_UserNameText.gameObject.SetActive(false);

        m_SexAgeText.GetComponent<Text>().text = data.strSex + data.strAge + "세";
        m_SexAgeText.gameObject.SetActive(false);   // 사용안함

        m_UserAvatarImage.gameObject.SetActive(data.bAvatarImage);  // 아바타 비/활성화
        if (true == GameManager_Talk.Instance.m_bWebServer)
        {
            GameObject.Find("GameManager_Talk").GetComponent<PicDown>().LoadFromInternet(m_UserAvatarImage,data.strUserName); // 웹서버에서 사진불러온다.
        }
        else
        {
            // 일반 사진 
        }


        // force update the canvas so that it can calculate the size needed for the text immediately
        Canvas.ForceUpdateCanvases();

        // 1: 쪽지방(클릭해서 대화방이동가능), 0: 그외방
        if ((true == data.bChatRoomActive) && (null != m_Button))
        {
            m_Button.enabled = true;
            Debug.Log("SetData + bChatRoomActive : true");
        }
        else
        {
            if (null != m_Button)
            {
                m_Button.enabled = false;
                Debug.Log("SetData + bChatRoomActive : false");
            }
        }

        // 데이터의 셀 크기를 설정하고, 텍스트가 셀의 테두리에 닿지 않도록 패딩을 추가한다.
        data.cellSize = m_RectTransform.rect.height + textBuffer.top + textBuffer.bottom;

        float fHeight = 0.0f;
        Vector3 v3 = someTextText.rectTransform.anchoredPosition3D;
        if ( false == data.bPosition )
        {
            // Right ( 순서 : 시간, 내용 )
            someTextText.alignment = TextAnchor.UpperRight;
            float fX = textBuffer.left;
            someTextText.rectTransform.anchoredPosition3D = new Vector3(fX, v3.y, v3.z);
        }
        else
        {
            // 아바타, 성별 or 나이, 거리 
            fHeight = m_UserAvatarImage.rectTransform.rect.height + textBuffer.top + textBuffer.bottom;

            // Left( 순서 : 아바타, 이름/시간, 내용 )
            someTextText.alignment = TextAnchor.UpperLeft;

            // 왼쪽(아바타,이름,나이,성별) 글 간격 계산 ( 단, 기준은 아바타으로 한다. )
            float fX = textBuffer.left + m_UserAvatarImage.rectTransform.rect.width + textBuffer.right;

            someTextText.rectTransform.anchoredPosition3D = new Vector3(fX, v3.y, v3.z);
        }

        if (data.cellSize < fHeight)
        {
            data.cellSize = fHeight;// + textBuffer.top + textBuffer.bottom;
        }

    }

}
