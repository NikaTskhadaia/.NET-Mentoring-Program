// Decompiled with JetBrains decompiler
// Type: MyCalculatorv1.MainWindow
// Assembly: MyCalculatorv1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E4247A5-25E4-47A6-84F4-A414933F7536
// Assembly location: C:\Users\Nikoloz_Tskhadaia\Downloads\DumpHomework\DumpHomework\MyCalculator.exe

using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MyCalculatorv1
{
    public partial class MainWindow : Window, IComponentConnector
    {

        public MainWindow() => this.InitializeComponent();

        private void Button_Click_1(object sender, RoutedEventArgs e) => this.tb.Text += ((Button)sender).Content.ToString();

        private void Result_click(object sender, RoutedEventArgs e) => this.result();

        private void result()
        {
            int operationIndex = 0;
            if (this.tb.Text.Contains("+"))
                operationIndex = this.tb.Text.IndexOf("+");
            else if (this.tb.Text.Contains("-"))
                operationIndex = this.tb.Text.IndexOf("-");
            else if (this.tb.Text.Contains("*"))
                operationIndex = this.tb.Text.IndexOf("*");
            else if (this.tb.Text.Contains("/"))
                operationIndex = this.tb.Text.IndexOf("/");
            string operation = this.tb.Text.Substring(operationIndex, 1);
            double num1 = Convert.ToDouble(this.tb.Text.Substring(0, operationIndex));
            int endOfFirstNumberIndex = Regex.Match(this.tb.Text.Substring(operationIndex + 1, this.tb.Text.Length - operationIndex - 1), @"[+-/*=]").Index;
            if (endOfFirstNumberIndex == 0)
            {
                endOfFirstNumberIndex = this.tb.Text.Length - operationIndex - 1;
            }
            double num2 = Convert.ToDouble(this.tb.Text.Substring(operationIndex + 1, endOfFirstNumberIndex));
            if (operation == "+")
            {
                TextBox tb = this.tb;
                tb.Text = tb.Text + "=" + (object)(num1 + num2);
            }
            else if (operation == "-")
            {
                TextBox tb = this.tb;
                tb.Text = tb.Text + "=" + (object)(num1 - num2);
            }
            else if (operation == "*")
            {
                TextBox tb = this.tb;
                tb.Text = tb.Text + "=" + (object)(num1 * num2);
            }
            else
            {
                TextBox tb = this.tb;
                tb.Text = tb.Text + "=" + (object)(num1 / num2);
            }
        }

        private void Off_Click_1(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void Del_Click(object sender, RoutedEventArgs e) => this.tb.Text = "";

        private void R_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb.Text.Length <= 0)
                return;
            this.tb.Text = this.tb.Text.Substring(0, this.tb.Text.Length - 1);
        }
    }
}
