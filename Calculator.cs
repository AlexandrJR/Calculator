using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {
        char decimalSeparator;

        bool openPanel = true;

        double numOne;
        double numTwo;
        string sign;

        int usedLength;

        double result = 0;
        public Calculator()
        {
            InitializeComponent();
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {
            decimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            this.BackColor = Color.Purple;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            Display.Font = new Font("Tahoma", 22);
            Display.Text = "0";
            Display.TabStop = false;

            string buttonName;
            Button button;
            for (int i = 0; i<10; i++)
            {
                buttonName = "button" + i;
                button = (Button)this.Controls[buttonName];
                button.Text = i.ToString();
                button.Font = new Font("Tahoma", 12);
                button.BackColor = Color.FromArgb(254, 199, 255);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if(Display.Text == "0")
            {
                Display.Text = button.Text;
            }
            else
            {
                Display.Text += button.Text;
            }
        }

        private void BackSpace_Click(object sender, EventArgs e)
        {
            string s = Display.Text;
            if(s.Length > 1)
            {
                s = s.Substring(0, s.Length - 1);
            }
            else
            {
                s = "0";
            }
            Display.Text = s;
        }

        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            if(Display.Text == string.Empty)
            {
                Display.Text = "0" + decimalSeparator;
            }
            else if (!Display.Text.Contains(decimalSeparator))
            {
                Display.Text += decimalSeparator;
            }
            
        }

        private void ButtonSign_Click(object sender, EventArgs e)
        {
            string s = Display.Text;
            if (!s.Contains("-") || s.Length != 1 && !s.Contains(sign))
            {
                double number = Convert.ToDouble(Display.Text);
                number *= -1;
                Display.Text = Convert.ToString(number);
            }
        }

        private void OperationClick(object sender, EventArgs e)
        {
            try
            {
                string s = Display.Text;
                numOne = Convert.ToDouble(s);

                Button button = (Button)sender;
                Display.Text += button.Text;

                usedLength = Display.Text.Length;

                sign = button.Text;

                if (sign == "√")
                {
                    Display.Text = Convert.ToString(Math.Sqrt(numOne));
                }
            }
            catch
            {
                MessageBox.Show("You can use just one operator!");
            }

        }

        private void buttonEquality_Click(object sender, EventArgs e)
        {
            string s = Display.Text;
            numTwo = Convert.ToDouble(s.Substring(usedLength, s.Length - usedLength));
            switch (sign)
            {
                case "+":
                    result = numOne + numTwo;
                break;
                case "-":
                    result = numOne - numTwo;
                    break;
                case "*":
                    result = numOne * numTwo;
                    break;
                case "/":
                    if (numTwo == 0)
                    {
                        MessageBox.Show("Cannot be devided by 0 !");
                    }
                    else
                    {
                        result = numOne / numTwo;
                    }
                    break;
                case "^":
                    result = Math.Pow(numOne, numTwo);
                    break;

            }
            Display.Text = Convert.ToString(result);       
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Display.Text = "0";
            
        }

        private void buttonSwitch_Click(object sender, EventArgs e)
        {
            if (openPanel)
            {
                this.Width = 523;
                buttonSwitch.Text = "<=";
                openPanel = false;
            }
            else
            {
                this.Width = 444;
                buttonSwitch.Text = "=>";
                openPanel = true;
            }
        }
    }
}
