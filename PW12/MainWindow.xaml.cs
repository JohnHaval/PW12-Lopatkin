﻿using System;
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
using CirclesAndYearsLibrary;

namespace PW12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        delegate void DelegateFindAllSquares(object sender, RoutedEventArgs e);
        bool error;
        Circles circles = new Circles();
        Centennial centennial = new Centennial();
        private void FindSquaresOfCircles_Click(object sender, RoutedEventArgs e)
        {
            if ((FirstRadius.Text != "") && (SecondRadius.Text != ""))
            {
                try
                {
                    circles.FirstCircle.FindSquare(circles.Radius = Convert.ToInt32(FirstRadius.Text)).ToString();
                    circles.SecondCircle.FindSquare(circles.Radius = Convert.ToInt32(SecondRadius.Text)).ToString();
                    FirstSquare.Text = circles.FirstCircle.Square.ToString();
                    SecondSquare.Text = circles.SecondCircle.Square.ToString();
                    SwitchDefault(2);
                }
                catch
                {
                    MessageForUser();
                }
            }
            else MessageForInputCheck();
        }

        private void FindSquareOfRing_Click(object sender, RoutedEventArgs e)
        {
            if (FindSquareOfRing.IsEnabled) SquareOfRing.Text = circles.FindSquareOfRing().ToString();
        }

        private void FirstRadius_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!error)
                e.Handled = "1234567890".IndexOf(e.Text) < 0;
            else e.Handled = true;
            error = false;
        }

        private void FindAllSquares_Click(object sender, RoutedEventArgs e)
        {
            DelegateFindAllSquares Finder = FindSquaresOfCircles_Click;
            Finder += FindSquareOfRing_Click;
            Finder(sender, e);
        }

        private void DisplayCentennial_Click(object sender, RoutedEventArgs e)
        {
            if (Year.Text != "")
            {
                centennial.Year = Convert.ToInt32(Year.Text);
                Centennial.Text = centennial.DisplayCentennial().ToString();
            }
            else MessageForInputCheck();
        }
        private void MessageForUser()
        {
            error = true;
            MessageBox.Show("Причина: " + Circles._infoaboutr1lessorequalr2, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Support_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная программа имеет следующие особенности:\n" +
                "1) Нельзя вводить более 5 символов в строку для радиусов и 8 для ввода года\n" +
                "2) Для быстрого закрытия программы можно использовать клавишу Esc\n" +
                "3) Для открытия доступа к кнопке \"Найти площадь кольца\" неообходимо" +
                " ввести первый радиус так, чтобы он был больше второго", "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Реализовать расчет задачи:" +
                "- Даны два круга с общим центром и радиусами R1 и R2(R1 > R2).Найти площади " +
                "этих кругов S1 и S2, а также площадь S3 кольца, внешний радиус которого равен " +
                "R1, внутренний радиус равен R2: S1 = PI * (R1)2, S2 = PI * (R2)2, S3 = S1 – S2.В" +
                " качестве значения PI использовать 3.14." +
                "- Дан номер некоторого года(целое положительное число).Определить " +
                "соответствующий ему номер столетия, учитывая, что, к примеру, началом 20 " +
                "столетия был 1901 год.", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Year_TextChanged(object sender, TextChangedEventArgs e)
        {
            Centennial.Clear();
        }

        private void FirstRadius_TextChanged(object sender, TextChangedEventArgs e)
        {
            SwitchDefault(1);
            FirstSquare.Clear();
            circles.Clear();
            circles.FirstCircle.Clear();
            circles.SecondCircle.Clear();
        }


        private void SecondRadius_TextChanged(object sender, TextChangedEventArgs e)
        {
            SwitchDefault(1);
            SecondSquare.Clear();
            circles.Clear();
            circles.FirstCircle.Clear();
            circles.SecondCircle.Clear();
        }
        private void FirstRadius_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FindSquareOfRing.IsEnabled == true) SwitchDefault(2);
            else SwitchDefault(1);
        }
        /// <summary>
        /// Данный метод позволяет быстро переключать дефолт на кнопках
        /// </summary>
        /// <param name="error">true, если начальные данные введены и необходимо найти кольцо;
        /// false, если необходимо откатиться и ввести начальные значения</param>
        private void SwitchDefault(int switcher)
        {
            switch (switcher)
            {
                case 1:
                    {
                        FindSquaresOfCircles.IsEnabled = true;
                        FindSquaresOfCircles.IsDefault = true;
                        MenuFindSquaresOfCircles.IsEnabled = true;
                        FindSquareOfRing.IsEnabled = false;
                        MenuFindSquareOfRing.IsEnabled = false;
                        DisplayCentennial.IsDefault = false;
                        SquareOfRing.Clear();
                        break;
                    }
                case 2:
                    {
                        FindSquareOfRing.IsEnabled = true;
                        FindSquareOfRing.IsDefault = true;
                        MenuFindSquareOfRing.IsEnabled = true;
                        FindSquaresOfCircles.IsEnabled = false;
                        MenuFindSquaresOfCircles.IsEnabled = false;
                        DisplayCentennial.IsDefault = false;
                        break;
                    }
                default:
                    {
                        FindSquaresOfCircles.IsDefault = false;
                        FindSquareOfRing.IsDefault = false;
                        DisplayCentennial.IsDefault = true;
                        break;
                    }
            }
        }

        private void Year_GotFocus(object sender, RoutedEventArgs e)
        {
            SwitchDefault(3);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Actions.IsSubmenuOpen)
            {
            if(e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D1) FindSquaresOfCircles_Click(sender, e);
                else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D2) FindSquareOfRing_Click(sender, e);
                else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D3) DisplayCentennial_Click(sender, e);                
            }                   
        }

        private void Actions_Click(object sender, RoutedEventArgs e)
        {
            MenuFindSquaresOfCircles.Focus();
        }
        private void MessageForInputCheck()
        {
            error = true;
            MessageBox.Show("Необходимо ввести число для дальнейшего выполнения рассчетов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
