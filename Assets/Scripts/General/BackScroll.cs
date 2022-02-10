using UnityEngine;
using System.Collections;
using DevelopeCommon;
using System;

public class BackScroll : MonoBehaviour 
{
    private float _width = 26.28f;

    private Transform _target_tm = null;
    private Transform _transform = null;

    private Vector3 _center_2_pos = Vector3.zero;
    private Vector3 _center_3_pos = Vector3.zero;
    private Vector3 _left_2_pos = Vector3.zero;
    private Vector3 _left_3_pos = Vector3.zero;
    private Vector3 _right_2_pos = Vector3.zero;
    private Vector3 _right_3_pos = Vector3.zero;

    private Vector3 _cur_center_2_pos = Vector3.zero;
    private Vector3 _cur_center_3_pos = Vector3.zero;
    private Vector3 _cur_left_2_pos = Vector3.zero;
    private Vector3 _cur_left_3_pos = Vector3.zero;
    private Vector3 _cur_right_2_pos = Vector3.zero;
    private Vector3 _cur_right_3_pos = Vector3.zero;

    private Transform _floor = null;
    private Transform[] _1_block_arr = new Transform[3];
    private Transform[] _2_block_arr = new Transform[3];
    private Transform[] _3_block_arr = new Transform[3];

    private const float BACK_2_SCROLL_SPEED = 0.15f;
    private const float BACK_3_SCROLL_SPEED = 0.3f;

    private Transform near_block_1 = null;
    private Transform near_block_2 = null;
    private Transform near_block_3 = null;

    void Awake()
    {
        _transform = new WeakReference(GetComponent<Transform>()).Target as Transform;

        _floor = DevCommon.FindHierachyTmByNameEquals(_transform, "floor");

        _1_block_arr[0] = DevCommon.FindHierachyTmByNameEquals(_transform, "back_Plane_left_1");
        _1_block_arr[1] = DevCommon.FindHierachyTmByNameEquals(_transform, "back_Plane_center_1");
        _1_block_arr[2] = DevCommon.FindHierachyTmByNameEquals(_transform, "back_Plane_right_1");

        _2_block_arr[0] = DevCommon.FindHierachyTmByNameEquals(_transform, "back_Plane_left_2");
        _2_block_arr[1] = DevCommon.FindHierachyTmByNameEquals(_transform, "back_Plane_center_2");
        _2_block_arr[2] = DevCommon.FindHierachyTmByNameEquals(_transform, "back_Plane_right_2");

        _3_block_arr[0] = DevCommon.FindHierachyTmByNameEquals(_transform, "back_Plane_left_3");
        _3_block_arr[1] = DevCommon.FindHierachyTmByNameEquals(_transform, "back_Plane_center_3");
        _3_block_arr[2] = DevCommon.FindHierachyTmByNameEquals(_transform, "back_Plane_right_3");

        _left_2_pos = _2_block_arr[0].position;
        _center_2_pos = _2_block_arr[1].position;
        _right_2_pos = _2_block_arr[2].position;

        _left_3_pos = _3_block_arr[0].position;
        _center_3_pos = _3_block_arr[1].position;
        _right_3_pos = _3_block_arr[2].position;

        near_block_1 = _1_block_arr[1];
        near_block_2 = _2_block_arr[1];
        near_block_3 = _3_block_arr[1];
    }
	
	void Start()
    {
	}

    public void SetTargetTm(Transform target_tm)
    {
        _target_tm = target_tm;
    }
	
	void Update () 
    {
        if (_target_tm == null)
        {
            return;
        }

        Vector3 target_pos = _target_tm.position;

        if (target_pos.x > near_block_1.position.x + _width * 0.5f) {
            MoveBlock(DIR.RIGHT, _1_block_arr, out near_block_1, 1);
        }
        else if(target_pos.x < near_block_1.position.x - _width * 0.5f) {
            MoveBlock(DIR.LEFT, _1_block_arr, out near_block_1, 1);
        }

        if (target_pos.x > near_block_2.position.x + _width * 0.5f) {
            MoveBlock(DIR.RIGHT, _2_block_arr, out near_block_2, 2);
        }
        else if (target_pos.x < near_block_2.position.x - _width * 0.5f) {
            MoveBlock(DIR.LEFT, _2_block_arr, out near_block_2, 2);
        }

        if (target_pos.x > near_block_3.position.x + _width * 0.5f) {
            MoveBlock(DIR.RIGHT, _3_block_arr, out near_block_3, 3);
        }
        else if (target_pos.x < near_block_3.position.x - _width * 0.5f) {
            MoveBlock(DIR.LEFT, _3_block_arr, out near_block_3, 3);
        }

        _floor.position = new Vector3(target_pos.x, _floor.position.y, _floor.position.z);

        _cur_left_2_pos = _left_2_pos + new Vector3(target_pos.x * BACK_2_SCROLL_SPEED, 0f, 0f);
        _cur_center_2_pos = _center_2_pos + new Vector3(target_pos.x * BACK_2_SCROLL_SPEED, 0f, 0f);
        _cur_right_2_pos = _right_2_pos + new Vector3(target_pos.x * BACK_2_SCROLL_SPEED, 0f, 0f);

        _cur_left_3_pos = _left_3_pos + new Vector3(target_pos.x * BACK_3_SCROLL_SPEED, 0f, 0f);
        _cur_center_3_pos = _center_3_pos + new Vector3(target_pos.x * BACK_3_SCROLL_SPEED, 0f, 0f);
        _cur_right_3_pos = _right_3_pos + new Vector3(target_pos.x * BACK_3_SCROLL_SPEED, 0f, 0f);

        _2_block_arr[0].position = _cur_left_2_pos;
        _2_block_arr[1].position = _cur_center_2_pos;
        _2_block_arr[2].position = _cur_right_2_pos;

        _3_block_arr[0].position = _cur_left_3_pos;
        _3_block_arr[1].position = _cur_center_3_pos;
        _3_block_arr[2].position = _cur_right_3_pos;
	}

