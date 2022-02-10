using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class CsvReader : SingletonMonoManager<CsvReader>
{
    string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    char[] TRIM_CHARS = { '\"' };

    private void Start()
    {
        //Util.LogColorMagenta("CsvReader Start");
    }

    

    public  void Read(string file, out List<Dictionary<string, object>> list)
    {
        list = new List<Dictionary<string, object>>();

        //string data_text = "";
        //FileStream fileStream = new FileStream("Assets/" + file + ".csv", FileMode.Open, FileAccess.Read);
        //StreamReader reader = new StreamReader(fileStream, System.Text.Encoding.ASCII);
        //data_text = reader.ReadToEnd();
        //reader.Close();
        //TextAsset data = AssetDatabase.LoadAssetAtPath("Assets/" + file + ".csv", typeof(TextAsset)) as TextAsset;
        TextAsset data = Resources.Load(file) as TextAsset;

        //var lines = Regex.Split(data_text, LINE_SPLIT_RE);
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                value = value.Replace("<br>", "\n"); // 추가된 부분. 개행문자를 \n대신 <br>로 사용한다.
                value = value.Replace("<c>", ",");

                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return;
    }

    //외부에서 파일을 변경할 필요가 있는 경우
    public void ReadEx(string file, out List<Dictionary<string, object>> list)
    {
        list = new List<Dictionary<string, object>>();

        string data_text = "";
        FileStream fileStream = new FileStream("./" + file + ".csv", FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(fileStream, System.Text.Encoding.ASCII);
        data_text = reader.ReadToEnd();
        reader.Close();

        //에디터에서만 가능한 경우
        //TextAsset data = AssetDatabase.LoadAssetAtPath("Assets/" + file + ".csv", typeof(TextAsset)) as TextAsset;
        
        var lines = Regex.Split(data_text, LINE_SPLIT_RE);
        //var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                value = value.Replace("<br>", "\n"); // 추가된 부분. 개행문자를 \n대신 <br>로 사용한다.
                value = value.Replace("<c>", ",");

                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
    }
}
