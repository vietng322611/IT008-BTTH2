using System;
using System.Collections.Generic;

namespace Lab02;

public class PhanSo
{
    private int _tu = 0;
    private int _mau = 1;

    public int Tu
    {
        get => _tu;
        set
        {
            var gcd = Gcd(_mau, int.Abs(value));
            _tu = value / gcd;
            _mau /= gcd;
        }
    }

    public int Mau
    {
        get => _mau;
        set
        {
            if (value == 0) throw new ArgumentException("Value cannot be 0");
            var gcd = Gcd(_tu, int.Abs(value));
            _mau = int.Abs(value) / gcd;
            _tu /= gcd;
            if (value < 0) _tu = -_tu;
        }
    }

    public PhanSo(int a, int b)
    {
        Tu = a;
        Mau = b;
    }

    public static PhanSo operator+(PhanSo a, PhanSo b)
    {
        var tu = a.Tu * b.Mau + b.Tu * a.Mau;
        var mau = a.Mau * b.Mau;
        return new PhanSo(tu, mau);
    }

    public static PhanSo operator-(PhanSo a, PhanSo b)
    {
        var tu = a.Tu * b.Mau - b.Tu * a.Mau;
        var mau = a.Mau * b.Mau;
        return new PhanSo(tu, mau);
    }
    
    public static PhanSo operator*(PhanSo a, PhanSo b)
    {
        return new PhanSo(a._tu * b._tu, a.Mau * b.Mau);
    }
    
    public static PhanSo operator/(PhanSo a, PhanSo b)
    {
        return new PhanSo(a.Tu * b.Mau, b.Tu * a.Mau);
    }
    
    public static bool operator==(PhanSo a, PhanSo b)
    {
        return (a.Tu == b.Tu) && (a.Mau == b.Mau);
    }
    
    public static bool operator>(PhanSo a, PhanSo b)
    {
        return a.Tu * b.Mau > b.Tu * a.Mau;
    }
    
    public static bool operator<(PhanSo a, PhanSo b)
    {
        return !(a > b || a == b);
    }
    
    public static bool operator!=(PhanSo a, PhanSo b)
    {
        return !(a == b);
    }

    private static int Gcd(int a, int b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return Math.Abs(a);
    }

    public override string ToString()
    {
        return $"{Tu}/{Mau}";
    }
}

public static class Bai04
{
    public static void Run()
    {
        Console.Write("Nhap phan so thu nhat: ");
        var a = NhapPhanSo();
        Console.Write("Nhap phan so thu hai: ");
        var b = NhapPhanSo();
        
        Console.WriteLine($"{a} + {b} = {a + b}");
        Console.WriteLine($"{a} - {b} = {a - b}");
        Console.WriteLine($"{a} * {b} = {a * b}");
        Console.WriteLine($"{a} : {b} = {a / b}");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();

        Console.Write("Nhap so phan so: ");
        ReadInput("Nhap 1 so lon hon 0: ", out var n);

        var arr = new List<PhanSo>();
        for (var i = 0; i <n; i++)
        {
            Console.Write($"Nhap phan so thu {i+1}: ");
            arr.Add(NhapPhanSo());
        }
        arr.Sort((x, y) =>
        {
            if (x > y) return 1;
            if (x == y) return 0;
            return -1;
        });
        
        Console.WriteLine("Phan so lon nhat: " + arr[n-1]);
        Console.Write("Mang phan so sau khi sap xep: ");
        foreach (var ps in arr)
        {
            Console.Write(ps + " ");
        }
    }

    private static PhanSo NhapPhanSo()
    {
        while (true)
        {
            var ps = Console.ReadLine()!;
            ps = ps.Trim();
            var numbers = ps.Split("/");
            if (numbers.Length is 1 or 2)
            {
                if (int.TryParse(numbers[0], out var a))
                {
                    if (numbers.Length == 1)
                        return new PhanSo(a, 1);
                    if (int.TryParse(numbers[1], out var b))
                        if (b != 0)
                            return new PhanSo(a, b);
                }
            }
            Console.Write("Phan so khong hop le, nhap lai: ");
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
}