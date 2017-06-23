using UnityEngine;

public class GameStartManager : MonoBehaviour {

    public GameObject builder;
    public GameObject bot;

    public static GameObject HumanBuilder { get; private set; }
    public static int HumanBuilderID { get; private set; }

    public static int playerCount = 6;
    public Transform eastSpawn;
    public Transform westSpawn;

    private GameObject[] builders;
    private Transform buildersParent;

    private void Awake()
    {
        builders = new GameObject[playerCount];
        buildersParent = GameObject.Find("Builders").transform;

        Debug.Log("Players: " + playerCount);
        HumanBuilderID = Random.Range(0, playerCount - 1);
        Debug.Log("Human builder ID: " + HumanBuilderID);
        
        InstantiateBuilders();

        HumanBuilder = builders[HumanBuilderID];
    }

    private void InstantiateBuilders()
    {
        GameObject castles = GameObject.Find("Castles");
        GameObject newBuilder;

        Vector3 offset = new Vector3(0.0f, 0.0f, 5.0f);
        for (int i = 0; i < playerCount; i++)
        {
            if (i == HumanBuilderID)
            {
                newBuilder = builder;
            }
            else
            {
                newBuilder = bot;
            }

            if ((i % 2) == 0)
            {
                newBuilder.transform.position = eastSpawn.position + (i / 2) * offset;
                newBuilder.GetComponent<BuilderController>().enemyCastle = castles.transform.Find("Castle West").Find("Attack Point").position;
            }
            else
            {
                newBuilder.transform.position = westSpawn.position + (i / 2) * offset;
                newBuilder.GetComponent<BuilderController>().enemyCastle = castles.transform.Find("Castle East").Find("Attack Point").position;
            }

            // Assign team in a different way
            newBuilder.GetComponent<BuilderController>().teamID = (Teams)(i % 2);
            newBuilder.GetComponent<BuilderController>().playerID = i;

            builders[i] = Instantiate(newBuilder, buildersParent);
        }
    }
}
