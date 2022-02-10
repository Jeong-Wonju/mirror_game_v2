using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPush : MonoBehaviour {

    private string m_URL = "http://catchone13.iptime.org:80/push.php";

    // Use this for initialization
    void Start () {

        //string token = "d6EZsNWoaJQ:APA91bEoUhtfBpa8wCyeUQNB-t0Ko6EzROB5iiF81lHX4uQymKS0J4ku5fG39VQsr2elxhNfYRQ6lqJiHyY18IX64MfZYbcNjnx9bhEZiOE6sxrHLvdN8PoFhFqXsDB7jaybAUQnf-qs";
        //Push(m_URL,token);
		
	}

    IEnumerator PushCo(string uploadURL,string token, string msg)
    {
        WWWForm form = new WWWForm();

        form.AddField("token", token);
        form.AddField("title", GameManager_Talk.Instance.m_UserID);
        form.AddField("message", msg);

        WWW push = new WWW(uploadURL,form);
        yield return push;
        if (push.error == null)
            Debug.Log("push done :" + push.text);
        else
            Debug.Log("Error during push: " + push.error);
    }

    public void Push(string token, string msg)
    {
        StartCoroutine(PushCo(m_URL,token,msg));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
