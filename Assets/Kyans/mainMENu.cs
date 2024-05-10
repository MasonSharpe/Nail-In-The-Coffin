using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    Canvas cv;

    void Awake()
    {
        cv = GetComponent<Canvas>();
    }

    void Start()
    {
        cv.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0)
                Pause();
            else
                Resume();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        cv.enabled = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        cv.enabled = false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartGameScene");
    }

    public void QuitGame()
    {
        //print("Quit");
        Application.Quit();
    }
}
