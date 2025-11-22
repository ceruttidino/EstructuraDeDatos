using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x, y;
    public TileType type = TileType.Floor;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        ApplyColor();
    }

    public void SetType(TileType newType)
    {
        type = newType;
        ApplyColor();
    }

    void ApplyColor()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case TileType.Floor: sr.color = Color.white; break;
            case TileType.Wall: sr.color = Color.black;break;
            case TileType.Start: sr.color = Color.blue;break;
            case TileType.End: sr.color = Color.red;break;
        }
    }

    public bool IsWalkable()
    {
        return type != TileType.Wall;
    }
}
