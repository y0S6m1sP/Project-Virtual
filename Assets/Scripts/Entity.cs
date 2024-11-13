using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Entity : MonoBehaviour
{

    public Animator Anim { private set; get; }
    public Rigidbody2D Rb { private set; get; }

    public int FacingDir { private set; get; } = 1;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    public void SetZeroVelocity()
    {
        Rb.velocity = Vector2.zero;
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        Rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public virtual void Flip()
    {
        FacingDir *= -1;
        transform.Rotate(0f, 180f, 0f);
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

}
