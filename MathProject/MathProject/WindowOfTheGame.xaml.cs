using System;
using System.ComponentModel;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MathProject
{
    public partial class WindowOfTheGame : Window
    {
       private bool wrongAnswerImageShown = false;
       private int totalQuestions;


        //תשובות רנדומליות שיוגרלו כאשר המשתמש צודק או טועה
        private string[] correctMessages = { "Well done!", "Excellent!", "The Kstisat Sartan on me;)", "Great job!", "Perfect!", "You are a genius! Come to Hasartan Haparikh:)", "You nailed it!" };
        private string[] incorrectMessages = { "That's not correct.", "Nice try, but not quite. ", "Keep going!", "Better luck next time." };

        public Frame MainFrame { get; set; }
        private OneGame game;
        private int score = 0;


        public int Score { get { return score; } }

        public WindowOfTheGame(int grade, int totalQuestions, char operation)
        {
            InitializeComponent();

            this.totalQuestions = totalQuestions;
            game = new OneGame(grade, operation);
            DisplayQuestion();
        }
        
        //פעולה המציגה את השאלות על המסך
        private void DisplayQuestion()
        {
            questionTextBlock.Text = $"How much is {game.Number1} {game.GetOperationSymbol()} {game.Number2}?";

            // איפוס הערך של wrongAnswerImageShown בכל פעם שהמשתמש מקבל שאלה חדשה
            wrongAnswerImageShown = false;
        }

        //פעולה המציגה על המסך את ההודעה והתמונה שמותאמת למשתמש לפי תשובתו לשאלה
        private void CheckAnswer_Click(object sender, RoutedEventArgs e)
        {
            int userAnswer;
            if (int.TryParse(answerTextBox.Text, out userAnswer))
            {
                HideAllImages(); // הסתרת כל התמונות

                //אם המשתמש צודק
                if (userAnswer == game.CorrectAnswer)
                {
                    resultTextBlock.Text = GetRandomCorrectMessage(); //מודפסת למשתמש תשובה רנדומלית
                    score++;
                    ShowAfterCheckAnswerImage(0);
                }

                //אם המשתמש טועה
                else
                {
                    if (IsReasonableWrongAnswer(userAnswer))
                    {
                        ShowReasonableWrongAnswerImage(); 
                    }
                    else
                    {
                        ShowWrongAnswerImage();
                    }
                    //מודפסת למשתמש תשובה רנומלית לא נכונה
                    resultTextBlock.Text = GetRandomIncorrectMessage() + " The right answer is " + game.CorrectAnswer + ".";

                }

                if (--totalQuestions > 0)
                {
                    game.GenerateNewProblem();
                    DisplayQuestion();

                    answerTextBox.Clear();

                }
                else
                {
                    if (score == 10)
                    {
                        finishButton.Visibility = Visibility.Visible;
                        BOBfinishButton.Visibility = Visibility.Visible;
                        ShowCompletionGif();
                        HideLikeImage();
                        HideCheckAnswerButton();
                    }
                    else
                    {
                        this.Close();
                        HideCheckAnswerButton();
                        finishButton.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                resultTextBlock.Text = "Invalid input. Please enter a valid number."; //אם מה שהמשתמש הזין זה לא מספר
                HideAllImages(); // הסתרת כל התמונות

                // הצגת תמונת טעות
                ShowWrongAnswerImage();
            }
        }


        //הצגת הגיף
        private void ShowCompletionGif()
        {
            completionGif.Visibility = Visibility.Visible;
        }

        private void ShowAfterCheckAnswerImage(int flag)
        {
            afterCheckAnswerImage.Visibility = Visibility.Visible;

            if (flag == 0)
            {
                 afterCheckAnswerImage.Visibility = Visibility.Visible;
            }

            else
            {
                afterCheckAnswerImage.Visibility = Visibility.Collapsed;
            }
        }

        //פעולה המציגה את התמונה שמוצגת כאשר המשתמש טועה
        private void ShowWrongAnswerImage()
        {
            wrongAnswerImage.Visibility = Visibility.Visible;   
        }

        private void ShowReasonableWrongAnswerImage()
        {
            reasonableWrongAnswerImage.Visibility = Visibility.Visible;
        }

        //פעולה המסתיר את התמונה שמוצגת כאשר המשתמש צודק
        private void HideLikeImage()
        {
            afterCheckAnswerImage.Visibility = Visibility.Collapsed;
        }
   

        //פעולה המסתירה את כל התמונות
        private void HideAllImages()
        {
            completionGif.Visibility = Visibility.Collapsed;
            afterCheckAnswerImage.Visibility = Visibility.Collapsed;
            wrongAnswerImage.Visibility = Visibility.Collapsed;
            reasonableWrongAnswerImage.Visibility = Visibility.Collapsed;
        }
        private bool IsReasonableWrongAnswer(int userAnswer)
        {
            return Math.Abs(userAnswer - game.CorrectAnswer) > 5;
        }

        //פעולה המסתירה את הכפתור שהמשתמש צריך ללחוץ עליו כדי לבדוק האם תשובתו נכונה
        private void HideCheckAnswerButton()
        {
            checkAnswerButton.Visibility = Visibility.Collapsed;
        }

        //תשובה נכונה רדנומלית שמוגרלת
        private string GetRandomCorrectMessage()
        {
            Random random = new Random();
            int index = random.Next(correctMessages.Length);
            return correctMessages[index];
        }

        //תשובה לא נכונה רדנומלית שמוגרלת
        private string GetRandomIncorrectMessage()
        {
            Random random = new Random();
            int index = random.Next(incorrectMessages.Length);
            return incorrectMessages[index];
        }

        private bool isResultDisplayed;

        public bool IsResultDisplayed
        {
            get { return isResultDisplayed; }
            set
            {
                isResultDisplayed = value;
                OnPropertyChanged(nameof(IsResultDisplayed));
            }
        }

        // פעולה לעדכון של המעמד בעקבות שינוי
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
