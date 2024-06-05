using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {

    }
    public void Startgame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void QuitGame()
    {
        Application.Quit();
        
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(GameManager.instance.levelNames[index]);
    }
}
