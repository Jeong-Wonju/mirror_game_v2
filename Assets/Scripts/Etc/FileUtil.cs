using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System;
using System.IO;

namespace DevelopeCommon
{
	public static class FileUtil
	{
		private static SHA1 _sha1;

		static FileUtil()
		{
			_sha1 = SHA1.Create();
		}

		public static string LocalRep()
		{
			string rep_path;

			// http://www.unitystudy.net/bbs/board.php?bo_table=dustin&wr_id=357

#if UNITY_IPHONE			
			string fileNameBase = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'));
			rep_path = fileNameBase.Substring(0, fileNameBase.LastIndexOf('/')) + "/Documents";
#elif UNITY_ANDROID
			rep_path = Application.persistentDataPath;
#else
			rep_path = Application.dataPath;
#endif

			return rep_path;
		}

		// convert filename fit in local repository
		static public string ConvertToLocalRep(string file_name)
		{
			return string.Format("{0}/{1}", LocalRep(), file_name);
		}

		public static string FileHash(string filename)
		{
			return FileHash(new FileInfo(filename));
		}

		public static string FileHash(FileInfo fi)
		{
			if (!fi.Exists)
			{
				return null;
			}

			string hash;
			using (var br = new BinaryReader(fi.OpenRead()))
			{
				hash = FileHash(br.ReadBytes((int)fi.Length));
				br.Close();
			}

			return hash;
		}

		public static string FileHash(byte[] bytes)
		{
			return BitConverter.ToString(_sha1.ComputeHash(bytes)).Replace("-", String.Empty);
		}
	}
}