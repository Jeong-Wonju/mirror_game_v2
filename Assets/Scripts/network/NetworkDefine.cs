using UnityEngine;
using System.Collections;


namespace NetworkDefine
{
	// ��Ŷ ��� OFFSET
	enum HEDER_OFFSET
	{
		SIZE = 0,	// ����� ������ ��Ŷ�� ũ��
		MSG_CODE = 2,	// �ش� ��Ŷ�� Message Code
		FUNC_CODE = 4,	// �ش� ��Ŷ�� Function Code
		RESULT = 6,	// ��Ŷ�� ���(�����) =>0: FAIL, 1: SUCCESS
		ERROR_CODE = 8,	// ���� �ڵ�(���� �����)
		UID = 12,	// ���� UID (��û��Java NIO)
	}

	enum NET_DEFINE
	{
		MAX_HEADER_LENGTH = 4,
		REFRESH_TICK = 2000,
		MAX_PACKET_LENGTH = 10240, //10K
		START_TICK = 5000,
	}
//	public const int MAX_HEADER_LENGTH = 16;
//	public const int MAX_PACKET_LENGTH = 2048;
	//-------------------------------------------------------------------------
	//��Ŷ funciton code
	//-------------------------------------------------------------------------
	//-------------------------------------------------------------------------
	// @brief		Packet Parsing�� ���Ǿ����� Fucntion code.
	// �������� Packet Parse�ÿ� Function Code�� ����Ͽ� �޽����� Parse �Ѵ�.\n
	// �̿� �� ��Ŷ���� �ڽŵ��� ������ �´� Fucntion Code�� ����Ͽ��� �Ѵ�. \n
	// FuncCode�� �̸��� "FC_" Prefix ���Ŀ� �޽��� �̸��� ��ҹ��� ȥ������ �Ǿ��ִ�.
	//-------------------------------------------------------------------------
	enum FC
	{
		//common
		Ping = 1,
		Login,
		Logout,
		Notice,

		//matching server
		MatchRequest = 100,
		MatchResponse,
		JoinMatch,
		MatchingReady,

		//game server
		RoomUsers = 200,
		GameStart,
		GameHit,
		GameEnd,
		GameReQuery,
		DisconnectedPlayer,

		//server packet 1000������.
		ServerMessageStart = 1000,
		SHostConfig,
		SHostRegister,
		SHostUpdate,
		SRegCreateRoom,
		SUpdateRoomUser,
		SCtrlCommand,
		SPlayResult,
		SNextGameSeq,
		STransferPacket,
	}

	enum MSG
	{
		//-------------------------------------------------------------------------
		// ���� message
		//-------------------------------------------------------------------------
		HDRQ_LOGIN = 1001,		/* ���� ���� */
		HDRE_LOGIN = 1002,
		HDRQ_PING = 1003,		/* PING		 */
		HDRE_PING = 1004,
		HDRQ_LOGOUT = 1005,
		HDRE_LOGOUT = 1006,
		HDRQ_NOTICE = 1007,
		HDRE_NOTICE = 1008,

		//-------------------------------------------------------------------------
		// ��Ī ����.
		//-------------------------------------------------------------------------
		HDRQ_MATCH_REQUEST = 2001,
		HDRE_MATCH_REQUEST = 2002,
		HDRQ_MATCH_RESPONSE = 2003,
		HDRE_MATCH_RESPONSE = 2004,
		HDRQ_JOINMATCH = 2005,
		HDRE_JOINMATCH = 2006,
		HDRQ_MATCH_READY = 2007,
		HDRE_MATCH_READY = 2008,

		//-------------------------------------------------------------------------
		// ���� ����.
		//-------------------------------------------------------------------------
		HDRQ_ROOM_USERS = 3001,
		HDRE_ROOM_USERS = 3002,
		HDRQ_GAME_START = 3003,
		HDRE_GAME_START = 3004,
		HDRQ_GAME_HIT = 3005,
		HDRE_GAME_HIT = 3006,
		HDRQ_GAME_END = 3007,
		HDRE_GAME_END = 3008,
		HDRQ_GAME_REQUERY = 3009,
		HDRE_GAME_REQUERY = 3010,
	}
}
