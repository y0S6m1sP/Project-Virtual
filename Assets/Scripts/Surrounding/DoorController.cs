using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] private int leftOrRight;

    private Animator anim;
    private bool isOpen = false;
    private bool isInteractable = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (StageManager.instance.CurrentState == StageManager.StageState.Complete)
        {
            isOpen = true;
            anim.SetBool("Open", true);
        }
        else
        {
            isOpen = false;
            anim.SetBool("Open", false);
        }

        if (isInteractable && isOpen && Input.GetKeyDown(KeyCode.W))
        {
            StageManager.instance.NextStage(leftOrRight);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInteractable = false;
    }
}
