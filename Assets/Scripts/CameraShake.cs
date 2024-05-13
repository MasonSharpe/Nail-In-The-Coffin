using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float elapsed = 0;
    public float magnitude = 0;
    public void Shake(float duration, float magnitude)
    {
        elapsed = duration;
        this.magnitude = magnitude;
    }
    public void Update()
    {
        if (elapsed < 0)
        {
            transform.localPosition = new Vector3(0, 0, -10); return;
        }
        Vector3 originalPos = transform.localPosition;

        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;
        // print(new Vector3(x, y, originalPos.z));
        transform.localPosition = new Vector3(x, y, originalPos.z);
        elapsed -= Time.deltaTime;
    }
}
