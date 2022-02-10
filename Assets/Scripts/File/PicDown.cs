using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PicDown : MonoBehaviour 
{
    public string con_url;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    string url = string.Empty;
    WWW Link = null;
    //public Image Web_image = null;
    IEnumerator LoadImageInternet(Image pic,string url,string userID)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error + " Pic userID : " + userID);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            
            Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width,myTexture.height), new Vector2(0, 0));

            pic.sprite = sprite;
            //Web_image.sprite = sprite;
        }
    }

    //Button Event
    public void LoadFromInternet(Image pic,string userID)
    {
        url = con_url + "/uploads/"+userID+".jpg";
        StartCoroutine(LoadImageInternet(pic,url,userID));
    }

    

    
}
