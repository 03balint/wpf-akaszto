using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace akaszto
{
    public partial class MainWindow : Window
    {
        const int MaxHiba = 6;

        Random r = new Random();
        string kivalasztott;
        char[] kitalalt;
        int hibak;
        bool jatekVege;
        List<char> tippeltek = new List<char>();

        string[] szavak = {
            "alma", "korte", "szilva", "banan", "citrom",
            "narancs", "cseresznye", "barack", "szolo", "asztal",
            "szekreny", "ablak", "kutya", "macska", "disznó",
            "farkas", "iskola", "tanar", "konyv", "ceruza",
            "fuzet", "tabla", "varos", "folyo", "tenger",
            "vonat", "repulo", "bicikli", "programozas", "szamitogep",
            "monitor", "billentyuzet", "egerpad", "nyomtato", "hangszoro"
        };

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void Tipp_Click(object sender, RoutedEventArgs e)
        {
            if (jatekVege)
            {
                return;
            }

            if (!IsGuessValid(out char letter))
            {
                return;
            }

            CheckGuess(letter);
        }

        private void UjJatek_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void Sugo_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Visibility = Visibility.Visible;
            mainFrame.Navigate(new SugoPage());
        }

        private void StartNewGame()
        {
            kivalasztott = szavak[r.Next(szavak.Length)].ToLower();
            kitalalt = new string('_', kivalasztott.Length).ToCharArray();
            hibak = 0;
            jatekVege = false;
            tippeltek.Clear();

            Tipp.IsEnabled = true;
            TippText.Clear();
            TippText.Focus();

            UpdateWordDisplay();
            UpdateInfoDisplay();
            DrawFigure();
        }

        private bool IsGuessValid(out char letter)
        {
            letter = ' ';

            if (string.IsNullOrEmpty(TippText.Text))
            {
                MessageBox.Show("Kérlek, adj meg egy betűt!");
                return false;
            }
            if (TippText.Text.Length != 1)
            {
                MessageBox.Show("Pontosan egy betűt adj meg!");
                return false;
            }
            if (!char.IsLetter(TippText.Text[0]))
            {
                MessageBox.Show("Csak betűt adhatsz meg!");
                return false;
            }

            letter = char.ToLower(TippText.Text[0]);

            if (tippeltek.Contains(letter))
            {
                MessageBox.Show("Ezt a betűt már tippelted!");
                return false;
            }

            return true;
        }

        private void CheckGuess(char letter)
        {
            tippeltek.Add(letter);
            TippText.Clear();
            TippText.Focus();

            bool talalt = false;
            for (int i = 0; i < kivalasztott.Length; i++)
            {
                if (kivalasztott[i] == letter)
                {
                    kitalalt[i] = letter;
                    talalt = true;
                }
            }

            if (!talalt)
            {
                hibak++;
            }

            UpdateWordDisplay();
            UpdateInfoDisplay();
            DrawFigure();

            if (IsGameWon())
            {
                EndGame("Gratulálok! Kitaláltad a szót!");
            }
            else if (hibak >= MaxHiba)
            {
                EndGame("Vesztettél!\nA keresett szó: " + kivalasztott);
            }
        }

        private void UpdateWordDisplay()
        {
            SzoHelye.Text = string.Join(" ", kitalalt).ToUpper();
        }

        private void UpdateInfoDisplay()
        {
            Hibak.Text = $"Hibák száma: {hibak} / {MaxHiba}";
            TippeltBetuk.Text = "Használt betűk: " +
                string.Join(", ", tippeltek.Select(c => char.ToUpper(c)));
        }

        private bool IsGameWon()
        {
            return !kitalalt.Contains('_');
        }

        private void EndGame(string uzenet)
        {
            jatekVege = true;
            Tipp.IsEnabled = false;
            MessageBox.Show(uzenet);
        }

        private void DrawFigure()
        {
            Fej.Text = hibak >= 1 ? " O" : "";
            Test1.Text = hibak >= 3 ? "|" : (hibak >= 2 ? " |" : "");
            Test2.Text = hibak >= 2 ? " |" : "";
            Balkez.Text = hibak >= 3 ? "\\" : "";
            Jobbkez.Text = hibak >= 4 ? "/" : "";
            Ballab.Text = hibak >= 5 ? "/" : "";
            Jobblab.Text = hibak >= 6 ? "\\" : "";
        }
    }
}