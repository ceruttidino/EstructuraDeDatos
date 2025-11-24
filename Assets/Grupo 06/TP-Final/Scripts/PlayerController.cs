using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MazeChecker mazeChecker;
    public float stepTime = 0.3f;
    Coroutine travelRoutine;

    public void StartTraverse()
    {    
        var path = mazeChecker.GetLastPath();

        if (path == null || path.Count == 0)
        {
            Debug.Log("no path to travel");
            return;
        }

        if (travelRoutine != null)
        {
            StopCoroutine(travelRoutine);
        }

        travelRoutine = StartCoroutine(Traverse(path));
    }

    IEnumerator Traverse(List<Tile> path)
    {
        transform.position = path[0].transform.position;
        for (int i = 1; i < path.Count; i++)
        { 
            Vector3 target = path[i].transform.position;          
            Vector3 start = transform.position;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / stepTime;
                transform.position = Vector3.Lerp(start, target, t);
                yield return null;
            }
            transform.position = target;
        }
        travelRoutine = null;
    }

}
