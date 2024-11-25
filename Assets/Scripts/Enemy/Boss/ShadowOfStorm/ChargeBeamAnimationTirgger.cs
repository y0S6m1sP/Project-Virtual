using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBeamAnimationTirgger : MonoBehaviour
{

    private BoxCollider2D Collider => GetComponentInParent<BoxCollider2D>();

    private void AttackOver()
    {
        Collider.enabled = false;
    }
}
