using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;
    [SerializeField] private GameObject level0;
    [SerializeField] private GameObject merchant;

    [Header("Level 1")]
    [SerializeField] private GameObject[] level1;
    [SerializeField] private GameObject Level1BossRoom;
    [SerializeField] private GameObject[] level1Enemies;
    [SerializeField] private GameObject[] level1Bosses;

    [Header("Level 2")]
    [SerializeField] private GameObject[] level2;
    [SerializeField] private GameObject[] level2Enemies;
    [SerializeField] private GameObject[] level2Bosses;

    [Header("Level 3")]
    [SerializeField] private GameObject[] level3;
    [SerializeField] private GameObject[] level3Enemies;
    [SerializeField] private GameObject[] level3Bosses;

    [Header("Final Boss")]
    [SerializeField] private GameObject finalBoss;

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;
    [SerializeField] private GameObject mana;

    public int currentLevel = 0;

    private Coroutine manaSpawnCoroutine;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void NextLevel(float delay)
    {
        StopSpawnMana();
        DestroyAllMana();
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
            UIManager.instance.runeSelect.ShowRuneSelect(() => swordSelected = true);
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
            StartSpawnMana();
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
                    Level1BossRoom.SetActive(true);
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

    public void StartSpawnMana()
    {
        manaSpawnCoroutine ??= StartCoroutine(SpawnManaRoutine());
    }

    public void StopSpawnMana()
    {
        if (manaSpawnCoroutine != null)
        {
            StopCoroutine(manaSpawnCoroutine);
            manaSpawnCoroutine = null;
        }
    }

    private IEnumerator SpawnManaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            SpawnMana();
        }
    }

    private void DestroyAllMana()
    {
        GameObject[] manaObjects = GameObject.FindGameObjectsWithTag("Mana");
        foreach (GameObject manaObject in manaObjects)
        {
            Destroy(manaObject);
        }
    }

    private void SpawnMana()
    {
        float randomX = Random.Range(leftBoundary.position.x, rightBoundary.position.x);
        float randomY = Random.Range(0, -2);
        Vector3 spawnPosition = new(randomX, randomY, spawnPoint.transform.position.z);
        Instantiate(mana, spawnPosition, Quaternion.identity);
    }

}
