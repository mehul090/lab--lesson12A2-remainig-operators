using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Name:mehul khosla
 * Date: August 11, 2017
 * Description: A calculator app with Windows Forms
 * 
 */

namespace COMP123_S2017_Lesson12A2
{
    public partial class CalculatorForm : Form
    {
        // PRIVATE INSTANCE VARIABLES
        private bool _isDecimalClicked;
        private double _firstOperand;
        private double _secondOperand;
        private double _result;

        private List<double> _operandList;

        private string _currentOperator;

        // PUBLIC PROPERTIES +++++++++++++++++++++++++++++++
        public bool IsDecimalClicked
        {
            get
            {
                return this._isDecimalClicked;
            }

            set
            {
                this._isDecimalClicked = value;
            }
        }

        public double FirstOperand
        {

            get
            {
                return this._firstOperand;
            }

            set
            {
                this._firstOperand = value;
            }

        }

        public double SecondOperand
        {

            get
            {
                return this._secondOperand;
            }

            set
            {
                this._secondOperand = value;
            }

        }

        public double Result
        {

            get
            {
                return this._result;
            }

            set
            {
                this._result = value;
            }

        }

        public List<double> OperandList
        {

            get
            {
                return this._operandList;
            }

            set
            {
                this._operandList = value;
            }

        }

        public string CurrentOperator
        {
            get
            {
                return this._currentOperator;
            }

            set
            {
                this._currentOperator = value;
            }
        }

        // CONSTRUCTORS +++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// This is the main constructor
        /// </summary>
        public CalculatorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the event handler for the "FormClosing" event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // this closes the application
        }

        /// <summary>
        /// This is a shared event handler for the Calculator Buttons
        /// Not including the Operator buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorButton_Click(object sender, EventArgs e)
        {
            Button calculatorButton = sender as Button; // downcasting

            if ((calculatorButton.Text == ".") && (this.IsDecimalClicked))
            {
                return;
            }

            if (calculatorButton.Text == ".")
            {
                this.IsDecimalClicked = true;
            }

            if (ResultTextBox.Text == "0")
            {
                if (calculatorButton.Text == ".")
                {
                    ResultTextBox.Text += calculatorButton.Text;
                }
                else
                {
                    ResultTextBox.Text = calculatorButton.Text;
                }
            }
            else
            {
                if (OperandList.Count > 0)
                {
                    ResultTextBox.Text = calculatorButton.Text;
                }
                else
                {
                    ResultTextBox.Text += calculatorButton.Text;
                }
            }


            //Debug.WriteLine("Calculator Button Clicked");
        }

        /// <summary>
        /// This is the Shared event handler for Operator Buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button operatorButton = sender as Button;
            double operand = 0;

            if ((operatorButton.Text != "C") && (operatorButton.Text != "⌫"))
            {
                operand = this._covertResult(ResultTextBox.Text);
            }
            

            switch (operatorButton.Text)
            {
                case "C":
                    this._clear();
                    break;
                case "=":
                    this._showResult(operand);
                    break;
                default:
                    this.CurrentOperator = operatorButton.Text;
                    this._calculate(operand, operatorButton.Text);
                    break;
            }
            //code for backspace(sort of)
            ResultTextBox.Text = ResultTextBox.Text.Remove(ResultTextBox.Text.Length - 1, 1);



        }

        /// <summary>
        /// This method displays the result in the ResultTextBox
        /// </summary>
        /// <returns></returns>
        private void _showResult(double operand)
        {

            Debug.WriteLine("OperandList Count: " + OperandList.Count);
            if (OperandList.Count > 0)
            {
                OperandList.Add(operand);
                this._calculate(operand, "=");
            }


            ResultTextBox.Text = this.Result.ToString();
        }

        /// <summary>
        /// This method converts the string result of the ResultTextBox into a number
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private double _covertResult(string resultString)
        {
            try
            {
                return Convert.ToDouble(resultString);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("An Error Occurred");
                Debug.WriteLine(exception.Message);
            }
            return 0;
        }

        /// <summary>
        /// This method sets operand1 as the first operand of the operation
        /// </summary>
        private void _calculate(double operand, string operatorString)
        {
            if (OperandList.Count > 1)
            {
                double result = 0;
                if (operatorString == "=")
                {
                    operatorString = this.CurrentOperator;
                }

                Debug.WriteLine("Current Operator: " + CurrentOperator);


                switch (operatorString)
                {
                    case "+":

                        result = OperandList[0] + OperandList[1];
                        break;
                    case "-":

                        result = OperandList[0] - OperandList[1];
                        break;
                    case "x":

                        result = OperandList[0] * OperandList[1];

                        break;
                    case "÷":

                        result = OperandList[0] / OperandList[1];

                        break;
                    case "±":

                        result = operand * -1;

                        break;






                }


                OperandList.Clear();
                this.OperandList.Add(result);
                this.Result = result;
            }
            else
            {
                this.OperandList.Add(operand);
            }

        }

        /// <summary>
        /// This method clears / resets the calculator
        /// </summary>
        private void _clear()
        {
            this.IsDecimalClicked = false;
            this.ResultTextBox.Text = "0";
            this.OperandList = new List<double>(); // create new list
            this.CurrentOperator = "C";
        }

        /// <summary>
        /// This is the event handler for the Form's "Load" event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            this._clear();
        }
    }
}
