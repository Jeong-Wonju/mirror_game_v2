using System;
using System.Runtime;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


namespace FoxSDK
{
    public partial struct GameNetMessage
    {
        public const int MAX_CHAT_MSG = 128;
        public const int MAX_NAME = 32;
        public const int LOGIN_SIZE = 102;

        /// <summary>
        /// Net message type.
        /// </summary>
        public enum MsgType
        {
            GENERAL_FAILURE = 1,

            ADMIN_MSG,          // ���� 

            DISCONNECT_NOTI,    // ���� ���� �˸�

            LOGOUT_REQ,         // ���α׷� ����
            LOGOUT_ANS,         // 
            LOGOUT_NOTI,        // 

            ENTRANCE_REQ,       // ����
            ENTRANCE_ANS,       // 
            ENTRANCE_NOTI,      // 

            EXIT_REQ,           // ���� ����/��� ��û 
            EXIT_ANS,
            EXIT_NOTI,          // ���� ����/��� ��Ȳ�� �˸�

            WORLDENTER_ANS,     // ���忡 ����
            WORLDLEAVE_REQ,     // ���忡�� ���� ��û
            WORLDLEAVE_ANS,     // ���忡�� ���� �˸�

            CHAT_MSG,           // ä�� 

            SYSTEM_MSG,         // �ý��� ���� �˸�

            ACCEPT_AUTH_ANS,         // �α��� ���� ���� ����
            ACCEPT_GAME_ANS,         // ���� ���� ���� ����

            MEMBERJOIN_REQ,     // ȸ������ 
            MEMBERJOIN_ANS,
            REMEMBERJOIN_ANS,   // �߸��� ȸ�� �˸�

            LOGIN_REQ,
            LOGIN_ANS,
            LOGIN_NOTI,

            SERVERLIST_REQ,
            SERVERLIST_ANS,

            USER_IDENTIFIER_REQ,
            USER_IDENTIFIER_ANS,

            FID_REQ,
            PUSH_ANS,

            //USERINFO_REQ,
            USERINFO_ANS,

            WHOLE_CHAT_REQ,     // ��ü ä�ù� 
            WHOLE_CHAT_ANS,     // ��ü ��û ���� �������� ���� 

            CHAT_REQ,           // ���濡�� ù��° ä�� , ĳ�� �Ҹ�
            CHAT_ANS,           // ��û ���� �������� �޽��� ����

            CHATUSER_REQ,       // ä���� ���� ���
            CHATUSER_ANS,

            CHATLIST_REQ,       // ��ȭ ���
            CHATLIST_ANS,
            STAGE_CLEAR_REQ,
            STAGE_CLEAR_ANS, // �������� Ŭ����
            USER_GOODS_UPGRADE_REQ,  // STAGE ��ȭ ��
            USER_GOODS_UPGRADE_ANS,
            USER_GOODS_GET_REQ, // �ڿ� ȹ��
            USER_GOODS_GET_ANS,
            RESPAWN_START_TIME,  // �ݶ��� �����ϱ� ���� ���� �ð�
            CONQUEST_REQ,         // �������� ���� ����
            CONQUEST_ANS,

            GET_CARD_REQ,       //ī�� ȹ��
            GET_CARD_ANS,

            MIX_CARD_REQ,       //ī�� �ռ�
            MIX_CARD_ANS,
            CASTLE_CONQUEST_REQ,
            CASTLE_CONQUEST_ANS,    //�� ���� ���� ����

            UNIT_CARD_APPOINT_REQ,
            UNIT_CARD_APPOINT_ANS, //�δ� ��ġ ���� ����

            SELECT_DROP_ID_REQ,
            SELECT_DROP_ID_ANS,

            BATTLE_START_REQ,   //���� ����
            BATTLE_START_ANS,

            CASTLE_UPGRADE_REQ, //�� ���׷��̵� 
            CASTLE_UPGRADE_ANS,

