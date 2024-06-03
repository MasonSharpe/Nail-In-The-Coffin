using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool isConnected = true;
    public Rigidbody2D rb;
    public TrailRenderer tr;
    public PlayerHammer hammer;
    public Transform body;
    public float movementSpeed = 3;
    float invincTimer = 0;



    void Start()
    {
        movementSpeed = GameManager.instance.upgrades[5].currentPurchases * 2 + 3.5f;

    }

    // Update is called once per frame


    private void Update()
    {
        if (LevelManager.instance.winScreen.enabled) return;

        Vector3 pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = 0;
        invincTimer -= Time.deltaTime;

        Vector2 velocity = (Vector2)new Vector3(pos.x, pos.y, 0) - (Vector2)transform.position;
        Vector2 normalized = velocity.normalized;
        if (velocity.magnitude > movementSpeed) velocity = normalized * movementSpeed;
        velocity *= hammer.stuck;
        if (velocity.magnitude > 0.5f)
        {
            rb.velocity = velocity;
            float angle = Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg;
            body.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }





}
