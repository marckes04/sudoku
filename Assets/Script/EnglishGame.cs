using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnglishGame : MonoBehaviour
{
    public GameObject mainPanel;

    public GameObject sudokuFieldPanel;

    public GameObject fieldPrefab;

     void Start()
    {
        for (int i = 0; i < 81; i++) 
        {
            GameObject.Instantiate(fieldPrefab, sudokuFieldPanel.transform);
        }
    }
}
