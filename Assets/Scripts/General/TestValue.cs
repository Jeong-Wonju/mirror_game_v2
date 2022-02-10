using UnityEngine;
using System.Collections;

public class TestValue : MonoBehaviour 
{
    public static TestValue Inst { get; private set; }

    public MONSTER_STATE test_monster_state = MONSTER_STATE.NONE;

    public float test_float_1 = 0f;
    public float test_float_2 = 0;
    public float test_float_3 = 0;
    public float test_float_4 = 0;
    public float test_float_5 = 0;

    public float test_int_1 = 0f;
    public float test_int_2 = 0;
    public float test_int_3 = 0;
    public float test_int_4 = 0;
    public float test_int_5 = 0;

    void Awake()
    {
        Inst = this;
    }

	void Start()
    {
	
	}
}
