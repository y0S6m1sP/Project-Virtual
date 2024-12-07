using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CheckForEnemies());
    }

    private IEnumerator CheckForEnemies()
    {
        while (true)
        {
            if (GameObject.FindWithTag("Enemy") == null)
            {
                ShowReward();
                yield break; // Exit the coroutine if no enemies are found
            }
            yield return new WaitForSeconds(1f); // Check every second
        }
    }

    private void ShowReward()
    {
        Debug.Log("Show reward");
    }
}