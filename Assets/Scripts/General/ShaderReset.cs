using UnityEngine;
using System.Collections;
using DevelopeCommon;

public class ShaderReset : MonoBehaviour
{
    void Awake()
    {
    }
    
	void Start()
    {
        DevCommon.ResetSahder(gameObject);
	}
}
