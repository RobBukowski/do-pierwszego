using UnityEngine;
using TMPro;

public class DayController : MonoBehaviour
{
    public TextMeshProUGUI logText;   // pole na opis tego, co siê sta³o
    public EventUI eventUI;
    public GameObject actionsPanel; // Panel na akcje - praca, dom, fucha

    private GameState gs;

    private void Start()
    {
        gs = GameState.Instance;
        Log("Dzieñ " + gs.day + ". Poranek w domu.");

        if (gs.day == 1)
        {
            StartIntroFactoryEvent();
        }
    }

    private void Log(string message)
    {
        if (logText != null)
        {
            logText.text = message;
        }
        else
        {
            Debug.Log(message);
        }
    }

    public void GoToWork()
    {
        if (gs.energy < 20)
        {
            Log("Jesteœ zbyt wyczerpany, ¿eby iœæ do pracy.");
            return;
        }

        gs.ChangeMoney(+20000);        // placeholder
        gs.ChangeEnergy(-20);
        gs.ChangeMentalHealth(-5);

        gs.NextDay();
        Log("Poszed³eœ do pracy w zak³adzie. Wróci³eœ zmêczony, ale z wyp³at¹. Dzieñ " + gs.day + ".");
    }

    public void TakeOddJob()
    {
        if (gs.energy < 30)
        {
            Log("Nie masz si³y na dodatkow¹ fuchê.");
            return;
        }

        gs.ChangeMoney(+35000);        // wiêcej kasy
        gs.ChangeEnergy(-30);
        gs.ChangeMentalHealth(-10);
        gs.ChangeRelationPartner(-5);  // mniej czasu dla rodziny

        gs.NextDay();
        Log("Wzi¹³eœ fuchê na boku. Wiêcej pieniêdzy, ale mniej si³ i czasu dla rodziny. Dzieñ " + gs.day + ".");
    }

    public void StayAtHome()
    {
        gs.ChangeEnergy(+15);
        gs.ChangeMentalHealth(+5);
        gs.ChangeRelationChild(+5);    // spêdzasz czas z dzieckiem
        gs.ChangeMoney(-5000);         // coœ trzeba zjeœæ itd.

        gs.NextDay();
        Log("Zosta³eœ w domu. Odpocz¹³eœ, ale pieni¹dze siê topniej¹. Dzieñ " + gs.day + ".");
    }

    private void StartIntroFactoryEvent()
    {
        // wy³¹cz zwyk³e przyciski akcji na czas eventu
        if(actionsPanel != null)
        {
            actionsPanel.SetActive(false);
        }

        string desc =
            "Jesieñ 1990. W zak³adzie mówi¹ o 'przekszta³ceniach' i prywatyzacji.\n" +
            "Kierownik wzywa Ciê do biura.\n\n" +
            "\"Mamy dwa wyjœcia: mo¿esz odejœæ z odpraw¹, albo zostaæ i liczyæ, ¿e siê utrzymasz.\n" +
            "Nikt nie wie, co bêdzie dalej.\"";

        eventUI.ShowEvent(
            desc,
            "Biorê odprawê",
            OnTakeSeverance,
            "Zostajê w zak³adzie",
            OnStayInFactory);
    }

    private void OnTakeSeverance()
    {
        if(!gs.tookSeverance)
        {
            gs.tookSeverance = true;
            gs.hasFactoryJob = false;

            gs.ChangeMoney(+200000); // placeholder - odprawa
            gs.ChangeMentalHealth(-5);

            Log("Podpisa³eœ odejœcie z zak³adu i dosta³eœ odprawê. " +
                "Masz trochê wiêcej pieniêdzy, ale nie masz ju¿ sta³ej pracy.");
        }

        if(actionsPanel != null)
        {
            actionsPanel.SetActive(true);
        }
    }

    private void OnStayInFactory()
    {
        if (!gs.tookSeverance)
        {
            gs.tookSeverance = false;
            gs.hasFactoryJob = true;

            gs.ChangeMentalHealth(+2);

            Log("Postanowi³eœ zostaæ w zak³adzie. Na razie masz etat, " +
            "ale wszyscy mówi¹, ¿e zwolnienia dopiero siê zaczynaj¹.");
        }

        if (actionsPanel != null)
        {
            actionsPanel.SetActive(true);
        }
    }
}
