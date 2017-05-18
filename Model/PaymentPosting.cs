using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payment_posting.Model
{


    public class PaymentPosting
    {
        

        public decimal NAmount
        {
            get { return nAmount; }
            set { nAmount = value; }
        }
        private decimal nAmount;



        public string SATM_Ref
        {
            get { return sATM_Ref; }
            set { sATM_Ref = value; }
        }
        private string sATM_Ref;


        public DateTime DDate
        {
            get { return dDate; }
            set { dDate = value; }
        }

        private DateTime dDate;


        public string SPrimeAccount
        {
            get { return sPrimeAccount; }
            set { sPrimeAccount = value; }
        }
        private string sPrimeAccount;


        public string SPaymentCenter
        {
            get { return sPaymentCenter; }
            set { sPaymentCenter = value; }
        }

        private string sPaymentCenter;


        public int NRow
        {
            get { return nRow; }
            set { nRow = value; }
        }

        private int nRow;

        public PaymentPosting()
        {

        }

        public PaymentPosting(decimal nAmount, string sATM_Ref, DateTime dDate, string sPrimeAccount, string sPaymentCenter, int nRow)
        {
            this.nAmount = nAmount;
            this.sATM_Ref = sATM_Ref;
            this.dDate = dDate;
            this.sPrimeAccount = sPrimeAccount;
            this.sPaymentCenter = sPaymentCenter;
            this.nRow = nRow;
        }

    }

}
