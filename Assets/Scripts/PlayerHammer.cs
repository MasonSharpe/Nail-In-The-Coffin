using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammer : MonoBehaviour
{
    public CameraShake shaker;

    public Transform hammer;
    public Transform hammerEnd;
    public Transform rotationPoint;
    public Transform lengthObject;
    public TrailRenderer trail;
    public CircleCollider2D trailCollider;
    public SpriteRenderer shadow;
    public float rotationDir;
    public float timeStopTimer = -1;
    public float shadowTimer = 0;
    public float state = 0;
    public float energy;
    public float hammerVisibility;
    public bool slamming;
    public bool canSwitch = true;
    public float stuck;
    public float stuckSlowdown;


    public float length;
    public float energyRegen;
    public float energyMax;
    public float rotationSpeed;
    public float shadowRecoveryTime;

    void Start()
    {
        length = GameManager.instance.upgrades[6].currentPurchases + 1;
        energyRegen = GameManager.instance.upgrades[2].currentPurchases * 0.5f + 1;
        energyMax = GameManager.instance.upgrades[3].currentPurchases * 1.5f + 3.5f;
        rotationSpeed = GameManager.instance.upgrades[4].currentPurchases * 0.5f + 1.5f;
        shadowRecoveryTime = GameManager.instance.upgrades[7].currentPurchases * 0.5f + 1;

        hammerEnd.GetComponent<TrailRenderer>().emitting = false;
        rotationDir = 1;
        lengthObject.localPosition = Vector3.up * length;
        stuck = 1;
        energy = energyMax;
        shadowTimer = 2;

    }
    

    private void Update()
    {
        if (LevelManager.instance.winScreen.enabled) return;
        if (!slamming)
        {
            energy = Mathf.Clamp(energy + Time.deltaTime * energyRegen, 0, energyMax);
            shadowTimer -= Time.deltaTime * shadowRecoveryTime;
            hammer.localPosition = Vector3.zero;
            hammer.localScale = new(0.57f, 0.57f, 0.57f);
        }
        else
        {
            float leg = lengthObject.localPosition.y;
            hammer.localScale = new(0.57f, 0.5f + leg, 0.57f);
        }

        timeStopTimer -= Time.unscaledDeltaTime;
        shadow.color = new Color(0, 0, 0, Mathf.Clamp((1 - Mathf.Clamp01(shadowTimer)) / 4f, 0f, 0.25f));
        Time.timeScale = timeStopTimer > 0 || LevelManager.instance.winScreen.enabled ? 0 : 1;
        Vector3 rotation = rotationSpeed * stuck * Time.deltaTime * 180 * rotationDir * Vector3.forward;
        if (!canSwitch) rotation *= 3;
        rotationPoint.Rotate(rotation);
        hammer.transform.rotation = rotationPoint.rotation;
        if (Input.GetKeyDown(KeyCode.Mouse0) && energy >= 1)
        {
            hammer.GetComponent<SpriteRenderer>().color = new(1, 1, 1, 0.7f);
            slamming = true;
            shadowTimer = 2;
            stuck = 1 - stuckSlowdown / 2;
            energy -= 1.5f;
            if (state == 1)
            {
                trail.emitting = true;
                stuck = 1 - stuckSlowdown;
                state = 2;
                energy += 1f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            hammer.GetComponent<SpriteRenderer>().color = Color.white;
            slamming = false;
            trail.emitting = false;
            stuck = 1;
            state = 0;
        }
        trail.emitting = slamming && state == 2 && Physics2D.IsTouchingLayers(trailCollider, PlayerManagement.instance.boardMask);
        if (state == 2)
        {
            shaker.Shake(Time.deltaTime, 0.02f);
        }

    }
}
