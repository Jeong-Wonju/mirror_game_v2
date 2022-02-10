using UnityEngine;
using System.Collections;
using DevelopeCommon;

public class Pet : MonoBehaviour 
{
    private Transform _transform = null;
    private Animation _animation = null;
    private Vector3 _cur_pos = Vector3.zero;
    private Transform _target_tm = null;
    private Vector3 _idle_pos = Vector3.zero;
    private float _time_accum = 0f;
    private Vector3 _target_forward = Vector3.right;
    public Vector3 _cur_forward = Vector3.right;

    private float _radius = 0.2f;
    private float _rotate_speed = 180f;
    private float _follow_speed = 1.5f;
    private float _follow_dist = 2.5f;
    private float _height = 2.5f;

    public Transform TransformRef { get { return _transform; } }

    void Awake()
    {
        _transform = transform;
        _animation = GetComponent<Animation>();
    }

	void Start()
    {   
        // 파티클로 이루어진 펫은 animation 이 없을 수 있다.
        if( _animation != null )
        {
            AnimationState ani_state = _animation[_animation.clip.name];
            if( name.Contains("pet_5") )
            {
                ani_state.speed = 2f;
            }
        }
	}

    public void SetTargetTm(Transform target_tm)
    {
        _target_tm = target_tm;

        _cur_pos = _target_tm.position + Vector3.up * 3.0f;
    }
	
	void Update()
    {
        if( _target_tm == null ) return;

        float delta_time = Time.deltaTime;
        _time_accum += delta_time * _rotate_speed;

        _idle_pos = new Vector3(Mathf.Sin(Mathf.Deg2Rad * _time_accum) * _radius, Mathf.Cos(Mathf.Deg2Rad * _time_accum) * _radius, 0f);

        if ((_cur_pos - (_target_tm.position + Vector3.up * _height)).sqrMagnitude > _follow_dist * _follow_dist)
        {
            _cur_pos = Vector3.Lerp(_cur_pos, _target_tm.position + Vector3.up * _height, delta_time * _follow_speed);
        }

        _transform.position = _cur_pos + _idle_pos;

        if (_cur_pos.x > _target_tm.position.x) {
            _target_forward = Vector3.left - Vector3.forward * 0.05f;
        }
        else {
            _target_forward = Vector3.right - Vector3.forward * 0.05f;
        }

        _cur_forward = Vector3.Slerp(_cur_forward, _target_forward, delta_time * 3f);

        _transform.forward = _cur_forward.normalized;
	}
}