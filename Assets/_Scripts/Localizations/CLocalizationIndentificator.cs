using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CLocalizationIndentificator : MonoBehaviour
{
    private Text text;
    [SerializeField] private string identificator = "DefaultText.NoText";

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        text.text = CGameManager.Instance.LanguageManager.GetText(identificator);
    }

    public void SetText(string newText)
	{
        identificator = newText;
        text = GetComponent<Text>();
        text.text =  CGameManager.Instance.LanguageManager.GetText(identificator);
    }
}
