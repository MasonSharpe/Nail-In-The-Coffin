using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailPoint : MonoBehaviour
{

    public bool counted = false;
    public float score = 0;
    public SpriteRenderer sprite;
    private void Update()
    {
        if (!counted) transform.Rotate(Time.deltaTime * 45 * Vector3.forward);
    }
}
