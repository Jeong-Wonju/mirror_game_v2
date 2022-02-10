using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using net_common;

public class StaticClass
{
    //public static bool IS_ERROR = false;

    /// <summary>
    /// Reset() 함수에서 초기화 해서는 안되는 변수들
    /// </summary>
    public static bool IS_GAME_SERVER_CONNECTED = false;
    public static string GAME_SERVER_ADDRESS = string.Empty;
    public static bool IS_SERVER_CLIENT_VERSION_SAME = true;            // 서버와 클라가 서로 버전이 같은지 여부
    public static string GCM_REGISTRATION_ID = string.Empty;
    public static string GOOGLE_USER_ID = string.Empty;

    

    public static void Reset()
    {
        //IS_ERROR = false;
    }
}

public class StaticClass_InGame
{
    public static bool IS_GAME_OVER = false;
    public static bool IS_APPLICATION_QUIT = false;
    public static int DIFFICULTY_INDEX = 0;                                 // 난이도 선택 인덱스
    public static int MAP_INDEX = 0;                                        // 맵 인덱스로 0 ~ 5까지 (맵 하나는 100개의 스테이지를 갖는다 : STAGE_COUNT_IN_MAP)
    public static bool IS_HOST = false;
    public static int START_SPIRIT_COUNT = 0;
    public static int SPIRIT_COUNT = 0;
    public static int BOSS_STAGE_NUM = 0;
    public static float EXP_IN_STAGE = 0f;                                  // 현재 진행중인 스테이지에서 모은 경험치

    public static List<string> CONSUME_ITEM_LIST = new List<string>();      // 인게임 진입시 사용된 소모성 아이템

    public static int INFINITY_START_ROUND = 0;

    /// <summary>
    /// Reset() 함수에서 초기화 해서는 안되는 변수들
    /// </summary>
    public static bool IS_INGAME_COMMON_ASSET_LOADED = false;
    public static string STAGE_NORMAL = string.Empty;
    public static int STAGE_INDEX = 0;                                      // 스테이지 인덱스로 0 ~ 599 까지 (MAP_INDEX 와 STAGE_INDEX_IN_MAP 의 조합으로 값이 결정된다.)
    public static int STAGE_INDEX_IN_MAP = 0;                               // 해당 맵에서 스테이지 인덱스 (0 ~ 99)
    public static int STAGE_INDEX_PLAY = 0;
    public static string PARTS_HAIR_NAME = string.Empty;
    //public static ST_Hero_Info PVP_FIGHTER_INFO;
    //public static ST_Hero_Info PVP_ASSASSIN_INFO;
    public static int PVP_PET_INDEX = -1;                                   // PVP 상대방 펫 인덱스 0이면 펫1, ~~ 3 이면 펫4
    public static int PVP_PET_LEVEL = -1;                                   // PVP 상대방 펫 레벨 
    public static int PVP_ENEMY_USER_INDEX = -1;                            // PVP 상대방 user_index
    public static string PVP_ENEMY_USER_ID = string.Empty;
    public static int PVP_ENEMY_HERO_LEVEL = -1;                            // PVP 상대방 영웅 레벨
    public static float PVP_START_TIME = 0f;                                // PVP 시작 시간
    public static float PVP_END_TIME = 0f;                                  // PVP 끝난 시간

    public static void Reset_Exit()
    {
        IS_GAME_OVER = false;
        IS_APPLICATION_QUIT = false;
        //MAP_INDEX = 0;
        IS_HOST = false;
        SPIRIT_COUNT = 0;
        BOSS_STAGE_NUM = 0;
        EXP_IN_STAGE = 0f;
        //CONSUME_ITEM_LIST.Clear();
    }
}
