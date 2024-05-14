using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool isConnected = true;
    public Rigidbody2D rb;
    public Rigidbody2D cameraRb;
    public TrailRenderer tr;
    public PlayerHammer hammer;
    public Transform body;
    public GameObject cameraMover;
    public float health = 20;
    float invincTimer = 0;


    private void Awake()
    {

    }

    void Start()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    // Update is called once per frame


    private void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = 0;
        invincTimer -= Time.deltaTime;

        Vector2 velocity = ((Vector2)new Vector3(pos.x, pos.y, 0) - (Vector2)transform.position) * 1.3f;
        Vector2 normalized = velocity.normalized;
        if (velocity.magnitude > 5) velocity = normalized * 5;

        if (velocity.magnitude > 0.15f)
        {
            rb.velocity = 3f * hammer.stuck * velocity;

            float angle = Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg;
            body.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        float xDist = Mathf.Clamp01(Mathf.Abs(Input.mousePosition.x - Screen.width / 2f) / (Screen.width / 2));
        float yDist = Mathf.Clamp01(Mathf.Abs(Input.mousePosition.y - Screen.height / 2f) / (Screen.height / 2f));
        float camX = xDist > 0.5f ? normalized.x * (xDist * 10 - 5f) : 0;
        float camY = yDist > 0.5f ? normalized.y * (yDist * 10 - 5f) : 0;
        cameraRb.velocity = new(camX, camY);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Damaging")
        {
            takeDamage(4);
        }
    }

    void takeDamage(float amount)
    {
        if (invincTimer < 0)
        {
            health -= amount;
            invincTimer = 0.25f;
            if (health <= 0)
            {

            }
        }
    }


}
