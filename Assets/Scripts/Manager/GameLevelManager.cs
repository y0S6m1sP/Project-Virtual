using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;
    [SerializeField] private GameObject level0;
    [SerializeField] private GameObject merchant;
    [SerializeField] private GameObject[] level1;
    [SerializeField] private GameObject[] level1Enemies;
    [SerializeField] private GameObject[] level1Bosses;
    [SerializeField] private GameObject[] level2;
    [SerializeField] private GameObject[] level2Enemies;
    [SerializeField] private GameObject[] level2Bosses;
    [SerializeField] private GameObject[] level3;
    [SerializeField] private GameObject[] level3Enemies;
    [SerializeField] private GameObject[] level3Bosses;
    [SerializeField] private GameObject finalBoss;
    [SerializeField] private GameObject spawnPoint;
    public int currentLevel = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void NextLevel(float delay)
    {
        StartCoroutine(TransitionToNextLevel(delay));
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

    private IEnumerator TransitionToNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

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

        if (currentLevel != 4 && currentLevel != 9 && currentLevel != 14)
        {
            bool swordSelected = false;
            UIManager.instance.swordSelect.ShowSwordSelect(() => swordSelected = true);
            yield return new WaitUntil(() => swordSelected);
        }

        ClearLevel();
        currentLevel++;
        TransitionManager.instance.transition.EndTransition();

        SetupStage();

        yield return new WaitForSeconds(1f);
        player.Rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(1f);
        if (currentLevel != 0)
        {
            SpawnRandomEnemy(currentLevel);
        }
    }

    private void SetupStage()
    {
        switch (currentLevel)
        {
            case >= 1 and <= 3:
                {
                    int randomIndex = Random.Range(0, level1.Length);
                    level1[randomIndex].SetActive(true);
                    break;
                }
            case 5:
                {
                    int randomIndex = Random.Range(0, level1.Length);
                    level1[randomIndex].SetActive(true);
                    break;
                }
            case >= 6 and <= 8:
                {
                    int randomIndex = Random.Range(0, level2.Length);
                    level2[randomIndex].SetActive(true);
                    break;
                }
            case 10:
                {
                    int randomIndex = Random.Range(0, level2.Length);
                    level2[randomIndex].SetActive(true);
                    break;
                }
            case >= 11 and <= 13:
                {
                    int randomIndex = Random.Range(0, level3.Length);
                    level3[randomIndex].SetActive(true);
                    break;
                }
            case 15:
                {
                    int randomIndex = Random.Range(0, level3.Length);
                    level3[randomIndex].SetActive(true);
                    break;
                }
            case 16:
                break;
            case 4 or 9 or 14:
                merchant.SetActive(true);
                break;
        }
    }

    private void SpawnRandomEnemy(int currentLevel)
    {
        GameObject[] enemiesArray = null;

        switch (currentLevel)
        {
            case >= 1 and <= 3:
            enemiesArray = level1Enemies;
            break;
            case 5:
            enemiesArray = level1Bosses;
            break;
            case >= 6 and <= 8:
            enemiesArray = level2Enemies;
            break;
            case 10:
            enemiesArray = level2Bosses;
            break;
            case >= 11 and <= 13:
            enemiesArray = level3Enemies;
            break;
            case 15:
            enemiesArray = level3Bosses;
            break;
            case 16:
            enemiesArray = new GameObject[] { finalBoss };
            break;
        }

        if (enemiesArray != null && enemiesArray.Length > 0)
        {
            int randomIndex = Random.Range(0, enemiesArray.Length);
            Vector3 spawnPosition = spawnPoint.transform.position;
            GameObject enemy = Instantiate(enemiesArray[randomIndex], spawnPosition, Quaternion.identity);
            FacePlayer(enemy);
        }
    }

    private void FacePlayer(GameObject enemy)
    {
        Vector3 playerPosition = PlayerManager.instance.player.transform.position;
        Vector3 enemyPosition = enemy.transform.position;

        if (playerPosition.x < enemyPosition.x)
            enemy.GetComponent<Enemy>().Flip();

    }

}
