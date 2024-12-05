using UnityEngine;

public class SurroundingAnimationTriggers : MonoBehaviour
{

    private Animator anim;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void AnimationFinishTrigger()
    {
        anim.SetBool("Active", false);
    }

    private void OpenCollider()
    {
        boxCollider2D.enabled = true;
    }

    private void CloseCollider()
    {
        boxCollider2D.enabled = false;
    }
}