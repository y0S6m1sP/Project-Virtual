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
    public StageType type;
    public string spritePath;
    public string hint;

    public StageNode(StageType type, string spritePath, string hint)
    {
        this.type = type;
        this.spritePath = spritePath;
        this.hint = hint;
    }

    public static StageNode StatsStage()
    {
        return new StageNode(StageType.Stats, "UI/Key", "Stats");
    }

    public static StageNode KeyStage()
    {
        return new StageNode(StageType.Key, "UI/Key", "Grants a Key");
    }

    public static StageNode BombStage()
    {
        return new StageNode(StageType.Bomb, "UI/Key", "Bomb");
    }

    public static StageNode ShrineStage()
    {
        return new StageNode(StageType.Shrine, "UI/Key", "Shrine");
    }

    public static StageNode ShopStage()
    {
        return new StageNode(StageType.Shop, "UI/Key", "Shop");
    }

    public static StageNode EventStage()
    {
        return new StageNode(StageType.Event, "UI/Key", "Event");
    }

    public static StageNode BossStage()
    {
        return new StageNode(StageType.Boss, "UI/Key", "Boss");
    }

    public static StageNode BaseStage()
    {
        return new StageNode(StageType.Stats, "UI/Key", "Next Level");
    }
}