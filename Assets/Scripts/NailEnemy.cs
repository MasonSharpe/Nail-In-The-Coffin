using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailEnemy : MonoBehaviour
{
    public PlayerManagement player;
    public Collider2D hitbox;
    public CameraShake shaker;
    public Rigidbody2D rb;
    public SpriteRenderer dashVisual;
    public int state;
    public float timeSpinning;

    public int dashState = 0;
    public float dashTimer;
    public Vector2 dashDirection;

    public float speed;
    public float dashSpeed;
    public float dashTime;
    public float dashWindupTime;
    public float dashEndlag;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManagement>();
        shaker = Camera.main.GetComponent<CameraShake>();
    }

    private void Update()
    {
        if (state == 0)
        {
            dashTimer -= Time.deltaTime;

            switch (dashState)
            {
                case 0:
                    rb.velocity = (speed * (player.transform.position - transform.position).normalized);

                    if ((player.transform.position - transform.position).magnitude < 2)
                    {
                        dashState = 1;
                        dashTimer = dashWindupTime;
                        dashDirection = (player.transform.position - transform.position).normalized;
                        dashVisual.enabled = true;
                        float angle = Mathf.Atan2(dashDirection.y, dashDirection.x) * Mathf.Rad2Deg;
                        dashVisual.transform.parent.localRotation = Quaternion.Euler(0, 0, angle);
                    }
                    break;

                case 1:
                    rb.velocity = Vector2.zero;
                    if (dashTimer < 0)
                    {
                        dashState = 2;
                        dashTimer = dashTime;
                    }
                    break;

                case 2:
                    float dashFalloff = dashTimer / dashTime;
                    rb.velocity = dashSpeed * dashFalloff * dashDirection.normalized;
                    if (dashTimer < 0)
                    {
                        dashState = 3;
                        dashTimer = dashEndlag;
                    }
                    dashVisual.enabled = false;
                    break;
                case 3:
                    rb.velocity = Vector2.zero;
                    if (dashTimer < 0)
                    {
                        dashState = 0;
                    }
                    break;
            }
        }
        else if (state == 1)
        {
            transform.position = player.hammer.hammerEnd.transform.position;
            timeSpinning += Time.deltaTime;
            if (timeSpinning > 1 && LevelManager.instance.tutorial && Tutorial.instance.phase == 5) Tutorial.instance.NextPhase();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && player.hammer.energy >= 1)
        {
            if ((player.hammer.hammerEnd.transform.position - transform.position).magnitude < 1 && state == 0 && player.hammer.state == 0)
            {
                shaker.Shake(0.07f, 0.05f);
                state = 1;
                player.hammer.timeStopTimer = 0.1f;
                player.hammer.state = 1;
                Time.timeScale = 0;
                rb.velocity = Vector2.zero;
                dashVisual.enabled = false;
                timeSpinning = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && state == 1)
        {
            state = 2;
            rb.velocity = Vector2.zero;

            LevelManager.instance.PlaceNail(this);

            hitbox.enabled = false;

        }
        if (Input.GetKey(KeyCode.Mouse0) && state == 2 && (player.hammer.hammerEnd.transform.position - transform.position).magnitude < 0.4f)
        {
            if (player.hammer.canSwitch)
            {
                player.hammer.rotationDir *= -1;
                player.hammer.canSwitch = false;
            }
        }
        else {
            player.hammer.canSwitch = true;
        }
        if ((player.transform.position - transform.position).magnitude > 20)
        {
            LevelManager.instance.enemiesSpawnedIn--;
            Destroy(gameObject);
        }
    }
}
