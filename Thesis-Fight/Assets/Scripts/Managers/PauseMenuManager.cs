using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {
    public string levelToLoad = "Main Menu";
    public GameObject menuCanvas;

    private void Update()
    {
        if (!StateManager.instance.IsBuilding && Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuCanvas.activeSelf)
            {
                StateManager.instance.IsMenuOpen = false;
                menuCanvas.SetActive(false);
            }
            else
            {
                StateManager.instance.IsMenuOpen = true;
                menuCanvas.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming...");
        GameObject.Find("Menu Canvas").SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        SceneManager.LoadScene(levelToLoad);
    }

    public void Exit()
    {
        Debug.Log("Exitting...");
        Application.Quit();
    }
}
