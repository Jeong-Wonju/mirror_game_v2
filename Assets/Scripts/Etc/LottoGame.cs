//==========================================================
// File : LottoGame.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 13일
// Unity Connect : 
// Remarks 
// -   로또 
//==========================================================
using DevelopeCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnhancedUI;
using GameDefine_Talk;

[System.Serializable]
public class RandomLotto_Element
{
    public Text text = null;
    public Image image = null;
    public int nLottoNumber = 0;
}

public class LottoGame : MonoBehaviour
{
    public RandomLotto_Element[] RandLottoDatas = new RandomLotto_Element[6];
    public LottoUI Lottoui = null;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SourceImageChange( int nIndex = 0, int nNumber = 0 )
    {
        string strPath = "Atlases/TextureImage/Lotto/" + nNumber;// + ".png";
        RandLottoDatas[nIndex].image.sprite = Resources.Load(strPath, typeof(Sprite)) as Sprite;
        RandLottoDatas[nIndex].nLottoNumber = nNumber;
    }
    

    public void AddRandomLottoElements( ref RandomLotto_Element[] randomlottoelements )
    {
        RandLottoDatas = randomlottoelements;
    }

    public void AddRandomLottoElement( Text text = null, int nLottoNumber = 0, Image image = null )
    {
        RandomLotto_Element rle = new RandomLotto_Element();
        rle.image = image;
        rle.text = text;
        rle.nLottoNumber = nLottoNumber;
    }
}
