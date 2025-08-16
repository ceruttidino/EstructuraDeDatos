using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TP01Executable TP01Executable;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        TP01Executable = GameObject.FindGameObjectWithTag("TP01Executable").GetComponent<TP01Executable>();
    }

    void Update()
    {
        if (TP01Executable.fruits == null || TP01Executable.fruits.Count == 0)
            text.text = "Frutas: (vacío)";
        else
            text.text = "Frutas: " + string.Join(", ", TP01Executable.fruits);
    }
}
