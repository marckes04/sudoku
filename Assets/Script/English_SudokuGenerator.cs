using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class English_SudokuGenerator 
{
   public static English_SudokuObject CreateSudokuObject()
    {
        English_SudokuObject english_sudokuObject = new English_SudokuObject();
        CreateRandomGroups(english_sudokuObject);
        return english_sudokuObject;
    }

    public static void CreateRandomGroups(English_SudokuObject english_SudokuObject) 
    {
        List<int> values = new List<int>() { 0, 1, 2 };
        int index  = Random.Range(0, values.Count);
        InsertGroup(english_SudokuObject, 1 + values[index]);
        values.RemoveAt(index);

        index = Random.Range(0, values.Count);
        InsertGroup(english_SudokuObject, 4 + values[index]);
        values.RemoveAt(index);

        InsertGroup(english_SudokuObject, 7 + values[index]);
        values.RemoveAt(index);

    }

    public static void InsertGroup(English_SudokuObject english_SudokuObject, int group)
    {
        english_SudokuObject.GetGroupIndex(group, out int startRow, out int startColumn);
        List<int> values = new List<int>()
        {
            1,2,3,4,5,6,7,8,9
        };
        for (int row = startRow; row < startRow + 3; row++) 
        { 
            for(int column = startColumn; column < startColumn+3; column++)
            {
                int index = Random.Range(0, values.Count);
                english_SudokuObject.Values[row, column]  = values[index];
                values.RemoveAt(index);
            }
        }
    }

}
