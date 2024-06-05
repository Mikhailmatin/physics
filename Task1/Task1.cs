namespace DefaultNamespace;

public class Tsk1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            QT.Text = "13860";
            QpchT.Text = "11300";
            FT.Text = "50";
            MT.Text = "5200";
            AT.Text = "0.5";
            BT.Text = "6";
            LT.Text = "2.2";
            GruntCB.SelectedIndex = 2;
        }

        Dictionary<string, double> grunt = new Dictionary<string, double>()
        {
            { "Песок крупный", 4 },
            { "Песок средней крупности", 3 },
            { "Песок мелкий маловлажный", 2 },
            { "Песок мелкий насыщеный водой", 3.5 },
            { "Песок пылевлажный маловлажный", 2.25 },
            { "Песок пылевлажный очень влажный", 1.75 },
            { "Песок пылевлажный насыщеный водой", 1.25 },
            { "Супеси при коэф. пористости 0,5", 3 },
            { "Супеси при коэф. пористости 0,7", 2 },
            { "Суглинки при коэф. пористости 0,5", 2.75 },
            { "Суглинки при коэф. пористости 0,7", 2.15 },
            { "Суглинки при коэф. пористости 1,0", 1.5 },
        };

        Dictionary<int, int> Cz = new Dictionary<int, int>()
        {
            {1, 20 },
            {2, 40 },
            {3, 50 },
            {4, 60 },
            {5, 70 }
        };

        double q, qpch, f, m, a, b, l;

        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                q = Convert.ToDouble(QT.Text);
                qpch = Convert.ToDouble(QpchT.Text);
                f = Convert.ToDouble(FT.Text);
                m = Convert.ToDouble(MT.Text);
                a = Convert.ToDouble(AT.Text);
                b = Convert.ToDouble(BT.Text);
                l = Convert.ToDouble(LT.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка ввода");
                return;
            }

            int g = 981;

            //Динамическая сила, Н
            double F = m * Math.Pow(2 * Math.PI * f, 2) / g;

            //Жесткость пружинных виброизоляторов, Н/м
            double k = qpch / 0.005;

            //Собственная частота колебаний, Гц
            double f0 = 5 / Math.Sqrt(0.5);

            //Коэффициент передачи
            double u = 1 / (Math.Pow(f / f0, 2) - 1);

            //Динамическая сила, передаваемая на основание, Н
            double F0 = F / Math.Pow(u, -1);

            //Минимальная площадь основания виброплощадки, см2
            double S0 = q / (grunt[GruntCB.Text] * Math.Pow(10,5)) * 10000;

            //Жесткость основания под виброплощадкой, Н/м
            double Kf = S0 * Cz[(int)Math.Round(grunt[GruntCB.Text])];

            //Собственная частота колебаний виброплощадки, Гц
            double Ff = 1 / (2 * Math.PI) * Math.Sqrt(Kf * g / (q - qpch));

            //Амплитуда перемещений основания виброплощадки, мм
            double af = F0 / (Kf * (Math.Pow(f, 2) / Math.Pow(Ff, 2) - 1)) * 10;

            //Угловая частота, с^-1
            double w = 2 * Math.PI * f;

            //Минимально необходимый вес фундамента, Н
            double Qf = g / 100 * 0.0005 * k / (0.000009 * Math.Pow(w, 2));

            //Частота колебаний фундамента, Гц
            double ff = 1 / (2 * Math.PI) * (Kf * g * g / (Qf + (q - qpch)));

            double Af = F0 / (Kf * (Math.Pow(50, 2) / ff - 1));

            Result.Text = Convert.ToString(Math.Round(Af, 3));
        }
    }
}