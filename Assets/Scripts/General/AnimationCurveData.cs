using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DevelopeCommon;

[System.Serializable]
public class AnimationCurveElement
{
    public string name = string.Empty;
    public AnimationCurve _x = null;
    public AnimationCurve _y = null;
    public float _magnitude_x = 1f;
    public float _magnitude_y = 0f;
}

public class AnimationCurveData : MonoBehaviour
{
    public AnimationCurveElement m_mu_mb_skill03_2 = null;
    public AnimationCurveElement m_mu_mb_skill02 = null;
    public AnimationCurveElement m_mu_mb_skill03_1 = null;
    public AnimationCurveElement m_mu_mb_skill04_3 = null;

    public AnimationCurveElement m_mu_mb_skill01 = null;
    public AnimationCurveElement m_mu_mb_skill03_3 = null;
    public AnimationCurveElement m_mu_mb_skill04_2 = null;

    public AnimationCurveElement m_mu_mb_attack01 = null;
    public AnimationCurveElement m_mu_mb_attack02 = null;
    public AnimationCurveElement m_mu_mb_hit_loop = null;

    public AnimationCurveElement _mon_1_attack_2 = null;
    public AnimationCurveElement _mon_1_dead = null;

    public AnimationCurveElement f_gu_fb_attack01 = null;
    public AnimationCurveElement f_gu_fb_attack02 = null;
    public AnimationCurveElement f_gu_fb_skill03_1 = null;
    public AnimationCurveElement f_gu_fb_skill04_2 = null;
    public AnimationCurveElement f_gu_fb_skill01 = null;
    public AnimationCurveElement f_gu_fb_skill03_2 = null;
    public AnimationCurveElement f_gu_fb_skill03_3 = null;
    public AnimationCurveElement f_gu_fb_skill04_3 = null;
    public AnimationCurveElement f_gu_fb_skill04_1 = null;
    public AnimationCurveElement f_gu_fb_hit_loop = null;

    public AnimationCurveElement mon_1000_attack01 = null;
    public AnimationCurveElement mon_1003_attack02 = null;
    public AnimationCurveElement mon_1020_attack02 = null;
    public AnimationCurveElement mon_1021_attack01 = null;
    public AnimationCurveElement mon_1021_attack02 = null;
    public AnimationCurveElement mon_1032_attack01 = null;
    public AnimationCurveElement mon_1053_attack02 = null;
    public AnimationCurveElement mon_1054_attack02 = null;
    public AnimationCurveElement mon_1060_attack02 = null;
    public AnimationCurveElement mon_1063_attack02 = null;
    public AnimationCurveElement mon_1072_attack02 = null;
    public AnimationCurveElement mon_1092_attack02 = null;
    public AnimationCurveElement mon_1111_attack02 = null;
    public AnimationCurveElement mon_1132_attack02 = null;
    public AnimationCurveElement mon_1150_attack02 = null;
    public AnimationCurveElement mon_1160_attack01 = null;
    public AnimationCurveElement mon_1160_attack02 = null;
    public AnimationCurveElement mon_1190_attack02 = null;
    public AnimationCurveElement mon_1210_attack02 = null;
    public AnimationCurveElement mon_1220_attack02 = null;
    public AnimationCurveElement mon_1222_attack02 = null;
    public AnimationCurveElement mon_1230_attack02 = null;
    public AnimationCurveElement mon_1240_attack02 = null;
    public AnimationCurveElement mon_1243_attack02 = null;
    public AnimationCurveElement mon_1252_attack02 = null;
    public AnimationCurveElement mon_1280_attack02 = null;
    public AnimationCurveElement mon_1290_attack02 = null;
    public AnimationCurveElement mon_1320_attack02 = null;
    public AnimationCurveElement mon_6000_skill01 = null;
    public AnimationCurveElement mon_6000_skill02 = null;
    public AnimationCurveElement mon_6000_skill04 = null;
    public AnimationCurveElement mon_6010_skill03 = null;
    public AnimationCurveElement mon_6010_skill04 = null;
    public AnimationCurveElement mon_6040_skill04 = null;

    public AnimationCurveElement mon_6030_skill02 = null;
    public AnimationCurveElement mon_6030_skill03 = null;
    public AnimationCurveElement mon_6030_skill04 = null;

    public AnimationCurveElement mon_6020_skill04 = null;
    public AnimationCurveElement mon_6050_skill04 = null;

    //public AnimationCurveElement mon_boss_6000 = null;

    private List<AnimationCurveElement> _list_animation_curve_element = new List<AnimationCurveElement>();

    public static AnimationCurveData Inst = null;

