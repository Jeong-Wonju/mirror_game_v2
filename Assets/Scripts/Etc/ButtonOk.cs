using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOk : MonoBehaviour
{
    public Bg   m_Bg    = null;
    public Text m_Text  = null;

    // Use this for initialization
    void Start (){}
	
	// Update is called once per frame
	void Update () {}


    public void ID()
    {
        //float fX = (float)NativeToolkit.GetLongitude();
        //float fY = (float)NativeToolkit.GetLatitude();
        //Debug.Log("fX =" + fX.ToString() + "fY =" + fY.ToString());
        NetworkManager_Talk.Instance.GetComponent<PacketSender>().SendMemberJoin(m_Text.text, 0.0f, 0.0f);
        Debug.Log(m_Text.text);

        m_Bg.gameObject.SetActive(false);
    }
}