    private void MoveBlock(DIR move_dir, Transform [] block_arr, out Transform out_near_block, int block_layer)
    {
        Vector3 target_pos = _target_tm.position;

        Transform far_block = null;
        Transform near_block = null;
        float far_block_dist = 0f;
        float near_block_dist = 0f;

        int far_block_index = 0;
        for (int i = 0; i < block_arr.Length; ++i)
            {
                if (i == 0)
                {
                    far_block = block_arr[i];
                    far_block_dist = (_target_tm.position - block_arr[i].position).magnitude;

                    near_block = block_arr[i];
                    near_block_dist = (_target_tm.position - block_arr[i].position).magnitude;
                    far_block_index = 0;
                }

                float dist = (_target_tm.position - block_arr[i].position).magnitude;

                if( dist > far_block_dist  )
                {
                    far_block = block_arr[i];
                    far_block_dist = dist;
                    far_block_index = i;
                }

                if( dist < near_block_dist )
                {
                    near_block = block_arr[i];
                    near_block_dist = dist;
                }
            }

        if (move_dir == DIR.RIGHT) {
            far_block.transform.position = new Vector3(near_block.transform.position.x + _width, far_block.transform.position.y, far_block.transform.position.z);
        }
        else {
            far_block.transform.position = new Vector3(near_block.transform.position.x - _width, far_block.transform.position.y, far_block.transform.position.z);
        }

        out_near_block = near_block;

        if (block_layer == 2)
        {
            if( far_block_index == 0 ) {
                _left_2_pos = far_block.position - new Vector3(target_pos.x * BACK_2_SCROLL_SPEED, 0f, 0f);
            }
            else if( far_block_index == 1 ) {
                _center_2_pos = far_block.position - new Vector3(target_pos.x * BACK_2_SCROLL_SPEED, 0f, 0f);
            }
            else {
                _right_2_pos = far_block.position - new Vector3(target_pos.x * BACK_2_SCROLL_SPEED, 0f, 0f);
            }
        }

        if (block_layer == 3)
        {
            if (far_block_index == 0) {
                _left_3_pos = far_block.position - new Vector3(target_pos.x * BACK_3_SCROLL_SPEED, 0f, 0f);
            }
            else if (far_block_index == 1) {
                _center_3_pos = far_block.position - new Vector3(target_pos.x * BACK_3_SCROLL_SPEED, 0f, 0f);
            }
            else {
                _right_3_pos = far_block.position - new Vector3(target_pos.x * BACK_3_SCROLL_SPEED, 0f, 0f);
            }
        }
    }

    public void ChangeTexture()
    {
        string tex_1_name = string.Format("bg_{0}_1", StaticClass_InGame.MAP_INDEX + 1);
        string tex_2_name = string.Format("bg_{0}_2", StaticClass_InGame.MAP_INDEX + 1);
        string tex_3_name = string.Format("bg_{0}_3", StaticClass_InGame.MAP_INDEX + 1);

        //StartCoroutine(AssetBundleInstance.Inst.LoadSpecificAssetBundleAync<Texture>("ab_inst_background", tex_1_name, OnTexture_1));
        //StartCoroutine(AssetBundleInstance.Inst.LoadSpecificAssetBundleAync<Texture>("ab_inst_background", tex_2_name, OnTexture_2));
        //StartCoroutine(AssetBundleInstance.Inst.LoadSpecificAssetBundleAync<Texture>("ab_inst_background", tex_3_name, OnTexture_3));
    }

    private void OnTexture_1(Texture tex)
    {
        _1_block_arr[0].GetComponent<Renderer>().material.mainTexture = tex;
        _1_block_arr[1].GetComponent<Renderer>().material.mainTexture = tex;
        _1_block_arr[2].GetComponent<Renderer>().material.mainTexture = tex;
    }

    private void OnTexture_2(Texture tex)
    {
        _2_block_arr[0].GetComponent<Renderer>().material.mainTexture = tex;
        _2_block_arr[1].GetComponent<Renderer>().material.mainTexture = tex;
        _2_block_arr[2].GetComponent<Renderer>().material.mainTexture = tex;
    }

    private void OnTexture_3(Texture tex)
    {
        _3_block_arr[0].GetComponent<Renderer>().material.mainTexture = tex;
        _3_block_arr[1].GetComponent<Renderer>().material.mainTexture = tex;
        _3_block_arr[2].GetComponent<Renderer>().material.mainTexture = tex;
    }
}
