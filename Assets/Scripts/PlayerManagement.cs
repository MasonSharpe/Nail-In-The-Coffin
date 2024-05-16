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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
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
