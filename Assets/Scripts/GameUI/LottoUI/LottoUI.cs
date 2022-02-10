//==========================================================
// File : LottoUI.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 14일
// Unity Connect : 
// Remarks 
// -   로또 UI 
//==========================================================
using DevelopeCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnhancedUI;
using GameDefine_Talk;
//using UnityEditor;

[System.Serializable]
public class LottoData
{
    public int number_1 = 0;
    public int number_2 = 0;
    public int number_3 = 0;
    public int number_4 = 0;
    public int number_5 = 0;
    public int number_6 = 0;
}

public class LottoUI : MonoBehaviour
{

    private Scriptable_Lotto_Element[]  m_SLottoElements    = null;             // 엑셀 테이블

    public LottoWindows                 m_LottoWindows      = null;
    public ExtractionButton             m_ExtractionButton  = null;
    public ExtractionCount              m_ExtractionCount   = null;
    public LeftButton                   m_LeftButton        = null;
    public RightButton                  m_RightButton       = null;

    // 차후에 DataAssetManager 변경한다. 2019-03-15
    public GameManager_Talk                  m_GameManager_Talk       = null;
    public bool                         m_bLoadData         = false;

    private void LoadScriptableLottoData()
    {
        //return;
        // pc
        //string path_file_scriptable_object = Application.dataPath + "/Data/scriptable/" + "lotto.asset";
        //DevDebug.LogColor("blue", "path_file_scriptable_object = {0}", path_file_scriptable_object);
        //string asset_path_file_scriptable_object = path_file_scriptable_object.Substring(path_file_scriptable_object.IndexOf("Asset"));

        // mobile
        Scriptable_Lotto scriptbale_obj = DataAssetManager.Instance.GetScriptableObject("lotto") as Scriptable_Lotto;

        m_SLottoElements = new Scriptable_Lotto_Element[scriptbale_obj._arr_data.Length];

        for (int i = 0; i < scriptbale_obj._arr_data.Length; ++i)
        {
            m_SLottoElements[i] = new Scriptable_Lotto_Element();
            m_SLottoElements[i].inning = scriptbale_obj._arr_data[i].inning;
            m_SLottoElements[i].number_1 = scriptbale_obj._arr_data[i].number_1;
            m_SLottoElements[i].number_2 = scriptbale_obj._arr_data[i].number_2;
            m_SLottoElements[i].number_3 = scriptbale_obj._arr_data[i].number_3;
            m_SLottoElements[i].number_4 = scriptbale_obj._arr_data[i].number_4;
            m_SLottoElements[i].number_5 = scriptbale_obj._arr_data[i].number_5;
            m_SLottoElements[i].number_6 = scriptbale_obj._arr_data[i].number_6;
            m_SLottoElements[i].bonus = scriptbale_obj._arr_data[i].bonus;
            DevDebug.LogColor("blue", " Lotto = {0}: {1},{2},{3},{4},{5},{6} 보너스:{7},", m_SLottoElements[i].inning, m_SLottoElements[i].number_1,
                m_SLottoElements[i].number_2, m_SLottoElements[i].number_3, m_SLottoElements[i].number_4, m_SLottoElements[i].number_5, m_SLottoElements[i].number_6, m_SLottoElements[i].bonus);
        }
    }


    // Use this for initialization
    void Start()
    {
    }

    private void RandomNumberCheck(ref LottoData lottodata)
    {
        int nLottoCount = 0;
        bool bLotto = false;

        // 1. 6개 번호를 랜덤으로 받을 때 같은 번호가 하나라도 나오면안된다
        while (6 != nLottoCount)
        {
            // 1. 랜덤값을 얻어온다. 
            int nValue = Random.Range(1, 46); //Random.Range(1(최소값 포함) , 46(최대값 미포함)

            // 2. 같은 값이 있는지 검사한다. 
            if ((lottodata.number_1 == nValue) ||
                 (lottodata.number_2 == nValue) ||
                 (lottodata.number_3 == nValue) ||
                 (lottodata.number_4 == nValue) ||
                 (lottodata.number_5 == nValue) ||
                 (lottodata.number_6 == nValue))
            {
                Debug.Log("이미 값이 있습니다. " + nValue);
                continue;
            }
            else
            {
                bLotto = true;
                Debug.Log("없는 번호입니다. " + nValue);
            }


            // 3. 마지막 까지 검사하고 이상없을때 카운터를 증가해준다. 
            if (true == bLotto)
            {
                if (0 == nLottoCount)
                {
                    lottodata.number_1 = nValue;
                }
                else if (1 == nLottoCount)
                {
                    lottodata.number_2 = nValue;
                }
                else if (2 == nLottoCount)
                {
                    lottodata.number_3 = nValue;
                }
                else if (3 == nLottoCount)
                {
                    lottodata.number_4 = nValue;
                }
                else if (4 == nLottoCount)
                {
                    lottodata.number_5 = nValue;
                }
                else if (5 == nLottoCount)
                {
                    lottodata.number_6 = nValue;
                }

                ++nLottoCount;
                bLotto = false;
            }
        }
    }

