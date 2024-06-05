using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starting : MonoBehaviour
{
    float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1) SceneManager.LoadScene("StartGameMenu Build");
    }
}
