using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CLanguageManager : MonoBehaviour
{
	private readonly Dictionary<string, string> lang = new Dictionary<string, string>();
	private SystemLanguage systemLanguage;

    public void Init()
    {
        LoadLanguage();
    }

	private void LoadLanguage()
	{
		systemLanguage = Application.systemLanguage;
		var file = Resources.Load<TextAsset>(systemLanguage.ToString());

		if(file == null)
		{
			systemLanguage = SystemLanguage.English;
			file = Resources.Load<TextAsset>(systemLanguage.ToString());
		}

		foreach (var line in file.text.Split('\n'))
		{
			var prop = line.Split(':');
			lang[prop[0]] = prop[1];
		}
	}

	public string GetText(string key)
	{
        
		if (!lang.ContainsKey(key))
		{
            return "Key doesn't exist";
        }

		return lang[key];
	}
}