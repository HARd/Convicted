/*
	Перехватывает Debug.Log сообщения и пишет их в отдельный каталог в профиле пользователя.
	На каждый запуск создает отдельный лог с включением времени в имя.
	Удаляет старые логи при запуске.
	Автор: Кущ А.Е.
*/


using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Logger : MonoBehaviour
{
	string logsPath;
	string fileName;

	[SerializeField]
	int maxLogsNumber = 10;

	List<string> errorList = new List<string>();

	void Awake()
	{
		logsPath = DataStorage.Instance.GetLogsPath();
		Directory.CreateDirectory(logsPath);
		string dateTime = String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now);

		fileName = String.Format(@"log_{0}.txt", dateTime);
		fileName = Path.Combine(logsPath, fileName);

		Log("created", null, LogType.Log);

		RemoveOldLogFiles();
	}

	void RemoveOldLogFiles()
	{
		FileInfo[] logFiles = new DirectoryInfo(logsPath).GetFiles("log_*.txt");
		foreach (var logFile in ((from fi in logFiles orderby fi.LastWriteTime descending select fi).Skip(maxLogsNumber)))
			logFile.Delete();
	}

	void OnEnable()
	{
		Application.logMessageReceived += Log;
		Log("enabled", null, LogType.Log);
	}

	void OnDisable()
	{
		Application.logMessageReceived -= Log;
		Log("disabled", null, LogType.Log);
	}

	void OnDestroy()
	{
		Log("destroyed", null, LogType.Log);
//		string url = string.Format("mailto:{0}?subject={1}&body={2}", "harbuz@ukr.net", "LogFile", WWW.EscapeURL(File.ReadAllText(fileName)));
//		Application.OpenURL(url);
//		Debug.Log(url);
	}
		
	public void Log(string condition, string stackTrace, LogType type)
	{
		string dateTime = String.Format("{0:HH:mm:ss}", DateTime.Now);
		string logRecord;
		if (type == LogType.Log || type == LogType.Warning)
		{
			logRecord = String.Format("{0}: {1} {2}\n", dateTime, type, condition);
			File.AppendAllText(fileName, logRecord);
		}
		else
		{
			if(!errorList.Contains(condition))
			{
				logRecord = String.Format("{0}: {1} {2}\n{3}\n", dateTime, type, condition, stackTrace);
				errorList.Add(condition);
				File.AppendAllText(fileName, logRecord);
			}
		}

	}
}
