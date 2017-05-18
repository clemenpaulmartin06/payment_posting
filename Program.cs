using payment_posting.Functions;
using payment_posting.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payment_posting
{
    class Program
    {


        static void Main(string[] args)
        {

           

            using (var db = new paymentEntities())
            {
                var q = db.view_payment_center();
                
                foreach(var item in q)
                {

                    //if (Convert.ToInt32(item.pc_id) < 11)
                    //{
                    //    StreamReader sr = new StreamReader(@item.data_path);
                    //    Functions.delimiterHat.parseFile(sr, item.separator, Convert.ToInt32(item.parse_amount_start), Convert.ToInt32(item.parse_amount_end), Convert.ToInt32(item.parse_atm_ref_start), Convert.ToInt32(item.parse_atm_ref_end), Convert.ToInt32(item.parse_date_start), Convert.ToInt32(item.parse_date_end), Convert.ToInt32(item.parse_prime_account_start), Convert.ToInt32(item.parse_prime_account_end), Convert.ToInt32(item.column_amount), Convert.ToInt32(item.column_atm_ref), Convert.ToInt32(item.column_date), Convert.ToInt32(item.column_prime_account), item.payment_center, Convert.ToInt32(item.nSpecial));
                    //}

                    
                    StreamReader sr = new StreamReader(@item.data_path);
                    Functions.delimiterHat.parseFile(sr, "filename here", item.separator, Convert.ToInt32(item.parse_amount_start), Convert.ToInt32(item.parse_amount_end), Convert.ToInt32(item.parse_atm_ref_start), Convert.ToInt32(item.parse_atm_ref_end), Convert.ToInt32(item.parse_date_start), Convert.ToInt32(item.parse_date_end), Convert.ToInt32(item.parse_prime_account_start), Convert.ToInt32(item.parse_prime_account_end), Convert.ToInt32(item.column_amount), Convert.ToInt32(item.column_atm_ref), Convert.ToInt32(item.column_date), Convert.ToInt32(item.column_prime_account), item.payment_center, Convert.ToInt32(item.nSpecial), item.specialStringForDate, item.result_path, item.error_path);

                }

            }



            foreach(var item in delimiterHat.payment.Distinct())
            {
                Console.WriteLine("Amount : {0} ATM Ref : {1} Date : {2} Prime Account : {3} Bayad Center : {4}, Row : {5}", item.NAmount, item.SATM_Ref, item.DDate, item.SPrimeAccount, item.SPaymentCenter, item.NRow);
            }
            Console.ReadLine();
        }
    }
}