            CASTLE_DEPLOY_REQ, //�� ���� 
            CASTLE_DEPLOY_ANS,

            ACCEPT_BATTLE_ANS,
            BATTLE_CONNECT_REQ,
            BATTLE_CONNECT_ANS,

            BATTLE_REPORT_ANS,

            REBELLION_REQ,  //�ݶ��� ħ��
            REBELLION_ANS,

            GACHA_REQ,  //��í
            GACHA_ANS,

            CRYSTAL_REQ, //ũ����Ż ��û3
            CRYSTAL_ANS,

            DRAGON_EXPEDITION_REQ,  //�巡�� Ž��
            DRAGON_EXPEDITION_ANS,

            PVP_DUMMYUSER_REG,

            PVP_USERLIST_REQ,
            PVP_USERLIST_ANS,

            PVP_REQ,
            PVP_ANS,

            PVP_LOSE_NOTI,

            PVP_STAGEINFO_REQ,
            PVP_STAGEINFO_ANS,

            PVP_GOODSGET_REQ,
            PVP_GOODSGET_ANS,

            CONTINUE_BATTLE_REPORT_REQ, // �߰� �������� ��û
            CONTINUE_BATTLE_REPORT_ANS,

            BATTLE_END_REQ,
            BATTLE_END_ANS,

            USER_UNIT_DECK_SAVE_REQ,    //���� ���ֵ� ����
            USER_UNIT_DECK_SAVE_ANS,
            OPEN_SHOP_REQ,
            OPEN_SHOP_ANS,

            CARD_STACK_REQ,
            CARD_STACK_ANS,

            CARD_DIVISION_REQ,
            CARD_DIVISION_ANS,

            BATTLE_DRAGON_EXPEDITION_REQ,   //�巡�� Ž�� ����
            BATTLE_DRAGON_EXPEDITION_ANS,
            BUY_SHOP_PRODUCT_REQ,
            BUY_SHOP_PRODUCT_ANS,
            VESITON_CHECK_ANS,  //���� üũ

            REFRESH_CARD_LIST_REQ,
            REFRESH_CARD_LIST_ANS,

            GET_MISSION_LIST_REQ,
            GET_MISSION_LIST_ANS,

            QUEST_CLEAR_REQ,
            QUEST_CLEAR_ANS,

            PVP_END_REQ, //PVP ����
            PVP_END_ANS,

            LOGIN_CHECK_ANS, //�α��� üũ

            PVP_COUNT_ADD_REQ, //PVP ���� Ƚ�� 
            PVP_COUNT_ADD_ANS,

            TUTORIAL_STEP_REQ,   //Ʃ�丮�� �Ϸ� �� ���� ������Ʈ
            TUTORIAL_STEP_ANS,

            TUTORIAL_GACHA_REQ, //Ʃ�丮�� �̱�
            TUTORIAL_GACHA_ANS,

            TUTORIAL_GOODSGET_REQ, //Ʃ�丮�� �ڿ�ȹ��
            TURORIAL_GOODSGET_ANS,

            CREATE_GAME_ID_REQ,
            CREATE_GAME_ID_ANS,

            GOOGLE_PLAY_LOGIN_REQ,
            GOOGLE_PLAY_LOGIN_ANS,

            GOOGLE_ACCOUNT_LINK_REQ,
            GOOGLE_ACCOUNT_LINK_ANS,

            GOOGLE_KEY_CHECK_REQ,
            GOOGLE_KEY_CHECK_ANS,

            TUTORIAL_COMPLETE_REQ,   //Ʃ�丮�� �Ϸ� �� ���� ������Ʈ
            TUTORIAL_COMPLETE_ANS,

            DRAGON_EXPEDITION_ADD_COUNT_REQ,
            DRAGON_EXPEDITION_ADD_COUNT_ANS,

