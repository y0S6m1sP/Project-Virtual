using System.Collections;
using UnityEngine;

public class ManaController : MonoBehaviour
{

    private Player player;
    private Rigidbody2D rb;
    private float stateTimer = 0;
    public bool isTrigger = false;

    [SerializeField] private float speed;
    public float trackingStrength = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void FixedUpdate()
    {
        stateTimer -= Time.deltaTime;

        if (stateTimer > 0 || !isTrigger)
        {
            return;
        }

        Vector2 directionToTarget = (player.transform.position - transform.position).normalized;

        Vector2 desiredVelocity = directionToTarget * speed;
        Vector2 steeringForce = (desiredVelocity - rb.velocity) * trackingStrength;

        rb.AddForce(steeringForce);

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Vector2.Distance(player.transform.position, transform.position) < 0.5f)
        {
            player.Stats.IncreaseMana();
            Destroy(gameObject);
        }
    }

    public void TriggerMana(Player player)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        this.player = player;
        stateTimer = 0.1f;
        isTrigger = true;
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = -direction * speed;
    }
}