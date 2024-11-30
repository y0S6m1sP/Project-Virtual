using UnityEngine;

public class ManaController: MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if (other.TryGetComponent<PlayerStats>(out var _target))
            {
                _target.IncreaseManaBy(5);
            }
        }
    }
}