            PVP_LOGIN_LOSE_NOTI_REQ,
            PVP_LOGIN_LOSE_NOTI_ANS,

            SELL_CARD_REQ,
            SELL_CARD_ANS,

            PVP_MATCHING_LIST_REQ,
            PVP_MATCHING_LIST_ANS,

            TUTORIAL_BATTLE_REQ,
            TUTORIAL_BATTLE_ANS,

            SKIP_PVP_REQ,
            SKIP_PVP_ANS,

            SKIP_CONQUEST_REQ,
            SKIP_CONQUEST_ANS,

            SKIP_REBELLION_REQ,
            SKIP_REBELLION_ANS,

            SKIP_DRAGON_EXPEDION_REQ,
            SKIP_DRAGON_EXPEDION_ANS,

            SKIP_BATTLE_START_REQ,
            SKIP_BATTLE_START_ANS,

            SKIP_BATTLE_CONNECT_REQ,
            SKIP_BATTLE_CONNECT_ANS,

            STAGE_REBELLION_CHECK_REQ,
            STAGE_REBELLION_CHECK_ANS,

            GAMBLE_REQ,
            GAMBLE_ANS,

            ALL_USER_GOODS_GET_REQ,
            ALL_USER_GOODS_GET_ANS,

            ATTENDANCE_CHECK_REQ,
            ATTENDANCE_CHECK_ANS,

            ATTENDANCE_GET_REQ,
            ATTENDANCE_GET_ANS,

            CHAT_MSG_REQ,
            CHAT_MSG_ANS,

            TUTORIAL_DRAGON_EXPEDION_REQ,
            TUTORIAL_DRAGON_EXPEDION_ANS,

            TUTORIAL_DRAGON_COMPLETE_REQ,
            TUTORIAL_DRAGON_COMPLETE_ANS,

            ALL_USER_GOODS_GET_CHECK_REQ,
            ALL_USER_GOODS_GET_CHECK_ANS,

            BATTLE_ACTION_REQ,
            BATTLE_ACTION_ANS,

            HERO_ACTION_REQ,
            HERO_ACTION_ANS,

            BATTLE_DELAY_TURN_REQ,
            BATTLE_DELAY_TURN_ANS,

            BATTLE_UNIT_MOVE_REQ,
            BATTLE_UNIT_MOVE_ANS,

            BATTLE_UNIT_ATTACK_REQ,
            BATTLE_UNIT_ATTACK_ANS,

            BATTLE_END_TURN_REQ,
            BATTLE_END_TURN_ANS,

            MAIL_LIST_REQ,
            MAIL_LIST_ANS,

            MAIL_ITEM_REQ,
            MAIL_ITEM_ANS,

            BATTLE_GIVE_UP_REQ,
            BATTLE_GIVE_UP_ANS,

            BATTLE_UNIT_DEFENSE_REQ,
            BATTLE_UNIT_DEFENSE_ANS,

            GUILD_CREATE_REQ,
            GUILD_CREATE_ANS,

            GUILD_JOIN_REQ,
            GUILD_JOIN_ANS,

            GUILD_JOINCANCEL_REQ,
            GUILD_JOINCANCEL_ANS,

            GUILD_GROUPLIST_REQ,
            GUILD_GROUPLIST_ANS,

            GUILD_MEMBERLIST_REQ,
            GUILD_MEMBERLIST_ANS,

            GUILD_MEMBERJOINLIST_REQ,
            GUILD_MEMBERJOINLIST_ANS,

            GUILD_MEMBERJOINPERMIT_REQ, //�¶�
            GUILD_MEMBERJOINPERMIT_ANS,

            GUILD_MEMBERJOINREJECT_REQ, //����
            GUILD_MEMBERJOINREJECT_ANS,

            GUILD_MEMBEREXILE_REQ, //��� �߹�
            GUILD_MEMBEREXILE_ANS,

            GUILD_INTRODUCE_REQ,
            GUILD_INTRODUCE_ANS,

