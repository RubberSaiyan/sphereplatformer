using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class pause : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseUI;
    public GameObject resume, menu, quit;

    void Start()
    {
        //pauseUI = GameObject.FindGameObjectWithTag("Pause");
        //resume = GameObject.FindGameObjectWithTag("Resume");
        //menu = GameObject.FindGameObjectWithTag("Menu");
        //quit = GameObject.FindGameObjectWithTag("Quit");
        DontDestroyOnLoad(pauseUI);
        DontDestroyOnLoad(resume);
        DontDestroyOnLoad(menu);
        DontDestroyOnLoad(quit);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetButtonDown("Submit"))
        {

        }

    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resume);
    }

    public void Menu()
    {
        Resume();
        SceneManager.LoadScene("level select");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
