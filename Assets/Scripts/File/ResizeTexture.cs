using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeTexture : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public enum ImageFilterMode : int
    {
        Nearest = 0,
        Biliner = 1,
        Average = 2
    }
    public static Texture2D DoResizeTexture(Texture2D source, Vector2 size)
    {
        Color[] aSourceColor = source.GetPixels(0);
        Vector2 vSourceSize = new Vector2(source.width, source.height);

        float xWidth = size.x;
        float xHeight = size.y;

        Texture2D oNewTex = new Texture2D((int)xWidth, (int)xHeight, TextureFormat.RGBA32, false);

        int xLength = (int)xWidth * (int)xHeight;
        Color[] aColor = new Color[xLength];

        Vector2 vPixelSize = new Vector2(vSourceSize.x / xWidth, vSourceSize.y / xHeight);

        Vector2 vCenter = new Vector2();
        for(int i = 0; i < xLength; ++i)
        {
            float xX = (float)i % xWidth;
            float xY = Mathf.Floor((float)i / xWidth);

            vCenter.x = (xX / xWidth) * vSourceSize.x;
            vCenter.y = (xY / xHeight) * vSourceSize.y;

            int xXFrom = (int)Mathf.Max(Mathf.Floor(vCenter.x - (vPixelSize.x * 0.5f)), 0);
            int xXTo = (int)Mathf.Min(Mathf.Ceil(vCenter.x + (vPixelSize.x * 0.5f)), vSourceSize.x);
            int xYFrom = (int)Mathf.Max(Mathf.Floor(vCenter.y - (vPixelSize.y * 0.5f)), 0);
            int xYTo = (int)Mathf.Min(Mathf.Ceil(vCenter.y + (vPixelSize.y * 0.5f)), vSourceSize.y);

            //Vector4 oColorTotal = new Vector4();
            Color oColorTemp = new Color();
            float xGridCount = 0;
            for(int iy = xYFrom; iy < xYTo; iy++)
            {
                for(int ix = xXFrom; ix < xXTo; ix++)
                {
                    oColorTemp += aSourceColor[(int)(((float)iy * vSourceSize.x) + ix)];

                    xGridCount++;
                }
            }

            aColor[i] = oColorTemp / (float)xGridCount;
        }

        oNewTex.SetPixels(aColor);
        oNewTex.Apply();

        return oNewTex;
    }
}
