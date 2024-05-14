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
    public float length;
    public float rotationDir;
    public float timeStopTimer = -1;
    public float state = 0;
    public float energy;
    public float hammerVisibility;
    public bool slamming;
    public bool canSwitch = true;

    public float energyRegen;
    public float energyMax;
    public float rotationSpeed;
    public float stuckSlowdown;
    public float stuck;

    void Start()
    {

        hammerEnd.GetComponent<TrailRenderer>().emitting = false;
        rotationDir = 1;
        lengthObject.localPosition = Vector3.up * length;
        stuck = 1;
    }
    

    private void Update()
    {
        energy = Mathf.Clamp(energy + Time.deltaTime * energyRegen, 0, energyMax);
        timeStopTimer -= Time.unscaledDeltaTime;
        Time.timeScale = timeStopTimer > 0 ? 0 : 1;
        Vector3 rotation = rotationSpeed * stuck * Time.deltaTime * 180 * rotationDir * Vector3.forward;
        rotationPoint.Rotate(rotation);
        hammer.Rotate(rotation);
        if (Input.GetKeyDown(KeyCode.Mouse0) && energy >= 1)
        {
            slamming = true;
            stuck = 1 - stuckSlowdown / 2;
            energy--;
            if (state == 1)
            {
                hammerEnd.GetComponent<TrailRenderer>().emitting = true;
                stuck = 1 - stuckSlowdown;
                state = 2;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            slamming = false;
            hammerEnd.GetComponent<TrailRenderer>().emitting = false;
            stuck = 1;
            state = 0;
        }
        float strength = slamming ? 1 : 0.4f;
        hammer.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0, 0, strength);
        hammerEnd.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0, 0, strength);
        if (state == 2)
        {
            shaker.Shake(Time.deltaTime, 0.02f);
        }

    }
}
