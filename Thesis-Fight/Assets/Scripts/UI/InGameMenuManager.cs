﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour {
    public static InGameMenuManager instance;

    public string levelToLoad = "Main Menu";
    public GameObject menuCanvas;

    public bool canOpen;
    public bool isOpen;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            
        }

        instance = this;

        canOpen = true;
    }

    private void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.Escape) && !GameStartManager.HumanBuilder.GetComponent<Build>().isBuilding)
        {
            if (menuCanvas.activeSelf)
            {
                isOpen = false;
                menuCanvas.SetActive(false);
                if (!VictoryManager.instance.gameOver)
                {
                    Time.timeScale = 1.0f;
                }
            }
            else
            {
                isOpen = true;
                menuCanvas.SetActive(true);

                // Try for a proper pause
                Time.timeScale = 0.0f;
            }
        }
    }

    public void Resume()
    {
        isOpen = false;
        menuCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
