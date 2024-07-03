using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mathGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region windows 
        FinalScores finalScores;
        GameWindow gameWindow;
        background BG;
        #endregion
        #region var
        string MathType;
        string Name;
        int Age;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose; 
            BG = new background();
        }

        /// <summary>
        /// Player check and style check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if you don't have all info don't play
                if (check() == false) return;
                //shove info to game window 
                gameWindow = new GameWindow(MathType, Name, Age);
                //go to game window 
                this.Hide();
                gameWindow.ShowDialog();
                this.Show();
            }
            catch
            {
                MessageBox.Show("ERROR! PLEASE CLOSE AND RESTART GAME", "loading GameWindow error", MessageBoxButton.OK);
            }

        }



        /// <summary>
        /// check player info and game style
        /// </summary>
        public bool check()
        {
            bool name = false;
            bool age = false;
            bool radio = false;

            //if name is empty
            try
            {
                if (string.IsNullOrEmpty(nameText.Text))
                {
                    nameText.Text = "* Please enter name";
                    nameText.Foreground = Brushes.Red;
                }
                else
                {
                    Name = nameText.Text;
                    name = true;
                }
            }
            catch { MessageBox.Show("ERROR! PLEASE ENTER A NAME", "name error", MessageBoxButton.OK); }


            try
            {
                //if age is empty
                if (string.IsNullOrEmpty(AgeText.Text))
                {
                    AgeText.Text = "* Please enter age";
                    AgeText.Foreground = Brushes.Red;
                }
                //if age = int
                if (Int32.TryParse(AgeText.Text, out int myAge))
                {
                    //if age is not 1-10
                    if (myAge > 10 || myAge < 1)
                    {
                        AgeText.Text = "* Please enter age 1-10";
                        AgeText.Foreground = Brushes.Red;
                    }
                    else
                    {
                        Age = myAge;
                        age = true;
                    }
                }
            }
            catch (Exception ex) {
                AgeText.Text = "* Please enter age";
                AgeText.Foreground = Brushes.Red;
            }

            try
            {
                if (addRadio.IsChecked == true)
                {
                    MathType = "+";
                    radio = true;
                }
                else if (subRadio.IsChecked == true)
                {
                    MathType = "-";
                    radio = true;
                }
                else if (multiRadio.IsChecked == true)
                {
                    MathType = "*";
                    radio = true;
                }
                else if (diviRadio.IsChecked == true)
                {
                    MathType = "/";
                    radio = true;
                }
                else
                {

                    radioErrorLabel.Content = "* Please select a playing option";
                    radioErrorLabel.Foreground = Brushes.Red;
                }
            }
            catch
            {

                return false;
            }


            if (radio == true && name == true && age == true) return true;
            return false;
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
    }
}
