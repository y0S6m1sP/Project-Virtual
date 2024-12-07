using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float stateTimer = 0;

    [SerializeField] private float speed;
    public float trackingStrength = 5f;

    private int amount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Setup(int amount)
    {
        this.amount = amount;

        Vector2 initialVelocity = new Vector2(Random.Range(-5, 5), Random.Range(7f, 10f)).normalized * speed;

        rb.velocity = initialVelocity;

        stateTimer = .5f;
    }

    void FixedUpdate()
    {
        stateTimer -= Time.deltaTime;

        if (stateTimer > 0)
        {
            return;
        }

        Vector2 directionToTarget = (PlayerManager.instance.player.transform.position - transform.position).normalized;

        Vector2 desiredVelocity = directionToTarget * speed;
        Vector2 steeringForce = (desiredVelocity - rb.velocity) * trackingStrength;

        rb.AddForce(steeringForce);

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<Player>() != null)
        {
            PlayerManager.instance.AddMoney(amount);
            Destroy(gameObject);
        }
    }
}
