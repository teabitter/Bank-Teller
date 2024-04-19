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

namespace Bank_Teller_Start
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Account currAccount;
        private string FILEPATH = @"..\..\accounts.txt";

        private void buttonFind_Click(object sender, EventArgs e)
        {
            currAccount = new Account(textBoxaccountNumber.Text);
            currAccount.FilePath = FILEPATH;

            if (currAccount.GetData())
            {
                labelAccountName.Text = currAccount.AccountName;
                labelBalance.Text = currAccount.mBalance.ToString("c2");
                buttonDeposit.Enabled = true;
                buttonWithdraw.Enabled = true;
            }
            else
            {
                MessageBox.Show(currAccount.LastError, "Error");
                Clear();
            }
        }

        private void Clear()
        {
            labelAccountName.Text = " ";
            labelBalance.Text = " ";
            buttonDeposit.Enabled = false;
            buttonWithdraw.Enabled = false;
        }

        private void buttonDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                currAccount.Deposit(decimal.Parse(textBoxAmount.Text));
                labelBalance.Text = currAccount.mBalance.ToString("c2");
            }

            catch(Exception ex)
            {
                MessageBox.Show("Please enter a numeric deposit amount", "Error");
            }
        }

        private void buttonWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                if(currAccount.Withdraw(decimal.Parse(textBoxAmount.Text)))
                {
                    labelBalance.Text = currAccount.mBalance.ToString("c");
                }
                else
                {
                    MessageBox.Show(currAccount.LastError, "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a numeric withdrawal amount", "Error");
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTotal_Click(object sender, EventArgs e)
        {
            decimal totaltotalDeposits = currAccount.totalDeposits;
            MessageBox.Show("Total deposits: " + totaltotalDeposits.ToString("c"));

            decimal totaltotalWithdrawals = currAccount.totalWithdrawals;
            MessageBox.Show("Total withdrawals: " + totaltotalWithdrawals.ToString("c"));   
        }
    }
}
