using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MazePainterUI : MonoBehaviour
{
    public GridManager gridManager;
    public MazeChecker mazeChecker;
    public TileType selection = TileType.Wall;
    public Camera cam;
    public TMP_Text currentSelectionText;

    private void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -cam.transform.position.z;

            Vector3 world = cam.ScreenToWorldPoint(mousePos);
            PaintAt(world);
        }
    }

    public void SetSelection(int sel)
    {
        selection = (TileType)sel;
        if(currentSelectionText) currentSelectionText.text = "Seleccion: " + selection.ToString();
    }

    public void PaintAt(Vector3 worldPos)
    {
        float size = gridManager.tileSize;
        int x = Mathf.RoundToInt((worldPos.x - gridManager.transform.position.x) / size);
        int y = Mathf.RoundToInt((worldPos.y - gridManager.transform.position.y) / size);
        Tile t = gridManager.GetTile(x, y);
        if (t == null) return;

        if (t.type == selection) return;

        if (selection == TileType.Start)
        {
            if (mazeChecker != null)
            {
                mazeChecker.SetStart(t);
            }
            else
            {
                ClearType(TileType.Start);
                t.SetType(TileType.Start);
                if (mazeChecker != null) mazeChecker.MarkPainted();
            }
            return;

        }

        if (selection == TileType.End)
        {
            if (mazeChecker != null)
            {
                mazeChecker.SetEnd(t); 
            }
            else
            {
                ClearType(TileType.End);
                t.SetType(TileType.End);
                if (mazeChecker != null) mazeChecker.MarkPainted();
            }
            return;
        }

        t.SetType(selection);
        if (mazeChecker != null) mazeChecker.MarkPainted();
    }

    public void ClearType(TileType type)
    {
        if (gridManager == null) return;
        var grid = gridManager.GetGrid();
        if (grid == null) return;

        for (int i = 0; i < gridManager.width; i++)
        {
            for (int j = 0; j < gridManager.height; j++)
            {
                if (grid[i, j] != null && grid[i, j].type == type)
                    grid[i, j].SetType(TileType.Floor);
            }
        }

        if (mazeChecker != null) mazeChecker.MarkPainted();
    }
}
