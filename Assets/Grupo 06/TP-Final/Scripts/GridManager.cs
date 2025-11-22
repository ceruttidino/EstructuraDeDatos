using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 20;
    public int height = 20;
    public float tileSize = 1f;
    public GameObject tilePrefab;

    Tile[,] grid;

    private void Start()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        if (tilePrefab == null) { Debug.LogError("Tile prefab missing"); return; }
        grid = new Tile[width, height];

        for (int x = 0; x < width; x++) 
        {
            for (int y = 0; y < height; y++) 
            {
                Vector3 pos = transform.position + new Vector3(x * tileSize, y * tileSize, 0f);
                GameObject go = Instantiate(tilePrefab,pos,Quaternion.identity,transform);
                Tile t = go.GetComponent<Tile>();
                t.x = x; t.y = y;
                t.SetType(TileType.Floor);
                grid[x,y] = t;
            }

        }
    }

    public Tile GetTile(int x, int y)
    {
        if(x  < 0 || y < 0 || x >= width || y >= height) return null;
        return grid[x,y];
    }

    public Tile[,] GetGrid() {  return grid; }
}
