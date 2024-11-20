using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Basic Stats")]
    public float moveSpeed;
    public float idleTime;
    public float moveTime;
    public float chaseTime;

    [Header("Attack Stats")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;
    [HideInInspector] public bool canBeParrried;

    public EnemyStateMachine StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
    }

    public bool CanAttack()
    {
        if (Time.time >= lastTimeAttacked + attackCooldown)
        {
            lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }

    public void OpenParryWindow()
    {
        canBeParrried = true;
        StartCoroutine(ShowParryHint());
    }

    public void CloseParryWindow()
    {
        canBeParrried = false;
    }

    private IEnumerator ShowParryHint()
    {
        Sr.material = Resources.Load<Material>("Outline");
        yield return new WaitForSeconds(0.2f);
        Sr.material = new Material(Shader.Find("Sprites/Default"));
    }

    public virtual void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDir, 50, whatIsPlayer);

    public bool IsPlayerInAttackDistance()
    {
        return IsPlayerDetected() && IsPlayerDetected().distance < attackDistance;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * FacingDir, transform.position.y));
    }

    public virtual void Die()
    {
    }

}
