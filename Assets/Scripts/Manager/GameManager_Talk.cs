using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnhancedUI;
using GameDefine_Talk;
using System.IO;
using System;
using DevelopeCommon;
using Aing;

public class GameManager_Talk : MonoSingletonFactory<GameManager_Talk>
{
    public IndividualChatWindows m_IndividualChatWindow = null;
    public WholeChatWindows m_WholeChatWindow = null;
    public GameObject m_accountCreateUI = null;
    public string m_UserID = "";
    public string m_Token = string.Empty;
    public UserInfoList m_UserInfoList = null;
    public DialogueList m_DialogueList = null;
    public bool m_bClient = false;
    public LOWER_CONTROLS_TYPE m_LowerControlsTypes = LOWER_CONTROLS_TYPE.LCT_NONE;
    public GAME_SCENE_TYPE m_GameSceneType = GAME_SCENE_TYPE.GST_NONE;
    public LottoUI m_LottoUI = null;
    public InGameScene m_InGameScene = null;
    public LogoScene m_LogoScene = null;

    public SmallList<UserInfoData> m_WholeChatDatas = null;
    public SmallList<UserInfoData> m_IndividualChatDatas = null;
    private Dictionary<string, SmallList<DialogueRoomData>> m_DyDialogueChatDatas = null;

    // 전체,쪽지 Scroller , UserInfo
    public UpperUI m_GameUpperUI = null;
    public GameObject m_GameObjectScroller = null;

    // 대화방 DialogueScroller 
    public DialogueWindows m_DialogueWindows = null;

    // 쪽지 기능사용
    public string m_strOtherID = "";
    public int m_nOtherDataIndex = 0;

    // 임시 IP 주소 변경
    public bool m_bIPlLbrary = false;   // 도서관 
    public bool m_bIPMyHouse = false;   // 우리집
    public bool m_bIPHouse = false;     // 집(다른)

    // 임시 사진 불러올때 변경
    public bool m_bWebServer = true;

    // 파일 업로드 
    public FileUpload m_FileUpload = null;


    // Use this for initialization
    void Start()
    {
        

        // 계정 생성 UI 비할성화 
        m_accountCreateUI.gameObject.SetActive(false);

        // 대화방 할당
        m_DyDialogueChatDatas = new Dictionary<string, SmallList<DialogueRoomData>>();

        // 로고 부터 시작
        m_GameSceneType = GAME_SCENE_TYPE.GST_LOGO;

        // Firebase 등록
        //Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        //Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    /*public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
        m_Token = token.Token;
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }*/


    private void ProcessLogoScene()
    {
        // 로고 씬을 활성화 한다. 
        m_LogoScene.SetActive(true);
        m_GameSceneType = GAME_SCENE_TYPE.GST_NONE;
    }

    private void ProcessGameScene()
    {
        m_InGameScene.SetActive(true);
        m_GameSceneType = GAME_SCENE_TYPE.GST_NONE;



        // 서버에 접속하지 않고 클라에서 실행
        if (true == m_bClient)
        {
            WholeNote();
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_GameSceneType)
        {
            case GAME_SCENE_TYPE.GST_NONE: { break; }
            case GAME_SCENE_TYPE.GST_LOGO: { ProcessLogoScene(); break; }
            case GAME_SCENE_TYPE.GST_INGAME: { ProcessGameScene(); break; }
            default:
                {
                    break;
                }
        }
    }

