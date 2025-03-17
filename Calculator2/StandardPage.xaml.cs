using System;
using Microsoft.Maui.Controls;

namespace Calculator2
{
    public partial class StandardPage : ContentPage
    {
        private string _currentInput = "";
        private string _operation = "";
        private double _firstNumber = 0;
        private bool _isNewEntry = true;

        public StandardPage()
        {
            InitializeComponent();
        }

        private void btnNumber_Clicked(object sender, EventArgs e)
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

        private void btnOperation_Clicked(object sender, EventArgs e)
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

        private void btnGetResult_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(lblValue.Text, out double secondNumber))
            {
                double result = 0;
                bool isUndefined = false; // Tanımsız durumu kontrolü için

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
                        {
                            isUndefined = true;
                        }
                        else
                        {
                            result = _firstNumber / secondNumber;
                        }
                        break;
                    case "x²":
                        result = Math.Pow(_firstNumber, 2);
                        break;
                    case "²√x":
                        result = Math.Sqrt(_firstNumber);
                        break;
                    case "1/x":
                        if (_firstNumber == 0)
                        {
                            isUndefined = true;
                        }
                        else
                        {
                            result = 1 / _firstNumber;
                        }
                        break;
                }

                if (isUndefined)
                {
                    lblValue.Text = "Tanımsız";
                }
                else
                {
                    lblValue.Text = result.ToString();
                }

                lblHistory.Text = $"{_firstNumber} {_operation} {secondNumber} =";
                _isNewEntry = true;
            }
        }


        private void btnCanc_Clicked(object sender, EventArgs e)
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
    }
}