    private void NumberCheck(ref bool bPrize, ref Scriptable_Lotto_Element SLElement, ref int nNumber)
    {
        if ((SLElement.number_1 == nNumber) || (SLElement.number_2 == nNumber) || (SLElement.number_3 == nNumber) ||
            (SLElement.number_4 == nNumber) || (SLElement.number_5 == nNumber) || (SLElement.number_6 == nNumber))
        {
            // 같은 숫자
            bPrize = true;
        }
    }

    private bool RandomPrizeNumberCheck(ref LottoData lottoData)
    {
        bool[] bPrize = new bool[6];

        for (int i = 0; i < m_SLottoElements.Length; ++i)
        {
            // 1. 숫자 하나씩 검사한다.
            for (int b = 0; b < 6; ++b)
            {
                bPrize[b] = false;
            }

            NumberCheck(ref bPrize[0], ref m_SLottoElements[i], ref lottoData.number_1);
            NumberCheck(ref bPrize[1], ref m_SLottoElements[i], ref lottoData.number_2);
            NumberCheck(ref bPrize[2], ref m_SLottoElements[i], ref lottoData.number_3);
            NumberCheck(ref bPrize[3], ref m_SLottoElements[i], ref lottoData.number_4);
            NumberCheck(ref bPrize[4], ref m_SLottoElements[i], ref lottoData.number_5);
            NumberCheck(ref bPrize[5], ref m_SLottoElements[i], ref lottoData.number_6);


            // 2. 당첨중 같은 당첨번호가 있는 확인에서 있으면 true, 없으면 false
            if ((true == bPrize[0]) && (true == bPrize[1]) && (true == bPrize[2]) & (true == bPrize[3]) &&
                 (true == bPrize[4]) && (true == bPrize[5]))
            {
                return true;
            }
        }

        return false;
    }

    public void LottoRandomExtraction()
    {
        if ( null != m_SLottoElements )
        {
            LottoData lottodata = null;
            for (int i = 0; i < 5; ++i)
            {
                lottodata = new LottoData();

                // 1. 먼저램덤으로 번호를 추출한다. 
                RandomNumberCheck(ref lottodata);

                // 2. 지금 까지 당첨된 번호와 비교해서 6개 번호가 일치하는지 검사를 해야한다. 
                bool bRPNCkeck = RandomPrizeNumberCheck(ref lottodata);
                if (true == bRPNCkeck)
                {
                    // 당첨 번호중에 같은 번호가 있다는 뜻이다. 다시 램덤번호를 추출한다. 
                    --i;
                    continue;
                }

                // 3. 저장 
                m_LottoWindows.SourceImageChange(i, 0, lottodata.number_1);
                m_LottoWindows.SourceImageChange(i, 1, lottodata.number_2);
                m_LottoWindows.SourceImageChange(i, 2, lottodata.number_3);
                m_LottoWindows.SourceImageChange(i, 3, lottodata.number_4);
                m_LottoWindows.SourceImageChange(i, 4, lottodata.number_5);
                m_LottoWindows.SourceImageChange(i, 5, lottodata.number_6);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( true == DataAssetManager.Instance.bDataLoadComplete  )
        {
            // 임시로 테이블 하나만 해서 체크하는 것이다. 차후에 수정하자..... 2019-03-15
            // 로또 스크립테이블 로드한다. 
            LoadScriptableLottoData();
            DataAssetManager.Instance.bDataLoadComplete = false;
        }
    }

}
