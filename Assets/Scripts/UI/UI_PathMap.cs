using System.Collections.Generic;
using UnityEngine;

public class UI_PathMap : MonoBehaviour
{

    [SerializeField] private RectTransform linePrefab;
    public Transform parent;

    [SerializeField] private Transform stage1;
    [SerializeField] private Transform stage2;
    [SerializeField] private Transform stage3;
    [SerializeField] private Transform stage4;
    [SerializeField] private Transform stage5;
    [SerializeField] private Transform stage6;

    private UI_MapNode[] stage1Nodes;
    private UI_MapNode[] stage2Nodes;
    private UI_MapNode[] stage3Nodes;
    private UI_MapNode[] stage4Nodes;
    private UI_MapNode[] stage5Nodes;
    private UI_MapNode[] stage6Nodes;

    private readonly List<RectTransform> lines = new();

    private void Start()
    {
        // InitNodes();
    }

    private void InitNodes()
    {
        stage1Nodes = stage1.GetComponentsInChildren<UI_MapNode>();
        stage2Nodes = stage2.GetComponentsInChildren<UI_MapNode>();
        stage3Nodes = stage3.GetComponentsInChildren<UI_MapNode>();
        stage4Nodes = stage4.GetComponentsInChildren<UI_MapNode>();
        stage5Nodes = stage5.GetComponentsInChildren<UI_MapNode>();
        stage6Nodes = stage6.GetComponentsInChildren<UI_MapNode>();
    }

    public void RenderMap()
    {
        InitNodes();
        ResetNodes();
        ClearMapLines();
        GameManager.Instance.GeneratePathMap();

        List<List<MapNode>> pathMap = GameManager.Instance.pathMap;
        UI_MapNode[][] stages = { stage1Nodes, stage2Nodes, stage3Nodes, stage4Nodes, stage5Nodes, stage6Nodes };

        for (int i = 0; i < pathMap.Count; i++)
        {
            for (int j = 0; j < pathMap[i].Count; j++)
            {
                MapNode node = pathMap[i][j];
                node.Position = stages[i][j].transform.position;
                stages[i][j].SetNode(node);
            }
        }

        for (int i = 0; i < pathMap.Count; i++)
        {
            for (int j = 0; j < pathMap[i].Count; j++)
            {
                MapNode node = pathMap[i][j];
                foreach (MapNode connection in node.Connections)
                {
                    DrawLine(node.Position, connection.Position);
                }
            }
        }
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        RectTransform line = Instantiate(linePrefab, parent);

        line.position = (start + end) / 2;

        float zRotation = 0;

        if (start.x > end.x)
            zRotation = 45;
        else if (start.x < end.x)
            zRotation = -45;

        line.localRotation = Quaternion.Euler(0, 0, zRotation);

        lines.Add(line);
    }

    private void ResetNodes()
    {
        if (stage1Nodes == null || stage2Nodes == null || stage3Nodes == null || stage4Nodes == null || stage5Nodes == null || stage6Nodes == null)
            return;

        foreach (UI_MapNode node in stage1Nodes)
        {
            node.ResetNode();
        }
        foreach (UI_MapNode node in stage2Nodes)
        {
            node.ResetNode();
        }
        foreach (UI_MapNode node in stage3Nodes)
        {
            node.ResetNode();
        }
        foreach (UI_MapNode node in stage4Nodes)
        {
            node.ResetNode();
        }
        foreach (UI_MapNode node in stage5Nodes)
        {
            node.ResetNode();
        }
        foreach (UI_MapNode node in stage6Nodes)
        {
            node.ResetNode();
        }
    }

    private void ClearMapLines()
    {
        foreach (RectTransform line in lines)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
    }

}