using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace kyoto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            pathetic = new Button[3, 3] {
                { buttonone, buttontwo, buttonthree },
                { buttonfour, buttonfive, buttonsix },
                { buttonseven, buttoneight, buttonnine },
            };
        }
        private List<(int, int)[]> cmb = new List<(int, int)[]> 
        {
            new (int, int)[] { (0, 0), (1, 0), (2, 0) },
            new (int, int)[] { (0, 1), (1, 1), (2, 1) },
            new (int, int)[] { (0, 2), (1, 2), (2, 2) },
            new (int, int)[] { (0, 0), (0, 1), (0, 2) },
            new (int, int)[] { (1, 0), (1, 1), (1, 2) },
            new (int, int)[] { (2, 0), (2, 1), (2, 2) },
            new (int, int)[] { (0, 0), (1, 1), (2, 2) },
            new (int, int)[] { (0, 2), (1, 1), (2, 0) },
        };
        private bool move(int a, int b, string c)
        {
            Button dreamcoretcl = pathetic[a, b];

            if ((string)dreamcoretcl.Content != "") return false;

            dreamcoretcl.Content = c;

            string winner = fwin();

            if (winner != "")
            {
                final(winner);
                return false;
            }

            return true;
        }
        private void movebot()
        {
            List<(int, int)> candidates = botmoveran();
            if (candidates.Count == 0)
            {
                final("");
                return;
            }
            int i = (int)new Random().NextInt64(candidates.Count);
            var (a, b) = candidates[i];
            move(a, b, whoplayer == X ? O : X);
        }

        private void pm(int a, int b)
        {
            bool shouldMove = move(a, b, whoplayer);
            if (shouldMove) movebot();
        }

        private List<(int, int)> botmoveran()
        {
            List<(int, int)> result = new List<(int, int)>();
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    Button but = pathetic[a, b];
                    if ((string)but.Content == "") result.Add((a, b));
                }
            }
            return result;
        }
        private string fwin()
        {
            foreach ((int, int)[] combination in cmb)
            {
                string result = vwincomb(combination);
                if (result != "") return result;
            }
            return "";
        }
        private string vwincomb((int, int)[] combination)
        {
            string prev = "";
            foreach (var (a, b) in combination)
            {
                Button but = pathetic[a, b];
                string cur = (string)but.Content;
                if (cur == "") return "";
                if (prev == "")
                {
                    prev = cur;
                    continue;
                }
                if (prev != cur) return "";
                prev = cur;
            }
            return prev;
        }
        private void choicesign()
        {
            if (whoplayer == "")
            {
                long v = new Random().NextInt64(2);
                whoplayer = v == 0 ? X : O;
                return;
            }
            whoplayer = whoplayer == X ? O : X;
        }
        private void start()
        {
            foreach (Button cell in pathetic)
            {
                cell.Content = "";
                cell.IsEnabled = true;
            }
            choicesign();
            Rezult.Content = "";
        }

        private void buttonpress(object sender, RoutedEventArgs e)
        {
            start();
        }

        private void final(string winner)
        {
            foreach (Button cell in pathetic)
            {
                cell.Content = "";
                cell.IsEnabled = false;
            }
            if (winner == "")
            {
                Rezult.Content = "Ого, ничья получается!";
                return;
            }
            Rezult.Content = $"{(winner == X ? "Крестики" : "Нолики")} победили!";
        }

        private void buttononecl(object mw, RoutedEventArgs meow)
        {
            pm(0, 0);
        }

        private void buttontwocl(object mw, RoutedEventArgs meow)
        {
            pm(0, 1);
        }

        private void buttonthreecl(object mw, RoutedEventArgs meow)
        {
            pm(0, 2);
        }

        private void buttonfourcl(object mw, RoutedEventArgs meow)
        {
            pm(1, 0);
        }

        private void buttonfivecl(object mw, RoutedEventArgs meow)
        {
            pm(1, 1);
        }

        private void buttonsixcl(object mw, RoutedEventArgs meow)
        {
            pm(1, 2);
        }

        private void buttonsevencl(object mw, RoutedEventArgs meow)
        {
            pm(2, 0);
        }

        private void buttoneightcl(object mw, RoutedEventArgs meow)
        {
            pm(2, 1);
        }

        private void buttonninecl(object mw, RoutedEventArgs meow)
        {
            pm(2, 2);
        }

        private const string X = "X";
        private const string O = "O";
        private Button[,] pathetic;
        private string whoplayer = "";
    }
}
