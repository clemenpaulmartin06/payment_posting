using payment_posting.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace payment_posting.Functions
{
    class delimiterHat
    {
        public static List<PaymentPosting> payment = new List<PaymentPosting>();


        public static void parseFile(StreamReader sr, string file_name, string separator, int parse_amount_start, int parse_amount_end, int parse_atm_ref_start, int parse_atm_ref_end, int parse_date_start, int parse_date_end, int parse_prime_account_start, int parse_prime_account_end, int column_amount, int column_atm_ref, int column_date, int column_prime_account, string payment_center, int nSpecial, string specialStringForDate, string result_path, string error_path)
        {



            if (separator == null)
            {
                string line = "";
                int r = 0;

                try
                {
                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length > 50)
                        {


                            r++;

                            var get_amount = line.Substring(parse_amount_start, parse_amount_end);
                            get_amount = get_amount.Insert(get_amount.Length - 2, ".");
                            var get_atm_ref = line.Substring(parse_atm_ref_start, parse_atm_ref_end);
                            var get_date = line.Substring(parse_date_start, parse_date_end);
                            var get_prime_account = line.Substring(parse_prime_account_start, parse_prime_account_end);

                            DateTime d;

                            if (DateTime.TryParseExact(get_date, "yyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                            {
                                d = DateTime.ParseExact(get_date, "yyMMddHHmmss", CultureInfo.InvariantCulture);
                            }
                            else if (DateTime.TryParseExact(get_date, "MMddyyHHmmss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                            {
                                d = DateTime.ParseExact(get_date, "MMddyyHHmmss", CultureInfo.InvariantCulture);
                            }
                            else if (DateTime.TryParseExact(get_date, "MMddyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                            {
                                d = DateTime.ParseExact(get_date, "MMddyy", CultureInfo.InvariantCulture);
                            }
                            else if (DateTime.TryParseExact(get_date, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                            {
                                d = DateTime.ParseExact(get_date, "yyMMdd", CultureInfo.InvariantCulture);
                            }
                            else if (DateTime.TryParseExact(get_date, "ddMMyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                            {
                                d = DateTime.ParseExact(get_date, "ddMMyy", CultureInfo.InvariantCulture);
                            }
                            else if (DateTime.TryParseExact(get_date, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                            {
                                d = DateTime.ParseExact(get_date, "ddMMyyyy", CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                d = DateTime.ParseExact(get_date, "MMddyyyy", CultureInfo.InvariantCulture);
                            }


                            if (get_amount != "" && get_atm_ref != "" && get_date != "")
                            {

                                if (get_atm_ref != "9999999999")
                                {
                                    payment.Add(new PaymentPosting { NAmount = Convert.ToDecimal(get_amount), DDate = d, SATM_Ref = get_atm_ref, SPaymentCenter = payment_center, SPrimeAccount = get_prime_account, NRow = r });
                                }

                            }

                        }

                    }
                    
                }
                catch
                {

                    using (StreamWriter w = File.AppendText(@error_path))
                    {
                        Log("Error from line " + r + " File " + file_name, w);
                    }

                }


            }
            else if (separator == " ")
            {

                string line = "";
                int r = 0;
                string getDate = "";


                try
                {


                    while ((line = sr.ReadLine()) != null)
                    {


                        r++;

                        if (nSpecial == 1)
                        {

                            if (line.Contains(specialStringForDate) == true)
                            {
                                getDate = line.Substring(parse_date_start, parse_date_end);
                                getDate = Regex.Replace(getDate, @"\s+", " ");


                            }



                            var get_amount = line.Substring(parse_amount_start, parse_amount_end);
                            var get_atm_ref = line.Substring(parse_atm_ref_start, parse_atm_ref_end);
                            var get_date = getDate;

                            if (line.Length > 0)
                            {
                                if (get_atm_ref.Substring(0, 1) == "0")
                                {
                                    payment.Add(new PaymentPosting { NAmount = Convert.ToDecimal(get_amount), DDate = Convert.ToDateTime(get_date), SATM_Ref = get_atm_ref, SPaymentCenter = payment_center, SPrimeAccount = "", NRow = r });
                                }
                            }




                        }
                        else
                        {

                            var get_amount = line.Substring(parse_amount_start, parse_amount_end);
                            var get_atm_ref = line.Substring(parse_atm_ref_start, parse_atm_ref_end);
                            var get_date = line.Substring(parse_date_start, parse_date_end);
                            var get_prime_account = line.Substring(parse_prime_account_start, parse_prime_account_end);

                            if (get_amount.Contains("."))
                                get_amount = line.Substring(parse_amount_start, parse_amount_end);
                            else
                                get_amount = get_amount.Insert(9, ".");


                            if (get_amount != "" && get_atm_ref != "" && get_date != "")
                            {

                                if (get_date.Contains("/"))
                                {
                                    get_date = line.Substring(parse_date_start, parse_date_end);
                                    payment.Add(new PaymentPosting { NAmount = Convert.ToDecimal(get_amount), DDate = Convert.ToDateTime(get_date), SATM_Ref = get_atm_ref, SPaymentCenter = payment_center, SPrimeAccount = get_prime_account });

                                }
                                else
                                {
                                    get_date = line.Substring(parse_date_start, parse_date_end);
                                    DateTime d = DateTime.ParseExact(get_date, "MMddyy", CultureInfo.InvariantCulture);

                                    payment.Add(new PaymentPosting { NAmount = Convert.ToDecimal(get_amount), DDate = d, SATM_Ref = get_atm_ref, SPaymentCenter = payment_center, SPrimeAccount = get_prime_account, NRow = r });
                                }

                            }
                        }
                        
                    }


                }
                catch
                {

                    using (StreamWriter w = File.AppendText(@error_path))
                    {
                        Log("Error from line " + r + " File " + file_name, w);
                    }

                }




            }
            else
            {

                int r = 0;
                try
                {

                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        r++;

                        if (line.Length > 0)
                        {
                            var get_amount = line.Split(Convert.ToChar(separator))[column_amount];
                            var get_atm_ref = line.Split(Convert.ToChar(separator))[column_atm_ref];
                            var get_date = line.Split(Convert.ToChar(separator))[column_date];
                            get_atm_ref = get_atm_ref.Substring(parse_atm_ref_start, parse_atm_ref_end);
                            payment.Add(new PaymentPosting { NAmount = Convert.ToDecimal(get_amount), DDate = Convert.ToDateTime(get_date), SATM_Ref = get_atm_ref, SPaymentCenter = payment_center, SPrimeAccount = "", NRow = r });
                        }

                    }



                }
                catch
                {

                    using (StreamWriter w = File.AppendText(@error_path))
                    {
                        Log("Error from line " + r + " File " + file_name, w);
                    }

                }

            }



        }



        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("{0} - ", DateTime.Now);
            w.WriteLine("{0}", logMessage);
        }


    }
}







