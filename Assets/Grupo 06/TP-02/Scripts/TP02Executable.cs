using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyLinkedList;
using NUnit.Framework;
using UnityEngine;

public class TP02Executable : MonoBehaviour
{
    public MyList<string> vegetables = new MyList<string>();
    public ReadInput readInput;

    void Start()
    {
        
    }

    public void Add()
    {
        if (!string.IsNullOrEmpty(readInput.inputText))
        {
            vegetables.Add(readInput.inputText);
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
            string[] vegetablesRange = readInput.inputText.Split(',');

            for (int i = 0; i < vegetablesRange.Length; i++)
            {
                vegetablesRange[i] = vegetablesRange[i].Trim();
            }

            vegetables.AddRange(vegetablesRange);
        }
        else
        {
            Debug.LogWarning("No se ingresó ningún texto para AddRange");
        }

    }

    public void AddList()
    {
        if (!string.IsNullOrEmpty(readInput.inputText))
        {
            string[] vegetablesRange = readInput.inputText.Split(',');

            MyList<string> vegetablesList = new MyList<string>();

            for(int i = 0;i < vegetablesRange.Length; i++)
            {
                vegetablesList.Add(vegetablesRange[i]);
            }

            vegetables.AddRange(vegetablesList);

        }
        else
        {
            Debug.LogWarning("No se ingresó ningún texto para AddRange");
        }

    }

    public void Remove()
    {
        if (!string.IsNullOrEmpty(readInput.inputText))
        {
            vegetables.Remove(readInput.inputText);
        }
        else
        {
            Debug.LogWarning("No se ingresó ningún texto en el InputField");
        }
    }

    public void RemoveByIndex()
    {
        if (int.TryParse(readInput.inputText, out int index) && index >= 0 && index < vegetables.Count)
        {
            vegetables.RemoveAt(index);
        }
        else
        {
            Debug.LogWarning("Índice inválido");
        }
    }

    public void IsEmpty()
    {
        vegetables.IsEmpty();

        if (vegetables.IsEmpty() == false)
        {
            Debug.Log("You have some vegetables");
        }
        else 
        {
            Debug.Log("You are poor");
        }
    }

    public void Clear()
    {
        vegetables.Clear(); 
    }
}
