using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SalesData.Models
{
    public class DealInfo
    {
        #region Constructors

        public DealInfo()
        {

        }

        public DealInfo( string csvString)
        {
            var values = SplitCsv(csvString);
            if(Int32.TryParse(values[0],out int number))
            {
                DealNo = number;
            }
            CustomerName = values[1];
            DealershipName = values[2];
            Vehicle = values[3];
            string dealPrice = values[4].Replace(",","");
            if(double.TryParse(dealPrice, out double val))
            {
                Price = val;
            }
            else
            {
                isValid = false;
            }
            
            if (DateTime.TryParse(values[5], out DateTime dateTime))
            {
                Date =  dateTime;
            }
            else
            {
                isValid = false;
            }
            
        }
        #endregion
        public int DealNo { get; set; }
        public string CustomerName { get; set; }
        public string DealershipName { get; set; }
        public string Vehicle { get; set; }

        public double Price { get; set; }
        public DateTime Date { get; set; }

        private bool isValid = true;
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        #region methods
        //Method to spit the string with quot
        private string[] SplitCsv(string line)
        {
            List<string> result = new List<string>();
            StringBuilder currentStr = new StringBuilder("");
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++) 
            {
                if (line[i] == '\"') 
                    inQuotes = !inQuotes;
                else if (line[i] == ',') 
                {
                    if (!inQuotes) 
                    {
                        result.Add(currentStr.ToString());
                        currentStr.Clear();
                    }
                    else
                        currentStr.Append(line[i]); 
                }
                else 
                    currentStr.Append(line[i]);
            }
            result.Add(currentStr.ToString());
            return result.ToArray(); 
        }
        #endregion 
    }
}