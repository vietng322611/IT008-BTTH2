namespace Lab02;

public static class Bai02
{
    public static void Run()
    {
        Console.Write("Nhap duong dan den thu muc: ");
        var path = Console.ReadLine()!;
        try
        {
            var folders = Directory.GetDirectories(path).ToHashSet();
            var files = Directory.GetFiles(path).ToHashSet();
            var items = folders.ToList().Concat(files);
            
            foreach (var item in items)
            {
                if (folders.Contains(item))
                    Console.Write("<DIR> ");
                Console.WriteLine(item.Replace(path + "\\", ""));
            }

            Console.WriteLine(files.Count + " file(s)");
            Console.WriteLine(folders.Count + " folder(s)");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Duong dan khong ton tai!");
        }
        catch (IOException)
        {
            Console.WriteLine("Duong dan khong hop le!");
        }
    }
}