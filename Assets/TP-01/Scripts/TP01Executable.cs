using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP01Executable : MonoBehaviour
{
    public SimpleList<int> fruits = new SimpleList<int>();
    private int[] fruitsCollection = new int[5];
    void Start()
    {
        for (int i = 0; i < fruitsCollection.Length; i++)
        {
            fruitsCollection[i] = i;
        }
    }

    void Update()
    {

    }

    public void Add()
    {
        fruits.Add(1);
    }

    public void AddRange()
    {
        fruits.AddRange(fruitsCollection);
    }

    public void Clear()
    {
        fruits.Clear();
    }

    public void Remove()
    {
        fruits.Remove(1);
    }

}

