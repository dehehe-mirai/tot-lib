namespace Scripts.Theater;

public class IdolTile
{
    public enum Type
    {
        Tile,
        Token
    };

    public string? Name;
    public int X;
    public int Y;
    public string Id = "";

    public IdolTile()
    {
    }

    private IdolTile(string? name)
    {
        this.Name = name;
    }

    public static IdolTile Of(string? name)
    {
        return new IdolTile(name);
    }

    public IdolTile SetXY(int x, int y)
    {
        X = x;
        Y = y;
        return this;
    }
    public IdolTile ID(string id)
    {
        this.Id = id;
        return this;
    }
}