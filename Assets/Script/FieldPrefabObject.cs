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
        _instance.GetComponentInChildren<Text>().text = number.ToString();
    }

}
