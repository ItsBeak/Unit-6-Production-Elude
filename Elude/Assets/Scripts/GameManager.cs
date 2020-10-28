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
                Time.timeScale = 0.0f;  // Freezes time

                pauseMenu.SetActive(true);
            }
            else if (isGamePaused == true)  // Closes the menu
            {
                isGamePaused = false;
                Time.timeScale = 1.0f;  // Unfreezes Time

                pauseMenu.SetActive(false);

            }
        }

        #endregion

        // FOR BUG TESTING -- TEMPORARY
        if (Input.GetKeyDown(KeyCode.O) == true)
        {
            GameOver();
        }
        else if (Input.GetKeyDown(KeyCode.P) == true)
        {
            WinGame();
        }

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
}
