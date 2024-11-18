using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;
    [SerializeField] private GameObject level0;
    [SerializeField] private GameObject[] level1;
    public int currentLevel = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void NextLevel()
    {
        StartCoroutine(TransitionToNextLevel());       
    }

    private void ClearLevel()
    {
        GameObject level = GameObject.Find("Level");
        if (level != null)
        {
            foreach (Transform child in level.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator TransitionToNextLevel()
    {
        Player player = PlayerManager.instance.player;
        
        // Freeze player movement
        player.Rb.bodyType = RigidbodyType2D.Static;

        // Initialize transition effect
        TransitionManager.instance.transition.StartTransition();

        // Move player to center position
        const float MOVE_DURATION = 0.5f;
        Vector3 startPosition = player.transform.position;
        Vector3 centerPosition = Vector3.zero;
        
        float elapsedTime = 0;
        while (elapsedTime < MOVE_DURATION)
        {
            float progress = elapsedTime / MOVE_DURATION;
            player.transform.position = Vector3.Lerp(startPosition, centerPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = centerPosition;

        // Wait before level change
        yield return new WaitForSeconds(0.5f);
        
        // Change level
        ClearLevel();
        currentLevel++;
        TransitionManager.instance.transition.EndTransition();

        // Set up new level
        if (currentLevel == 1)
        {
            level1[0].SetActive(true);
        }
        else if (currentLevel == 2)
        {
            level0.SetActive(true);
            currentLevel = 0;
        }

        // Re-enable player movement
        yield return new WaitForSeconds(1f);
        player.Rb.bodyType = RigidbodyType2D.Dynamic;
    }


}
