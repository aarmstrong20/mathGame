using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mathGame
{
    /// <summary>
    /// Interaction logic for FinalScores.xaml
    /// </summary>
    public partial class FinalScores : Window
    {
        #region window
        MainWindow main;
        GameWindow gameWindow;
        #endregion
        #region var
        string Name;
        int Age;
        int Correct;
        int Incorrect;
        int Seconds;
        #endregion
        public FinalScores(string name, int age, int correct, int incorrect, int seconds)
        {
            InitializeComponent();
            main = new MainWindow();
            Name = name;
            Age = age;
            Correct = correct;
            Incorrect = incorrect;
            Seconds = seconds;

            display();
            SoundPlayer simpleSound = new SoundPlayer("FinalScores.wav");
            simpleSound.Play();
        }


        private void display()
        {
            nameLabel.Content = Name;
            ageLabel.Content = Age;
            incorrectLabel.Content = "Incorrect: " + Incorrect;
            Correctlabel.Content = "Correct: " + Correct;
            SecondsLabel.Content = Seconds + " Seconds";
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

    }
}
