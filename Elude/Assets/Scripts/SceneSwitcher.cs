using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public float fadeTimer;

    public Image fadePanel;

    private bool isFading;

    public string sceneName;



    void Start()
    {
        isFading = false;
        fadeTimer = 2;
        fadePanel.color = Color.black;
        fadePanel.CrossFadeAlpha(0, 1f, false);
        fadePanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isFading)
        {
            fadeTimer -= Time.deltaTime;
            fadePanel.CrossFadeAlpha(1, 0.5f, false);

            if (fadeTimer <= 0)
            {
                SceneManager.LoadScene(sceneName);
            }

        }



    }

    public void ButtonPressed()
    {
        isFading = true;
        fadePanel.gameObject.SetActive(true);
        fadePanel.CrossFadeAlpha(0, 0f, false);

    }
}
