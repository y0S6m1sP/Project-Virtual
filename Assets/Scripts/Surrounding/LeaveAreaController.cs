using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveAreaController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        UIManager.instance.ShowMap(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        UIManager.instance.ShowMap(false);
    }
}
