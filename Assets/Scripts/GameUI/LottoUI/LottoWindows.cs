//==========================================================
// File : LottoWindows.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 21일
// Unity Connect : 
// Remarks 
// -   로또 윈도우 창
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LottoNumber
{
    public Text text = null;
    public Image image = null;
    public int nLottoNumber = 0;
}

[System.Serializable]
public class RandomLottoElement
{
    public LottoNumber[] LottoNumberDatas = new LottoNumber[6];
}

public class LottoWindows : MonoBehaviour
{
    public RandomLottoElement[] RandomLottoElementDatas = new RandomLottoElement[5];

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

    public void SourceImageChange(int nDataIndex = 0, int nNumberIndex = 0, int nValue = 0 )
    {
        string strPath = "Atlases/TextureImage/Lotto/" + nValue;// + ".png";
        RandomLottoElementDatas[nDataIndex].LottoNumberDatas[nNumberIndex].image.sprite = Resources.Load(strPath, typeof(Sprite)) as Sprite;
        RandomLottoElementDatas[nDataIndex].LottoNumberDatas[nNumberIndex].nLottoNumber = nValue;
    }


    public void AddRandomLottoElements(ref RandomLottoElement[] randomlottoelements)
    {
        RandomLottoElementDatas = randomlottoelements;
    }

    public void AddRandomLottoNumber(Text text = null, int nLottoNumber = 0, Image image = null)
    {
        LottoNumber rle = new LottoNumber();
        rle.image = image;
        rle.text = text;
        rle.nLottoNumber = nLottoNumber;
    }

    public void AddRandomLottoElement( int nIndex = 0, Text text = null, int nLottoNumber = 0, Image image = null)
    {
        RandomLottoElementDatas[nIndex].LottoNumberDatas[nIndex].image = image;
        RandomLottoElementDatas[nIndex].LottoNumberDatas[nIndex].text = text;
        RandomLottoElementDatas[nIndex].LottoNumberDatas[nIndex].nLottoNumber = nLottoNumber;
    }
}
