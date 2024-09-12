using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class English_SudokuGenerator
{
    public static void CreateSudokuObject(
        out English_SudokuObject finalObject, out English_SudokuObject gameObject)
    {
        _finalSodokuObject = null;
        English_SudokuObject english_sudokuObject = new English_SudokuObject();
        CreateRandomGroups(english_sudokuObject);
        if (TryToSolve(english_sudokuObject))
        {
            english_sudokuObject = _finalSodokuObject;
        }
        else
        {
            throw new System.Exception("Something went wrong");
        }
        finalObject = english_sudokuObject;
        gameObject = RemoveSomeRandomNumbers(english_sudokuObject);
    }

    private static English_SudokuObject RemoveSomeRandomNumbers(English_SudokuObject english_SudokuObject)
    {
        English_SudokuObject newSodokuObject = new English_SudokuObject();
        newSodokuObject.Values = (int[,])english_SudokuObject.Values.Clone();

        // Reinitialize values list to avoid issues on scene reset
        List<Tuple<int,int>> values = GetValues(); // Ensure it's correctly initialized
        int endValueIndex = 10;
        if(EnglishGameSettings.EasyMiddleHard_Number == 1){endValueIndex = 71;}
        if(EnglishGameSettings.EasyMiddleHard_Number == 2){endValueIndex = 61;}
        if (EnglishGameSettings.EasyMiddleHard_Number == 3) { endValueIndex = 51;}

        bool isFinish = false;

        while (!isFinish && values.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, values.Count); // Correct random index range
            // Check the count before getting a random index
            if (values.Count > 0)
            {
               
                if (index >= 0 && index < values.Count)
                {
                    var searchedIndex = values[index]; // Safely access the value

                    // Proceed to look for this index in the Sudoku grid

                   
                        if (index >= 0 && index < values.Count)
                        {
                      
                           // Remove the element safely

                            English_SudokuObject nextSodokuObject = new English_SudokuObject();
                            nextSodokuObject.Values = (int[,])newSodokuObject.Values.Clone();

                          
                                nextSodokuObject.Values[searchedIndex.Item1, searchedIndex.Item2] = 0; // Remove number

                                if (TryToSolve(nextSodokuObject, true))
                                {
                                    newSodokuObject = nextSodokuObject; // Update object if solvable
                                }
                           
                        }
                    
                }
            }

            // End the loop when fewer than 30 values remain
            values.RemoveAt(index);
            if (values.Count < endValueIndex)
            {
                isFinish = true;
            }
       
        }

        return newSodokuObject;
    }


    private static List<Tuple<int,int>> GetValues()
    {
        List<Tuple<int,int>> values = new List<Tuple <int,int>>();

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                values.Add(new Tuple<int, int>(i,j));
            }
        }

        return values;
    }

    private static English_SudokuObject _finalSodokuObject;

    private static bool TryToSolve(English_SudokuObject english_SudokuObject, bool OnlyOne = false)
    {
        // Find empty fields which can be filled
        if (HasEmptyFieldsToFill(english_SudokuObject, out int row, out int column, OnlyOne))
        {
            List<int> possibleValues = GetPossibleValues(english_SudokuObject, row, column);

            foreach (int possibleValue in possibleValues)
            {
                English_SudokuObject nextSodokuObject = new English_SudokuObject();
                nextSodokuObject.Values = (int[,])english_SudokuObject.Values.Clone();
                nextSodokuObject.Values[row, column] = possibleValue;

                if (TryToSolve(nextSodokuObject, OnlyOne))
                {
                    return true;
                }
            }
        }

        // Check if sudoku object still has empty fields
        if (HasEmptyFields(english_SudokuObject))
        {
            return false;
        }

        _finalSodokuObject = english_SudokuObject;
        return true;
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

    private static List<int> GetPossibleValues(English_SudokuObject english_SudokuObject, int row, int column)
    {
        List<int> possibleValues = new List<int>();

        for (int value = 1; value < 10; value++)
        {
            if (english_SudokuObject.IsPossibleNumberInPosition(value, row, column))
            {
                possibleValues.Add(value);
            }
        }

        return possibleValues;
    }

    private static bool HasEmptyFieldsToFill(English_SudokuObject english_SudokuObject, out int row, out int column, bool OnlyOne = false)
    {
        row = 0;
        column = 0;
        int amountOfPossibleValues = 10;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (english_SudokuObject.Values[i, j] == 0)
                {
                    int currentAmount = GetPossibleAmountOfValues(english_SudokuObject, i, j);

                    if (currentAmount != 0 && currentAmount < amountOfPossibleValues)
                    {
                        amountOfPossibleValues = currentAmount;
                        row = i;
                        column = j;
                    }
                }
            }
        }

        if (OnlyOne && amountOfPossibleValues == 1)
        {
            return true;
        }

        return amountOfPossibleValues != 10;
    }

    private static int GetPossibleAmountOfValues(English_SudokuObject english_SudokuObject, int row, int column)
    {
        int amount = 0;

        for (int value = 1; value <= 9; value++)  // Changed from 0-9 to 1-9
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

        int index = UnityEngine.Random.Range(0, values.Count);
        InsertGroup(english_SudokuObject, 1 + values[index]);
        values.RemoveAt(index); // List size is now 2

        index = UnityEngine.Random.Range(0, values.Count);
        InsertGroup(english_SudokuObject, 4 + values[index]);
        values.RemoveAt(index); // List size is now 1

        // Use the remaining item
        InsertGroup(english_SudokuObject, 7 + values[0]);
        values.RemoveAt(0); // List is now empty
    }

    public static void InsertGroup(English_SudokuObject english_SudokuObject, int group)
    {
        english_SudokuObject.GetGroupIndex(group, out int startRow, out int startColumn);

        List<int> values = new List<int>()
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9
        };

        for (int row = startRow; row < startRow + 3; row++)
        {
            for (int column = startColumn; column < startColumn + 3; column++)
            {
                int index = UnityEngine.Random.Range(0, values.Count);
                english_SudokuObject.Values[row, column] = values[index];
                values.RemoveAt(index);
            }
        }
    }
}
