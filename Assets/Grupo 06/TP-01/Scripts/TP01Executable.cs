using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP01Executable : MonoBehaviour
{
    public SimpleList<string> fruits = new SimpleList<string>();
    public ReadInput readInput;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Add()
    {
        if (!string.IsNullOrEmpty(readInput.inputText))
        {
            fruits.Add(readInput.inputText);
        }
        else 
        {
            Debug.LogWarning("No se ingresó ningún texto en el InputField");
        }
    }

    public void AddRange()
    {
        if (!string.IsNullOrEmpty(readInput.inputText))
        {
            string[] fruitsRange = readInput.inputText.Split(',');

            for (int i = 0; i < fruitsRange.Length; i++)
            {
                fruitsRange[i] = fruitsRange[i].Trim();
            }

            fruits.AddRange(fruitsRange);
        }
        else
        {
            Debug.LogWarning("No se ingresó ningún texto para AddRange");
        }

    }

    public void Clear()
    {
        fruits.Clear();
    }

    public void Remove()
    {
        if (!string.IsNullOrEmpty(readInput.inputText))
        {
            fruits.Remove(readInput.inputText);
        }
        else
        {
            Debug.LogWarning("No se ingresó ningún texto en el InputField");
        }
    }

}

