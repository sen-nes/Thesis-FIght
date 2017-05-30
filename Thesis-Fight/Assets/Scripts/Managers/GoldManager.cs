using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour {

    public static GoldManager instance;
    public float incomePeriod;
    public Text currentGold;

    private int[] income;
    private int[] gold;

    // Request queue

    private void Awake()
    {
        int numberOfPlayers = GameObject.FindGameObjectsWithTag("Builder").Length;
        income = new int[numberOfPlayers];
        gold = new int[numberOfPlayers];
        
        for (int i = 0; i < numberOfPlayers; i++)
        {
            income[0] = 15;
            gold[i] = 1000;
        }
    }

    private void Start()
    {
        instance = this;
        StartIncomeDistribution();
    }

    private void Update()
    {
        // Place in builder probably
        currentGold.text = gold[0].ToString();
    }

    public void AddIncome(int playerID, int incomeValue)
    {
        income[playerID] += incomeValue;
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
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    private void StartIncomeDistribution()
    {
        InvokeRepeating("DistributeIncome", incomePeriod, incomePeriod);
    }

    private void DistributeIncome()
    {
        for(int i = 0; i < income.Length; i++)
        {
            gold[i] += income[i];
        }
    }
}
