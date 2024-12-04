using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public SpriteRenderer Sr { private set; get; }

    private void Start()
    {
        Sr = GetComponentInChildren<SpriteRenderer>();
    }

    private bool canStart = false;
    private void Update()
    {
        if (canStart && Input.GetKeyDown(KeyCode.E))
        {
            GameLevelManager.Instance.NextLevel(0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Sr.material = Resources.Load<Material>("WhiteOutline");
        canStart = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Sr.material = new Material(Shader.Find("Sprites/Default"));
        canStart = false;
    }
}
