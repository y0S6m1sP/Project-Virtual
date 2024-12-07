using UnityEngine;

public class InteractableController : MonoBehaviour
{
    public SpriteRenderer Sr;
    public bool isInteractable = false;

    protected virtual void Start()
    {
        Sr = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Sr.material = Resources.Load<Material>("WhiteOutline");
        isInteractable = true;
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        Sr.material = new Material(Shader.Find("Sprites/Default"));
        isInteractable = false;
    }
}
