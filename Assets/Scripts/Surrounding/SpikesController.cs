using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Spike damage");
    }
   
}
