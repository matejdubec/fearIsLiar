using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLanguageManager : MonoBehaviour
{
	private readonly Dictionary<string, string> lang = new Dictionary<string, string>();
	private SystemLanguage systemLanguage;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		LoadLanguage();
	}


	private void LoadLanguage()
	{
		systemLanguage = Application.systemLanguage;
		var file = Resources.Load<TextAsset>(systemLanguage.ToString());
		if (!file)
		{
			systemLanguage = SystemLanguage.English;
			file = Resources.Load<TextAsset>(systemLanguage.ToString());
		}

		foreach(var line in file.text.Split('\n'))
		{
			var prop = line.Split(':');
			lang[prop[0]] = prop[1];
		}
	}
}
