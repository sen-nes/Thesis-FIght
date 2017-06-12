using UnityEngine;

public class GameStartManager : MonoBehaviour {

    public GameObject builder;

    public static GameObject HumanBuilder { get; private set; }
    public static int HumanBuilderID { get; private set; }

    public static int playerCount = 2;
    public Transform eastSpawn;
    public Transform westSpawn;

    private GameObject[] builders;

    private void Awake()
    {
        builders = new GameObject[playerCount];

        Debug.Log("Players: " + playerCount);
        HumanBuilderID = Random.Range(0, playerCount - 1);
        Debug.Log("Human builder ID: " + HumanBuilderID);
        
        InstantiateBuilders();

        HumanBuilder = builders[HumanBuilderID];
    }

    private void InstantiateBuilders()
    {
        GameObject castles = GameObject.Find("Castles");
        for (int i = 0; i < playerCount; i++)
        {
            // Check whether it's the human player

            if ((i % 2) == 0)
            {
                builder.transform.position = eastSpawn.position;
                builder.GetComponent<BuilderController>().enemyCastle = castles.transform.Find("Castle West").Find("Attack Point").position;
            }
            else
            {
                builder.transform.position = westSpawn.position;
                builder.GetComponent<BuilderController>().enemyCastle = castles.transform.Find("Castle East").Find("Attack Point").position;
            }

            // Assign team in a different way
            builder.GetComponent<BuilderController>().teamID = (Team)(i % 2);
            builder.GetComponent<BuilderController>().playerID = i;

            builders[i] = Instantiate(builder);
        }
    }
}
