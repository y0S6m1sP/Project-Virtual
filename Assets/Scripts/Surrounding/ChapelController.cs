using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapelController : MonoBehaviour
{
    public SpriteRenderer Sr { private set; get; }

    private void Start()
    {
        Sr = GetComponentInChildren<SpriteRenderer>();
    }

    private bool canStart = false;
    private bool isSelected = false;
    private void Update()
    {
        if (canStart && Input.GetKeyDown(KeyCode.E))
        {
            if (isSelected) return;
            
            UIManager.instance.runeSelect.ShowRuneSelect(() =>
            {
                isSelected = true;
                UIManager.instance.runeSelect.HideRuneSelect();
                UIManager.instance.ShowMap(true);
            });
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
        UIManager.instance.ShowMap(false);
    }
}
