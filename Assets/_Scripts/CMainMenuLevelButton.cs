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

    public void Init(CConfigLevel cLevel)
    {
        completionString =  CGameManager.Instance.LanguageManager.GetText("DefaultText.CompletedIn");

        levelNameText.text = CGameManager.Instance.LanguageManager.GetText($"Level.{cLevel.SceneId}");
        descriptionText.text = CGameManager.Instance.LanguageManager.GetText(cLevel.Description);
        PhobiaText.text = CGameManager.Instance.LanguageManager.GetText($"Phobia.{cLevel.PhobiaId}");
        CompletionTimeText.text = cLevel.TimeOfCompletion > 0 ? $"{completionString}: {cLevel.TimeOfCompletion} s" : $"{completionString}: N/A";
        previewSprite.sprite = cLevel.Icon;

        Button button = this.GetComponent<Button>();
        CConfigLevel captured = cLevel;
        button.onClick.AddListener(() => { CGameManager.Instance.LoadLevel(captured); });
    }

    private void OnDestroy()
    {
        Button button = this.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }
}
