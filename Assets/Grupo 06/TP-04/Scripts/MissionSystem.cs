using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionSystem : MonoBehaviour
{
    private MyQueue<string> missions = new MyQueue<string>();

    [SerializeField] private TextMeshProUGUI missionText;
    [SerializeField] private GameObject winText;

    void Start()
    {
        missions.Enqueue("Derrota a 5 caballeros ancestrales dentro del castillo");
        missions.Enqueue("Recolecta 2 pociones de misericordia demoniaca");
        missions.Enqueue("Derrota a Emilianus, the Great Cyclop");

        UpdateMissionText();
        winText.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            CompleteMission();
        }
    }

    void CompleteMission()
    {
        if (missions.Count > 0)
        {
            missions.Dequeue();
            UpdateMissionText();
        }

        if (missions.Count == 0)
        {
            missionText.text = "";
            winText.SetActive(true);
        }
    }

    void UpdateMissionText()
    {
        if (missions.Count > 0)
        {
            missionText.text = "Misión: " + missions.Peek();
        }
        else 
        {
            missionText.text = "";
        }
    }
}

