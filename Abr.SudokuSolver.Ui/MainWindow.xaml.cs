using Abr.SudokuSolver.Logik;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Abr.SudokuSolver.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public int[][] Arr { get; set; } = new int[][]
        {
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };

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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Arr = testField;

            var solver = new Solver(Arr, 30);
            solver.SudokuSolved -= Solver_SudokuSolved;
            solver.SudokuSolved += Solver_SudokuSolved;
            var solved = solver.Solve();
            Arr = solved;

            OnPropertyChanged(nameof(Arr));
        }

        private void Solver_SudokuSolved(object? sender, int e)
        {
            MessageBox.Show("Sudoku gelöst.");
        }
    }
}
