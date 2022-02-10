//==========================================================
// File : DataAssetManager.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 14일
// Unity Connect : 
// Remarks 
// -   데이터 관리 
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDefine_Talk;

public class DataAssetManager : MonoSingletonFactory<DataAssetManager>
{
    public LoadingBar m_LoadingBar;

    #region Scriptable Objects
    private bool                                    m_bDataLoadComplete          = false;
    private int                                     m_nScriptableObjectCount    = (int)SCRIPTABLE_OBJECT_TYPE.SOT_TOTAL-1;
    private Dictionary<string, ScriptableObject>    _dic_scriptable_object      = new Dictionary<string, ScriptableObject>();
    #endregion

    public bool bDataLoadComplete { get { return m_bDataLoadComplete; } set { m_bDataLoadComplete = value; } }

    //protected override void Awake()
    //{
    //    gameObject.SetActive(false);
    //}
    
    void Start(){}

    public void DataAssetLoad()
    {
        // 활성화 
        gameObject.SetActive(true);

        // 로딩 시작 
        m_LoadingBar.LoadingStart(m_nScriptableObjectCount);

        /*StartCoroutine(AssetBundleInstance.Inst.LoadSpecificAssetBundleAync<Scriptable_Lotto>("ab_text_scriptable", "lotto",
            OnAssetBundleScriptable_Login_Callback));*/

        m_LoadingBar.LoadingUpdate();
    }

    private void Update()
    {
        // 데이터 로드가 완료되면 true로 바꾼다. 
        if ( true == m_LoadingBar.bLoadComplete )
        {
            m_bDataLoadComplete = true;
            m_LoadingBar.bLoadComplete = false;
        }
    }

    private void OnAssetBundleScriptable_Login_Callback(ScriptableObject obj)
    {
        // 데이터 저장하기 
        AddScriptableObject(obj.name, obj);

        // 데이터 카운트
        m_LoadingBar.LoadingUpdate();
    }

    public void AddScriptableObject(string scriptable_object_name, ScriptableObject scriptable_object)
    {
        _dic_scriptable_object.Add(scriptable_object_name, scriptable_object);
    }

    // 데이터 불러오기 
    public ScriptableObject GetScriptableObject(string scriptable_object_name)
    {
#if UNITY_EDITOR
        if (!_dic_scriptable_object.ContainsKey(scriptable_object_name))
        {
            UnityEngine.Debug.LogError(string.Format("{0} not found", scriptable_object_name));
        }
#endif

        return _dic_scriptable_object[scriptable_object_name];
    }
}