            GUILD_NOTICE_REQ,
            GUILD_NOTICE_ANS,

            GUILD_LEAVE_REQ, //Ż��
            GUILD_LEAVE_ANS,

            GUILD_BATTLEINFO_REQ,
            GUILD_BATTLEINFO_ANS,

            GUILD_BATTLEREADY_REQ,
            GUILD_BATTLEREADY_ANS,

            GUILD_BATTLEMATCHING_REQ,
            GUILD_BATTLEMATCHING_ANS,

            GUILD_ENEMYDECK_REQ,
            GUILD_ENEMYDECK_ANS,

            GUILD_BATTLE_REQ,
            GUILD_BATTLE_ANS,

            ITEM_SELL_REQ,
            ITEM_SELL_ANS,

            CHARACTER_INVENTORY_REQ,
            CHARACTER_INVENTORY_ANS,

			AUTO_BATTLE_ATTACK_REQ,
            AUTO_BATTLE_ATTACK_ANS,            GAME_ERROR_ANS = 60000,
            BATTLE_ERROR_ANS = 60001,

            _MSG_RECV_HEART = 65534,

            _MSG_SEND_HEART = 65535,

        };


        public enum ErrorCode
        {
            ERROR_DUPLICATE_NICKNAME,
            ERROR_INVAILDATE_USER_DATA,

            ERROR_NOT_ENOUGH_RESOURCE,
            ERROR_NOT_ENOUGH_FOOD,

            ERROR_NETWORK_CONNECT,
            ERROR_CARD_INVENTORY_MAX,

            ERROR_CONQUEST_TEAM_INVALIDATE,
            ERROR_CONQUEST_ENEMY_INVALIDATE,

            ERROR_PVP_TEAM_INVALIDATE,
            ERROR_PVP_ENEMY_INVALIDATE,

            ERROR_DRAGON_TEAM_INVALIDATE,
            ERROR_DRAGON_ENEMY_INVALIDATE,

            ERROR_CARD_DATA_INVALIDATE,
            ERROR_CANNOT_SELL_CARD_BY_CHECK,

            ERROR_USER_ID_NOT_FOUND,
            ERROR_MIX_BY_STACKED_CARD,

            ERROR_UNDEFINED,

            ERROR_DECK,

            ERROR_STAGE_INFO_SELECT,
            ERROR_STAGE_INFO_UPDATE,
            ERROR_OBJECTID_NULL,

            ERROR_STAGE_OBJ_INFO_SELECT,
            ERROR_STAGE_OBJ_INFO_OBJECTID_NULL,

            ERROR_DROP_INFO,

            ERROR_BATTLE_UNDEFINED,

            ERROR_LUCK_TICKET,

            ERROR_INVALID_CHAR_NICKNAME,
            ERROR_GUILD_ALREADY_WAIT,
            ERROR_GOOGLE,
        };

        public enum BATTLE_ERROR_CODE
		{
            BATTLE_ERROR_ACTION_SUCCESS,
            BATTLE_ERROR_NOT_ENAUGH_MP,
            BATTLE_ERROR_SKILL_DATA_NOT_FOUND,
            BATTLE_ERROR_HERO_SKILL_DATA_NOT_FOUND,
            BATTLE_TARGET_NOT_FOUND,
            BATTLE_ERROR_CANNOT_MOVE,
        };

        public interface NetMsgHeadInterface
        {
            byte[] toBytesArray();
            void WriteSendBuffer(byte[] buf, int offset);
            void ReadRecvBuffer(byte[] bytes);
        }

