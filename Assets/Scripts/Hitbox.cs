using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Transform attackCheck;
    public Vector2 attackCheckSize;

    private void OnDrawGizmos()
    {
        if (attackCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackCheck.position, attackCheckSize);
        }
    }
}
