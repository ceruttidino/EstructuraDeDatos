using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MazePainterUI : MonoBehaviour
{
    public GridManager gridManager;
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
        if(t == null)return;

        if (selection == TileType.Start)
        {
            ClearType(TileType.Start);
        }
        else if (selection == TileType.End) 
        {
            ClearType(TileType.End);
        }
        t.SetType(selection);
    }

    public void ClearType(TileType type)
    {
        var grid = gridManager.GetGrid();
        for(int i = 0; i < gridManager.width; i++)
        {
            for(int j = 0; j < gridManager.height; j++)
            {
                if (grid[i,j].type == type) grid[i,j].SetType(TileType.Floor);
            }
        }
    }
}
