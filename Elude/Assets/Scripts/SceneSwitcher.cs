using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public float fadeTimer;
    public Image fadePanel;

    private bool toggleChange;

    public string sceneName;

    void Start()
    {
        toggleChange = false;
        fadeTimer = 2;                          // Sets the fade timer
        fadePanel.color = Color.black;          // Sets the panel colour to black, just in case for some reason it is changed in editor
        fadePanel.CrossFadeAlpha(0, 1f, false); // Fades out the panel, fading into the level
        fadePanel.gameObject.SetActive(true);   // Makes sure the panel is active
    }

    void Update()
    {
        if (toggleChange)   // Runs if the scene transition is toggled
        {
            fadeTimer -= Time.deltaTime;
            fadePanel.CrossFadeAlpha(1, 0.5f, false);   // Fades in the panel in preparation for the scene change

            if (fadeTimer <= 0)
            {
                SceneManager.LoadScene(sceneName);  // Changes the scene
            }
        }
    }

    public void ToggleSceneChange()
    {
        toggleChange = true;    
        fadePanel.gameObject.SetActive(true);
        fadePanel.CrossFadeAlpha(0, 0f, false);

    }

    public void ExitGame() // Closes the game
    {
        Application.Quit();
    }
}
