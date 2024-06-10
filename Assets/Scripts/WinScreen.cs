using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void GoToTitle()
    {
        SceneManager.LoadScene("StartGameMenu Build");
    }

    private void Start()
    {
        text.text = "Ending Money: " + GameManager.instance.money + "$";
    }
}
