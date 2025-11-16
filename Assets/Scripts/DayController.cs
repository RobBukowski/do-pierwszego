using UnityEngine;
using TMPro;

public class DayController : MonoBehaviour
{
    public TextMeshProUGUI logText;   // pole na opis tego, co siê sta³o
    public EventUI eventUI;
    public GameObject actionsPanel; // Panel na akcje - praca, dom, fucha

    private GameState gs;
    private bool notebooksEventDone = false;

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

    private void CheckEventsAfterAction()
    {
        // Event zeszytów: pierwszy raz, gdy zaczyna siê dzieñ 2
        if (!notebooksEventDone && gs.day == 2)
        {
            StartNotebooksEvent();
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

        CheckEventsAfterAction();
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

        CheckEventsAfterAction();
    }

    public void StayAtHome()
    {
        gs.ChangeEnergy(+15);
        gs.ChangeMentalHealth(+5);
        gs.ChangeRelationChild(+5);    // spêdzasz czas z dzieckiem
        gs.ChangeMoney(-5000);         // coœ trzeba zjeœæ itd.

        gs.NextDay();
        Log("Zosta³eœ w domu. Odpocz¹³eœ, ale pieni¹dze siê topniej¹. Dzieñ " + gs.day + ".");

        CheckEventsAfterAction();
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

    private void StartNotebooksEvent()
    {
        notebooksEventDone = true;

        // wy³¹cz przyciski akcji na czas eventu
        if (actionsPanel != null)
        {
            actionsPanel.SetActive(false);
        }

        string desc =
            "Wieczorem dziecko siada do lekcji.\n" +
            "\"Pani mówi³a, ¿e musimy mieæ nowe zeszyty, bo stare ju¿ ca³e zapisane...\" \n\n" +
            "Wiesz, ¿e za kilka dni trzeba zap³aciæ czynsz. Ka¿dy wydatek zaczyna boleæ.";

        eventUI.ShowEvent(
            desc,
            "Kupujesz zeszyty mimo wszystko",
            OnBuyNotebooks,
            "Mówisz, ¿e nie staæ was teraz",
            OnRefuseNotebooks
        );
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

    private void OnBuyNotebooks()
    {
        gs.ChangeMoney(-1000);            // symboliczny koszt
        gs.ChangeRelationChild(+5);
        gs.ChangeMentalHealth(+1);

        Log("Kupujesz zeszyty. To drobiazg, ale wiesz, ¿e dziecko nie bêdzie siê czu³o gorsze od innych.");

        if (actionsPanel != null)
        {
            actionsPanel.SetActive(true);
        }
    }

    private void OnRefuseNotebooks()
    {
        gs.ChangeRelationChild(-5);
        gs.ChangeMentalHealth(-3);

        Log("Mówisz, ¿e teraz nie staæ was na nowe zeszyty. Dziecko milknie. " +
            "Wiesz, ¿e zapamiêta takich momentów wiêcej.");

        if (actionsPanel != null)
        {
            actionsPanel.SetActive(true);
        }
    }
}
