using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [Header("Resources")]
    public int money = 50000;         // stare z³ote na start
    public int energy = 100;          // 0–100
    public int relationPartner = 70;  // 0–100
    public int relationChild = 70;    // 0–100
    public int mentalHealth = 70;     // 0–100

    [Header("Time")]
    public int day = 1;

    [Header("Job")]
    public bool hasFactoryJob = true; // Zaczynamy pracujac w zakladzie
    public bool tookSeverance = false; // odprawa

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeMoney(int amount)
    {
        money += amount;
    }

    public void ChangeEnergy(int amount)
    {
        energy = Mathf.Clamp(energy + amount, 0, 100);
    }

    public void ChangeRelationPartner(int amount)
    {
        relationPartner = Mathf.Clamp(relationPartner + amount, 0, 100);
    }

    public void ChangeRelationChild(int amount)
    {
        relationChild = Mathf.Clamp(relationChild + amount, 0, 100);
    }

    public void ChangeMentalHealth(int amount)
    {
        mentalHealth = Mathf.Clamp(mentalHealth + amount, 0, 100);
    }

    public void NextDay()
    {
        day++;
        // tu póŸniej dorzucisz logikê nowego dnia
    }
}
