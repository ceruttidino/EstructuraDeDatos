using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTextTP02 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TP02Executable TP02Executable;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        TP02Executable = GameObject.FindGameObjectWithTag("TP02Executable").GetComponent<TP02Executable>();
    }

    void Update()
    {
        if (TP02Executable.vegetables == null || TP02Executable.vegetables.Count == 0)
            text.text = "Vegetales: (vacío)";
        else
            text.text = "Vegetales: " + string.Join(", ", TP02Executable.vegetables);
    }
}
