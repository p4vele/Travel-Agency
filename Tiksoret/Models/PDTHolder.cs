﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Configuration;
using System.IO;

namespace Tiksoret.Models
{
    public class PDTHolder
    {
        public double GrossTotal { get; set; }

        public int InvoiceNumber { get; set; }
        public string PaymentStatus { get; set; }
        public string PayerFirstName { get; set; }
        public double PaymentFee { get; set; }
        public string BusinessEmail { get; set; }
        public string PayerEmail { get; set; }
        public string TxToken { get; set; }
        public string PayerLastName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ItemName { get; set; }
        public string Currency { get; set; }
        public string TransactionId { get; set; }
        public string SubscriberId { get; set; }
        public string Custom { get; set; }

        private static string authToken, txToken, query, strResponse;


        public static PDTHolder Success(string tx)
        {
            authToken = WebConfigurationManager.AppSettings["PDTToken"];
            txToken = tx;
            query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);
            string url = WebConfigurationManager.AppSettings["PayPalSubmitUrl"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();
            if (strResponse.StartsWith("SUCCESS"))
                return PDTHolder.Parse(strResponse);
            return null;
        }

        private static PDTHolder Parse(string postData)
        {
            String sKey, sValue;
            PDTHolder ph = new PDTHolder();
            try
            {
                String[] StringArray = postData.Split('\n');
                for (int i = 1; i < StringArray.Length; i++)
                {
                    String[] StringArray1 = StringArray[i].Split('=');

                    sKey = StringArray1[0];
                    sValue = HttpUtility.UrlDecode(StringArray1[1]);
                    switch (sKey)
                    {
                        case "mc_gross":
                            ph.GrossTotal = Convert.ToDouble(sValue);
                            break;
                        case "invoice":
                            ph.InvoiceNumber = Convert.ToInt32(sValue);
                            break;
                        case "payment_status":
                            ph.PaymentStatus = Convert.ToString(sValue);
                            break;
                        case "first_name":
                            ph.PayerFirstName = Convert.ToString(sValue);
                            break;
                        case "mc_fee":
                            ph.PaymentFee = Convert.ToDouble(sValue);
                            break;
                        case "business":
                            ph.BusinessEmail = Convert.ToString(sValue);
                            break;
                        case "payer_email":
                            ph.PayerEmail = Convert.ToString(sValue);
                            break;
                        case "Tx Token":
                            ph.TxToken = Convert.ToString(sValue);
                            break;
                        case "last_name":
                            ph.PayerLastName = Convert.ToString(sValue);
                            break;
                        case "receiver_email":
                            ph.ReceiverEmail = Convert.ToString(sValue);
                            break;
                        case "item_name":
                            ph.ItemName = Convert.ToString(sValue);
                            break;
                        case "mc_currency":
                            ph.Currency = Convert.ToString(sValue);
                            break;
                        case "txn_id":
                            ph.TransactionId = Convert.ToString(sValue);
                            break;
                        case "custom":
                            ph.Custom = Convert.ToString(sValue);
                            break;
                        case "subscr_id":
                            ph.SubscriberId = Convert.ToString(sValue);
                            break;
                    }
                }
                return ph;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}