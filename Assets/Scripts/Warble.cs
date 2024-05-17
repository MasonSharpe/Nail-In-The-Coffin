using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warble : MonoBehaviour
{
    public Transform UIParent;
    public Vector3 startingPanelPos;

    private void Start()
    {
        startingPanelPos = UIParent.position;
    }


    private void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = -Camera.main.transform.position.z;
        pos -= new Vector3(Screen.width / 2, Screen.height / 2, 0);
        UIParent.position = startingPanelPos + (pos / 60);
    }
}
