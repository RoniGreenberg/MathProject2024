using System;
using System.Windows;
using System.Windows.Controls;

namespace MathProject
{
    public partial class MainWindow : Window
    {
        private int grade;
        private int totalQuestions = 10; // כמות השאלות
        public Frame MainFrame { get; set; }

        // העברה מ-WelcomePage ל-GamePage
        MainWindow main = (MainWindow)Application.Current.MainWindow;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame = mainFrame; //זה יתלווה לשם ה-XAML 

            SongMediaElement.Play(); //הפעלת השיר
        }

        private void Button_ClickA(object sender, RoutedEventArgs e)
        {
            grade = 1; //עבור כיתה א יהיו תרגילי חיבור
            StartGame(grade, '+');
        }

        private void Button_ClickB(object sender, RoutedEventArgs e)
        {
            grade = 2; //עבור כיתה ב יהיו תרגילי חיסור
            StartGame(grade, '-');
        }

        private void Button_ClickC(object sender, RoutedEventArgs e)
        {
            grade = 3; //עבור כיתה ג יהיו תרגילי כפל
            StartGame(grade, '*');
        }

        private void Button_ClickD(object sender, RoutedEventArgs e)
        {
            grade = 4; //עבור כיתה ד יהיו תרגילי חילוק
            StartGame(grade, '/');
        }

        private void StartGame(int grade, char operation)
        {
            WindowOfTheGame gameWindow = new WindowOfTheGame(grade, totalQuestions, operation);
            gameWindow.ShowDialog();

            // הצגת הציון הסופי בסיום עשר השאלות
            MessageBox.Show($"Your final score: {gameWindow.Score} out of {totalQuestions}");
            gameWindow.Close(); // ואז אני סוגרת את חלון המשחק 

            return;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void mainFrame_Navigated_1(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

    }
}
