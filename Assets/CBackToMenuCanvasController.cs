using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CBackToMenuCanvasController : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private CLocalizationIndentificator mainText;
    [SerializeField] private Button button;
    [SerializeField] private CLocalizationIndentificator buttonText;

    public delegate void ButtonAction();

    public void Activate(bool state)
    {
        this.gameObject.SetActive(state);
    }


    public void SetMainText(string identificator)
    {
        mainText.SetText(identificator);
    }

    public void SetButtonText(string identificator)
    {
        buttonText.SetText(identificator);
    }

    public void SetButtonAction(ButtonAction buttonAction)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { buttonAction(); });
    }
}
