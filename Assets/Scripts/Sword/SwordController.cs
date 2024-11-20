using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private EntityStats targetStats;
    [SerializeField] private SwordStats swordStats;
    [SerializeField] private float speed;

    private bool triggered;

    public void Setup(EntityStats _targetStats)
    {
        targetStats = _targetStats;
    }

    void Update()
    {
        if (!targetStats) return;
        if (triggered) return;

        transform.position = Vector2.MoveTowards(transform.position, targetStats.transform.position, speed * Time.deltaTime);
        transform.right = targetStats.transform.position - transform.position;

        if (Vector2.Distance(transform.position, targetStats.transform.position) < .1f)
        {
            triggered = true;
            swordStats.DoDamage(targetStats);
            Destroy(gameObject);

        }
    }
}
