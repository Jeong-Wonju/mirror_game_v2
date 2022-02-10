using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using EnhancedUI;
using GameDefine_Talk;

public class PacketReceiver : MonoBehaviour
{
    public object SharedObject { get; private set; }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    ulong ReaduLong(byte[] buffer, ref int offset)
    {
        ulong u = System.BitConverter.ToUInt64(buffer, offset);
        offset += 8;
        return u;
    }

    // 2byte 얻어오기 
    ushort ReaduShort(byte[] buffer, ref int offset)
    {
        ushort u = System.BitConverter.ToUInt16(buffer, offset);
        offset += 2;
        return u;
    }

    // 4byte 얻어오기 
    int ReadInt(byte[] buffer, ref int offset)
    {
        int i = System.BitConverter.ToInt32(buffer, offset);
        offset += 4;
        return i;
    }

    float ReadFloat(byte[] buffer, ref int offset)
    {
        float f = System.BitConverter.ToSingle(buffer, offset);
        offset += 4;
        return f;
    }

    // 문자열 얻어오기 
    string ReadStringA(byte[] buffer, ref int offset, int strlen)
    {
        byte[] temp = new byte[strlen];
        temp.Initialize();
        System.Buffer.BlockCopy(buffer, offset, temp, 0, strlen);
        string s = System.Text.Encoding.UTF8.GetString(temp);
        offset += strlen;
        return s;
    }

    string ReadStringW(byte[] buffer, ref int offset, int strlen, int maxlen)
    {
        byte[] temp = new byte[strlen * 2];
        temp.Initialize();
        System.Buffer.BlockCopy(buffer, offset, temp, 0, strlen * 2);
        string s = System.Text.Encoding.Unicode.GetString(temp);
        offset += (maxlen * 2);
        return s;
    }

    void iCmdToStr(_PTCODE code)
    {
        Debug.Log("(R <--)" + code);
    }

    void ProductEnd(byte[] buffer)
    {
        Debug.Log("ProductEnd");

    }

    void LoginAns(byte[] buffer)
    {
        Debug.Log("ans Login");

        int offset = 4;

        // 유저 ID
        int ID = ReadInt(buffer, ref offset);

        ushort len = ReaduShort(buffer, ref offset);
        string Nick = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

        // lv 
        int nLv = ReadInt(buffer, ref offset);
        UserInfo.Instance.m_LevelText.text = nLv.ToString();

        int nGold = ReadInt(buffer, ref offset);
        UserInfo.Instance.m_GoldText.text = nGold.ToString();


        GameManager_Talk.Instance.m_UserID = Nick;
        NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendUserInfo(GameManager_Talk.Instance.m_UserID);

        NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendFireID(GameManager_Talk.Instance.m_Token);
    }

    void AcceptAns(byte[] buffer)
    {

        Debug.Log("Accept Ans");

        // 계정 검사
        string filepath = "";

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            filepath = Application.persistentDataPath + "/AingTalk/" + "info.txt";
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            filepath = Application.dataPath + "/AingTalk/" + "info.txt";
        }

