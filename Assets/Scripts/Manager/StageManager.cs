using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    public static StageManager instance;

    [Header("Spawn Point")]
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform enemySpawnPoint1;
    [SerializeField] private Transform enemySpawnPoint2;

    [Header("Enemies")]
    [SerializeField] private GameObject[] enemies;

    private int stageLevel = 0;
    private int enemyCount = 0;
    public List<List<StageNode>> path = new();
    public StageNode currentNode;

    public StageState CurrentState { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        GeneratePath();
        PlayerManager.instance.player.transform.position = playerSpawnPoint.position;
        CurrentState = StageState.Complete;
    }

    public void CheckEnemyCount()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            ChangeState(StageState.Complete);
        }
    }

    public void NextStage(int leftOrRight)
    {
        if (stageLevel < path.Count)
        {
            stageLevel++;
            currentNode = path[stageLevel][leftOrRight];
            ChangeState(StageState.Start);
        }
    }

    private void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemies.Length);
        GameObject enemy = Instantiate(enemies[randomIndex], enemySpawnPoint1.position, Quaternion.identity);
        enemyCount++;
    }

    #region Path Generation

    private void GeneratePath()
    {
        path.Clear();

        for (int i = 0; i <= 10; i++)
        {
            List<StageNode> stageNodes = GenerateStageNodes(i);
            path.Add(stageNodes);
        }
    }

    private List<StageNode> GenerateStageNodes(int level)
    {
        List<StageNode> nodes = new();
        List<StageNode> pool = new() { StageNode.StatsStage(), StageNode.KeyStage(), StageNode.BombStage(), StageNode.EventStage() };
        int randomCount = 0;

        switch (level)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 6:
            case 7:
                randomCount = 2;
                break;
            case 4:
            case 8:
                randomCount = 1;
                nodes.Add(StageNode.ShrineStage());
                break;
            case 5:
            case 9:
                randomCount = 1;
                pool = new() { StageNode.ShopStage() };
                break;
            case 10:
                randomCount = 0;
                pool = new() { StageNode.BossStage() };
                break;
        }

        for (int i = 0; i < randomCount; i++)
        {
            int randomIndex = Random.Range(0, pool.Count);
            nodes.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex);
        }

        return nodes;
    }

    #endregion

    #region Stage State Management
    public enum StageState
    {
        Start,
        Reward,
        Complete,
    }

    public void ChangeState(StageState state)
    {
        CurrentState = state;
        HandleStateChange();
    }

    private void HandleStateChange()
    {
        switch (CurrentState)
        {
            case StageState.Start:
                StartCoroutine(StartStage());
                break;
            case StageState.Reward:
                StartCoroutine(RewardStage());
                break;
            case StageState.Complete:
                StartCoroutine(CompleteStage());
                break;
        }
    }

    private IEnumerator StartStage()
    {
        PlayerManager.instance.player.transform.position = playerSpawnPoint.position;
        yield return null;
        SpawnRandomEnemy();
    }

    private IEnumerator RewardStage()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Reward Stage");
    }

    private IEnumerator CompleteStage()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Complete Stage");
    }

    #endregion
}
