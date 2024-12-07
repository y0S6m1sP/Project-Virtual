using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Animator transition;

    public int currentLevel = 0;

    public List<List<MapNode>> pathMap = new();

    public MapNode currentNode;

    // TODO: if have time, refactor this to be more dynamic
    private static Vector2 battle1StartPosition = new(-26.93f, -2.03f);
    private static Vector2 rune2StartPosition = new(0, -13.99f);
    private static Vector2 shop11StartPosition = new(-15.02f, -1.96f);
    private static Vector2 boss1StartPosition = new(-10.04f, 0);
    private Vector2 playerStartPosition;

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

    public bool IsSelectNodeValid(MapNode node)
    {
        if (currentNode == null)
            return pathMap[0].Contains(node);

        return currentNode.Connections.Contains(node);
    }

    public void SetCurrentNode(MapNode node)
    {
        Debug.Log(IsSelectNodeValid(node));
        if (IsSelectNodeValid(node))
        {
            currentNode = node;
            switch (node.Type)
            {
                case NodeType.Money:
                case NodeType.Stats:
                    StartCoroutine(nameof(LoadScene), "Battle1");
                    playerStartPosition = battle1StartPosition;
                    break;
                case NodeType.Rune:
                    StartCoroutine(nameof(LoadScene), "Rune2");
                    playerStartPosition = rune2StartPosition;
                    break;
                case NodeType.Shop:
                    StartCoroutine(nameof(LoadScene), "Shop1");
                    playerStartPosition = shop11StartPosition;
                    break;
                case NodeType.Boss:
                    StartCoroutine(nameof(LoadScene), "Boss1");
                    playerStartPosition = boss1StartPosition;
                    break;
                
            }
            
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneName);
        while (!SceneManager.GetActiveScene().name.Equals(sceneName))
        {
            yield return null;
        }
        PlayerManager.instance.player.transform.position = playerStartPosition;
        transition.SetTrigger("Start");
    }

}
