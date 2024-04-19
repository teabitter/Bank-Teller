using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Bank_Teller_Start
{
    public class Account
    {
        private decimal _mBalance;

        public decimal mBalance
        {
            get { return _mBalance; }
            set { _mBalance = value; }
        }

        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string FilePath { get; set; }
        public string LastError { get; set; }

        public decimal totalDeposits = 0;
        public decimal totalWithdrawals = 0;

        public Account(string pAccountId)
        {
            AccountId = pAccountId;
            AccountName = " ";
            mBalance = 0.0M;
        }

        public bool GetData()
        {
            StreamReader infile = null;
            LastError = " ";
            try
            {
                infile = File.OpenText(FilePath);
                while (!infile.EndOfStream)
                {
                    string entireLine = infile.ReadLine();
                    string[] fields = entireLine.Split(',');

                    if (fields[0] == AccountId)
                    {
                        AccountName = fields[1];
                        decimal.TryParse(fields[2], out _mBalance);
                        return true;
                    }
                }
                LastError = "Account " + AccountId + "not found";
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
            finally
            {
                if (infile != null)
                    infile.Close();
            }
        }

        public void Deposit(decimal amount)
        {
            if(amount < 0)
            {
                throw new ArgumentException("Deposit must be a positive value");
            }
            mBalance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if(amount < 0)
            {
                throw new ArgumentException("Withdrawal must be a positive value");
            }
            if(amount > mBalance)
            {
                throw new ArgumentException("Insufficient funds for withdrawal");
            }

            if (amount <= mBalance)
            {
                mBalance -= amount;

                totalWithdrawals += amount;
                return true;
            }
            else
            {
                LastError = "Balance is too low to withdraw the requested amount";
                return false;
            }
        }
    }
}
