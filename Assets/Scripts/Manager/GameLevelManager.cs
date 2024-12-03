using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance;
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

    public List<List<MapNode>> pathMap = new();

    private Coroutine manaSpawnCoroutine;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
        {
            Instance = this;
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

    public void GeneratePathMap()
    {
        pathMap.Clear();

        List<MapNode> stage1 = GenerateStage();
        List<MapNode> stage2 = GenerateStage();
        List<MapNode> stage3 = GenerateStageWithShop();
        List<MapNode> stage4 = GenerateStage();
        List<MapNode> stage5 = GenerateStageWithShop();
        List<MapNode> stage6 = GenerateBossStage();

        GenerateConnections(stage1, stage2);
        GenerateConnections(stage2, stage3);
        GenerateConnections(stage3, stage4);
        GenerateConnections(stage4, stage5);
        GenerateConnections(stage5, stage6);

        pathMap.Add(stage1);
        pathMap.Add(stage2);
        pathMap.Add(stage3);
        pathMap.Add(stage4);
        pathMap.Add(stage5);
        pathMap.Add(stage6);
    }

    private List<MapNode> GenerateStage()
    {
        List<MapNode> nodes = new();
        MapNode moneyNode = new() { Type = NodeType.Money };
        MapNode statsNode = new() { Type = NodeType.Stats };
        MapNode runeNode = new() { Type = NodeType.Rune };
        MapNode emptyNode = new() { Type = NodeType.Empty };

        List<MapNode> allNodes = new() { moneyNode, statsNode, runeNode, emptyNode };
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, allNodes.Count);
            nodes.Add(allNodes[randomIndex]);
            allNodes.RemoveAt(randomIndex);
        }

        return nodes;
    }

    private List<MapNode> GenerateStageWithShop()
    {
        List<MapNode> nodes = new();
        MapNode moneyNode = new() { Type = NodeType.Money };
        MapNode statsNode = new() { Type = NodeType.Stats };
        MapNode runeNode = new() { Type = NodeType.Rune };
        MapNode shopNode = new() { Type = NodeType.Shop };
        MapNode emptyNode = new() { Type = NodeType.Empty };

        List<MapNode> allNodes = new() { moneyNode, statsNode, runeNode, shopNode, emptyNode };
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, allNodes.Count);
            nodes.Add(allNodes[randomIndex]);
            allNodes.RemoveAt(randomIndex);
        }

        return nodes;
    }

    private List<MapNode> GenerateBossStage()
    {
        List<MapNode> nodes = new();
        MapNode emptyNode = new() { Type = NodeType.Empty };
        MapNode bossNode = new() { Type = NodeType.Boss };
        nodes.Add(emptyNode);
        nodes.Add(bossNode);
        nodes.Add(emptyNode);
        return nodes;
    }

    public void GenerateConnections(List<MapNode> from, List<MapNode> to)
    {
        for (int i = 0; i < from.Count; i++)
        {
            if (from[i].Type == NodeType.Empty)
                continue;

            if (from[i].Type != NodeType.Empty && to[i].Type != NodeType.Empty)
                from[i].Connections.Add(to[i]);

            if (i == 0)
            {
                if (from[i].Connections.Count == 0)
                    from[i].Connections.Add(to[i + 1]);
                else
                {
                    if (Random.Range(0, 100) < 50)
                    {
                        if (to[i + 1].Type != NodeType.Empty)
                            from[i].Connections.Add(to[i + 1]);
                    }
                }
            }

            if (i == 1)
            {
                while (from[i].Connections.Count == 0)
                {
                    if (Random.Range(0, 100) < 50)
                    {
                        if (to[i + 1].Type != NodeType.Empty)
                            from[i].Connections.Add(to[i + 1]);
                    }

                    else
                    {
                        if (to[i - 1].Type != NodeType.Empty)
                            from[i].Connections.Add(to[i - 1]);
                    }
                }
            }

            if (i == 2)
            {
                if (from[i].Connections.Count == 0)
                    from[i].Connections.Add(to[i - 1]);
                else
                {
                    if (Random.Range(0, 100) < 50)
                    {
                        if (to[i - 1].Type != NodeType.Empty)
                            from[i].Connections.Add(to[i - 1]);
                    }
                }
            }
        }

        for (int i = 0; i < from.Count; i++)
        {
            if (from[i].Type == NodeType.Empty)
            {
                bool isConnected = false;
                foreach (var node in from)
                {
                    if (node.Connections.Contains(to[i]))
                    {
                        isConnected = true;
                        break;
                    }
                }

                if (!isConnected)
                {
                    if (to[i].Type == NodeType.Empty) continue;
                    if (i == 0) from[i + 1].Connections.Add(to[i]);
                    else if (i == 1)
                    {
                        from[i - 1].Connections.Add(to[i]);
                        from[i + 1].Connections.Add(to[i]);
                    }
                    else if (i == 2) from[i - 1].Connections.Add(to[i]);
                }
            }
        }
    }

}
