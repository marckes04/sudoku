using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FieldPrefabObject {
    private int _row;
    private int _column;
    private GameObject _instance;

    public FieldPrefabObject(GameObject instance, int row, int column)
    {
        _instance = instance;
        _row = row;
        _column = column;
    }

    public bool IsChangeAble = true;


    public bool TryGetTextByName(string name, out Text text)
    {
        text = null;
        Text[] texts = _instance.GetComponentsInChildren<Text>();
        foreach (var currentText in texts)
        {
            if(currentText.name.Equals(name))
            {
                text = currentText;
                return true;
            }
        }
        return false;
    }


    public int Row { get => _row; set => _row = value; }
    public int Column { get => _column; set => _column = value; }

    public void SetHoverMode()
    {
        _instance.GetComponent<Image>().color = new Color(0.7f,0.99f,0.99f);
    }

    public void UnSetHoverMode()
    {
        _instance.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

    public void SetNumber(int number)
    {
        if(TryGetTextByName("Value", out Text text))
        {
            text.text = number.ToString();
            for (int i = 0; i < 10; i++)
            {
                if(TryGetTextByName($"Number_{i}", out Text textNumber))
                {
                    textNumber.text = "";
                }
            }
        }
    }

    public void SetSmallNumber(int number)
    {
        if(TryGetTextByName($"Number_{number}", out Text text))
        {
            text.text = number.ToString();
            if(TryGetTextByName("Value", out Text textValue))
            {
                textValue.text = "";
            }
        }
    }

}
