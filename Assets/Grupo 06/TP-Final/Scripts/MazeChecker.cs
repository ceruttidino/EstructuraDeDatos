using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MazeChecker : MonoBehaviour
{
    public GridManager gridManager;
    public PathFindingAStar pathfinder;
    public TMP_Text resultText;

    Tile startTile, endTile;
    List<Tile> lastPath;

    bool recalc = true;

    private void Start()
    {
        if(gridManager == null) gridManager = GetComponent<GridManager>();
        if(pathfinder == null) pathfinder = GetComponent<PathFindingAStar>();

        FindStartEnd();
        recalc = true;
    }

    private void Update()
    {
       if(!recalc) return;
        Recalculate();
        recalc = false;
       
    }

    public void MarkPainted()
    {
        recalc = true;
    }

    public void Recalculate()
    {
        
        if (startTile != null && endTile != null) 
        { 
            lastPath = pathfinder.FindPath(startTile, endTile);
            if (lastPath != null) 
            {
                resultText.text = $"Solucion: SI (pasos: {lastPath.Count - 1})";
            }
            else
            {
                lastPath = null ;
                resultText.text = "Solucion: NO";
            }
        }
        else
        {
            lastPath = null;
            resultText.text = "Falta Start/End";
        }

    }

    public void FindStartEnd()
    {
        if(gridManager == null) return;
        Tile[,] grid = gridManager.GetGrid();
        if (grid == null) return;

        startTile = null; endTile = null;
        for (int x = 0; x < gridManager.width; x++)
        {
            for (int y = 0; y < gridManager.height; y++)
            {
                Tile t = grid[x, y];
                if (t == null) continue;
                if (t.type == TileType.Start) startTile = t;
                else if (t.type == TileType.End) endTile = t;
            }
        }
    }
    public List<Tile> GetLastPath() => lastPath;

    public void SetStart(Tile t)
    {
        if (startTile != null && startTile != t)
            startTile.SetType(TileType.Floor);
        startTile = t;
        if (startTile != null) startTile.SetType(TileType.Start);
        MarkPainted();
    }

    public void SetEnd(Tile t)
    {
        if (endTile != null && endTile != t)
            endTile.SetType(TileType.Floor);
        endTile = t;
        if (endTile != null) endTile.SetType(TileType.End);
        MarkPainted();
    }

}
