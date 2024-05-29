using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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
            if (collision.transform.parent.GetComponent<NailEnemy>().dashState == 2)
            {
                if (!(LevelManager.instance.tutorial && health > 3)) health--;
            }
        }
    }
}
