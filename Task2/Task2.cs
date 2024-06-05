namespace DefaultNamespace;

public class Task2
{
    {
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Определить, какая часть динамических сил от вибрации частотой f Гц, создающейся электродвигателем, будет изолирована прокладкой из резины средней жесткости толщиной h, см");

            Console.WriteLine("\nВведите частоту вибрации f, Гц:");
            int f = int.Parse(Console.ReadLine());

            Console.WriteLine("\nВведите толщину прокладки h, см:");
            int h = int.Parse(Console.ReadLine());
            
            double Xct = 0.015 * h;
            
            int n = f * 60;
            
            int k = (int) Math.Round(9 * Math.Pow(10, 6) / (Xct * Math.Pow(n, 2)));

            Console.WriteLine($"\nПрокладка из резины толщиной {h} см примерно {k}% динамических сил от вибрации частотой {f} Гц будет передано основанию. а {100 - k}% будет изолировано");

            Console.ReadKey();
        }
    }
}