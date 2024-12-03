using System.Collections.Generic;
using UnityEngine;


public enum NodeType
{
    Money,
    Stats,
    Rune,
    Shop,
    Boss,
    Empty
}

public class MapNode
{
    public NodeType Type;
    public Vector3 Position;
    public List<MapNode> Connections = new();
}