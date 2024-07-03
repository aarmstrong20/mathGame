using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace mathGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        #region otherWindows
        FinalScores finalScores;
        background BG;
        #endregion
        #region variables 
        string MathType;
        string Name;
        int Age;
        int seconds = 0;
        int correct=0;
        int incorrect = 0;
        int num1;
        int num2;
        int numQuestionsAnswered = 1;
        bool gameStarted = false;
        #endregion
        /// <summary>
        /// start Console
        /// </summary>
        public GameWindow(string mathType, string name, int age)
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose; 
            BG = new background();
            MathType = mathType;
            Name = name;
            Age = age;

            SoundPlayer simpleSound = new SoundPlayer("StartGame.wav");
            simpleSound.Play();

        }

        /// <summary>
        /// start Timer and find first Question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //first question
            gameStarted = true;
            newQuestions();
            //creates timer
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            //start timer
            timer.Start();
            //show timer
            timer.Tick += _timer_Tick; 
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            seconds++;
            timerLabel.Content = seconds + " seconds";
        }

        /// <summary>
        /// generate questions
        /// </summary>
       public void newQuestions()
       {
           Random r = new Random();
           num1 = r.Next(1, 10);
           num2 = r.Next(1, 10);
       
           QuestionLabel.Content = "Question " + numQuestionsAnswered++ + ": " + num1 + " " + MathType + " " + num2 + " = ";
       }

        /// <summary>
        /// submit answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int ANum;
            //check if number
            try
            {
                Int32.TryParse(AnswerText.Text, out ANum);
                check(ANum);
            }
            catch {
                AnswerText.Text = "* Please enter a number";
                AnswerText.Foreground = Brushes.Red;}


              
            
        }
        private void check(int Anum)
        {
            if (gameStarted == true)
            {
                try
                {
                    if (Anum == Equation())
                    {
                        //make good sound
                        SoundPlayer simpleSound = new SoundPlayer("CorrectQuestion.wav");
                        simpleSound.Play();
                        //add correct
                        correct++;
                    }
                    else
                    {
                        //make bad sound
                        SoundPlayer simpleSound = new SoundPlayer("IncorrectQuestion.wav");
                        simpleSound.Play();
                        //add incorrect
                        incorrect++;
                    }
                    //check number of questions answered.
                    if (numQuestionsAnswered > 10)
                    {
                        this.Hide();
                        finalScores = new FinalScores(Name, Age, correct, incorrect, seconds);
                        finalScores.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        AnswerText.Text = "";
                        newQuestions();
                    }
                }
                catch
                {
                    MessageBox.Show("ERROR! PLEASE CLOSE AND RESTART GAME", "Math Type error", MessageBoxButton.OK);
                }
            }
            else return;

        }

        private int Equation( )
        {
            int correctA;
            try
            {

                switch (MathType)
                {
                    case "*":
                        return correctA = num1 * num2;
                    case "/":
                        return correctA = num1 / num2;
                    case "+":
                        return correctA = num1 + num2;
                    case "-":
                        return correctA = num1 - num2;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR! PLEASE CLOSE AND RESTART GAME", "Math Type error", MessageBoxButton.OK);
            }
            return 0;
        }




        /// <summary>
        /// if hit cancel Go back to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        /// <summary>
        /// if hit x go back to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// click on text box to clear text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
            tb.Foreground = Brushes.Black;
        }

        private void enter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }
    }
}
