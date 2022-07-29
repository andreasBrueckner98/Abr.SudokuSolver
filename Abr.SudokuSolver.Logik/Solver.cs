using System;
using System.Collections.Generic;
using System.Linq;

namespace Abr.SudokuSolver.Logik
{
    public class Solver
    {
        private const int c_sudokuFieldSize = 9;
        private readonly int[][] _sudokuField;
        private readonly int _maxCountTries;
        private int _currentCountTries;

        public event EventHandler<(int, int, int)> SolvedNumber;
        public event EventHandler<int> SudokuSolved;
        public event EventHandler<int> CurrentRountChanged;

        public Solver(int[][] sudokuField, int maxCountTries)
        {
            _sudokuField = sudokuField;
            _maxCountTries = maxCountTries;
        }

        public int[][] Solve()
        {
            bool anyFieldUnassigned;

            while ((anyFieldUnassigned = IsAnyFieldUnassigned()) && _currentCountTries <= _maxCountTries)
            {
                _currentCountTries++;
                CurrentRountChanged?.Invoke(this, _currentCountTries);

                for (int column = 0; column < c_sudokuFieldSize; column++)
                {
                    for (int row = 0; row < c_sudokuFieldSize; row++)
                    {
                        if (_sudokuField[column][row] == 0)
                        {
                            var possibleNumbers = new List<int>(9);

                            for (int number = 1; number < c_sudokuFieldSize + 1; number++)
                            {
                                if (!IsNumberInColumn(number, column) && !IsNumberInRow(number, row) && !IsNumberInField(number, column, row))
                                {
                                    possibleNumbers.Add(number);
                                }
                            }

                            if (possibleNumbers.Count == 1)
                            {
                                var number = possibleNumbers.FirstOrDefault();
                                _sudokuField[column][row] = number;
                                SolvedNumber?.Invoke(this, (column, row, number));
                            }
                        }
                    }
                }
            }

            if (!anyFieldUnassigned)
                SudokuSolved?.Invoke(this, _currentCountTries);

            return _sudokuField;
        }

        private bool IsAnyFieldUnassigned()
        {
            for (int column = 0; column < c_sudokuFieldSize; column++)
            {
                for (int row = 0; row < c_sudokuFieldSize; row++)
                {
                    if (_sudokuField[column][row] == 0)
                        return true;
                }
            }

            return false;
        }

        private bool IsNumberInColumn(int number, int column)
        {
            for (int i = 0; i < c_sudokuFieldSize; i++)
            {
                if (_sudokuField[column][i] == number)
                    return true;
            }

            return false;
        }

        private bool IsNumberInRow(int number, int row)
        {
            for (int i = 0; i < c_sudokuFieldSize; i++)
            {
                if (_sudokuField[i][row] == number)
                    return true;
            }

            return false;
        }

        private bool IsNumberInField(int number, int columnIndex, int rowIndex)
        {
            int[] possibleColumns = new int[3];
            int[] possibleRows = new int[3];

            // determine columns
            if (columnIndex <= 2)
            {
                possibleColumns[0] = 0;
                possibleColumns[1] = 1;
                possibleColumns[2] = 2;
            }
            else if (columnIndex <= 5)
            {
                possibleColumns[0] = 3;
                possibleColumns[1] = 4;
                possibleColumns[2] = 5;
            }
            else
            {
                possibleColumns[0] = 6;
                possibleColumns[1] = 7;
                possibleColumns[2] = 8;
            }

            // determine rows
            if (rowIndex <= 2)
            {
                possibleRows[0] = 0;
                possibleRows[1] = 1;
                possibleRows[2] = 2;
            }
            else if (rowIndex <= 5)
            {
                possibleRows[0] = 3;
                possibleRows[1] = 4;
                possibleRows[2] = 5;
            }
            else
            {
                possibleRows[0] = 6;
                possibleRows[1] = 7;
                possibleRows[2] = 8;
            }

            // check
            foreach (var column in possibleColumns)
            {
                foreach (var row in possibleRows)
                {
                    if (_sudokuField[column][row] == number)
                        return true;
                }
            }

            return false;
        }

        public void PrintOnConsole()
        {
            for (int column = 0; column < c_sudokuFieldSize; column++)
            {
                for (int row = 0; row < c_sudokuFieldSize; row++)
                {
                    Console.Write(_sudokuField[column][row]);
                    Console.Write(" | ");
                }

                Console.WriteLine();
            }
        }
    }
}