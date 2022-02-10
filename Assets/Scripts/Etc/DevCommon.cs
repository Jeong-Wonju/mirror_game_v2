using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;

public enum BONE_TYPE
{
    NONE,
    HEAD,
    PELVIS,
    HAND_LEFT,
    HAND_RIGHT,
    FOOT_LEFT,
    FOOT_RIGHT
}

namespace DevelopeCommon
{
    public static class DevCommon
    {
        /*
               * 상대경로 얻어오는 함수
               * 사용법
         * 
         *   string path_folder = AssetDatabase.GetAssetPath(selected_object);
         *   DirectoryInfo dir_info      = new DirectoryInfo(path_folder);
         *   FileInfo [] file_png_infos  = dir_info.GetFiles("*.png");
         *
         *   foreach (FileInfo f in file_png_infos)
         *   {
         *      string relative_path = "Assets/" + DevelopeCommon.RelativePath(Application.dataPath, f.FullName.Replace("\\", "/"));
         *   }
         */
        public static string RelativePath(string fromPath, string toPath)
        {
            if (String.IsNullOrEmpty(toPath)) return "";
            if (String.IsNullOrEmpty(fromPath)) return toPath.Replace("\\", "/");

            toPath = toPath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);       // + Path.DirectorySeparatorChar;
            fromPath = fromPath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);   // + Path.DirectorySeparatorChar;
            if (fromPath[fromPath.Length - 1] != Path.DirectorySeparatorChar) fromPath += Path.DirectorySeparatorChar;
            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

