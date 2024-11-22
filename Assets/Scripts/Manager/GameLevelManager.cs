using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;
    [SerializeField] private GameObject level0;
    [SerializeField] private GameObject[] level1;
    [SerializeField] private GameObject[] level1Enemies;
    [SerializeField] private GameObject spawnPoint;
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

        player.Rb.bodyType = RigidbodyType2D.Static;

        TransitionManager.instance.transition.StartTransition();

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

        yield return new WaitForSeconds(0.5f);

        bool swordSelected = false;
        UIManager.instance.swordSelect.ShowSwordSelect(() => swordSelected = true);
        yield return new WaitUntil(() => swordSelected);
        
        ClearLevel();
        currentLevel++;
        TransitionManager.instance.transition.EndTransition();

        if (currentLevel < 4)
        {
            int randomIndex = Random.Range(0, level1.Length);
            level1[randomIndex].SetActive(true);
        }
        else if (currentLevel == 4)
        {
            level0.SetActive(true);
            currentLevel = 0;
        }

        yield return new WaitForSeconds(1f);
        player.Rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(1f);
        if (currentLevel != 0)
        {
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, level1Enemies.Length);
        Vector3 spawnPosition = spawnPoint.transform.position;
        GameObject enemy = Instantiate(level1Enemies[randomIndex], spawnPosition, Quaternion.identity);
        FacePlayer(enemy);
    }

    private void FacePlayer(GameObject enemy)
    {
        Vector3 playerPosition = PlayerManager.instance.player.transform.position;
        Vector3 enemyPosition = enemy.transform.position;

        if (playerPosition.x < enemyPosition.x)
            enemy.GetComponent<Enemy>().Flip();

    }

}