    // 전체 쪽지 
    private void WholeNote()
    {
        if (m_WholeChatDatas == null)
        {
            m_WholeChatDatas = new SmallList<UserInfoData>();
        }

        SmallList<MessageControlData> MCDatas = new SmallList<MessageControlData>();

        MessageControlData MCData = new MessageControlData();
        MCData.m_bActive = true;
        MCData.m_strFile = ""; // 이미지 파일 위치
        MCDatas.Add(MCData);

        MCData = new MessageControlData();
        MCData.m_bActive = true;
        MCData.m_strFile = ""; // 이미지 파일 위치
        MCDatas.Add(MCData);

        UserInfoData data = new UserInfoData();
        data.cellSize = 0;
        data.bAvatarImage = true;
        data.strUserName = "홍길동";
        data.strAge = "30";
        data.strSex = "남";
        data.strDistance = "100Km";
        data.bChatRoomActive = false;
        data.m_MessageControlData = MCDatas;
        data.someText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Nullam augue enim, scelerisque ac diam nec, efficitur aliquam orci.Vivamus laoreet, libero ut aliquet convallis, dolor elit auctor purus, eget dapibus elit libero at lacus.Aliquam imperdiet sem ultricies ultrices vestibulum.Proin feugiat et dui sit amet ultrices.Quisque porta lacus justo, non ornare nulla eleifend at.Nunc malesuada eget neque sit amet viverra.Donec et lectus ac lorem elementum porttitor.Praesent urna felis, dapibus eu nunc varius, varius tincidunt ante.Vestibulum vitae nulla malesuada, consequat justo eu, dapibus elit.Nulla tristique enim et convallis facilisis.";
        m_WholeChatDatas.Add(data);

        MCDatas = new SmallList<MessageControlData>();

        MCData = new MessageControlData();
        MCData.m_bActive = true;
        MCData.m_strFile = ""; // 이미지 파일 위치 
        MCDatas.Add(MCData);

        data = new UserInfoData();
        data.cellSize = 0;
        data.bAvatarImage = true;
        data.strUserName = "디니야";
        data.strSex = "여";
        data.strAge = "33";
        data.bChatRoomActive = false;
        data.strDistance = "50Km";
        data.m_MessageControlData = MCDatas;
        data.someText = " Nunc convallis, ipsum a porta viverra, tortor velit feugiat est, eget consectetur ex metus vel diam.";
        m_WholeChatDatas.Add(data);

        data = new UserInfoData();
        data.cellSize = 0;
        data.bAvatarImage = false;
        data.strUserName = "화림이";
        data.strSex = "여";
        data.strAge = "34";
        data.strDistance = "10Km";
        data.bChatRoomActive = false;
        data.m_MessageControlData = null;
        data.someText = "Fusce mollis elementum sem euismod malesuada. Aenean et convallis turpis. Suspendisse potenti";
        m_WholeChatDatas.Add(data);


        MCDatas = new SmallList<MessageControlData>();
        MCData = new MessageControlData();
        MCData.m_bActive = true;
        MCData.m_strFile = ""; // 이미지 파일 위치
        MCDatas.Add(MCData);

        data = new UserInfoData();
        data.cellSize = 0;
        data.bAvatarImage = true;
        data.strUserName = "이간희";
        data.strAge = "30";
        data.strSex = "남";
        data.bChatRoomActive = false;
        data.strDistance = "100Km";
        data.m_MessageControlData = MCDatas;
        data.someText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Nullam augue enim, scelerisque ac diam nec, efficitur aliquam orci.Vivamus laoreet, libero ut aliquet convallis, dolor elit auctor purus, eget dapibus elit libero at lacus.Aliquam imperdiet sem ultricies ultrices vestibulum.Proin feugiat et dui sit amet ultrices.Quisque porta lacus justo, non ornare nulla eleifend at.Nunc malesuada eget neque sit amet viverra.Donec et lectus ac lorem elementum porttitor.Praesent urna felis, dapibus eu nunc varius, varius tincidunt ante.Vestibulum vitae nulla malesuada, consequat justo eu, dapibus elit.Nulla tristique enim et convallis facilisis.";
        m_WholeChatDatas.Add(data);

        MCDatas = new SmallList<MessageControlData>();
        MCData = new MessageControlData();
        MCData.m_bActive = true;
        MCData.m_strFile = ""; // 이미지 파일 위치 
        MCDatas.Add(MCData);

        MCData = new MessageControlData();
        MCData.m_bActive = true;
        MCData.m_strFile = ""; // 이미지 파일 위치 
        MCDatas.Add(MCData);

        MCData = new MessageControlData();
        MCData.m_bActive = true;
        MCData.m_strFile = ""; // 이미지 파일 위치 
        MCDatas.Add(MCData);

        data = new UserInfoData();
        data.cellSize = 0;
        data.bAvatarImage = true;
        data.strUserName = "대웅이";
        data.strSex = "남";
        data.strAge = "31";
        data.bChatRoomActive = false;
        data.strDistance = "0Km";
        data.m_MessageControlData = MCDatas;
        data.someText = " Nunc convallis, ipsum a porta viverra, tortor velit feugiat est, eget consectetur ex metus vel diam.";
        m_WholeChatDatas.Add(data);

        data = new UserInfoData();
        data.cellSize = 0;
        data.bAvatarImage = true;
        data.strUserName = "신홍길동";
        data.strAge = "35";
        data.strSex = "남";
        data.strDistance = "1000Km";
        data.bChatRoomActive = false;
        data.m_MessageControlData = MCDatas;
        data.someText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Nullam augue enim, scelerisque ac diam nec, efficitur aliquam orci.Vivamus laoreet, libero ut aliquet convallis, dolor elit auctor purus, eget dapibus elit libero at lacus.Aliquam imperdiet sem ultricies ultrices vestibulum.Proin feugiat et dui sit amet ultrices.Quisque porta lacus justo, non ornare nulla eleifend at.Nunc malesuada eget neque sit amet viverra.Donec et lectus ac lorem elementum porttitor.Praesent urna felis, dapibus eu nunc varius, varius tincidunt ante.Vestibulum vitae nulla malesuada, consequat justo eu, dapibus elit.Nulla tristique enim et convallis facilisis.";
        m_WholeChatDatas.Add(data);

        m_UserInfoList.GetComponent<UserInfoList>().AddUserInfoDatas(ref m_WholeChatDatas);

        m_LowerControlsTypes = LOWER_CONTROLS_TYPE.LCT_WHOLE_NOTE;
        m_bClient = false;


        m_UserInfoList.ResizeScroller();

        Debug.Log("WholeNote Connect Ok");

    }

