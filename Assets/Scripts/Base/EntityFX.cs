using System.Collections;
using TMPro;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [Header("Popup Text")]
    [SerializeField] private GameObject popUpTextPrefab;

    [Header("Hit FX")]
    [SerializeField] private GameObject hitFXPrefab;

    public void CreatePopupText(string _text)
    {
        if (popUpTextPrefab == null) return;

        Vector3 offset = new(Random.Range(-1f, 1f), Random.Range(1, 3), 0);

        GameObject popUpText = Instantiate(popUpTextPrefab, transform.position + offset, Quaternion.identity);

        popUpText.GetComponent<TextMeshPro>().text = _text;
    }

    public void CreateHitFX(Transform _target)
    {
        if (hitFXPrefab == null) return;

        float xPosition = -.5f;
        float yPosition = Random.Range(-.5f, .5f);

        float zRotation = Random.Range(-45, 45);
        float yRotation = 0;
        if (GetComponent<Entity>().FacingDir == 1)
        {
            yRotation = 180;
            xPosition *= -1;
        }

        Vector3 rotation = new(0, yRotation, zRotation);

        GameObject hitFX = Instantiate(hitFXPrefab, _target.position + new Vector3(xPosition, yPosition), Quaternion.identity);

        hitFX.transform.Rotate(rotation);

        Destroy(hitFX, .5f);
    }
}