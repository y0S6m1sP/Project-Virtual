using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Idle", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OpenDoor();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CloseDoor();
    }

    private void OpenDoor()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Open", true);
        anim.SetBool("Close", false);
    }

    private void CloseDoor()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Open", false);
        anim.SetBool("Close", true);
    }
}