    void Awake()
    {
        Inst = this;

        //_list_animation_curve_element.Add(mon_boss_6000);

        _list_animation_curve_element.Add(mon_6050_skill04);
        _list_animation_curve_element.Add(mon_6020_skill04);
        _list_animation_curve_element.Add(mon_6030_skill02);
        _list_animation_curve_element.Add(mon_6030_skill03);
        _list_animation_curve_element.Add(mon_6030_skill04);

        _list_animation_curve_element.Add(mon_6040_skill04);
        _list_animation_curve_element.Add(mon_6000_skill01);
        _list_animation_curve_element.Add(mon_6000_skill02);
        _list_animation_curve_element.Add(mon_6000_skill04);
        _list_animation_curve_element.Add(mon_6010_skill03);
        _list_animation_curve_element.Add(mon_6010_skill04);

        _list_animation_curve_element.Add(mon_1320_attack02);
        _list_animation_curve_element.Add(mon_1290_attack02);
        _list_animation_curve_element.Add(mon_1280_attack02);
        _list_animation_curve_element.Add(mon_1252_attack02);
        _list_animation_curve_element.Add(mon_1243_attack02);
        _list_animation_curve_element.Add(mon_1240_attack02);
        _list_animation_curve_element.Add(mon_1230_attack02);
        _list_animation_curve_element.Add(mon_1222_attack02);
        _list_animation_curve_element.Add(mon_1220_attack02);
        _list_animation_curve_element.Add(mon_1210_attack02);
        _list_animation_curve_element.Add(mon_1190_attack02);
        _list_animation_curve_element.Add(mon_1160_attack02);
        _list_animation_curve_element.Add(mon_1160_attack01);
        _list_animation_curve_element.Add(mon_1150_attack02);
        _list_animation_curve_element.Add(mon_1132_attack02);
        _list_animation_curve_element.Add(mon_1111_attack02);
        _list_animation_curve_element.Add(mon_1092_attack02);
        _list_animation_curve_element.Add(mon_1072_attack02);
        _list_animation_curve_element.Add(mon_1063_attack02);
        _list_animation_curve_element.Add(mon_1060_attack02);
        _list_animation_curve_element.Add(mon_1054_attack02);
        _list_animation_curve_element.Add(mon_1053_attack02);
        _list_animation_curve_element.Add(mon_1032_attack01);
        _list_animation_curve_element.Add(mon_1021_attack02);
        _list_animation_curve_element.Add(mon_1021_attack01);
        _list_animation_curve_element.Add(mon_1020_attack02);
        _list_animation_curve_element.Add(mon_1003_attack02);
        _list_animation_curve_element.Add(mon_1000_attack01);

        _list_animation_curve_element.Add(m_mu_mb_skill03_2);
        _list_animation_curve_element.Add(m_mu_mb_skill02);
        _list_animation_curve_element.Add(m_mu_mb_skill03_1);
        _list_animation_curve_element.Add(m_mu_mb_skill04_3);
        _list_animation_curve_element.Add(m_mu_mb_skill01);
        _list_animation_curve_element.Add(m_mu_mb_skill03_3);
        _list_animation_curve_element.Add(m_mu_mb_skill04_2);
        _list_animation_curve_element.Add(m_mu_mb_attack01);
        _list_animation_curve_element.Add(m_mu_mb_attack02);
        _list_animation_curve_element.Add(m_mu_mb_hit_loop);

        _list_animation_curve_element.Add(_mon_1_attack_2);
        _list_animation_curve_element.Add(_mon_1_dead);

        _list_animation_curve_element.Add(f_gu_fb_attack01);
        _list_animation_curve_element.Add(f_gu_fb_attack02);
        _list_animation_curve_element.Add(f_gu_fb_skill03_1);
        _list_animation_curve_element.Add(f_gu_fb_skill04_2);
        _list_animation_curve_element.Add(f_gu_fb_skill01);
        _list_animation_curve_element.Add(f_gu_fb_skill03_2);
        _list_animation_curve_element.Add(f_gu_fb_skill03_3);
        _list_animation_curve_element.Add(f_gu_fb_skill04_3);
        _list_animation_curve_element.Add(f_gu_fb_skill04_1);
        _list_animation_curve_element.Add(f_gu_fb_hit_loop);
    }

    void Start()
    {
	}

    public float GetX(string animation_curve_key, float time_accum)
    {
        AnimationCurveElement animation_curve_element = null;
        for (int i = 0; i < _list_animation_curve_element.Count; ++i)
        {
            if (_list_animation_curve_element[i].name.Equals(animation_curve_key))
            {
                animation_curve_element = _list_animation_curve_element[i];
                break;
            }
        }

        if (animation_curve_element == null)
        {
            //DevDebug.LogColor("red", "{0} is not found", animation_curve_key);
            return 0f;
        }

        return animation_curve_element._x.Evaluate(time_accum) * animation_curve_element._magnitude_x;
    }

    public float GetY(string animation_curve_key, float time_accum)
    {
        AnimationCurveElement animation_curve_element = null;
        for (int i = 0; i < _list_animation_curve_element.Count; ++i)
        {
            if (_list_animation_curve_element[i].name.Equals(animation_curve_key))
            {
                animation_curve_element = _list_animation_curve_element[i];
                break;
            }
        }

        if (animation_curve_element == null) return 0f;

        return animation_curve_element._y.Evaluate(time_accum) * animation_curve_element._magnitude_y;
    }
}
