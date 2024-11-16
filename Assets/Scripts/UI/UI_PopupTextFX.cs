using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PopupText : MonoBehaviour
{
    private TextMeshPro text;

    [SerializeField] private float speed;
    [SerializeField] private float colorDisapearSpeed;

    private float timer;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.up, speed * Time.deltaTime);
        timer -= Time.deltaTime;

        float alpha = text.color.a - colorDisapearSpeed * Time.deltaTime;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

        if (text.color.a < 0)
            Destroy(gameObject);
    }
}
