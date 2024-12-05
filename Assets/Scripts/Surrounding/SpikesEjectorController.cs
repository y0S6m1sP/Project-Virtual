using System.Collections;
using UnityEngine;

public class SpikesEjectorController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ActiveSpikes());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Spike ejector");
    }

    private IEnumerator ActiveSpikes()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            anim.SetBool("Active", true);
        }

    }
}