        // 계정 ID가 있으면 
        if (System.IO.File.Exists(filepath) == true)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);

            string strUserID = file.ReadLine();
            UserInfo.Instance.m_strUserID.text = strUserID;

            Debug.Log("user ID : " + strUserID);

            file.Close();

            //float fX = (float)NativeToolkit.GetLongitude();
            //float fY = (float)NativeToolkit.GetLatitude();
            //Debug.Log("fX =" + fX.ToString() + "fY =" + fY.ToString());
            NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendLogin(strUserID, 0.0f, 0.0f);
            Debug.Log(strUserID);
        }
        else
        {
            // 계정이 없으면 계정생성 UI 활성화 
            GameManager_Talk.Instance.m_accountCreateUI.gameObject.SetActive(true);
        }
    }

    void MemberJoinAns(byte[] buffer)
    {
        // 2019-02-19
        Debug.Log("ans member join");

        // 계정생성 UI 비활성화
        GameManager_Talk.Instance.m_accountCreateUI.gameObject.SetActive(false);

        int offset = 4;

        // 응답
        int Answer = ReadInt(buffer, ref offset);
        ushort UserIDLen = (ushort)ReaduShort(buffer, ref offset);
        GameManager_Talk.Instance.m_UserID = ReadStringW(buffer, ref offset, UserIDLen, Define.MAX_NICKLEN);
        UserInfo.Instance.m_strUserID.text = GameManager_Talk.Instance.m_UserID;


        Debug.Log(GameManager_Talk.Instance.m_UserID);

        if (GameManager_Talk.Instance.m_UserID == "") return;

        string filepath = "";

        // 계정을 폰에 저장 
        if (Application.platform == RuntimePlatform.Android)
        {
            // AingTalk : 폰 폴더생성 해서 저장함 
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/AingTalk/");
            filepath = Application.persistentDataPath + "/AingTalk/" + "info.txt";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/AingTalk/");
            filepath = Application.persistentDataPath + "/AingTalk/" + "info.txt";
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            System.IO.Directory.CreateDirectory(Application.dataPath + "/AingTalk/");
            filepath = Application.dataPath + "/AingTalk/" + "info.txt";
        }

        System.IO.StreamWriter file = new System.IO.StreamWriter(filepath);

        file.WriteLine(GameManager_Talk.Instance.m_UserID);
        Debug.Log("file save ID : " + GameManager_Talk.Instance.m_UserID);

        file.Close();

        Debug.Log("Member Join Ans");

        // 로그인
        //float fX = (float)NativeToolkit.GetLongitude();
        //float fY = (float)NativeToolkit.GetLatitude();
        GetComponent<PacketSender>().SendLogin(GameManager_Talk.Instance.m_UserID, 0.0f, 0.0f);
    }

    // 2019-07-22
    public void WholeChatAns(byte[] buffer)
    {
        Debug.Log("WholeChat Ans");

        int offset = 4;

        int nChatRoomType = ReadInt(buffer, ref offset);

        ushort len = ReaduShort(buffer, ref offset);
        string strNick = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

        len = ReaduShort(buffer, ref offset);
        string strContent = ReadStringW(buffer, ref offset, len, Define.MAX_MSGLEN);

        //len = ReaduShort(buffer, ref offset);
        //string strTime = ReadStringW(buffer, ref offset, len, Define.MAX_MSGLEN);

        // 2020-10-27
        int nTime = ReadInt(buffer, ref offset);
        string strTime = nTime.ToString();

        int nDistance = ReadInt(buffer, ref offset);

        // 시간계산 해서 저장한다. 
       //System.DateTime
        GameManager_Talk.Instance.SetWholeNote( strNick, strContent, strTime, nDistance );
        GameManager_Talk.Instance.RefreshWholeChat();
    }

    public void ChatAns(byte[] buffer)
    {
        Debug.Log("Chat Ans");

        int offset = 4;

        int nChatRoomType = ReadInt(buffer, ref offset);

        ushort len = ReaduShort(buffer, ref offset);
        string strNick = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

        len = ReaduShort(buffer, ref offset);
        string strContent = ReadStringW(buffer, ref offset, len, Define.MAX_MSGLEN);

        // 전체, 쪽지, 대화방  
        if (0 == nChatRoomType)
        {
            GameManager_Talk.Instance.SetWholeNote(strNick, strContent);
            GameManager_Talk.Instance.RefreshWholeChat();
        }
        else if (1 == nChatRoomType)
        {
            // 2020-12-21
            GameManager_Talk.Instance.SetIndividualNote("", strNick, strContent);
            GameManager_Talk.Instance.RefreshIndividualChat();   // 2019-07-16
        }
        else if (2 == nChatRoomType)
        {
            // 2020-11-03
            len = ReaduShort(buffer, ref offset);
            string strTime = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

            int nPosition = ReadInt(buffer, ref offset);    // 2020-11-30

            GameManager_Talk.Instance.SetDialogueChatRoom(nPosition, GameManager_Talk.Instance.m_UserID, strNick, strContent, strTime);

            GameManager_Talk.Instance.RefreshDialogueChat();
        }


        Debug.Log("IndividualNote Connect Ok");
    }

    public void UserInfoAns(byte[] buffer)  
    {
        Debug.Log("User Info Ans");
        int offset = 4;

        //// 응답   
        //int Count = ReadInt(buffer, ref offset);

        //Debug.Log("otherUser count : " + Count);

        //if (GameManager_Talk.Instance.m_WholeChatDatas == null)
        //{
        //    GameManager_Talk.Instance.m_WholeChatDatas = new SmallList<UserInfoData>();
        //}

        //for (int i = 0; i < Count; ++i)
        //{

        //    UserInfoData data = new UserInfoData();
        //    SmallList<MessageControlData> MCDatas = new SmallList<MessageControlData>();

        //    MessageControlData MCData = new MessageControlData();
        //    MCData.m_bActive = true;
        //    MCData.m_strFile = ""; // 이미지 파일 위치
        //    MCDatas.Add(MCData);

        //    MCData = new MessageControlData();
        //    MCData.m_bActive = true;
        //    MCData.m_strFile = ""; // 이미지 파일 위치
        //    MCDatas.Add(MCData);

        //    int ID = ReadInt(buffer, ref offset);
        //    ushort len = ReaduShort(buffer, ref offset);
        //    string Nick = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

        //    data.cellSize = 0;
        //    data.bAvatarImage = true;
        //    data.strUserName = Nick;
        //    data.strSex = i % 2 == 0 ? "남" : "여";
        //    // 내 정보 
        //    if (GameManager_Talk.Instance.m_UserID == Nick)
        //    {
        //        //float fX = (float)NativeToolkit.GetLongitude();
        //        //float fY = (float)NativeToolkit.GetLatitude();
        //        //data.strDistance = fX.ToString() + "," + fY.ToString() + "Km";
        //        data.strDistance = "100Km";
        //    }
        //    else
        //    {
        //        data.strDistance = "100Km";
        //    }
        //    data.strAge = i % 2 == 0 ? "23" : "35";
        //    data.bChatRoomActive = false;
        //    data.m_MessageControlData = MCDatas;
        //    data.someText = "";
        //    GameManager_Talk.Instance.m_WholeChatDatas.Add(data);

        //    //m_NtMger.m_GameMgr.m_UserInfoList.GetComponent<UserInfoList>().AddUserInfoData(ID, Nick);
        //}

        //GameManager_Talk.Instance.m_UserInfoList.GetComponent<UserInfoList>().AddUserInfoDatas(ref GameManager_Talk.Instance.m_WholeChatDatas);

        //GameManager_Talk.Instance.m_UserInfoList.ResizeScroller();

        // 전체 채팅 패킷을 날린다. 2019-07-22 
        NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendChatUser(0);
    }

    public void ChatUserAns(byte[] buffer)
    {
        Debug.Log("ChatUserAns");
        int offset = 4;

        int nChatRoomType = ReadInt(buffer, ref offset);
        int nChatUserCount = ReadInt(buffer, ref offset);

        Debug.Log("ChatRoomType :  " + nChatRoomType + "otherUser count : " + nChatUserCount);

        if (0 == nChatRoomType)
        {
            if (null == GameManager_Talk.Instance.m_WholeChatDatas)
            {
                GameManager_Talk.Instance.m_WholeChatDatas = new SmallList<UserInfoData>();
            }

            // 2020-11-26
            for (int i = 0; i < nChatUserCount; ++i)
            {
                ushort len = ReaduShort(buffer, ref offset);
                string Nick = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

                len = ReaduShort(buffer, ref offset);
                string ChatMsg = ReadStringW(buffer, ref offset, len, Define.MAX_MSGLEN);

                int nSecond = ReadInt(buffer, ref offset);
                int nDistance = ReadInt(buffer, ref offset);

                GameManager_Talk.Instance.SetWholeNote(Nick, ChatMsg, nSecond.ToString(), nDistance );
            }
        }
        else if (1 == nChatRoomType)
        {
            if (null == GameManager_Talk.Instance.m_IndividualChatDatas)
            {
                GameManager_Talk.Instance.m_IndividualChatDatas = new SmallList<UserInfoData>();
            }

            for (int i = 0; i < nChatUserCount; ++i)
            {
                int ID = ReadInt(buffer, ref offset);

                ushort len = ReaduShort(buffer, ref offset);
                string Nick = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

                // 2020-12-01
                len = ReaduShort(buffer, ref offset);
                string Other = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

                len = ReaduShort(buffer, ref offset);
                string ChatMsg = ReadStringW(buffer, ref offset, len, Define.MAX_MSGLEN);

                GameManager_Talk.Instance.SetIndividualNote(Nick, Other, ChatMsg);
            }
        }

        if (0 == nChatRoomType)
        {
            GameManager_Talk.Instance.RefreshWholeChat();
            // 쪽지 패킷보낸다.
            NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendChatUser(1);
        }
    }

    public void ChatListAns(byte[] buffer)
    {
        Debug.Log("ChatListAns");
        int offset = 4;

        ushort len = ReaduShort(buffer, ref offset);
        string strOtherNick = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);
        int nChatListCount = ReadInt(buffer, ref offset);

        Debug.Log("ChatListAns :  " + "otherUser count : " + nChatListCount);

        // 2019-04-04
        SmallList<DialogueRoomData> dialogueroomdatas = GameManager_Talk.Instance.GetDialogueChatListRegister(strOtherNick);
        if (null == dialogueroomdatas)
        {
            dialogueroomdatas = GameManager_Talk.Instance.AddDialogueChatListRegister(strOtherNick, new SmallList<DialogueRoomData>());
        }

        if (dialogueroomdatas.Count == nChatListCount)
        {
            // 같으면 
            GameManager_Talk.Instance.m_DialogueList.GetComponent<DialogueList>().AddDialogueRoomDatas(ref dialogueroomdatas);
            GameManager_Talk.Instance.RefreshDialogueChat();
            return;
        }

        for (int i = 0; i < nChatListCount; ++i)
        {
            // 자신 
            len = ReaduShort(buffer, ref offset);
            string Nick = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

            // 상대방 
            len = ReaduShort(buffer, ref offset);
            strOtherNick = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

            // 상대방 메시지 
            len = ReaduShort(buffer, ref offset);
            string strOtherMsg = ReadStringW(buffer, ref offset, len, Define.MAX_MSGLEN);

            // 메시지 보낸시간 
            len = ReaduShort(buffer, ref offset);
            string strTime = ReadStringW(buffer, ref offset, len, Define.MAX_NICKLEN);

            // 위치 
            int nPosition = ReadInt(buffer, ref offset);

            GameManager_Talk.Instance.SetDialogueChatRoom(nPosition, Nick, strOtherNick, strOtherMsg, strTime);
        }

        GameManager_Talk.Instance.RefreshDialogueChat();

    }

    public void PushAns(byte[] buffer)
    {
        Debug.Log("PushAns");
        int offset = 4;

        int len = ReadInt(buffer, ref offset);
        string strfID = ReadStringW(buffer, ref offset, len, Define.MAX_FIDLEN);

        len = ReadInt(buffer, ref offset);
        string strMsg = ReadStringW(buffer, ref offset, len, Define.MAX_MSGLEN);

        //Debug.Log(strfID);
        //Debug.Log(strMsg);

        GameManager_Talk.Instance.GetComponent<SendPush>().Push(strfID, strMsg);
    }

    public void PacketParse(_PTCODE code, byte[] buffer)
    {
        iCmdToStr(code);

        switch (code)
        {
            case _PTCODE.GENERAL_FAILURE: break;
            case _PTCODE.ACCEPT_ANS: AcceptAns(buffer); break;
            case _PTCODE.MEMBERJOIN_ANS: MemberJoinAns(buffer); break;
            case _PTCODE.LOGIN_ANS: LoginAns(buffer); break;
            case _PTCODE.USERINFO_ANS: UserInfoAns(buffer); break;
            case _PTCODE.WHOLE_CHAT_ANS: WholeChatAns(buffer); break;  // 2019-07-22
            case _PTCODE.CHAT_ANS: ChatAns(buffer); break;
            case _PTCODE.CHATUSER_ANS: ChatUserAns(buffer); break;
            case _PTCODE.CHATLIST_ANS: ChatListAns(buffer); break;
            case _PTCODE.PUSH_ANS: PushAns(buffer); break;
            default: Debug.Log("PacketParse : default " + code); break;
        }
    }

}
