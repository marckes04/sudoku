using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class English_SudokuGenerator 
{
   public static English_SudokuObject CreateSudokuObject()
    {
        _finalSodokuObject = null;
        English_SudokuObject english_sudokuObject = new English_SudokuObject();
        CreateRandomGroups(english_sudokuObject);
        if(TryToSolve(english_sudokuObject))
        {
            english_sudokuObject = _finalSodokuObject;
        }
        else
        {
            throw new System.Exception("Something went wrong");
        }
        return english_sudokuObject;
    }

    private static English_SudokuObject _finalSodokuObject;

    private static bool TryToSolve(English_SudokuObject english_SudokuObject)
    {

        //Find Empty Fields which can be fills
        if(HasEmptyFieldsToFill(english_SudokuObject, out int row, out int column))
        {
            List<int> possibleValues = GetPossibleValues(english_SudokuObject,row,column);
            foreach (int possibleValue in possibleValues) 
            { 
                English_SudokuObject nextSodokuObject = new English_SudokuObject();
                nextSodokuObject.Values = (int[,]) english_SudokuObject.Values.Clone();
                nextSodokuObject.Values[row,column] = possibleValue;
               if(TryToSolve(nextSodokuObject))
                {
                   return true;
                }
            }
        }

        // Has sodukuobject empty fields

        if (HasEmptyFields(english_SudokuObject))
        {
            return false;
        }
        _finalSodokuObject = english_SudokuObject;
        return true;
        // finish
    }

    private static bool HasEmptyFields(English_SudokuObject english_SudokuObject)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (english_SudokuObject.Values[i, j] == 0)
                {

                   return true;

                }
            }
        }
        return false;
    }

    private static List<int> GetPossibleValues(
        English_SudokuObject english_SudokuObject, int row, int column)
    {
        List<int> possibleValues = new List<int>();
        for (int value = 1; value < 10; value++)
        {
            if(english_SudokuObject.IsPossibleNumberInPosition(value, row, column))
            {
                possibleValues.Add(value);
            }
        }
        return possibleValues;
    }
    private static bool HasEmptyFieldsToFill(
        English_SudokuObject english_SudokuObject, out int row, out int column) 
    {
        row = 0; 
        column = 0;
        int amountOfPossibleValues = 10;
        for (int i = 0;i <9; i++)
        {
            for (int j = 0;j < 9; j++)
            {
                if (english_SudokuObject.Values[i,j] == 0)
                {

                    int currentAmount = GetPossibleAmountOfValues(english_SudokuObject, i, j);
                    if ( currentAmount!=0)
                    {
                        if(currentAmount < amountOfPossibleValues)
                        {
                            amountOfPossibleValues = currentAmount;
                            row = i;
                            column = j;
                        }
                    }
                   
                }
            }
        }
        if (amountOfPossibleValues == 10)
        {
            return false;
        }
        return true;
    }

    private static int GetPossibleAmountOfValues(
        English_SudokuObject english_SudokuObject, int row, int column) 
    {
        int amount = 0;
        for (int value = 0; value < 9; value++)
        {
            if (english_SudokuObject.IsPossibleNumberInPosition(value, row, column))
            {
 amount++;
            }
        }
        return amount;
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
