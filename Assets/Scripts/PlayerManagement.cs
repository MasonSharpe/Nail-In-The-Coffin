using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    public static PlayerManagement instance;
    public PlayerHammer hammer;
    public PlayerMovement movement;

    public float health;
    public float maxHealth;

    private void Awake()
    {
        instance = this;
    }
    


}
