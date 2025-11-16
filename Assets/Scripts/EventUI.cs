using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI optionAText;
    public TextMeshProUGUI optionBText;
    public Button optionAButton;
    public Button optionBButton;

    public void ShowEvent(
        string description,
        string optionALabel,
        UnityAction onOptionA,
        string optionBLabel,
        UnityAction onOptionB)
    {
        panel.SetActive(true);
        descriptionText.text = description;

        optionAText.text = optionALabel;
        optionBText.text = optionBLabel;

        optionAButton.onClick.RemoveAllListeners();
        optionBButton.onClick.RemoveAllListeners();

        optionAButton.onClick.AddListener(onOptionA);
        optionBButton.onClick.AddListener(onOptionB);

        optionAButton.onClick.AddListener(Hide);
        optionBButton.onClick.AddListener(Hide);

    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
