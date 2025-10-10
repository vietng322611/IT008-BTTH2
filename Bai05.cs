using System.Numerics;

namespace Lab02;

public static class Utils
{
    public static T ReadInput<T>(string errorMsg, out T num) where T: INumber<T>
    {
        while (!T.TryParse(Console.ReadLine(), null, out num!))
            Console.Write(errorMsg);
        return num;
    }
}

public class KhuDat
{
    protected string diaDiem = "";
    protected int giaBan;
    protected double dienTich;
    
    public string DiaDiem => diaDiem;
    public int GiaBan => giaBan;
    public double DienTich => dienTich;

    public virtual void GetInput()
    {
        Console.Write("Nhap dia diem: ");
        diaDiem = Console.ReadLine()!;
        
        Console.Write("Nhap gia ban: ");
        Utils.ReadInput("Gia ban khong hop le, nhap lai: ", out giaBan);
        
        Console.Write("Nhap dien tich: ");
        Utils.ReadInput("Dien tich khong hop le, nhap lai: ", out dienTich);
    }
    
    public virtual void Print()
    {
        Console.WriteLine("Dia diem: " + diaDiem);
        Console.WriteLine("Gia ban: " + giaBan);
        Console.WriteLine("Dien tich: " + dienTich);
    }
}

public class NhaPho : KhuDat
{
    private int _namXayDung;
    private int _soTang;

    public int NamXayDung => _namXayDung; 
    public int SoTang => _soTang;

    public override void GetInput()
    {
        base.GetInput();
        Console.Write("Nhap nam xay dung: ");
        Utils.ReadInput("Nam xay dung khong hop le, nhap lai: ", out _namXayDung);
        
        Console.Write("Nhap so tang: ");
        Utils.ReadInput("So tang khong hop le, nhap lai: ", out _soTang);
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine("Nam xay dung: " + _namXayDung);
        Console.WriteLine("So tang: " + _soTang);
    }
}

public class ChungCu : KhuDat
{
    private int _tang;

    public int Tang => _tang;

    public override void GetInput()
    {
        base.GetInput();
        Console.Write("Nhap tang: ");
        Utils.ReadInput("Tang khong hop le, nhap lai: ", out _tang);
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine("Tang: " + _tang);
    }
}

public static class Bai05
{
    public static void Run()
    {
        List<KhuDat> nha = [];
        Console.Write("Nhap so luong can quan ly: ");
        Utils.ReadInput("Gia tri khong hop le, nhap lai: ", out int n);
        Console.Clear();
        
        for (var i = 0; i < n; i++)
        {
            KhuDat? chung = null;
            Console.WriteLine($"So thu tu: {i+1}");
            Console.Write("Nhap loai can quan ly (khu dat, nha pho, chung cu): ");
            while (chung == null)
            {
                var loai = Console.ReadLine()!;
                switch (loai)
                {
                    case "khu dat":
                        chung = new KhuDat();
                        break;
                    case "nha pho":
                        chung = new NhaPho();
                        break;
                    case "chung cu":
                        chung = new ChungCu();
                        break;
                    default:
                        Console.Write("Khong hop le, nhap lai: ");
                        break;
                }
            }
            chung.GetInput();
            nha.Add(chung);
            Console.Clear();
        }

        Console.WriteLine("Danh sach khu dat, nha pho, chung cu: ");
        foreach (var item in nha)
        {
            switch (item)
            {
                case NhaPho:
                    Console.WriteLine("Loai: Nha pho");
                    break;
                case ChungCu:
                    Console.WriteLine("Loai: Chung cu");
                    break;
                default:
                    Console.WriteLine("Loai: Khu dat");
                    break;
            }

            item.Print();
            Console.WriteLine("-----------------------------------");
        }
        var tongGia = nha.Sum(item => item.GiaBan);
        Console.WriteLine("Tong gia ban: " + tongGia);
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();

        Console.WriteLine("Danh sach khu dat co dien tich > 100m2 / nha pho co dien tich > 60m2 va nam xay dung >= 2019:");
        foreach (var item in nha.Where(item => item is not ChungCu))
        {
            if (item is NhaPho nhaPho)
            {
                if ((!(nhaPho.DienTich > 60)) || (nhaPho.NamXayDung < 2019)) continue;
                Console.WriteLine("Loai: Nha pho");
            }
            else
            {
                if (item.DienTich <= 100) continue;
                Console.WriteLine("Loai: Khu dat");
            }
            item.Print();
            Console.WriteLine("-----------------------------------");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();

        Console.WriteLine("Tim kiem");
        Console.Write("Nhap dia diem: ");
        var diaDiem = Console.ReadLine()!;
        
        Console.Write("Nhap gia: ");
        Utils.ReadInput("Gia tri khong hop le, nhap lai: ", out int gia);
        
        Console.Write("Nhap dien tich: ");
        Utils.ReadInput("Gia tri khong hop le, nhap lai: ", out int dienTich);
        
        foreach (var item in nha
                     .Where(item => string.Equals(item.DiaDiem, diaDiem, StringComparison.CurrentCultureIgnoreCase))
                     .Where(item => item.GiaBan <= gia)
                     .Where(item => item.DienTich >= dienTich)
                 )
        {
            switch (item)
            {
                case NhaPho:
                    Console.WriteLine("Loai: Nha pho");
                    break;
                case ChungCu:
                    Console.WriteLine("Loai: Chung cu");
                    break;
                default:
                    Console.WriteLine("Loai: Khu dat");
                    break;
            }

            item.Print();
            Console.WriteLine("-----------------------------------");
        }
    }
}