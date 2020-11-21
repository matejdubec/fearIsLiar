using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CMainMenuLevelButton : MonoBehaviour
{

    [SerializeField] private Text levelNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text PhobiaText;
    [SerializeField] private Text CompletionTimeText;
    [SerializeField] private Image previewSprite;

    private string completionString = null;
	private void Awake()
	{
        completionString = CLanguageManager.Instance.GetText("DefaultText.CompletedIn");
    }

	public void Setup(CConfigLevel cLevel, CSceneLoader loader)
	{
        levelNameText.text = CLanguageManager.Instance.GetText($"Level.{cLevel.Id}");
        descriptionText.text = CLanguageManager.Instance.GetText(cLevel.Description);
        PhobiaText.text = CLanguageManager.Instance.GetText($"Phobia.{cLevel.PhobiaId}");
        CompletionTimeText.text = cLevel.TimeOfCompletion > 0 ? $"{completionString}: {cLevel.TimeOfCompletion}" : $"{completionString}: N/A";
        previewSprite.sprite = cLevel.Icon;

        Button button = this.GetComponent<Button>();
        string captured = cLevel.Id.ToString();
        button.onClick.AddListener(() => { loader.LoadScene(captured); });
    }
}
