using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalesData.Models
{
    [Serializable]
    public class SalesDataModel
    {
        [Required(ErrorMessage = "Please select file.")]
        public HttpPostedFileBase SalesFile { get; set; }

        [FileExtensions(Extensions = "csv", ErrorMessage = "Only csv files allowed.")]
        public string SalesFileName 
        {
            get
            {
                if (SalesFile != null)
                    return SalesFile.FileName;
                else
                    return "";
            }
        }

        public string Message { get; set; }

        public List<DealInfo> deals = new List<DealInfo>();

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

        public string MostPopular
        {
            get
            {
                string vehicle = (string)this.deals.GroupBy(d => d.Vehicle).
                    Select(g => new { key = g.Key , Ct = g.Count() })
                    .OrderByDescending(s => s.Ct).FirstOrDefault().key;
                return vehicle;
            }
        }
    }
}