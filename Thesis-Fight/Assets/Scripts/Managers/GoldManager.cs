using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour {

    // Consider assigning a gold manager to each builder
    public static GoldManager instance;

    public float incomePeriod = 15;
    public float incomePercentage = 0.02f;

    public Text currentGold;

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
            income[0] = 5;
            gold[i] = 1000;
        }

        StartIncomeDistribution();
    }

    public void AddIncome(int playerID, int value)
    {
        income[playerID] += (int)(value * incomePercentage);
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
        InvokeRepeating("DistributeIncome", incomePeriod, incomePeriod);
    }

    private void DistributeIncome()
    {
        for(int i = 0; i < income.Length; i++)
        {
            gold[i] += income[i];
        }
        
        // Updat e
        currentGold.text = gold[GameStartManager.HumanBuilderID].ToString();
    }
}
