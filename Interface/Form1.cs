using DividersProject;
using System.Numerics;
using System.Security.AccessControl;

namespace Interface
{

    public partial class Form1 : Form
    {
        MyBigInteger number;
        public Form1()
        {
            InitializeComponent();
            gB_Num.Enabled = false;
            gB_lineSeg.Enabled = false;
        }




        private void btn_GetPrimes_Click(object sender, EventArgs e)
        {
            txBx_Primes.Clear();
            MyBigInteger[] result = Dividers.AllPrimes(new MyBigInteger(txBx_Start.Text), new MyBigInteger(txBx_End.Text));
            Printer<BigInteger>.PrintInTextBox(mbiConvector(result), ref txBx_Primes);
        }
        private void btn_PrimeCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dividers.IsPrime(new MyBigInteger(txBx_Number.Text)))
                {
                    lbl_NumIsPrime.Text = "Число простое";
                }
                else { lbl_NumIsPrime.Text = "Число составное"; }
            }
            catch { MessageBox.Show("Требуется натуральное число!"); }
        }

        private void txBx_Number_TextChanged(object sender, EventArgs e)
        {
            lbl_NumIsPrime.Text = "Статус неопределен";
            txBx_PrimeDecomposition.Text = "";
            txBx_AllDividers.Text = "";
            lbl_IsDivider.Text = "Статус неопределен";
            if (txBx_Number.Text == String.Empty)
            {
                gB_Num.Enabled = false;
            }
            else
            {
                gB_Num.Enabled = true;
                if (long.TryParse(txBx_Number.Text, out long number))
                {
                    btn_DivCheck.Enabled = true;
                    btn_GetDecomposition.Enabled = true;
                    btn_GetDivList.Enabled = true;
                    btn_PrimeCheck.Enabled = true;
                    this.number = new (number);
                }
                else
                {
                    btn_PrimeCheck.Enabled = false;
                    btn_GetDecomposition.Enabled = false;
                    btn_DivCheck.Enabled = false;
                    btn_GetDivList.Enabled = false;
                }
            }
        }

        private void btn_GetDecomposition_Click(object sender, EventArgs e)
        {
            (MyBigInteger[] dividers, MyBigInteger[] powers) = Dividers.Factorize(number);
            txBx_PrimeDecomposition.Text = "";
            for (int i = 0; i < dividers.Length; i++)
            {
                txBx_PrimeDecomposition.Text += dividers[i].ToString();
                if (powers[i] > 1)
                    txBx_PrimeDecomposition.Text += "^" + powers[i].ToString();
                if (i < dividers.Length - 1)
                    txBx_PrimeDecomposition.Text += " * ";
            }
        }

        private void btn_DivCheck_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txBx_Divider.Text, out double number) && number > 0)
            {
                if (Dividers.IsDivider(new MyBigInteger(txBx_Number.Text), new MyBigInteger(txBx_Divider.Text)))
                {
                    lbl_IsDivider.Text = "Является делителем";
                }
                else { lbl_IsDivider.Text = "Не является делителем"; }
            }
            else { MessageBox.Show("Требуется ввести число!"); }
        }

        private void txBx_Divider_TextChanged(object sender, EventArgs e)
        {
            lbl_IsDivider.Text = "Статус неопределен";
        }

        private void btn_GetDivList_Click(object sender, EventArgs e)
        {
            txBx_AllDividers.Text = "";
            BigInteger[] allDividers = mbiConvector(Dividers.AllDividers(new MyBigInteger(txBx_Number.Text)));
            Array.Sort(allDividers);

            Printer<BigInteger>.PrintInTextBox(allDividers, ref txBx_AllDividers);
        }
        private bool lineCheck()
        {
            if (txBx_Start.Text != String.Empty && double.TryParse(txBx_Start.Text, out double number))
            {
                if (txBx_End.Text != String.Empty && double.TryParse(txBx_End.Text, out double number2))
                {
                    if (long.Parse(txBx_Start.Text) < long.Parse(txBx_End.Text))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private void txBx_Start_TextChanged(object sender, EventArgs e)
        {
            if (lineCheck())
            {
                gB_lineSeg.Enabled = true;
            }
            else { gB_lineSeg.Enabled = false; }
            txBx_Primes.Clear();
            txBx_lineDiv.Clear();
        }

        private void txBx_End_TextChanged(object sender, EventArgs e)
        {
            if (lineCheck())
            {
                gB_lineSeg.Enabled = true;
            }
            else { gB_lineSeg.Enabled = false; }
            txBx_Primes.Clear();
            txBx_lineDiv.Clear();
        }

        private void btn_lineDiv_Click(object sender, EventArgs e)
        {
            txBx_lineDiv.Clear();
            var result = Dividers.FindNumsWithDividers(new MyBigInteger(textBox2.Text), new MyBigInteger(txBx_Start.Text), new MyBigInteger(txBx_End.Text));
            Printer<BigInteger>.PrintInTextBox(mbiConvector(result), ref txBx_lineDiv);
        }
        private BigInteger[] mbiConvector(MyBigInteger[] nums)
        {
            BigInteger[] result = new BigInteger[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                result[i] = long.Parse(nums[i].ToString());
            }
            return result;
        }
    }

    public static class Printer<T>
    {
        public static string LogConcat(T[] arr, int start, int end)
        {
            if (start > end)
                return "";
            if (start == end)
                return arr[start].ToString();
            if (start == end - 1)
                return arr[start].ToString() + " " + arr[end].ToString();

            int center = (start + end) / 2;

            return LogConcat(arr, start, center) + " " + LogConcat(arr, center + 1, end);
        }
        public static void PrintInTextBox(T[] arr, ref TextBox txbx)
        {
            txbx.Text = LogConcat(arr, 0, arr.Length - 1);
        }
    }
}