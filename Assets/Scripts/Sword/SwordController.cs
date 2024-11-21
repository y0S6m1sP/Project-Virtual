using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private EntityStats targetStats;
    [SerializeField] private SwordStats swordStats;
    [SerializeField] private float speed;
    public float trackingStrength = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Setup(EntityStats _targetStats, Sprite _sprite)
    {
        var sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = _sprite;

        targetStats = _targetStats;

        Vector2 initialVelocity = new Vector2(-5f * PlayerManager.instance.player.FacingDir, Random.Range(0f, 10f)).normalized * speed;

        rb.velocity = initialVelocity;
    }

    void FixedUpdate()
    {

        if (!targetStats) return;

        Vector2 directionToTarget = (targetStats.transform.position - transform.position).normalized;

        Vector2 desiredVelocity = directionToTarget * speed;
        Vector2 steeringForce = (desiredVelocity - rb.velocity) * trackingStrength;

        rb.AddForce(steeringForce);

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<Enemy>() != null)
        {
            if (hit.TryGetComponent<EnemyStats>(out var _target))
            {
                swordStats.DoDamage(_target);
                Destroy(gameObject);
            }
        }
    }
}