            Uri relativeUri = null;
            try
            {
                relativeUri = fromUri.MakeRelativeUri(toUri);
            }
            catch
            {
                relativeUri = toUri;
            }
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.ToUpperInvariant() == "FILE")
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath.Replace("\\", "/");
        }

        // 한글인지 여부
        public static bool IsHangul(string input)
        {
            if (input.Equals(null) || input.Length.Equals(0)) return false;

            bool is_hangle_exit = false;
            for (int i = 0; i < input.Length; i++)
            {
                string rtnVal = string.Empty;
                char cStr = input[i];

                rtnVal += cStr + " : ";
                rtnVal += Char.GetUnicodeCategory(Convert.ToChar(cStr)).ToString();

                            //if (cStr >= '\xAC00' && cStr <= '\xD7AF') rtnVal += "    한글완성형";
                            //else if (cStr >= '\x3130' && cStr <= '\x318F') rtnVal += "    한글자음또는모음";
                            //else rtnVal += "    한글아님";

                if (cStr >= '\xAC00' && cStr <= '\xD7AF')
                {
                    is_hangle_exit = true;
                    break;
                }

                if (cStr >= '\x3130' && cStr <= '\x318F')
                {
                    is_hangle_exit = true;
                    break;
                }
            }

            return is_hangle_exit;
        }

        // 로컬 기기 IP Address 얻기
        public static string GetLocalIPAddress()
        {
            // Local IP 얻음
            //var host = Dns.GetHostEntry(Dns.GetHostName());
            //foreach (var ip in host.AddressList)
            //{
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        return ip.ToString();
            //    }
            //}
            //throw new Exception("Local IP Address Not Found!");

            string publicIPAddress = string.Empty;

            // External IP 얻음
            String url = "http://bot.whatismyipaddress.com/";
            String result = null;

            try
            {
                WebClient client = new WebClient();
                result = client.DownloadString(url);
                return result;
            }
            catch (Exception ex) { return "127.0.0.1"; }
        }

        static public T CreateObject<T>(string name, GameObject parentObj = null) where T : MonoBehaviour
        {
            GameObject go = new GameObject();
            go.name = name;
            if (parentObj != null)
            {
                go.transform.parent = parentObj.transform;
            }
            return go.AddComponent<T>();
        }

        static public T InstantiateFromMemory<T>(T t, string name, Vector3 pos, Quaternion qut, Transform parentTm = null) where T : MonoBehaviour
        {
            T clone = GameObject.Instantiate<T>(t);

            if (parentTm == null)
            {
                clone.transform.position = pos;
                clone.transform.rotation = qut;
            }
            else
            {
                clone.transform.localPosition = pos;
                clone.transform.localRotation = qut;
            }

            clone.transform.parent = parentTm;
            clone.transform.name = name;

            if (!clone.gameObject.activeSelf)
            {
                clone.gameObject.SetActive(true);
            }

            return clone;
        }

        static public Transform InstantiateFromMemory(Transform t, string name, Vector3 pos, Quaternion qut, Transform parentTm = null)
        {
            Transform clone = GameObject.Instantiate(t);

            if (parentTm == null)
            {
                clone.transform.position = pos;
                clone.transform.rotation = qut;
            }
            else
            {
                clone.transform.localPosition = pos;
                clone.transform.localRotation = qut;
            }

            clone.transform.parent = parentTm;
            clone.transform.name = name;

            if (!clone.gameObject.activeSelf)
            {
                clone.gameObject.SetActive(true);
            }

            return clone;
        }

        /// <summary>
        /// Resources 하위폴더에 있어야 한다. 파일이름가지 포함
        /// 예) _menu = DevCommon.InstantiateFromResource<Menu_Game>("menu/d20_menu_game", "menu_game", Vector3.zero, Quaternion.identity, parentTm);
        /// </summary>
		static public T InstantiateFromResource<T>(string resPath, string name, Vector3 pos, Quaternion qut, Transform parentTm = null) where T : MonoBehaviour
        {
            //BeginTime(resPath);
            GameObject go = GameObject.Instantiate(Resources.Load(resPath), pos, qut) as GameObject;
            T t = go.GetComponent<T>();
            go.transform.parent = parentTm;
            go.transform.name = name;

            if (parentTm == null)
            {
                go.transform.position = pos;
                go.transform.rotation = qut;
            }
            else
            {
                go.transform.localPosition = pos;
                go.transform.localRotation = qut;
            }

            go.transform.localScale = Vector3.one;
            //EndTime(resPath);

            return t;
        }

        static public T InstantiateFromResource<T>(string path_name, string name, Transform parentTm = null) where T : MonoBehaviour
        {
            //BeginTime(resPath);
            GameObject go = GameObject.Instantiate(Resources.Load(path_name)) as GameObject;
            T t = go.GetComponent<T>();
            go.transform.parent = parentTm;
            go.transform.name = name;

            go.transform.localScale = Vector3.one;
            //EndTime(resPath);

            return t;
        }

        static public GameObject InstantiateFromResource(string resPath, string name, Vector3 pos, Quaternion qut, Transform parentTm = null)
        {
            GameObject go = GameObject.Instantiate(Resources.Load(resPath), pos, qut) as GameObject;
            if (parentTm == null)
            {
                go.transform.position = pos;
                go.transform.rotation = qut;

                go.transform.parent = parentTm;
            }
            else
            {
                go.transform.parent = parentTm;

                go.transform.localPosition = pos;
                go.transform.localRotation = qut;
            }

            go.transform.name = name;

            return go;
        }

        // name_to_find 의 예) scroll_ranking/grid 
        static public GameObject GetChild(Transform root, string name_to_find)
        {
            Transform t = root.Find(name_to_find);
            DevDebug.Assert(t != null, "{0} not found", name_to_find);
            return t.gameObject;
        }

        static public T GetParentComponent<T>(Transform t) where T : Component
        {
            if (t.parent == null)
            {
                return null;
            }

            if (t.parent.GetComponent<T>() == null)
            {
                return GetParentComponent<T>(t.parent);
            }

            return t.parent.GetComponent<T>();
        }

        static public Vector3 GetBezier(Vector3 p0, Vector3 c, Vector3 p1, float lerp)
        {
            Vector3 pos = Vector3.zero;

            lerp = Mathf.Lerp(0, 10000.0f, lerp);

            float t = lerp / (10000.0f - 1.0f);
            pos = (1.0f - t) * (1.0f - t) * p0
                        + 2.0f * (1.0f - t) * t * c
                        + t * t * p1;

            return pos;
        }

        static public Vector3 GetRadiusVector(float radius, float degree)
        {
            Vector3 point = new Vector3(radius * Mathf.Cos(degree * Mathf.Deg2Rad)
                                        , 0
                                        , radius * Mathf.Sin(degree * Mathf.Deg2Rad));

            return point;
        }

        static public Vector3 GetVectorByTransform(Vector3 srcVector, Transform t)
        {
            Vector3 retVector = Vector3.zero;
            Matrix4x4 rotMat = Matrix4x4.TRS(Vector3.zero, t.rotation, Vector3.one);
            retVector = rotMat.MultiplyPoint3x4(srcVector);
            retVector += t.position;

            return retVector;
        }

        static public Transform FindHierachyTmByNameEquals(Transform parent, string obj_name)
        {
            Transform[] tm_arr = parent.transform.GetComponentsInChildren<Transform>(true);

            for (int i = 0; i < tm_arr.Length; ++i)
            {
                if (tm_arr[i].name.Equals(obj_name))
                {
                    return tm_arr[i];
                }
            }

            return null;
        }

        static public Transform FindHierachyTmByNameContains(Transform parent, string obj_name)
        {
            Transform[] tm_arr = parent.transform.GetComponentsInChildren<Transform>(true);

            for (int i = 0; i < tm_arr.Length; ++i)
            {
                if (tm_arr[i].name.Contains(obj_name))
                {
                    return tm_arr[i];
                }
            }

            return null;
        }

        static public Vector3 PointInCurve(Transform[] path, float t)
        {
            Vector3[] posArr = new Vector3[path.Length];
            for (int i = 0; i < path.Length; ++i)
            {
                posArr[i] = path[i].position;
            }

            return PointInCurve(posArr, t);
        }

        static public Vector3 PointInCurve(Vector3[] path, float t)
        {
            t = Math.Min(Mathf.Max(t, 0.0f), 1.0f);

            Vector3[] vector3s = PathControlPointGenerator(path);

            Vector3 pos = Interp(vector3s, t);

            return pos;
        }

        static public void DrawCurve(Transform[] path, Color color)
        {
            Vector3[] posArr = new Vector3[path.Length];
            for (int i = 0; i < path.Length; ++i)
            {
                posArr[i] = path[i].position;
            }

            DrawCurve(posArr, color);
        }

        static public void DrawCurve(Vector3[] path, Color color)
        {
            Vector3[] vector3s = PathControlPointGenerator(path);

            //Line Draw:
            Vector3 prevPt = Interp(vector3s, 0);
            Gizmos.color = color;
            int SmoothAmount = path.Length * 20;
            for (int i = 1; i <= SmoothAmount; i++)
            {
                float pm = (float)i / SmoothAmount;
                Vector3 currPt = Interp(vector3s, pm);
                Gizmos.DrawLine(currPt, prevPt);
                prevPt = currPt;
            }
        }

        public static Vector3[] PathControlPointGenerator(Vector3[] path)
        {
            Vector3[] suppliedPath;
            Vector3[] vector3s;

            //create and store path points:
            suppliedPath = path;

            //populate calculate path;
            int offset = 2;
            vector3s = new Vector3[suppliedPath.Length + offset];
            Array.Copy(suppliedPath, 0, vector3s, 1, suppliedPath.Length);

            //populate start and end control points:
            //vector3s[0] = vector3s[1] - vector3s[2];
            vector3s[0] = vector3s[1] + (vector3s[1] - vector3s[2]);
            vector3s[vector3s.Length - 1] = vector3s[vector3s.Length - 2] + (vector3s[vector3s.Length - 2] - vector3s[vector3s.Length - 3]);

            //is this a closed, continuous loop? yes? well then so let's make a continuous Catmull-Rom spline!
            if (vector3s[1] == vector3s[vector3s.Length - 2])
            {
                Vector3[] tmpLoopSpline = new Vector3[vector3s.Length];
                Array.Copy(vector3s, tmpLoopSpline, vector3s.Length);
                tmpLoopSpline[0] = tmpLoopSpline[tmpLoopSpline.Length - 3];
                tmpLoopSpline[tmpLoopSpline.Length - 1] = tmpLoopSpline[2];
                vector3s = new Vector3[tmpLoopSpline.Length];
                Array.Copy(tmpLoopSpline, vector3s, tmpLoopSpline.Length);
            }

            return (vector3s);
        }

        //andeeee from the Unity forum's steller Catmull-Rom class ( http://forum.unity3d.com/viewtopic.php?p=218400#218400 ):
        public static Vector3 Interp(Vector3[] pts, float t)
        {
            int numSections = pts.Length - 3;
            int currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections - 1);
            float u = t * (float)numSections - (float)currPt;

            Vector3 a = pts[currPt];
            Vector3 b = pts[currPt + 1];
            Vector3 c = pts[currPt + 2];
            Vector3 d = pts[currPt + 3];

            return .5f * (
                (-a + 3f * b - 3f * c + d) * (u * u * u)
                + (2f * a - 5f * b + 4f * c - d) * (u * u)
                + (-a + c) * u
                + 2f * b
            );
        }

        public static int ParseDigit(string strIncludingDigit)
        {
            string strTemp = Regex.Replace(strIncludingDigit, @"\D", "");
            int value = int.Parse(strTemp);
            return value;
        }

        private static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static byte[] StringToUTF8ByteArray(string pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        // 예) filePath = Application.persistentDataPath + "/settings.txt";
        public static void SerializeObject<T>(T pObject, string filePath)
        {
            try
            {
                if (pObject == null)
                {
                    UnityEngine.Debug.LogError("pObjectNull : filePath = " + filePath);
                }

                string XmlizedString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(pObject.GetType());
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());

                StreamWriter writer;
                FileInfo t = new FileInfo(filePath);
                if (!t.Exists)
                {
#if UNITY_EDITOR
                    DevDebug.Log(string.Format("file is empty so created"));
                    DevDebug.Log(string.Format("--> filePath = {0}", filePath));
#endif

                    writer = t.CreateText();
                }
                else
                {
#if UNITY_EDITOR
                    DevDebug.Log(string.Format("file already exist so delete and recreate"));
#endif
                    t.Delete();
                    writer = t.CreateText();
                }
                writer.Write(XmlizedString);
                writer.Close();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(string.Format("Exception : {0}", e.ToString()));
            }
        }

        // 예) filePath = Application.persistentDataPath + "/settings.txt";
        public static T DeserializeObject<T>(string filePath)
        {
            try
            {
                StreamReader r = File.OpenText(filePath);
                string _info = r.ReadToEnd();
                r.Close();

                XmlSerializer xs = new XmlSerializer(typeof(T));
                MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(_info));
                //XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                return (T)xs.Deserialize(memoryStream);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(string.Format("Exception : {0}", e.ToString()));
                return default(T);
            }
        }

        public static string EncryptObject<T>(T pObject, string filePath)
        {
            try
            {
                if (pObject == null)
                {
                    UnityEngine.Debug.LogError("pObjectNull : filePath = " + filePath);
                }

                string XmlizedString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(pObject.GetType());
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());

                // encrypt
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");
                // 256-AES key
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(XmlizedString);
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                // http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
                rDel.Padding = PaddingMode.PKCS7;
                // better lang support
                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                string encrypted_str = Convert.ToBase64String(resultArray, 0, resultArray.Length);

                StreamWriter writer;
                FileInfo t = new FileInfo(filePath);
                if (!t.Exists)
                {
#if UNITY_EDITOR
                    DevDebug.Log(string.Format("file is empty so created"));
                    DevDebug.Log(string.Format("--> filePath = {0}", filePath));
#endif

                    writer = t.CreateText();
                }
                else
                {
#if UNITY_EDITOR
                    DevDebug.Log(string.Format("file already exist so delete and recreate"));
#endif
                    t.Delete();
                    writer = t.CreateText();
                }

                writer.Write(encrypted_str);
                writer.Close();

                return encrypted_str;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(string.Format("Exception : {0}", e.ToString()));
                return null;
            }
        }

        public static T DecryptObject<T>(string filePath)
        {
            try
            {
                StreamReader r = File.OpenText(filePath);
                string encrypted_str = r.ReadToEnd();
                r.Close();

                return DecryptObjectFromString<T>(encrypted_str);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(string.Format("Exception : {0}", e.ToString()));
                return default(T);
            }
        }

        private static T DecryptObjectFromString<T>(string encrypted_str)
        {
            try
            {
                // decrypt string to xml
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");
                // AES-256 key
                byte[] toEncryptArray = Convert.FromBase64String(encrypted_str);
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                // http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
                rDel.Padding = PaddingMode.PKCS7;
                // better lang support
                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                string decrypted_str = UTF8Encoding.UTF8.GetString(resultArray);

                DevelopeCommon.DevDebug.Log("Decyprted xml: " + decrypted_str);

                // deserialize
                XmlSerializer xs = new XmlSerializer(typeof(T));
                MemoryStream memoryStream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(decrypted_str));
                //XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                return (T)xs.Deserialize(memoryStream);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(string.Format("Exception : {0}", e.ToString()));
                return default(T);
            }
        }

        static public string EncryptString(string key, string str_val)
        {
            // encrypt
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            // 256-AES key
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(str_val);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            // http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
            rDel.Padding = PaddingMode.PKCS7;
            // better lang support
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            string encrypted_str = Convert.ToBase64String(resultArray, 0, resultArray.Length);

            return encrypted_str;
        }

        static public string DecryptString(string key, string encrypted_str)
        {
            // decrypt string to xml
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            // AES-256 key
            byte[] toEncryptArray = Convert.FromBase64String(encrypted_str);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            // http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
            rDel.Padding = PaddingMode.PKCS7;
            // better lang support
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            string decrypted_str = UTF8Encoding.UTF8.GetString(resultArray);

            return decrypted_str;
        }

        static public bool IsMobile()
        {
            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return true;
            }

            return false;
        }

        static public string ExtractOnlyChar(string str)
        {
            string tempStr = str;
            tempStr = Regex.Replace(tempStr, @"\d", "");
            return tempStr;
        }

        static public string ExtractOnlyDigit(string str)
        {
            string tempStr = str;
            tempStr = Regex.Replace(tempStr, @"\D", "");
            return tempStr;
        }

        static public Color GetRGBColor(float r, float g, float b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        public static string GetChecksum(string absPathFile)
        {
            if (!File.Exists(absPathFile))
            {
                return null;
            }

            using (FileStream stream = File.OpenRead(absPathFile))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] byteCheckSum = md5.ComputeHash(stream);
                return BitConverter.ToString(byteCheckSum).Replace("-", String.Empty);
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

#region FILE_FUNTION
        public static bool DeleteFile(string entier_file_path)
        {
            FileInfo t = new FileInfo(entier_file_path);
            if (t.Exists)
            {
                Debug.Log("Delete File : " + Path.GetFileName(entier_file_path));
                t.Delete();

                string meta_file = entier_file_path + ".meta";
                if( IsFileExistInAssets(meta_file) )
                {
                    DeleteFile(meta_file);
                }

                return true;
            }

            return false;
        }

        public static bool IsFileExistInAssets(string entire_file_path)
        {
            FileInfo t = new FileInfo(entire_file_path);
            if (t.Exists)
            {
                return true;
            }

            return false;
        }

        // DevCommon.GetFilePathAssetToEntire(@"Assets\03_SoccerKing\08_Resource\08_Excel\CharacterPropertyTable.xlsx");
        public static string GetFilePathAssetToEntire(string assetFilePath)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, assetFilePath);

            return filePath;
        }

        public static string GetFilePathEntierToAsset(string entier_file_path)
        {
            int asset_index = entier_file_path.IndexOf("Assets");
            string asset_path = entier_file_path.Substring(asset_index);
            return asset_path;
        }

        public static string GetPersistentFilePath(string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            path = path.Replace('\\', '/');
            return path;
        }

        public static string[] GetFileNameArrFromPath(string path)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }

            DirectoryInfo dir_info = new DirectoryInfo(path);

            if (dir_info == null)
            {
                return null;
            }

            List<string> list_file = new List<string>();

            foreach (var file in dir_info.GetFiles())
            {
                if (file.Extension.Equals(".meta")) continue;
                list_file.Add(file.Name);
            }

            return list_file.ToArray();
        }
        #endregion

        #region Render
        static public void ResetSahder(GameObject go)
        {
            Renderer[] renderer_arr = go.GetComponentsInChildren<Renderer>();

            for (int a = 0; a < renderer_arr.Length; ++a)
            {
                Material[] material_arr = renderer_arr[a].sharedMaterials;

                for (int b = 0; b < material_arr.Length; ++b)
                {
                    if (material_arr[b] == null || material_arr[b].shader == null) continue;

                    material_arr[b].shader = Shader.Find(material_arr[b].shader.name);
                }

            }
        }
        #endregion

        static public Transform GetBoneTransform(Transform root, BONE_TYPE bone_type)
        {
            if( bone_type == BONE_TYPE.HEAD )
            {
                Transform head_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01_HeadNub");

                if (head_tm == null) head_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01 HeadNub");
                if (head_tm == null) head_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001 HeadNub");
                if (head_tm == null) head_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001_HeadNub");

                //return new WeakReference(head_tm).Target as Transform;
                return root;
            }
            else if( bone_type == BONE_TYPE.PELVIS )
            {
                Transform pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01 Pelvis");

                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01_Pelvis");
                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001 Pelvis");
                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001_Pelvis");

                //return new WeakReference(pelvis_tm).Target as Transform;
                return root;
            }
            else if (bone_type == BONE_TYPE.HAND_LEFT)
            {
                Transform pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01 L Hand");

                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01_L_Hand");
                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001 L Hand");
                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001_L_Hand");

                //return new WeakReference(pelvis_tm).Target as Transform;
                return root;
            }
            else if (bone_type == BONE_TYPE.HAND_RIGHT)
            {
                Transform pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01 R Hand");

                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01_R_Hand");
                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001 R Hand");
                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001_R_Hand");

                //return new WeakReference(pelvis_tm).Target as Transform;
                return root;
            }
            else if (bone_type == BONE_TYPE.FOOT_LEFT)
            {
                Transform pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01 L Foot");

                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01_L_Foot");
                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001 L Foot");
                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001_L_Foot");

                //return new WeakReference(pelvis_tm).Target as Transform;
                return root;
            }
            else if (bone_type == BONE_TYPE.FOOT_RIGHT)
            {
                Transform pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01 R Foot");

                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01_R_Foot");
                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001 R Foot");
                if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001_R_Foot");

                //return new WeakReference(pelvis_tm).Target as Transform;
                return root;
            }

            return root;
        }

        static public Transform GetBipRootTransform(Transform root)
        {
            Transform BipRoot_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01");

            if (BipRoot_tm == null) BipRoot_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001");
            if (BipRoot_tm == null) BipRoot_tm = DevCommon.FindHierachyTmByNameEquals(root, "Body");
            if (BipRoot_tm == null) BipRoot_tm = DevCommon.FindHierachyTmByNameEquals(root, "Scorpion_bip01");

            //if (BipRoot_tm == null) Debug.Log("error~~~~~~~~~~~~~!!!! : bip root not exist~~~~~~~~~~~~~~~~~~~~~~~~!!!!!");
            if (BipRoot_tm == null) DevDebug.LogColor("red", "error~~~~~~~~~~~~~!!!! : bip root not exist~~~~~~~~~~~~~~~~~~~~~~~~!!!!!");

            return new WeakReference(BipRoot_tm).Target as Transform;
        }

       static  public Transform GetHeadTransform(Transform root)
       {
            Transform head_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01_HeadNub");

            if( head_tm == null) head_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01 HeadNub");
            if (head_tm == null) head_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001 HeadNub");
            if (head_tm == null) head_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001_HeadNub");
            if (head_tm == null) head_tm = DevCommon.FindHierachyTmByNameEquals(root, "Head");
            if (head_tm == null) head_tm = DevCommon.FindHierachyTmByNameEquals(root, "root");

            return new WeakReference(head_tm).Target as Transform;
       }

       static public Transform GetPelvisTransform(Transform root)
       {
           Transform pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01 Pelvis");

           if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip01_Pelvis");
           if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001 Pelvis");
           if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Bip001_Pelvis");
            if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Body");
            if (pelvis_tm == null) pelvis_tm = DevCommon.FindHierachyTmByNameEquals(root, "Scorpion_bip01");

            return new WeakReference(pelvis_tm).Target as Transform;
       }

       public static string GetIndexString(int i)
       {
           if( i < 10 )
           {
               return string.Format("0{0}", i);
           }

           return i.ToString();
       }

        /// <summary>
       /// DevCommon.ChangeAllLayers(_weapon_go, LayerMask.NameToLayer("auto_hero_weapon_me"));
        /// </summary>
       public static void ChangeAllLayers(GameObject go, int layer)
       {
           go.layer = layer;
           foreach (Transform child in go.transform)
           {
               ChangeAllLayers(child.gameObject, layer);
           }
       }

       #region 확장 메서드
       public static void X(this Transform tm, float x)
       {
           Vector3 pos = tm.position;
           pos.x = x;
           tm.position = pos;
       }

       public static void Y(this Transform tm, float y)
       {
           Vector3 pos = tm.position;
           pos.y = y;
           tm.position = pos;
       }

       public static void Z(this Transform tm, float z)
       {
           Vector3 pos = tm.position;
           pos.z = z;
           tm.position = pos;
       }

        public static void LX(this Transform tm, float x)
        {
            Vector3 pos = tm.localPosition;
            pos.x = x;
            tm.localPosition = pos;
        }

        public static void LY(this Transform tm, float y)
        {
            Vector3 pos = tm.localPosition;
            pos.y = y;
            tm.localPosition = pos;
        }

        public static void LZ(this Transform tm, float z)
        {
            Vector3 pos = tm.localPosition;
            pos.z = z;
            tm.localPosition = pos;
        }
        #endregion

       public static string RemoveSpace(string str)
       {
            str = str.Replace("\r\n", string.Empty)
                  .Replace("\r", string.Empty)
                  .Replace("\n", string.Empty)
                  .Replace("\\n", string.Empty);

           int start = 0;
           int num = 0;
           string tmp = str;
           while (tmp.IndexOf(" ") > 0)
           {
               num = tmp.IndexOf(" ");
               string tmp1 = tmp.Substring(0, num);
               start = num + 1;
               tmp1 += tmp.Substring(num + 1);
               tmp = tmp1;
           }
           return tmp;
       }

       public static Vector3 GetParabola(Vector3 start_pos, Vector3 velocity, float time, float gravity = 9.8f)
       {
           Vector3 ret = Vector3.zero;

           ret.x = start_pos.x + velocity.x * time;
           ret.y = start_pos.y + (velocity.y * time) - (0.5f * gravity * time * time);
           ret.z = start_pos.z + velocity.z * time;

           return ret;
       }

        
    }
}
