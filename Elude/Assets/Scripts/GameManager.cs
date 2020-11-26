using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



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
    public GameObject firstSelected;    

    public int collectibleCounter;
    public int collectibleTarget;

    public GameObject door;
    public GameObject doorAudioSource;

    private void Start()
    {
        //internalTimer = timer;
        isGamePaused = false;
        collectibleCounter = 0;
        Cursor.lockState = CursorLockMode.Locked;
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

        

        if (Input.GetKeyDown("escape") || Input.GetKeyDown(KeyCode.Joystick1Button7))     // Toggles the pause menu
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelected);
            Pause();
            
        }


        if (collectibleCounter == collectibleTarget)
        {

            door.GetComponent<Door_Open_Anim_Script>().cogsmet = true;

            doorAudioSource.SetActive(true);

        }

        if (isGamePaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
        Pause();
    }
    public void ExitGame() // Closes the game
    {
        Application.Quit();
    }

    public void Pause()
    {
        if (isGamePaused == false)      // Opens the menu
        {
            isGamePaused = true;
        }
        else if (isGamePaused == true)  // Closes the menu
        {
            isGamePaused = false;
        }

        if (isGamePaused == true)
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            //Cursor.lockState = CursorLockMode.Confined;
        }
        else if (isGamePaused == false)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
