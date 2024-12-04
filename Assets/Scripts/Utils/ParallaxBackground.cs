using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;

    void Start()
    {
        cam = Camera.main.gameObject;
        xPosition = transform.position.x;
    }

    private void Update() {
        float distanceToMove =  cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);
    }
}