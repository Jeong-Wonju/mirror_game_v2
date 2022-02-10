#if UNITY_EDITOR
    #define DEBUG
#endif

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System;
using System.Reflection;

namespace DevelopeCommon
{
	public class DevDebug
	{
		private static FileInfo _system_log_file { get; set; }

		public static void SetupLogFile()
		{
			// log file output
			string system_log_file = FileUtil.LocalRep() + "/log_output.log";
			DevDebug._system_log_file = new System.IO.FileInfo(system_log_file);
		}

		public static void SetupUnhandledException()
		{
			Application.RegisterLogCallback(HandleException);
		}

		private static void HandleException(string condition, string stack_trace, LogType log_type)
		{
			if (log_type == LogType.Exception)
				DevDebug.LogError("######## Unhandled Exception: " + condition + "\n" + stack_trace);
		}

		private static void FileLog(string log_message)
		{
			//if (_system_log_file == null)
			//	return;

			//try
			//{
			//	using (TextWriter writer = new StreamWriter(_system_log_file.Open(FileMode.Append, FileAccess.Write, FileShare.Read)))
			//	{
			//		writer.WriteLine(String.Format("{0}|{1}", DateTime.Now.ToString("HH:mm:ss.fff"), log_message));
			//	}
			//}
			//catch (Exception)
			//{
			//}
		}

		[Conditional("DEBUG")]
		static public void Log(string log)
		{
			//FileLog(log);
			UnityEngine.Debug.Log(log);
		}

		[Conditional("DEBUG")]
		static public void LogColor(string color, string log_format, params object[] args)
		{
            string log1 = string.Format(log_format, args);
			string col_log = string.Format("<color={0}>{1}</color> : {2}", color, log1, DateTime.Now.ToString("HH:mm:ss.fff"));
			UnityEngine.Debug.Log(col_log);
		}

		[Conditional("DEBUG")]
		static public void Log(string log_message_format, params object[] args)
		{
			string log = string.Format(log_message_format, args);
			//FileLog(log);
			UnityEngine.Debug.Log(log);
		}

		[Conditional("DEBUG")]
		static public void LogWarning(string log)
		{
			//FileLog(log);
			UnityEngine.Debug.LogWarning(log);
		}

		[Conditional("DEBUG")]
		static public void LogWarning(string log_message_format, params object[] args)
		{
			string log = string.Format(log_message_format, args);
			string color = "#ff00ffff";
			//FileLog(log);
			string col_log = string.Format("<color={0}>{1}</color>", color, log);
			UnityEngine.Debug.LogWarning(col_log);
		}

		[Conditional("DEBUG")]
		static public void LogError(string log)
		{
			//FileLog(log);
			UnityEngine.Debug.LogWarning(log);
		}

		[Conditional("DEBUG")]
		static public void LogError(string log_message_format, params object[] args)
		{
			string log = string.Format(log_message_format, args);
			string color = "#ff0000ff";
			//FileLog(log);
			string col_log = string.Format("<color={0}>{1}</color>", color, log);
			UnityEngine.Debug.LogError(col_log);
		}

		[Conditional("DEBUG")]
		public static void Assert(bool p)
		{
			Assert(p, string.Empty);
		}

		[Conditional("DEBUG")]
		static public void Assert(bool shouldbe, string msg)
		{
			if (!shouldbe)
			{
                string msg2 = string.Format("[******** Assert ********] {0}", msg);
                LogColor("red", msg2);

				UnityEngine.Debug.Break();
			}
		}

        [Conditional("DEBUG")]
        static public void AssertObject(bool shouldbe, UnityEngine.Object obj, string msg)
        {
            if (!shouldbe)
            {
                string msg2 = string.Format("[******** Assert ********] {0}", msg);
                LogColor("red", msg2);
                UnityEngine.Debug.Log("object", obj);

                UnityEngine.Debug.Break();
            }
        }

        [Conditional("DEBUG")]
        static public void LogJson(JSONObject json_obj, string title)
        {
            DevDebug.LogColor("magenta", "JSON: ##### {0}", title);
            DevDebug.LogColor("magenta", "JSON: type = {0}", json_obj.type);

            if (json_obj.type == JSONObject.Type.STRING)
            {
                DevDebug.LogColor("magenta", "JSON: string = {0}", json_obj.str);
            }

            if (json_obj.keys != null)
            {
                DevDebug.LogColor("magenta", "JSON: key count = {0}", json_obj.keys.Count);

                for (int i = 0; i < json_obj.keys.Count; ++i)
                {
                    DevDebug.LogColor("magenta", "JSON: key = {0}", json_obj.keys[i]);
                }
            }

            if (json_obj.list != null)
            {
                DevDebug.LogColor("magenta", "JSON: list count = {0}", json_obj.list.Count);

                for (int i = 0; i < json_obj.list.Count; ++i)
                {
                    DevDebug.LogColor("magenta", "JSON: list = {0}", json_obj.list[i].str);
                }
            }
        }

        static public void Assert(bool shouldbe, string log_message_format, params object[] args)
        {
            if (!shouldbe)
            {
                string log = string.Format(log_message_format, args);
                string color = "#ff0000ff";
                string col_log = string.Format("<color={0}>{1}</color>", color, log);

                Log(col_log, "red");
                UnityEngine.Debug.Break();
            }
        }

        [Conditional("DEBUG")]
        public static void ClearLogConsole()
        {
#if UNITY_EDITOR
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
            var type = assembly.GetType("UnityEditorInternal.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
#endif
        }

        /// <summary>
        /// 디바이스에서 봐야 할 로그
        /// </summary>
        static public void LogDevice(string log_message_format, params object[] args)
        {
            string log = string.Format(log_message_format, args);
            //FileLog(log);
            UnityEngine.Debug.Log(string.Format("### DeviceUnityLog : {0}", log));
        }

        public class PerformanceTimer
		{
			private DateTime _start_time;

			public PerformanceTimer()
			{
				Reset();
			}

			public void Reset()
			{
				_start_time = DateTime.Now;
			}

			public float elapse
			{
				get
				{
					TimeSpan span = DateTime.Now.Subtract(_start_time);
					return ((float)span.Ticks / (float)TimeSpan.TicksPerSecond);
				}
			}
		};

		static private PerformanceTimer _system_timer = null;
		static private float _last_time_mark = 0;

		// put timemark log.
		[Conditional("DEBUG")]
		static public void Timemark(string keyword)
		{
			if (_system_timer == null)
				_system_timer = new PerformanceTimer();

			float current_time = _system_timer.elapse;

			DevDebug.Log(String.Format("PERFORMACE_TIMEMARK({0}) : total:{1} / gap:{2}",
				keyword, current_time, current_time - _last_time_mark));

			_last_time_mark = current_time;
		}
    }
}