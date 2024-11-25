using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBeamController : MonoBehaviour
{

    private EntityStats enemyStats;

    public void Setup(EntityStats enemyStats)
    {
        this.enemyStats = enemyStats;

        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<Player>() != null)
        {
            if (hit.TryGetComponent<PlayerStats>(out var _target))
            {
                enemyStats.DoDamage(_target);
            }
        }
    }
}
