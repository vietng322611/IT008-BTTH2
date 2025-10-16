using System;
using System.Collections.Generic;

namespace Lab02;

public static class Bai03
{
    public static void Run()
    {
        Console.Write("Nhap so hang cua ma tran: ");
        ReadInput("So hang khong hop le, nhap lai: ", out var nRow);
        Console.Write("Nhap so cot cua ma tran: ");
        ReadInput("So cot khong hop le, nhap lai: ", out var nCol);
        
        var matrix = ReadMatrix(nRow, nCol);
        Console.Clear();
        
        while (true)
        {
            var choice = AskChoice();
            switch (choice)
            {
                case 1:
                    PrintMatrix(matrix, nRow, nCol);
                    break;
                case 2:
                    Console.Write("Nhap phan tu muon tim kiem: ");
                    ReadInput("Gia tri khong hop le, nhap lai: ", out var value);
                    var (x, y) = Lookup(matrix, nRow, nCol, value);
                    if (x == -1)
                    {
                        Console.WriteLine("Khong tim thay gia tri " + value);
                    }
                    else
                    {
                        Console.WriteLine($"Gia tri {value} nam o hang {x+1} cot {y+1}");
                    }
                    break;
                case 3:
                    Console.Write("Cac phan tu la so nguyen to: ");
                    Console.WriteLine(string.Join(" ", GetAllPrime(matrix, nRow, nCol)));
                    break;
                case 4:
                    var primes = RowWithMostPrime(matrix, nRow, nCol);
                    if (primes >= 0)
                        Console.WriteLine("Hang co nhieu so nguyen to nhat: " + (primes + 1));
                    else
                        Console.WriteLine("Khong co so nguyen to");
                    break;
                default:
                    return;
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static int AskChoice()
    {
        while (true)
        {
            Console.WriteLine("1. In mang 2 chieu");
            Console.WriteLine("2. Tim kiem phan tu trong mang");
            Console.WriteLine("3. In cac so nguyen to trong mang");
            Console.WriteLine("4. Tim hang co nhieu so nguyen to nhat");
            Console.WriteLine("0. Quit");
            Console.Write("Enter your choice: ");
            int choice = Console.ReadKey().KeyChar;
            if (choice is < '0' or > '4')
            {
                Console.Clear();
                continue;
            }
            Console.WriteLine();
            return choice - '0';
        }
    }
    
    private static void ReadInput(string errorMsg, out int number)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), null, out number))
                if (number > 0) return;
            
            Console.Write(errorMsg);
        }
    }
    
    private static int[,] ReadMatrix(int nRow, int nCol)
    {
        var matrix = new int[nRow, nCol];
        for (var i = 0; i < nRow; i++)
        {
            Console.Write("Nhap hang " + (i + 1) + ": ");
            var j = 0;
            while (j < nCol)
            {
                var line = Console.ReadLine()!;
                foreach (var num in line.Split(" "))
                {
                    if (int.TryParse(num, out matrix[i, j]))
                    {
                        j++;
                        if (j == nCol) break;
                        continue;
                    }
                    Console.WriteLine(num + " co chua ky tu khong hop le, nhap lai hang:");
                    break;
                }
            }
        }

        return matrix;
    }
    
    private static void PrintMatrix(int[,] matrix, int nRow, int nCol)
    {
        for (var i = 0; i < nRow; i++)
        {
            for (var j = 0; j < nCol; j++)
                Console.Write(matrix[i, j] + " ");

            Console.WriteLine();
        }
    }

    private static (int row, int col) Lookup(int[,] matrix, int nRow, int nCol, int value)
    {
        for (var i = 0; i < nRow; i++)
            for (var j = 0; j < nCol; j++)
                if (Math.Abs(matrix[i, j] - value) < 0.001)
                    return (i, j);
        return (-1, -1);
    }

    private static bool IsPrime(int n)
    {
        if (n < 2) return false;
        for (var i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0) return false;
        return true;
    }
    
    private static List<int> GetAllPrime(int[,] matrix, int nRow, int nCol)
    {
        List<int> primes = [];
        for (var i = 0; i < nRow; i++)
            for (var j = 0; j < nCol; j++)
                if (IsPrime(matrix[i, j]))
                    primes.Add(matrix[i, j]);
        return primes;
    }
    
    private static int RowWithMostPrime(int[,] matrix, int nRow, int nCol)
    {
        List<int> primes = [];
        var best = -1;
        for (var i = 0; i < nRow; i++)
        {
            List<int> tmp = [];
            for (var j = 0; j < nCol; j++)
                if (IsPrime(matrix[i, j]))
                    tmp.Add(matrix[i, j]);
            if (!(tmp.Count > primes.Count)) continue;
            primes = tmp;
            best = i;
        }
        return best;
    }
}