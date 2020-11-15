using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CLocalizationIndentificator : MonoBehaviour
{
    private Text text;
    [SerializeField] private string identificator = "DefaultText.NoText";

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = CLanguageManager.Instance.GetText(identificator);
    }
}
