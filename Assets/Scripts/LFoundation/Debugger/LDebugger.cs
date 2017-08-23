using System;
using System.IO;
using UnityEngine;

namespace UnityEngine
{
	
	public class LDebugger
	{

		public static bool EnableLog;
		public static bool EnableTime=true;
		public static bool EnableStack=false;
		public static bool EnableSave = false;

		public static string LogFileDir=Application.persistentDataPath+"/LDebugerLog/";
		public static string LogFileName="";
		public static string Prefix=">> ";
		public static StreamWriter LogFileWriter;

		#region Common Log Function
		public static void Log(object message)
		{
			if (!LDebugger.EnableLog) 
			{
				return;
			}

			string msg = GetLogTime () + message;
			Debug.Log (Prefix+msg);
			LogToFile ("[I]"+msg);
		}

		public static void Log(object message,Object context)
		{
			if (!LDebugger.EnableLog) 
			{
				return;
			}

			string msg = GetLogTime () + message;
			Debug.Log (Prefix+msg,context);
			LogToFile ("[I]"+msg);
		}

		public static void LogError(object message)
		{
			string msg = GetLogTime () + message;
			Debug.LogError (Prefix+msg);
			LogToFile ("[E]"+msg,true);
		}

		public static void LogError(object message,Object context)
		{
			string msg = GetLogTime () + message;
			Debug.LogError ("[E]"+msg,context);
			LogToFile ("[E]"+msg,true);
		}

		public static void LogWarning(object message)
		{
			string msg = GetLogTime () + message;
			Debug.LogWarning (Prefix+msg);
			LogToFile ("[W]"+msg);
		}

		public static void LogWarning(object message,Object context)
		{
			string msg = GetLogTime () + message;
			Debug.LogWarning (Prefix + msg, context);
			LogToFile ("[W]"+msg);
		}
		#endregion


		#region Common Log Function
		public static void Log(string tag,string message)
		{
			if (!LDebugger.EnableLog) 
			{
				return;
			}
			message = GetLogText (tag, message);
			Debug.Log (Prefix+message);
			LogToFile ("[I]"+message);
		}

		public static void Log(string tag,string format,params object[] args)
		{
			if (!LDebugger.EnableLog) 
			{
				return;
			}
			string message = GetLogText (tag,string.Format(format,args));
			Debug.Log (Prefix+message);
			LogToFile ("[I]"+message);
		}

		public static void LogError(string tag,string message)
		{
			message = GetLogText (tag, message);
			Debug.LogError (Prefix+message);
			LogToFile ("[E]"+message);
		}

		public static void LogError(string tag,string format,params object[] args)
		{
			string message = GetLogText (tag,string.Format(format,args));
			Debug.LogError (Prefix+message);
			LogToFile ("[E]"+message);
		}

		public static void LogWarning(string tag,string message)
		{
			message = GetLogText (tag, message);
			Debug.LogWarning (Prefix+message);
			LogToFile ("[W]"+message);
		}

		public static void LogWarning(string tag,string format,params object[] args)
		{
			string message = GetLogText (tag,string.Format(format,args));
			Debug.LogWarning (Prefix+message);
			LogToFile ("[W]"+message);
		}
		#endregion


		#region Log Util Function
		private static string GetLogText(string tag,string message)
		{
			string str = "";
			if (LDebugger.EnableTime) 
			{
				str = DateTime.Now.ToString ("HH::mm::ss.fff")+" ";
			}
			str = str + tag + "::" + message;
			return str;
		}

		private static string GetLogTime()
		{
			string str = "";
			if (LDebugger.EnableTime) 
			{
				str = DateTime.Now.ToString ("HH::mm::ss.fff")+" ";
			}
			return str;
		}


		private static void LogToFile(string message,bool EnableStack=false)
		{
			if (!EnableSave) 
			{
				return;
			}
			if (LogFileWriter == null) 
			{
				LogFileName = DateTime.Now.GetDateTimeFormats ('s') [0].ToString ();
				LogFileName = LogFileName.Replace ("-", "_");
				LogFileName = LogFileName.Replace (":","_");
				LogFileName = LogFileName.Replace (" ","");
				LogFileName += ".log";
				string fullPath = LogFileDir + LogFileName;
				try
				{
					if(!Directory.Exists(LogFileDir))
					{
						Directory.CreateDirectory(LogFileDir);
					}
					LogFileWriter=File.AppendText(fullPath);
					LogFileWriter.AutoFlush=true;
				}
				catch(Exception e)
				{
					LogFileWriter = null;
					Debug.LogError ("LogToFile() "+e.Message+e.StackTrace);
					return;
				}
			}

			if (LogFileWriter != null) 
			{
				try
				{
					LogFileWriter.WriteLine(message);
					if(EnableStack||LDebugger.EnableStack)
					{
						LogFileWriter.WriteLine(StackTraceUtility.ExtractStackTrace());

					}

				}
				catch(Exception e) 
				{
                    Debug.LogError("LogToFile() " + e.Message + e.StackTrace);
					return;
				}
			}
		}
		#endregion
	}
}
