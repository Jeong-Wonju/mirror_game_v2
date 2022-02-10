using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputID : MonoBehaviour
{

    public Text m_Text;
    bool m_b;

	// Use this for initialization
	void Start ()
    {
        m_Text = GetComponent<Text>();
        m_b = false;    


    }   
	
	// Update is called once per frame
	void Update ()
    {
        // ID 값을 읽어온다. 

        if( (false == m_b) &&  (m_Text.text != "") )
        {
            Debug.Log(m_Text.text);
            m_b = true;
        }

    }
}
