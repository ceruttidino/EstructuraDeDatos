using NUnit.Framework;
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
        if(travelRoutine != null) StopCoroutine(travelRoutine);
        var path = mazeChecker.GetLastPath();
        if(path == null)
        {
            Debug.Log("no path to travel");
            return;
        }
        travelRoutine = StartCoroutine(Traverse(path));
    }

    IEnumerator Traverse(List<Tile> path)
    {
        transform.position = path[0].transform.position;
        for (int i = 1; i < path.Count; i++)
        { 
            Vector3 target = path[i].transform.position;
            float t = 0f;
            Vector3 start = transform.position;
            while(t < 1f)
            {
                t += Time.deltaTime / stepTime;
                transform.position = Vector3.Lerp(start, target, t);
                yield return null;
            }
            transform.position = target;
            yield return null;

        }
        travelRoutine = null;
    }

}