        /// <summary>
        /// Net message head.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct NetMsgHead : NetMsgHeadInterface
        {
            // first 2bytes size, next 2bytes type.
            public ushort size;
            public ushort type;

            public NetMsgHead(ushort s = 0, ushort t = 0)
            {
                size = s;
                type = t;
            }


            public void WriteSendBuffer(byte[] buf, int offset) { }

            // use for safe mode.
            public byte[] toBytesArray() { return null; }
            public void ReadRecvBuffer(byte[] bytes) { }


        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BASE_REQ : NetMsgHeadInterface
        {
            public NetMsgHead head;

            public byte[] sndbuf;

            public void WriteSendBuffer(Byte[] buf, int offset)
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GENERAL_FAILURE;

                sndbuf = new byte[offset];
                sndbuf.Initialize();

                System.Buffer.BlockCopy(buf, 0, sndbuf, 0, offset);

                head.size = (ushort)(head.size + offset);
            }

            virtual public byte[] toBytesArray()
            {
                int pos = 0;
                int DataByteSize = sndbuf.GetLength(0);

                byte[] bytes = new byte[head.size + DataByteSize];

                BitConverter.GetBytes(head.size).CopyTo(bytes, pos); pos += 2;
                BitConverter.GetBytes(head.type).CopyTo(bytes, pos); pos += 2;

                System.Buffer.BlockCopy(sndbuf, 0, bytes, pos, DataByteSize);

                return bytes;
            }

            public void ReadRecvBuffer(byte[] bytes) { }

        };



        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _MEMBERJOIN_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.MEMBERJOIN_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _LOGIN_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.LOGIN_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _SERVERLIST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.SERVERLIST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _CREATE_GAME_ID_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CREATE_GAME_ID_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GOOGLE_PLAY_LOGIN_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GOOGLE_PLAY_LOGIN_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GOOGLE_ACCOUNT_LINK_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GOOGLE_ACCOUNT_LINK_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GOOGLE_KEY_CHECK_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GOOGLE_KEY_CHECK_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _USER_IDENTIFIER_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.USER_IDENTIFIER_REQ;

                return base.toBytesArray();
            }

        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _REQ_CONQUEST : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CONQUEST_REQ;

