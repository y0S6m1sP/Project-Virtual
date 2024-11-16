using System.Collections;
using TMPro;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [Header("Popup Text")]
    [SerializeField] private GameObject popUpTextPrefab;

    public void CreatePopupText(string _text)
    {
        if (popUpTextPrefab == null) return;

        Vector3 offset = new(Random.Range(-1f, 1f), Random.Range(1, 3), 0);

        GameObject popUpText = Instantiate(popUpTextPrefab, transform.position + offset, Quaternion.identity);

        popUpText.GetComponent<TextMeshPro>().text = _text;
    }
}