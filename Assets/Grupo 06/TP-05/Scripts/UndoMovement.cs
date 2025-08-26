using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UndoMovement : MonoBehaviour
{
    private MyStack<Vector3> positions = new MyStack<Vector3>();

    private void Start()
    {
        positions.Push(transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += Vector3.up;
            positions.Push(transform.position);
            Debug.Log("Nueva posicion guardada " + transform.position);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right;
            positions.Push(transform.position);
            Debug.Log("Nueva posicion guardada " + transform.position);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left;
            positions.Push(transform.position);
            Debug.Log("Nueva posicion guardada " + transform.position);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += Vector3.down;
            positions.Push(transform.position);
            Debug.Log("Nueva posicion guardada " + transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Count: " + positions.Count);

            if (positions.TryPop(out Vector3 prevPos))
            {
                transform.position = prevPos;
                Debug.Log("Volviste a la posicion previa");
            }
            else 
            {
                Debug.Log("No hay movimiento previo");
            }
        }

    }
}
