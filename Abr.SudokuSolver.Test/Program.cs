using System;
using Abr.SudokuSolver.Logik;

int[][] testField = new int[][]
{
    new int[] { 0, 9, 0, 0, 0, 0, 7, 0, 2 },
    new int[] { 0, 0, 2, 9, 0, 0, 0, 0, 4 },
    new int[] { 8, 0, 0, 4, 6, 2, 3, 0, 0 },
    new int[] { 0, 0, 1, 2, 0, 6, 8, 9, 3 },
    new int[] { 0, 4, 0, 5, 0, 8, 0, 7, 0 },
    new int[] { 6, 8, 3, 7, 0, 9, 4, 0, 0 },
    new int[] { 0, 0, 5, 3, 7, 1, 0, 0, 8 },
    new int[] { 1, 0, 0, 0, 0, 4, 5, 0, 0 },
    new int[] { 3, 0, 4, 0, 0, 0, 0, 6, 0 },
};

var solver = new Solver(testField, 30);
solver.SolvedNumber += (sender, e) => Console.WriteLine($"Solved Number. (Column: {e.Item1}, Row: {e.Item2}, Number: {e.Item3}");
solver.SudokuSolved += (sender, e) => Console.WriteLine($"Solved whole Sudoku. (Needed tries: {e})");
solver.CurrentRountChanged += (sender, e) => Console.WriteLine($"### Round {e} started... ###");

Console.WriteLine("Start solving the Sudoku...");
solver.Solve();
Console.WriteLine($"Finished solving the Sudoku.:\n");
solver.PrintOnConsole();