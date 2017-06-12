using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour {

    // Consider assigning a gold manager to each builder
    public static GoldManager instance;

    public int startingIncome = 5;
    public int startingGold = 250;
    public float incomeDistributionPeriod = 10;
    public float incomePercentage = 0.02f;
    public float incomeShowDuration;

    public Text currentGold;
    public Text incomeText;

    private int[] income;
    private int[] gold;

    // Request queue

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                // Would this statement destroy the whole object and not just this script
                Destroy(gameObject);
            }
            
        }

        instance = this;
    }

    private void Start()
    {
        int playerCount = GameStartManager.playerCount;
        income = new int[playerCount];
        gold = new int[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            income[0] = startingIncome;
            gold[i] = startingGold;
        }

        currentGold.text = startingGold.ToString();
        incomeText.text = "+" + startingIncome + "g";
        incomeText.gameObject.SetActive(false);
        StartIncomeDistribution();
    }

    public void AddIncome(int playerID, int value)
    {
        income[playerID] += (int)(value * incomePercentage);
        
        if (playerID == GameStartManager.HumanBuilderID)
        {
            incomeText.text = "+" + income[playerID] + "g";
        }
    }

    public void AddGold(int playerID, int value)
    {
        gold[playerID] += value;
        currentGold.text = gold[GameStartManager.HumanBuilderID].ToString();
    }

    public bool HasGold(int playerID, int cost)
    {
        if (gold[playerID] >= cost)
        {
            return true;
        }

        return false;
    }

    public void Pay(int playerID, int cost)
    {
        if (gold[playerID] >= cost)
        {
            gold[playerID] -= cost;
            AddIncome(playerID, cost);
            currentGold.text = gold[GameStartManager.HumanBuilderID].ToString();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    private void StartIncomeDistribution()
    {
        // Create own invoke coroutine
        InvokeRepeating("DistributeIncome", incomeDistributionPeriod, incomeDistributionPeriod);
    }

    private void DistributeIncome()
    {
        for(int i = 0; i < income.Length; i++)
        {
            gold[i] += income[i];
        }

        StartCoroutine(ShowIncome());
        currentGold.text = gold[GameStartManager.HumanBuilderID].ToString();
    }

    public IEnumerator ShowIncome()
    {
        incomeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(incomeShowDuration);
        incomeText.gameObject.SetActive(false);
    }
}