                return base.toBytesArray();
            }

        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _SKIP_CONQUEST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.SKIP_CONQUEST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _STAGE_CLEAR_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.STAGE_CLEAR_REQ;

                return base.toBytesArray();
            }

        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _USER_GOODS_UPGRADE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.USER_GOODS_UPGRADE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _USER_GOODS_GET_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.USER_GOODS_GET_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GET_CARD_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GET_CARD_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _MIX_CARD_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.MIX_CARD_REQ;

                return base.toBytesArray();
            }

        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _CASTLE_CONQUEST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CASTLE_CONQUEST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _UNIT_CARD_APPOINT_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.UNIT_CARD_APPOINT_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _SELECT_DROP_ID_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.SELECT_DROP_ID_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BATTLE_START_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_START_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _SKIP_BATTLE_START_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.SKIP_BATTLE_START_REQ;

                return base.toBytesArray();
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _CASTLE_UPGRADE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CASTLE_UPGRADE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _CASTLE_DEPLOY_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CASTLE_DEPLOY_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BATTLE_CONNECT_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_CONNECT_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _SKIP_BATTLE_CONNECT_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.SKIP_BATTLE_CONNECT_REQ;

                return base.toBytesArray();
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _REBELLION_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.REBELLION_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _SKIP_REBELLION_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.SKIP_REBELLION_REQ;

                return base.toBytesArray();
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GACHA_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GACHA_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _CRYSTAL_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CRYSTAL_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _DRAGON_EXPEDITION_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.DRAGON_EXPEDITION_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _PVP_DUMMYUSER_REG : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.PVP_DUMMYUSER_REG;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _CONTINUE_BATTLE_REPORT_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CONTINUE_BATTLE_REPORT_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BATTLE_END_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_END_REQ;

                return base.toBytesArray();
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _PVP_USERLIST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.PVP_USERLIST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _PVP_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.PVP_REQ;

                return base.toBytesArray();
            }
        };

        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILDBATTLE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_BATTLE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _SKIP_PVP_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.SKIP_PVP_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _PVP_STAGEINFO_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.PVP_STAGEINFO_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _PVP_GOODSGET_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.PVP_GOODSGET_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _USER_UNIT_DECK_SAVE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.USER_UNIT_DECK_SAVE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _OPEN_SHOP_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.OPEN_SHOP_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BUY_SHOP_PRODUCT_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BUY_SHOP_PRODUCT_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _CARD_STACK_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CARD_STACK_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _CARD_DIVISION_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CARD_DIVISION_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BATTLE_DRAGON_EXPEDITION_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_DRAGON_EXPEDITION_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _SKIP_DRAGON_EXPEDION_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.SKIP_DRAGON_EXPEDION_REQ;

                return base.toBytesArray();
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _PVP_END_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.PVP_END_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _REFRESH_CARD_LIST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.REFRESH_CARD_LIST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _QUEST_CLEAR_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.QUEST_CLEAR_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GET_MISSION_LIST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GET_MISSION_LIST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _TUTORIAL_STEP_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.TUTORIAL_STEP_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _TUTORIAL_COMPLETE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.TUTORIAL_COMPLETE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _DRAGON_EXPEDITION_ADD_COUNT_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.DRAGON_EXPEDITION_ADD_COUNT_REQ;

                return base.toBytesArray();
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _PVP_COUNT_ADD_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.PVP_COUNT_ADD_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _PVP_LOGIN_LOSE_NOTI_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.PVP_LOGIN_LOSE_NOTI_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _SELL_CARD_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.SELL_CARD_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _PVP_MATCHING_LIST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.PVP_MATCHING_LIST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _TUTORIAL_GACHA_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.TUTORIAL_GACHA_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _TUTORIAL_GOODSGET_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.TUTORIAL_GOODSGET_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _TUTORIAL_BATTLE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.TUTORIAL_BATTLE_REQ;

                return base.toBytesArray();
            }
        };

        //////////////////////////////////////////////////////////////////////////
        // chat

        /// <summary>
        /// send and recv chat msg.
        /// </summary>
        /*[ StructLayout( LayoutKind.Sequential , Pack = 1 ) ]
		public struct SEND_RECV_MSG_CHAT_MSG : NetMsgHeadInterface
		{
			public void initNetHead()
			{
				// use one byte aligned , you must make sure the size is right.
				head.size = (ushort)( sizeof( int ) + sizeof( int ) + MAX_NAME + MAX_CHAT_MSG );
				head.type = (ushort)MsgType._MSG_SEND_CHAT_MSG;
			}

			public void WriteSendBuffer(byte[] buf, int offset) { }

			public NetMsgHead head;

			public int	ChatType;

			public GameBitArray32 Name;
			public GameBitArray32Bytes Chat;

			public byte[] toBytesArray()
			{
				int pos = 0;
				byte[] bytes = new byte[ head.size ];
				BitConverter.GetBytes( head.size ).CopyTo( bytes , pos ); pos += 2;
				BitConverter.GetBytes( head.type ).CopyTo( bytes , pos ); pos += 2;

				BitConverter.GetBytes( ChatType ).CopyTo( bytes , pos ); pos += 4;

				for ( int i = 0; i < MAX_NAME ; i++ ) 
				{
					bytes[ pos ] = Name[ i ]; pos += 1;
				}

				for ( int i = 0; i < MAX_CHAT_MSG ; i++ ) 
				{
					bytes[ pos ] = Chat[ i ]; pos += 1;
				}

				return bytes;
			}

			public void ReadRecvBuffer( byte[] bytes )
			{
				int pos = 0;
				head.size = BitConverter.ToUInt16( bytes , pos ); pos += 2;
				head.type = BitConverter.ToUInt16( bytes , pos ); pos += 2;

				ChatType = BitConverter.ToInt32( bytes , pos ); pos += 4;

				for ( int i = 0; i < MAX_NAME ; i++ ) 
				{
					Name[ i ] = bytes[ pos ]; pos += 1;
				}

				for ( int i = 0; i < MAX_CHAT_MSG ; i++ ) 
				{
					Chat[ i ] = bytes[ pos ]; pos += 1;
				}

			}
		};
		*/


        /// <summary>
        /// send and recv heart msg.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SEND_RECV_MSG_HEART : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType._MSG_SEND_HEART;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }

            public NetMsgHead head;

            public byte[] toBytesArray()
            {
                int pos = 0;
                byte[] bytes = new byte[head.size];
                BitConverter.GetBytes(head.size).CopyTo(bytes, pos); pos += 2;
                BitConverter.GetBytes(head.type).CopyTo(bytes, pos); pos += 2;

                return bytes;
            }

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = BitConverter.ToUInt16(bytes, pos); pos += 2;
                head.type = BitConverter.ToUInt16(bytes, pos); pos += 2;

            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _STAGE_REBELLION_CHECK_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.STAGE_REBELLION_CHECK_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GAMBLE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GAMBLE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _ALL_USER_GOODS_GET_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.ALL_USER_GOODS_GET_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _ATTENDANCE_CHECK_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.ATTENDANCE_CHECK_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _ATTENDANCE_GET_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.ATTENDANCE_GET_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _CHAT_MSG_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CHAT_MSG_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _TUTORIAL_DRAGON_EXPEDION_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.TUTORIAL_DRAGON_EXPEDION_REQ;

                return base.toBytesArray();
            }
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _TUTORIAL_DRAGON_COMPLETE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.TUTORIAL_DRAGON_COMPLETE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _ALL_USER_GOODS_GET_CHECK_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.ALL_USER_GOODS_GET_CHECK_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BATTLE_ACTION_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_ACTION_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BATTLE_DELAY_TURN_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_DELAY_TURN_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BATTLE_UNIT_ATTACK_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_UNIT_ATTACK_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BATTLE_UNIT_MOVE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_UNIT_MOVE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _BATTLE_END_TURN_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_END_TURN_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _MAIL_LIST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.MAIL_LIST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _MAIL_ITEM_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.MAIL_ITEM_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class BATTLE_GIVE_UP_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_GIVE_UP_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class BATTLE_UNIT_DEFENSE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.BATTLE_UNIT_DEFENSE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_CREATE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_CREATE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_JOIN_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_JOIN_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_LEAVE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_LEAVE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_INTRODUCE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_INTRODUCE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_NOTICE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_NOTICE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_GROUPLIST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_GROUPLIST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_BATTLEINFO_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_BATTLEINFO_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_MEMBERLIST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_MEMBERLIST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_JOINLIST_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_MEMBERJOINLIST_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_JOINAGREE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_MEMBERJOINPERMIT_REQ;

                return base.toBytesArray();
            }
        };

        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_JOINREJECT_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_MEMBERJOINREJECT_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_MEMBEREXILE_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_MEMBEREXILE_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_BATTLEREADY_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_BATTLEREADY_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_BATTLEMATCHING_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_BATTLEMATCHING_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_JOINCANCEL_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_JOINCANCEL_REQ;

                return base.toBytesArray();
            }
        };

        

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _GUILD_ENEMYDECK_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.GUILD_ENEMYDECK_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _HERO_ACTION_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.HERO_ACTION_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _AUTO_BATTLE_ATTACK_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.AUTO_BATTLE_ATTACK_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _CHARACTER_INVENTORY_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.CHARACTER_INVENTORY_REQ;

                return base.toBytesArray();
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class _ITEM_SELL_REQ : _BASE_REQ
        {
            override public byte[] toBytesArray()
            {
                head.type = (ushort)MsgType.ITEM_SELL_REQ;

                return base.toBytesArray();
            }
        };

        //////////////////////////////////////////////////////////////////////////
    }
}

