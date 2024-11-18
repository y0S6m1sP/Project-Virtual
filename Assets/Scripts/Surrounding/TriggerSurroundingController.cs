using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerableSurroundingController : MonoBehaviour
{

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Idle", true);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Open();
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        Close();
    }

    protected virtual void Open()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Open", true);
        anim.SetBool("Close", false);
    }

    protected virtual void Close()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Open", false);
        anim.SetBool("Close", true);
    }
}
