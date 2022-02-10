using UnityEngine;
using System.Collections;

public enum GAME_SECTION
{  
    LOBBY,
    INGAME,
}

public enum GAME_MODE
{
    NONE,
    STAGE,
    BOSS_RAID,
    PVP,
    P2P,
    INFINITE,
}

public enum POPUP_TYPE
{
    NONE,
    SKILL,
    HERO,
    RELIC,
    RANKING,
}

public enum INSTANCE_STATUS
{
    INACTIVE,
    SLEEP,
    ACTIVE,
}

public enum HERO_TYPE
{
    NONE,
    FIGHTER,
    ASSASSIN,
}

public enum AUTO_HERO
{
    NONE,
    ME,
    YOU,
}

public enum DIR
{
    CENTER,
    LEFT,
    RIGHT
}

public enum HERO_STATE
{
    NONE,
    MOVE,
    ATTACK_1,
    ATTACK_2,
    ATTACK_3,
    ATTACK_4,
    SKILL_1,
    SKILL_2,
    SKILL_3,
    SKILL_4,
    SKILL_5,
    HIT,
    DIE,
    VICTORY,
    DAMAGED_BACK,
    STUN,
    COMBO_SUCCESS,
}

public enum HERO_AUTO_STATE
{
    NONE,
    MOVE_TO_TARGET,
    ATTACK,
    HIT,
    DIE,
    VICTORY,
    IDLE,
}

public enum HERO_ANIMATION_TYPE
{
    NONE,
    MOVE,
    ATTACK_1,
    ATTACK_2,
    ATTACK_3,
    ATTACK_4,
    SKILL_1,
    SKILL_2,
    SKILL_3,
    SKILL_4,
    SKILL_5,
    IDLE,
    HIT,
    HIT_LOOP,
    DIE,
    VICTORY,
    STUN,
    COMBO_SUCCESS,
}

public enum MONSTER_STATE
{
    NONE,
    MOVE,
    DEAD,
    ATTACK_1,
    ATTACK_2,
    ATTACK_3,
    ATTACK_CHAIN,
    DAMAGED_BACK,
    DAMAGED,
    STUN,
    HIT,
    STAND,
    SKILL_1,
    SKILL_2,
    SKILL_3,
    SKILL_4,
    SKILL_5,
    READY,
    ESCAPE,
    IDLE,
    EXIT,
}

public enum MONSTER_ANIMATION_TYPE
{
    NONE,
    IDLE_1,
    IDLE_2,
    WALK,
    RUN,
    ATTACK_1,
    ATTACK_2,
    ATTACK_3,
    DEAD,
    HIT,
    STUN,
    SKILL_1,
    SKILL_2,
    SKILL_3,
    SKILL_4,
    SKILL_5,
}

public enum MONSTER_SCRIPT_TYPE
{
    NONE,
    MONSTER_NORMAL,
    MONSTER_BOSS,
    MONSTER_ETC,
}

public enum MONSTER_TYPE
{
    NONE,
    MILI,
    RANGE,
    MAGIC,
    ELITE,
    BOSS,
    ETC,
}

//public enum CHARACTER_STATUS
//{
//    NONE,
//    FIRE,
//    WATER,
//    GROUND,
//}

public enum COMBO_STATE 
{ 
    STOP, 
    TIME_GAUGE_UP, 
    COMBO_MINUS_TIME 
}

public enum CAMERA_EFFECT
{
    NONE,
    SIZE,
    ZOOM,
}

public enum SKILL_BTN_TYPE
{
    NONE,
    SKILL_1,
    SKILL_2,
    SKILL_3,
    SKILL_4,
    SKILL_5,
}

public enum SKILL_TRIGGER
{
    NONE,
    TIMING,                 // 스킬 사용 후 특정 시간에 발동
    RIGHT_NOW,              // 스킬 사용 후 바로 발동
    PROJECTILE,             // 발사체에 피격시
}

public enum OBJECT_TYPE
{
    NONE,
    MONSTER,
    HERO,
}

public enum SKILL_ACTION_TYPE
{
    NONE,
    NORMAL,                 // 일반적인 스킬 
    PROJECTILE,             // 발사체를 사용하는 스킬
    SELF,                   // 자신에게 스킬이 적용되는 경우 예) 휠윈드 무적
    //ACTION,                 // 액션스킬
    //BARRAGE_ATTACK,         // 연타
}

public enum TEXT_NATION
{
    NONE,
    KOREA,
    USA,
    TAIWAN,
    CHINA,
    JAPAN,
    SPAIN,
    THAILAND,
}

public enum EQUIP_PART_TYPE
{
    NONE,
    UP,
    DOWN,
    HAIR
}

public enum ITEM_TYPE
{
    NONE,
    CROWN,
    PACKAGE,
    SPIRIT,
    HEART,
    ELEMENTAL_FIRE,
    ELEMENTAL_WIND,
    ELEMENTAL_EARTH,
    ELEMENTAL_WATER,
    CONSUME_SPIRIT,
    CONSUME_EXP,
    CONSUME_GRINDER,
    CONSUME_REFINE_KIT,
    PET_1,
    PET_2,
    PET_3,
    PET_4,
    DAILY_PACKAGE_1,
    //PET_5,
}

public enum PET_FUNCTION
{
    NONE,
    TAKL,
    SPIRIT_UP
}

public enum AUTO_TYPE
{
    NONE,
    MOVE,
    IDLE,
    ATTACK,
    SKILL,
}

public enum MISSION_TYPE
{
    NONE,
    MONSTER_KILL,               // 몬스터 죽이기
    MIDDLE_BOSS_MONSTER_KILL,   // 중간 보스 : 2017.2.6 아직 기능 추가 되지 않음
    ELEMENTAL_GET,              // 엘리멘타 얻기 : 2017.2.6 보스를 물리치고 엘리멘탈 드롭되는 기능 아직 추가 되지 않음
    PVP_WIN,                    // PVP 승리 : 
    GAWI_BAWI_BO_PLAY,          // 가위바위보 플레이하기
    SPIRIT_GET,                 // 스피릿 얻기
    HEART_USE,                   // 하트 사용 : 2017.2.6 아직 기능 추가 되지 않음
    GOLD_BOX_OPEN,              // 골드박스 열기 : 2017.2.6 골드박스 아직 추가 되지 않음
    CONSUME_ITEM_USE            // 아이템 사용
}