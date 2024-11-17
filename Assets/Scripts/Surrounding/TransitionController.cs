using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public float maxSize;
    public float speed;
    public bool canGrow;

    void Update()
    {
        Vector3 scaleChange = new(speed * Time.deltaTime, speed * Time.deltaTime, 0);
        
        if (canGrow)
        {
            transform.localScale += scaleChange;
            if (transform.localScale.x >= maxSize || transform.localScale.y >= maxSize)
            {
                transform.localScale = new(maxSize, maxSize, transform.localScale.z);
            }
        }
        else 
        {
            transform.localScale -= scaleChange;
            if (transform.localScale.x <= 0 || transform.localScale.y <= 0)
            {
                transform.localScale = new(0, 0, transform.localScale.z);
            }
        }
    }
}
