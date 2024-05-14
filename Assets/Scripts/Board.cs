using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public bool enableTopCollision;
    public bool enableRightCollision;
    public bool enableLeftCollision;
    public bool enableBottomCollision;

    public BoxCollider2D top;
    public BoxCollider2D right;
    public BoxCollider2D left;
    public BoxCollider2D bottom;

    private void Awake()
    {
        top.enabled = enableTopCollision;
        right.enabled = enableRightCollision;
        left.enabled = enableLeftCollision;
        bottom.enabled = enableBottomCollision;
    }
}
