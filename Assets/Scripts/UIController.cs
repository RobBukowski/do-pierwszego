using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Text fields")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI partnerText;
    public TextMeshProUGUI childText;
    public TextMeshProUGUI mentalText;
    public TextMeshProUGUI dayText;

    private GameState gs;

    private void Start()
    {
        gs = GameState.Instance;
    }

    private void Update()
    {
        if (gs == null) return;

        moneyText.text = $"Pieni¹dze: {gs.money} z³";
        energyText.text = $"Energia: {gs.energy}";
        partnerText.text = $"Partner: {gs.relationPartner}";
        childText.text = $"Dziecko: {gs.relationChild}";
        mentalText.text = $"Psychika: {gs.mentalHealth}";
        dayText.text = $"Dzieñ: {gs.day}";
    }
}
