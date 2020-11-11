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
    //[SerializeField] private Image previewSprite;

    private string completionString = "Completed in: ";

    public void Setup(CConfigLevel cLevel, CSceneLoader loader)
	{
        levelNameText.text = cLevel.Id.ToString();
        descriptionText.text = cLevel.Description;
        PhobiaText.text = cLevel.PhobiaId.ToString();
        CompletionTimeText.text = cLevel.TimeOfCompletion > 0 ? completionString + cLevel.TimeOfCompletion.ToString() : completionString + "N/A";
        //previewSprite.sprite = cLevel.Icon;

        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(() => loader.LoadScene(cLevel.Id.ToString()));
    }
}