    public void RefreshWholeChat()
    {
        m_UserInfoList.GetComponent<UserInfoList>().AddUserInfoDatas(ref m_WholeChatDatas);
        m_UserInfoList.ResizeScroller();
        m_LowerControlsTypes = LOWER_CONTROLS_TYPE.LCT_WHOLE_NOTE;

        // 2019-07-04
        m_GameUpperUI.m_AingTalkName.text = "전체";
    }

    public void SetWholeNote(string strUserID = "", string strContent = "", string strTime = "", int nDistance = 0)
    {
        // 정렬 
        // 있는지 검사 
        int nCount = m_WholeChatDatas.Count;
        UserInfoData userData = null;
        for (int i = 0; i < nCount; ++i)
        {
            userData = m_WholeChatDatas[i];
            if (strUserID == userData.strUserName)
            {
                userData.someText = strContent;
                //m_UserInfoList.ResizeScroller();
                return;
            }
        }

        SmallList<MessageControlData> MCDatas = new SmallList<MessageControlData>();

        MessageControlData MCData = new MessageControlData();
        MCData.m_bActive = true;
        MCData.m_strFile = ""; // 이미지 파일 위치
        MCDatas.Add(MCData);

        MCData = new MessageControlData();
        MCData.m_bActive = false;
        MCData.m_strFile = ""; // 이미지 파일 위치
        MCDatas.Add(MCData);

        UserInfoData data = new UserInfoData();
        data.cellSize = 0;
        data.bAvatarImage = true;
        data.strUserName = strUserID;
        data.strAge = "30";
        data.strSex = "남";
        data.strDistance = nDistance.ToString() + "Km";
        data.bChatRoomActive = false;
        data.m_MessageControlData = MCDatas;
        data.someText = strContent;
        data.strTime = strTime;


        m_WholeChatDatas.Add(data);
    }

    // 개인 쪽지 2020-12-08
    public void SetIndividualNote(string strUserID = "", string strOtherID = "", string strContent = "")
    {
        // 2019-07-16
        // 정렬 
        // 있는지 검사 
        int nCount = m_IndividualChatDatas.Count;
        UserInfoData userData = null;
        for (int i = 0; i < nCount; ++i)
        {
            userData = m_IndividualChatDatas[i];
            if (strOtherID == userData.strUserName) // 2020-12-21
            {
                userData.someText = strContent;
                return;
            }
        }

        UserInfoData data = new UserInfoData();
        data.cellSize = 0;
        data.bAvatarImage = true;
        //data.strUserName = strUserID;
        data.strUserName = strOtherID;
        data.strSex = "남";
        data.strAge = "31";
        data.bChatRoomActive = true;
        data.strDistance = "0Km";
        data.someText = strContent;
        m_IndividualChatDatas.Add(data);

        Debug.Log("IndividualNote Connect Ok");
    }

