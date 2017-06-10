using UnityEngine;

public class GameStartManager : MonoBehaviour {

    public static int HumanPlayer { get; set; }
    public static int playerCount = 2;

    GameObject[] builders;
    GameObject[] castles;

    private void Awake()
    {
        builders = new GameObject[2];
        castles = GameObject.FindGameObjectsWithTag("Castle");
        Debug.Log("Found " + castles.Length + " castles.");

        HumanPlayer = Random.Range(0, playerCount - 1);
        Debug.Log("Human player ID: " + HumanPlayer);
    }

    private void Start()
    {
        InstantiateBuilders();

        if (castles != null)
        {
            SetupCastles();
        }
    }

    private void InstantiateBuilders()
    {
        for (int i = 0; i < playerCount; i++)
        {
            // Set player teams

            // Spawn at appropriate points
        }
    }

    private void SetupCastles()
    {
        if (castles.Length == 2)
        {
            //for (int i = 0; i < castles.Length; i++)
            //{
            //    if (castles[i].name.Contains("East"))
            //    {
            //        castles[i].GetComponent<CastleDetails>().TeamID = (int)Team.TEAM_EAST;
            //    }
            //    else
            //    {
            //        castles[i].GetComponent<CastleDetails>().TeamID = (int)Team.TEAM_WEST;
            //    }
            //}
        }
        else
        {
            Debug.Log("Can't have more than two castles.");
        }
    }
}
