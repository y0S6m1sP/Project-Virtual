using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

abstract public class Entity : MonoBehaviour
{

    public Animator Anim { private set; get; }
    public Rigidbody2D Rb { private set; get; }
    public SpriteRenderer Sr { private set; get; }
    public EntityStats Stats { get; private set; }
    public CinemachineImpulseSource impulseSource { get; private set; }

    [Header("Knockback info")]
    [HideInInspector] protected Vector2 knockbackPower;
    [SerializeField] protected float knockbackDuration = 1f;
    public bool isKnocked;

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int KnockbackDir { get; private set; }
    public int FacingDir { private set; get; } = 1;

    public System.Action onFlipped;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        Sr = GetComponentInChildren<SpriteRenderer>();
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        Stats = GetComponent<EntityStats>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    protected virtual void Update()
    {

    }

    public void SetZeroVelocity()
    {
        if (Rb.bodyType == RigidbodyType2D.Static) return;
        Rb.velocity = Vector2.zero;
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (Rb.bodyType == RigidbodyType2D.Static) return;

        Rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        // Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public virtual void Flip()
    {
        FacingDir *= -1;
        transform.Rotate(0f, 180f, 0f);

        onFlipped?.Invoke();
    }

    public virtual void FlipController(float _xInput)
    {
        if (_xInput > 0 && FacingDir == -1)
        {
            Flip();
        }
        else if (_xInput < 0 && FacingDir == 1)
        {
            Flip();
        }
    }

    public virtual void Knockback() => StartCoroutine(nameof(HitKnockback));

    public virtual void SetupKnockbackDir(Transform _damageDirection)
    {
        if (_damageDirection.position.x > transform.position.x)
            KnockbackDir = -1;
        else if (_damageDirection.position.x < transform.position.x)
            KnockbackDir = 1;
    }

    public void SetupKnockbackPower(Vector2 _knockbackpower) => knockbackPower = _knockbackpower;

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;  

        if (knockbackPower.x > 0 || knockbackPower.y > 0) // This line makes player immune to freeze effect when he takes hit
        {
            Vector2 knockbackVelocity = new(knockbackPower.x * KnockbackDir, knockbackPower.y);
            Rb.AddForce(knockbackVelocity, ForceMode2D.Impulse);
            // Rb.velocity = Vector2.Lerp(Rb.velocity, knockbackVelocity, 0.5f);
        }

        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
        SetupZeroKnockbackPower();
    }

    protected virtual void SetupZeroKnockbackPower()
    {

    }

}
