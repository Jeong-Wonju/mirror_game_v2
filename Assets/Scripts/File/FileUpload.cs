using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;



// 사진올리기 2019-03-12
public class FileUpload : MonoBehaviour
{

    //private string m_LocalFileName = "D:/testfile.png";
    private string m_URL = "http://test.fantajoy.com:80/upload.php";


    // Use this for initialization
    void Start()
    {
        //Debug.Log("fileupload.php");
        //UploadFile(m_LocalFileName, m_URL);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator UploadFileCo(string localFilePath, string uploadURL)
    {
        WWW localFile = new WWW("file:///" + localFilePath);
        yield return localFile;
        if (localFile.error == null)
            Debug.Log("Loaded file successfully");
        else
        {
            Debug.Log("Open file error: " + localFile.error);
            yield break; // stop the coroutine here
        }

        //Texture2D texture2D = rotateTexture(localFile.texture, true);

        int w = Screen.width;
        int h = Screen.height;

        float ratio = h / w;

        Texture2D texture2D = ResizeTexture.DoResizeTexture(localFile.texture, new Vector2(200, 200));
        //Texture2D texture2D = ResizeTexture.DoResizeTexture(GetComponent<IVPickerExample>().texture, new Vector2(200, 200));

        /*Texture2D croppedTexture = new Texture2D((int)cropArea.rectTransform.rect.width, (int)cropArea.rectTransform.rect.height);
        Texture2D originalTexture = (Texture2D)originalImage.mainTexture;

        croppedTexture.SetPixels(originalTexture.GetPixels((int)cropArea.rectTransform.anchoredPosition.x, (int)cropArea.rectTransform.anchoredPosition.y, (int)cropArea.rectTransform.rect.width, (int)cropArea.rectTransform.rect.height));
        croppedTexture.Apply();
        croppedImage.texture = croppedTexture;*/


        byte[] encode_jpg = texture2D.EncodeToJPG();

        WWWForm postForm = new WWWForm();

        //postForm.AddBinaryData("theFile",localFile.bytes);

        //경로만 추출
        //Application.dataPath
        string path = Path.GetDirectoryName(localFilePath);
        string exec = Path.GetExtension(localFilePath);

        Debug.Log("exec : " + exec);
        Debug.Log("unicodeID : " + GameManager_Talk.Instance.m_UserID);

        // utf-8 인코딩
        //byte[] bytesForEncoding = Encoding.UTF8.GetBytes(GameManager_Talk.Instance.m_UserID);
        //string encodedString = System.Convert.ToBase64String(bytesForEncoding);

        // utf-8 디코딩
        //byte[] decodedBytes = Convert.FromBase64String(encodedString);
        //string decodedString = Encoding.UTF8.GetString(decodedBytes);

        //byte[] contents = Encoding.Default.GetBytes(GameManager_Talk.Instance.m_UserID);
        //string str = Encoding.UTF8.GetString(contents);

        

        string combine_name = Path.Combine(path, GameManager_Talk.Instance.m_UserID + exec);

        Debug.Log("file path : " + combine_name);

        postForm.AddBinaryData("file", encode_jpg,  combine_name, "");
        postForm.AddField("filename", GameManager_Talk.Instance.m_UserID + exec,Encoding.UTF8);

        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
            Debug.Log("upload done :" + upload.text);
        else
            Debug.Log("Error during upload: " + upload.error);
    }

    Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }

    public void UploadFile(string localFilePath)
    {
        string img_file = GetComponent<IVPickerExample>().imagePath;
        StartCoroutine(UploadFileCo(img_file, m_URL));
    }


}
