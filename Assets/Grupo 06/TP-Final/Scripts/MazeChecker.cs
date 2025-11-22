using NUnit.Framework;
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

    private void Update()
    {
        FindStartEnd();
        if (startTile != null && endTile != null) 
        {
            lastPath = pathfinder.FindPath(startTile, endTile);
            if (lastPath != null) resultText.text = "Solución: SÍ (pasos: " + (lastPath.Count - 1) + ")";
            else resultText.text = "Solucion: No";
        }
        else
        {
            resultText.text = "Faltan Start/End";
            lastPath = null;
        }
    }

    public void FindStartEnd()
    {
        Tile[,] grid = gridManager.GetGrid();
        startTile = null; endTile = null;
        for(int x = 0; x < gridManager.width; x++)
        {
            for (int y = 0; y < gridManager.height; y++)
            { 
                Tile t = grid[x, y];
                if(t.type == TileType.Start)startTile = t;
                else if(t.type == TileType.End)endTile = t;
            }
        }
    }

    public List<Tile> GetLastPath() => lastPath;
}
