using System;
using Microsoft.Maui.Controls;

namespace Calculator2
{
    public partial class ScientificPage : ContentPage
    {
        private string _currentInput = "";
        private string _operation = "";
        private double _firstNumber = 0;
        private bool _isNewEntry = true;

        public ScientificPage()
        {
            InitializeComponent();
        }

        private void OnNumberClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (_isNewEntry)
                {
                    lblValue.Text = "";
                    _isNewEntry = false;
                }

                if (button.Text == "," && lblValue.Text.Contains(","))
                    return;

                lblValue.Text += button.Text;
            }
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (double.TryParse(lblValue.Text, out _firstNumber))
                {
                    _operation = button.Text;
                    _currentInput = "";
                    _isNewEntry = true;
                    lblHistory.Text = $"{_firstNumber} {_operation}";
                }
            }
        }

        private void OnScientificClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (double.TryParse(lblValue.Text, out double number))
                {
                    double result = number;
                    switch (button.Text)
                    {
                        case "π":
                            result = Math.PI;
                            break;
                        case "e":
                            result = Math.E;
                            break;
                        case "x²":
                            result = Math.Pow(number, 2);
                            break;
                        case "1/x":
                            result = number != 0 ? 1 / number : double.NaN;
                            break;
                        case "|x|":
                            result = Math.Abs(number);
                            break;
                        case "exp":
                            result = Math.Exp(number);
                            break;
                        case "²√x":
                            result = Math.Sqrt(number);
                            break;
                        case "n!":
                            result = Factorial(number);
                            break;
                        case "log":
                            result = Math.Log10(number);
                            break;
                        case "ln":
                            result = Math.Log(number);
                            break;
                        case "10^x":
                            result = Math.Pow(10, number);
                            break;
                    }
                    lblValue.Text = double.IsNaN(result) ? "Tanımsız" : result.ToString();
                    _isNewEntry = true;
                }
            }
        }

        private void OnEqualClicked(object sender, EventArgs e)
        {
            if (double.TryParse(lblValue.Text, out double secondNumber))
            {
                double result = 0;
                bool isUndefined = false;

                switch (_operation)
                {
                    case "+":
                        result = _firstNumber + secondNumber;
                        break;
                    case "-":
                        result = _firstNumber - secondNumber;
                        break;
                    case "*":
                        result = _firstNumber * secondNumber;
                        break;
                    case "÷":
                        if (secondNumber == 0)
                            isUndefined = true;
                        else
                            result = _firstNumber / secondNumber;
                        break;
                    case "x^y":
                        result = Math.Pow(_firstNumber, secondNumber);
                        break;
                    case "mod":
                        if (secondNumber == 0)
                            isUndefined = true;
                        else
                            result = _firstNumber % secondNumber;
                        break;
                }

                lblHistory.Text = $"{_firstNumber} {_operation} {secondNumber} =";
                lblValue.Text = isUndefined ? "Tanımsız" : result.ToString();
                _isNewEntry = true;
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Text == "C")
                {
                    lblValue.Text = "0";
                    lblHistory.Text = "0";
                    _currentInput = "";
                    _firstNumber = 0;
                    _operation = "";
                    _isNewEntry = true;
                }
                else if (button.Text == "CE")
                {
                    lblValue.Text = "0";
                    _isNewEntry = true;
                }
                else if (button.Text == "⌫" && lblValue.Text.Length > 0)
                {
                    lblValue.Text = lblValue.Text.Substring(0, lblValue.Text.Length - 1);
                    if (string.IsNullOrEmpty(lblValue.Text))
                        lblValue.Text = "0";
                }
            }
        }

        private void OnSecondFunctionClicked(object sender, EventArgs e)
        {
            DisplayAlert("Info", "İkinci fonksiyon seti etkinleştirildi!", "Tamam");
        }

        private double Factorial(double num)
        {
            if (num < 0) return double.NaN;
            if (num == 0 || num == 1) return 1;
            double fact = 1;
            for (int i = 2; i <= num; i++)
                fact *= i;
            return fact;
        }
    }
}