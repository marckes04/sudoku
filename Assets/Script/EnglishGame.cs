using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnglishGame : MonoBehaviour
{
    public GameObject mainPanel;

    public GameObject sudokuFieldPanel;

    public GameObject fieldPrefab;

    public GameObject ControllPanel;

    public GameObject ControllPrefab;

    public Button InformationButton;

    
    private FieldPrefabObject _currentHoveredFieldPrefab;

    void Start()
    {
        CreateFieldPrefabs();
        CreateControlPrefabs();
        CreateSudokuObject();
    }

    private Dictionary<Tuple <int,int>, FieldPrefabObject> _englishFieldPrefabDictionary = 
        new Dictionary<Tuple <int,int>, FieldPrefabObject>();
    
    private English_SudokuObject _currentSudokuObject;

    private void CreateSudokuObject()
    {
        _currentSudokuObject = English_SudokuGenerator.CreateSudokuObject();
        
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                var currentValue = _currentSudokuObject.Values[row,column];
                if (currentValue != 0)
                {
                    FieldPrefabObject fieldObject = _englishFieldPrefabDictionary[new Tuple<int, int>(row, column)];
                    fieldObject.SetNumber(currentValue);
                    fieldObject.IsChangeAble = false;
                }
            }
        }
    }

    private bool IsInformationButtonActive = false;

    public void ClickOn_InformationButton()
    {
        Debug.Log($"Click on InformationButton");
        if (IsInformationButtonActive)
        { 
            IsInformationButtonActive=false;
            InformationButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
        else
        {
            IsInformationButtonActive = true;
            InformationButton.GetComponent<Image>().color = new Color(0.70f, 0.99f,0.99f);
        }
    }



    private void CreateFieldPrefabs()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                // Instantiate the field prefab and parent it to the sudokuFieldPanel
                GameObject instance = GameObject.Instantiate(fieldPrefab, sudokuFieldPanel.transform);

                // Create and store the FieldPrefabObject with a unique grid position (row, column)
                FieldPrefabObject englishFieldPrefabObject = new FieldPrefabObject(instance, row, column);
                _englishFieldPrefabDictionary.Add(new Tuple<int,int>(row, column), englishFieldPrefabObject);

                // Capture the current instance in the closure to avoid capturing the last instance in the loop
                instance.GetComponent<Button>().onClick.AddListener(() => OnClick_FieldPrefab(englishFieldPrefabObject));
            }
        }
    }

    private void CreateControlPrefabs()
    {
        for (int i = 1; i < 10; i++)
        {
           
               
                GameObject instance = GameObject.Instantiate(fieldPrefab, ControllPanel.transform);
               instance.GetComponentInChildren<Text>().text = i.ToString();
               English_ControlPrefabObject english_ControlPrefabObject = new English_ControlPrefabObject();
               english_ControlPrefabObject.number = i; 
               instance.GetComponent<Button>().onClick.AddListener(() => ClickOn_ControlPrefab(english_ControlPrefabObject));
            
        }
    }


    private void ClickOn_ControlPrefab(English_ControlPrefabObject english_ControlPrefab)
    {
        Debug.Log($"Click on control prefab: {english_ControlPrefab.number}");
        if(_currentHoveredFieldPrefab != null)
        {
            if(IsInformationButtonActive)
            {
                _currentHoveredFieldPrefab.SetSmallNumber(english_ControlPrefab.number);
            }
            else
            {
                int currentNumber = english_ControlPrefab.number;
                int row = _currentHoveredFieldPrefab.Row;
                int column = _currentHoveredFieldPrefab.Column;
                if (_currentSudokuObject.IsPossibleNumberInPosition(currentNumber,row,column))
                {
                    _currentHoveredFieldPrefab.SetNumber(english_ControlPrefab.number);
                }
            }
        }
    }

    
    private void OnClick_FieldPrefab(FieldPrefabObject englishFieldPrefabObject)
    {
        Debug.Log($"Clicked on prefab row:{englishFieldPrefabObject.Row}, Column: {englishFieldPrefabObject.Column}");

        if(englishFieldPrefabObject.IsChangeAble)
        {
            // Unset hover mode on the previously hovered field
            if (_currentHoveredFieldPrefab != null)
            {
                _currentHoveredFieldPrefab.UnSetHoverMode();
            }

            // Set hover mode on the currently clicked field
            _currentHoveredFieldPrefab = englishFieldPrefabObject;
            englishFieldPrefabObject.SetHoverMode();
        }
       
    }
}