    public void RefreshIndividualChat()
    {
        m_UserInfoList.GetComponent<UserInfoList>().AddUserInfoDatas(ref m_IndividualChatDatas);
        m_UserInfoList.ResizeScroller();
        m_LowerControlsTypes = LOWER_CONTROLS_TYPE.LCT_INDIVIDUAL_NOTE;

        // 2019-07-04
        m_GameUpperUI.m_AingTalkName.text = "개인";
    }

    public void SetDialogueChatRoom(int nPosition, string strNick, string strOther, string strOtherMsg, string strTime)
    {
        // S->C : ChatListAns(...), ChatAns(...)
        // m_DyDialogueChatDatas 찾는다.
        // 자기자신하고 대화방에서 대화를 할수없다. 
        SmallList<DialogueRoomData> dialogueroomdatas = GetDialogueChatListRegister(strOther);
        if ( true == m_UserID.Equals(strOther) )
        {
            dialogueroomdatas = GetDialogueChatListRegister(strNick);
        }

       if (null == dialogueroomdatas)
        {
            Debug.Log("SetDialogueChatRoom(...) 함수 호출 :" + strNick + "," + strOther + "," + strOtherMsg + "," + strTime);
            return;
        }

        DialogueRoomData data = new DialogueRoomData();
        // true : 왼쪽, false : 오른쪽 (자신)
        //data.bPosition = strNick.Equals(m_UserID) ? true : false;
        data.bPosition = nPosition == 1 ? true : false; //2019-04-16
        data.cellSize = 0;
        data.bAvatarImage = data.bPosition;
        data.strOtherName = strOther;
        data.strUserName = strNick;
        data.strSex = "남";
        data.strAge = "31";
        data.bChatRoomActive = true;
        data.strDistance = "0Km";
        data.someText = strOtherMsg;
        data.strTiem = strTime;
        dialogueroomdatas.Add(data);

        m_DialogueList.GetComponent<DialogueList>().AddDialogueRoomDatas(ref dialogueroomdatas);

        Debug.Log("DialogueChatRoom Connect Ok");
    }

    public void RefreshDialogueChat()
    {
        m_DialogueList.ResizeScroller();
        m_LowerControlsTypes = LOWER_CONTROLS_TYPE.LCT_DIALOGUE_ROOM;

        // 2019-07-04
        m_GameUpperUI.m_AingTalkName.text = "대화방";
    }

    public SmallList<DialogueRoomData> GetDialogueChatListRegister(string strOther)
    {
        //  없거나 카운트가 0 이하일 때 로고를 남긴다.
        if ((m_DyDialogueChatDatas == null) || (m_DyDialogueChatDatas.Count <= 0))
        {
            // TODO : 에러값을 남깁니다.
            return null;
        }

        // 2. 있는지 검사 해서 있으면 값을 리턴시키고, 없으면 null로 리턴시킨다.
        if ( true == IsDialogueChatListRegister(strOther) )
        {
            return m_DyDialogueChatDatas[strOther];
        }

        return null;
    }

    public void OnClick()
    {
        Debug.Log("클릭");
    }

    private bool IsDialogueChatListRegister(string strID)
    {
        // 1. 값이 있으면 true, 없으면 false를 리턴시킵니다.
        if (true == m_DyDialogueChatDatas.ContainsKey(strID))
        {
            return true;
        }

        return false;

    }

    public SmallList<DialogueRoomData> AddDialogueChatListRegister(string strID, SmallList<DialogueRoomData> dialogueRoomDatas)
    {
        // 1. m_DyDialogueChatDatas 관리하는 녀석이 null이면 
        if ( null == m_DyDialogueChatDatas )
        {
            // TODO : 에러값을 남깁니다.
            Debug.Log("AddDialogueChatListRegister : null == m_DyDialogueChatDatas");
            return null;
        }

        // 2. 있는지 없는지 검사해서 없으면 레지스트에 등록합니다. 
        if (false == IsDialogueChatListRegister(strID) )
        {
            m_DyDialogueChatDatas.Add(strID, dialogueRoomDatas);
            return dialogueRoomDatas;
        }

        // TODO : 있으면 있다가 에러값을 남깁니다. 
        Debug.Log("AddDialogueChatListRegister : true == IsDialogueChatListRegister(strID)");
        return null;
    }

}
