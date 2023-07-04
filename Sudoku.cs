using System;

public class Sudoku
{
    private int size;
    private int[][] grid;

    public Sudoku()
    {
        size = 9;
        grid = new int[size][];

        for (int i = 0; i < size; i++)
        {
            grid[i] = new int[size];
        }
    }

    public void Input()
    {
        Console.WriteLine("Please enter board data:");

        for (int i = 0; i < size; i++)
        {
            string[] inputArray = Console.ReadLine().Split(' ');
            int[] row = Array.ConvertAll(inputArray, int.Parse);

            for (int j = 0; j < size; j++)
            {
                grid[i][j] = row[j];
            }
        }
    }

    public bool CheckRow(int row, int number)
    {
        for (int i = 0; i < size; i++)
        {
            if (Math.Abs(grid[row][i]) == number)
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckColumn(int column, int number)
    {
        for (int i = 0; i < size; i++)
        {
            if (Math.Abs(grid[i][column]) == number)
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckArea(int startRow, int startColumn, int number)
    {
        for (int i = startRow; i < startRow + 3; i++)
        {
            for (int j = startColumn; j < startColumn + 3; j++)
            {
                if (Math.Abs(grid[i][j]) == number)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void Minus()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (grid[i][j] > 0)
                {
                    grid[i][j] *= -1;
                }
            }
        }
    }

    public void Plus()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (grid[i][j] < 0)
                {
                    grid[i][j] *= -1;
                }
            }
        }
    }

    public bool Solve(int row = 0, int col = 0, bool forward = true)
    {
        if (row == size)
        {
            return true;
        }

        if (grid[row][col] < 0)
        {
            if (forward)
            {
                if (col + 1 == size)
                {
                    return Solve(row + 1, 0, forward);
                }
                else
                {
                    return Solve(row, col + 1, forward);
                }
            }
            else
            {
                if (col - 1 < 0)
                {
                    return Solve(row - 1, grid[0].Length - 1, forward);
                }
                else
                {
                    return Solve(row, col - 1, forward);
                }
            }
        }
        else
        {
            if (row == -1)
            {
                return false;
            }

            int? number = null;

            for (int i = grid[row][col]; i < 10; i++)
            {
                if (CheckRow(row, i) && CheckColumn(col, i) && CheckArea(row / 3 * 3, col / 3 * 3, i))
                {
                    number = i;
                    break;
                }
            }

            if (number.HasValue)
            {
                grid[row][col] = number.Value;
                forward = true;

                if (col + 1 == size)
                {
                    return Solve(row + 1, 0, forward);
                }
                else
                {
                    return Solve(row, col + 1, forward);
                }
            }
            else
            {
                grid[row][col] = 0;
                forward = false;

                if (col - 1 < 0)
                {
                    return Solve(row - 1, grid[0].Length - 1, forward);
                }
                else
                {
                    return Solve(row, col - 1, forward);
                }
            }
        }
    }

    public void Print()
    {
        Console.WriteLine("Answer:");
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write(grid[i][j] + " ");
            }
            Console.WriteLine();
        }
    }

    public static void Main(String[] args)
    {
        Sudoku solver = new Sudoku();
        solver.Input();
        solver.Minus();
        bool solved = solver.Solve();
        solver.Plus();
        
        if (solved)
        {
            solver.Print();
        }
        else
        {
            Console.WriteLine("The sudoku is unsolvable.");
        }
    }
}