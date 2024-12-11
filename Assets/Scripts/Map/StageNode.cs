using System.Collections.Generic;

public enum StageType
{
    Stats,
    Key,
    Bomb,
    Shrine,
    Shop,
    Event,
    Boss,
}

public class StageNode
{
    public StageType Type;

    public StageNode(StageType type)
    {
        Type = type;
    }

    public static StageNode StatsStage()
    {
        return new StageNode(StageType.Stats);
    }

    public static StageNode KeyStage()
    {
        return new StageNode(StageType.Key);
    }

    public static StageNode BombStage()
    {
        return new StageNode(StageType.Bomb);
    }

    public static StageNode ShrineStage()
    {
        return new StageNode(StageType.Shrine);
    }

    public static StageNode ShopStage()
    {
        return new StageNode(StageType.Shop);
    }

    public static StageNode EventStage()
    {
        return new StageNode(StageType.Event);
    }

    public static StageNode BossStage()
    {
        return new StageNode(StageType.Boss);
    }
}