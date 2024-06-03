using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerManagement : MonoBehaviour
{
    public static PlayerManagement instance;
    public PlayerHammer hammer;
    public PlayerMovement movement;
    public LayerMask boardMask;

    public Image healthCircle;
    public Image energyCircle;

    public float health;
    public float maxHealth;
    public float healthRegenAmount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        maxHealth = GameManager.instance.upgrades[1].currentPurchases * 2.5f + 2.5f;
        healthRegenAmount = GameManager.instance.upgrades[0].currentPurchases * 0.5f + 1;

        health = maxHealth;
    }
    private void Update()
    {
        healthCircle.fillAmount = health / maxHealth;
        energyCircle.fillAmount = hammer.energy / hammer.energyMax;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DamageHitbox")
        {
            NailEnemy enemy = collision.transform.parent.GetComponent<NailEnemy>();
            if (enemy.dashState == 2 && enemy.state != 2)
            {
                if (!(LevelManager.instance.tutorial && health > 3))
                {
                    health--;
                    if (health <= 0)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
            }
        }
    }
}
