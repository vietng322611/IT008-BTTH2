using System;
using System.Globalization;

namespace Lab02;

public static class Bai01
{
    public static void Run()
    {
        Console.Write("Nhap thang va nam (mm/yyyy): ");
        var input = Console.ReadLine();
        DateTime dt;
        while (!TryParseDatetime(input!, out dt))
        {
            Console.Write("Ngay thang khong hop le, nhap lai: ");
            input = Console.ReadLine();
        }

        var month = dt.Month;
        var year = dt.Year;
        dt = new DateTime(year, month, 1);
        var dow = DayOfWeek(1, month, year);

        Console.WriteLine(" Sun  Mon  Tue  Wed  Thu  Fri  Sat");

        var padding = dow * 5;
        Console.Write("".PadLeft(padding));
        
        while (dt.Month == month)
        {
            var trailing = dt.Day > 9 ? " " : "  ";
            Console.Write("  " + dt.Day + trailing);
            dow++;
            dt = dt.AddDays(1);
            if (dow < 7) continue;
            Console.WriteLine();
            dow = 0;
        }
    }

    private static bool TryParseDatetime(string datetime, out DateTime result)
    {
        result = default;
        var parts = datetime.Replace(" ", "").Split('/');
        if (parts.Length != 2) return false;
        
        if (!int.TryParse(parts[0], out var month)) return false;
        if (!int.TryParse(parts[1], out var year)) return false;
        
        try
        {
            result = new DateTime(year, month, 1);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    private static int DayOfWeek(int day, int month, int year)
    {
        int[] t = [0, 3, 2, 5, 0, 3, 5, 1, 4, 6, 2, 4];
        year -= (month < 3) ? 1 : 0;
        
        return (year + year/4 - year/100 + year/400 + t[month-1] + day) % 7;
    }
}