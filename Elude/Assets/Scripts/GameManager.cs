using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{

    public bool toggleTimer = false;

    #region Timer (Scrapped)

    //public float timer;
    //private float internalTimer;

    #endregion

    public Player player;

    private bool isGamePaused;
    public GameObject pauseMenu;

    public int collectibleCounter;
    public int collectibleTarget;

    public GameObject door;

    private void Start()
    {
        //internalTimer = timer;
        isGamePaused = false;
        collectibleCounter = 0;
    }

    void Update()
    {
        #region Timer (Scrapped)

        //if (toggleTimer)
        //{
        //    internalTimer -= Time.deltaTime;
        //}
        //if (internalTimer <= 0f)
        //{
        //    toggleTimer = false;
        //    internalTimer = timer;
        //    GameOver();
        //}

        #endregion

        #region PauseMenu

        if (Input.GetKeyDown("escape"))     // Toggles the pause menu
        {
            if (isGamePaused == false)      // Opens the menu
            {
                isGamePaused = true;
            }
            else if (isGamePaused == true)  // Closes the menu
            {
                isGamePaused = false;
            }
        }

        if (isGamePaused == true)
        {
            Time.timeScale = 0.0f;  
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (isGamePaused == false)  
        {
            Time.timeScale = 1.0f; 
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        #endregion

        if (collectibleCounter == collectibleTarget)
        {
            door.SetActive(false);
        }
    }

    public void GameOver()
    {
        GetComponent<SceneSwitcher>().sceneName = "LoseMenu";
        GetComponent<SceneSwitcher>().ToggleSceneChange();
    }
    public void WinGame()
    {
        GetComponent<SceneSwitcher>().sceneName = "WinMenu";
        GetComponent<SceneSwitcher>().ToggleSceneChange();
    }
    public void Resume()
    {
        isGamePaused = false;
    }
    public void ExitGame() // Closes the game
    {
        Application.Quit();
    }
}
