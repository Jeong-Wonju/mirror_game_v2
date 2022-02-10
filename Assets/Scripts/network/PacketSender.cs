using UnityEngine;
using System.Collections;

public class PacketSender : MonoBehaviour
{
    byte[] buf = new byte[1024];

    void Start()
    {
        //SharedObject.g_PacketSender = this;
    }


    void MakeBuf(_PTCODE code, int offset)
    {
        byte[] sndbuf = new byte[offset];
        sndbuf.Initialize();

        System.Buffer.BlockCopy(buf, 0, sndbuf, 0, offset);

        NetworkManager_Talk.Instance.PacketSend(code, sndbuf);
    }


    void WriteString(string v, ref int offset, int max_len)
    {
        int len = v.Length * 2;
        System.Buffer.BlockCopy(v.ToCharArray(), 0, buf, offset, len);
        offset += (max_len * 2);
    }

    void WriteFloat(float v, ref int offset)
    {
        byte[] byte_v = System.BitConverter.GetBytes(v);
        System.Buffer.BlockCopy(byte_v, 0, buf, offset, byte_v.Length);
        offset += 4;
    }

    void WriteWord(ushort v, ref int offset)
    {
        NetworkManager_Talk.reverseIntToByte2(buf, v, offset);
        offset += 2;
    }

    void WriteInt(int v, ref int offset)
    {
        NetworkManager_Talk.reverseIntToByte4(buf, v, offset);
        offset += 4;
    }

    

    public void SendMemberJoin(string ID, float fX, float fY)
    {
        int offset = 0;

        WriteString(ID, ref offset, Define.MAX_NICKLEN);

        // 2019-02-20
        WriteFloat(fX, ref offset);
        WriteFloat(fY, ref offset);

        MakeBuf(_PTCODE.MEMBERJOIN_REQ, offset);
    }

    public void SendLogin(string userID, float fX, float fY )
    {
        int offset = 0;

        WriteInt(userID.Length, ref offset);
        WriteString(userID, ref offset, Define.MAX_NICKLEN);
        WriteFloat(fX, ref offset);
        WriteFloat(fY, ref offset);

        MakeBuf(_PTCODE.LOGIN_REQ, offset);

    }

    public void SendFireID(string fID)
    {
        int offset = 0;

        WriteInt(fID.Length, ref offset);
        WriteString(fID, ref offset, Define.MAX_FIDLEN);

        MakeBuf(_PTCODE.FID_REQ, offset);
    }
    

    public void SendUserInfo(string userID)
    {
        int offset = 0;

        
        WriteString(userID, ref offset, Define.MAX_NICKLEN);

        MakeBuf(_PTCODE.USERINFO_REQ, offset);
    }

    // 2019-07-22
    public void SendWholeChatReq(int nChatRoomType, string userID, string strContent, int nTime, int nDistance )
    {
        int offset = 0;

        WriteInt(nChatRoomType, ref offset);

        WriteWord((ushort)userID.Length, ref offset);
        WriteString(userID, ref offset, Define.MAX_NICKLEN);

        WriteWord((ushort)strContent.Length, ref offset);
        WriteString(strContent, ref offset, Define.MAX_MSGLEN);

        WriteInt(nTime, ref offset);    // 2019-08-19

        WriteInt(nDistance, ref offset);

        MakeBuf(_PTCODE.WHOLE_CHAT_REQ, offset);
    }

    public void SendChat(int nChatRoomType, string userID, string strContent)
    {
        int offset = 0;

        WriteInt(nChatRoomType, ref offset);

        WriteWord((ushort)userID.Length, ref offset);
        WriteString(userID, ref offset,Define.MAX_NICKLEN);

        WriteWord((ushort)strContent.Length, ref offset);
        WriteString(strContent, ref offset,Define.MAX_MSGLEN);

        MakeBuf(_PTCODE.CHAT_REQ, offset);
    }

    public void SendChatUser( int nChatRoomType )
    {
        int offset = 0;

        WriteInt(nChatRoomType, ref offset);
        MakeBuf(_PTCODE.CHATUSER_REQ, offset);
    }

    public void SendChatList(string OtherID, int nChatRoomType = 0)
    {
        int offset = 0;
        WriteWord((ushort)OtherID.Length, ref offset);
        WriteString(OtherID, ref offset, (ushort)OtherID.Length);

        MakeBuf(_PTCODE.CHATLIST_REQ, offset);
    }


}
