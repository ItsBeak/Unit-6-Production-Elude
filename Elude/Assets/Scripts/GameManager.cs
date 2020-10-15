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

    private void Start()
    {
        //internalTimer = timer;
        isGamePaused = false;
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

        if (Input.GetKeyDown(KeyCode.O) == true)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.P) == true)
        {
            WinGame();
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
