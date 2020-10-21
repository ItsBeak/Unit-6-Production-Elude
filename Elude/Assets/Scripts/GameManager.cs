using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{

    public bool toggleTimer = false;

    //public float timer;
    //private float internalTimer;

    public Player player;

    private bool isGamePaused;
    public GameObject pauseMenu;

    private SceneSwitcher sw;

    public int collectibleCounter;
    public int collectibleTarget;

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

        if (Input.GetKeyDown("escape"))
        {
            if (isGamePaused == false)
            {
                isGamePaused = true;
                Time.timeScale = 0.0f;

                pauseMenu.SetActive(true);
            }
            else if (isGamePaused == true)
            {
                isGamePaused = false;
                Time.timeScale = 1.0f;

                pauseMenu.SetActive(false);

            }
        }

        #endregion

        // FOR BUG TESTING -- TEMPORARY
        if (Input.GetKeyDown(KeyCode.O) == true)
        {
            GameOver();
        }

        // FOR BUG TESTING -- TEMPORARY
        if (Input.GetKeyDown(KeyCode.P) == true)
        {
            WinGame();
        }

        if (collectibleCounter == collectibleTarget)
        {
            // open door
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
