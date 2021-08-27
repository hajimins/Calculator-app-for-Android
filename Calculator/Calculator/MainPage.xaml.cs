using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Calculator
{
    public class Calculate
    {
        public double num1 { get; set; }
        public string op1 { get; set; }
        public string par { get; set; }
        public double num2 { get; set; }
        public string op2 { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        string cal = string.Empty;
        string show = string.Empty;
        List<Calculate> CalculatorList = new List<Calculate>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped_C(object sender, EventArgs e)
        {
            cal = string.Empty;
            show = string.Empty;
            CalculatorList.Clear();
            LabelPink.Text = show;
        }

        private void TapGestureRecognizer_Tapped_par(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)label.GestureRecognizers[0];

            string str = tapGesture.CommandParameter.ToString();

            int bo = 0;

            if (bo == 0)
            {
                str = "(";
                bo++;
            }
            else
            {
                str = ")";
            }

            cal += str;

            show += str;

            LabelPink.Text = show;
        }

        private void TapGestureRecognizer_Tapped_op(object sender, EventArgs e) //async is deleted here
        {
            Label label = (Label)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)label.GestureRecognizers[0];

            string str = tapGesture.CommandParameter.ToString();

            Calculate calculate = new Calculate();
            if (str == "(" || str == ")")
                calculate.par = str;
            var a = cal;
            calculate.num1 = Convert.ToDouble(cal);
            calculate.op1 = str;

            CalculatorList.Add(calculate);

            show += str;

            cal = string.Empty;

            if (str == "=")
            {
                show += Hesabla();
            }

            LabelPink.Text = show;
        }
        private string Hesabla()
        {
            string summ;
            for (int i = 0; i < CalculatorList.Count - 1; i++)
            {
                Calculate item = CalculatorList[i];
                Calculate item2 = new Calculate();

                if (i < CalculatorList.Count)
                {
                    item2 = CalculatorList[i + 1];

                    if (item.op1 == "-")
                    {
                        item2.num1 = -1 * item2.num1;
                    }

                    for (int j = i; j < CalculatorList.Count - 1; j++)
                    {
                        if (item.op1 == "×" || item.op1 == "÷" || item.op1 == "%")
                        {
                            if (item.op1 == "%")
                            {
                                item2.num2 = item.num1 * item2.num1 / 100;
                                item.num2 = 0;
                            }

                            if (item.op1 == "×")
                            {
                                item2.num2 = item.num1 * item2.num1;
                                item.num2 = 0;
                            }

                            if (item.op1 == "÷")
                            {
                                item2.num2 = item.num1 / item2.num1;
                                item.num2 = 0;
                            }
                        }
                    }
                }
                if (item.op1 == "+" || item.op1 == "-")
                {
                    item.num2 = item.num1;
                    item2.num2 = item2.num1;
                }
            }
            double s = 0;

            foreach (Calculate itemm in CalculatorList)
            {
                s += itemm.num2;
            }

            summ = s.ToString();
            return summ;
        }
        private void TapGestureRecognizer_Tapped_rb(object sender, EventArgs e) //async is deleted here
        {
            Label label = (Label)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)label.GestureRecognizers[0];

            string str = tapGesture.CommandParameter.ToString();

            cal += str;

            show += str;

            LabelPink.Text = show;
        }
    }
}