using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;
    [SerializeField] private float yParallaxEffect;

    private float xPosition;
    private float yPosition;

    void Start()
    {
        cam = Camera.main.gameObject;
        xPosition = transform.position.x;
        yPosition = transform.position.y;
    }

    private void Update() {
        float distanceToMove =  cam.transform.position.x * parallaxEffect;
        float distanceToMoveY = cam.transform.position.y * yParallaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove, yPosition + distanceToMoveY);
    }
}