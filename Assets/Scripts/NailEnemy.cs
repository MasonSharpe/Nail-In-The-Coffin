using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailEnemy : MonoBehaviour
{
    public PlayerHammer player;
    public Collider2D hitbox;
    public int state = 0;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHammer>();
    }

    private void Update()
    {
        if (state == 0)
        {
            transform.Translate((player.transform.position - transform.position).normalized * 3 * Time.deltaTime);
        }
        else if (state == 1)
        {
            transform.position = player.hammerEnd.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && player.energy >= 1)
        {
            if ((player.hammerEnd.transform.position - transform.position).magnitude < 1 && state == 0)
            {
                state = 1;
                hitbox.enabled = false;
                player.timeStopTimer = 0.1f;
                player.state = 1;
                Time.timeScale = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && state == 1)
        {
            state = 2;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            state = 0;
        }
        if (Input.GetKey(KeyCode.Mouse0) && state == 2 && (player.hammerEnd.transform.position - transform.position).magnitude < 0.3f)
        {
            if (player.canSwitch)
            {
                player.rotationDir *= -1;
                player.canSwitch = false;
            }
        }
        else {
            player.canSwitch = true;
        }
    }
}
