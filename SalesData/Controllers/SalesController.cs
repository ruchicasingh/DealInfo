using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesData.Models;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SalesData.Controllers
{
    public class SalesController : Controller
    {
        
        public ActionResult Index()
        {
            SalesDataModel salesDataModel = new SalesDataModel();
            salesDataModel.SalesFile = null;
            return View("View", salesDataModel);
        }

        public ActionResult view()
        {
            SalesDataModel salesDataModel = new SalesDataModel();
            salesDataModel.SalesFile = null;
            return View("View", salesDataModel);
        }


        public ActionResult UploadFile(SalesDataModel salesDataModel)
        {
            if (salesDataModel == null || salesDataModel.SalesFile ==null)
            {
                if ( HttpContext?.Application?.Get("SalesFile") !=null)
                {
                    ModelState.Clear();
                    salesDataModel =(SalesDataModel)HttpContext.Application.Get("SalesFile");
                    if (salesDataModel.deals.Count > 0)
                    {
                        return View("View", salesDataModel);
                    }
                }
                else
                {
                    salesDataModel = new SalesDataModel();
                    salesDataModel.SalesFile = null;
                    return View("View", salesDataModel);
                }
            }
            else if(HttpContext!=null)
            {
                HttpContext.Application.Clear();
                HttpContext.Application.Add("SalesFile", salesDataModel);
            }
            
            try
            {
                if (ModelState.IsValid)
                {                               
                    StreamReader reader = new StreamReader(salesDataModel.SalesFile.InputStream,Encoding.Default);

                    string csvFormat = "DealNumber,CustomerName,DealershipName,Vehicle,Price,Date";
                    string line = reader.ReadLine();                    
                    if(line== csvFormat)
                    {
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            DealInfo dealInfo = new DealInfo(line);
                            if(dealInfo.IsValid)
                            {
                                salesDataModel.deals.Add(dealInfo);
                            }
                            else
                            {
                                salesDataModel.Message = "'" + salesDataModel.SalesFileName + "' file has incorrect data.";
                                salesDataModel.IsValid = false;
                                return View("View", salesDataModel);
                            }
                            
                        }
                        salesDataModel.Message = "'" + salesDataModel.SalesFileName + "' file details are below";
                        salesDataModel.IsValid = true;
                    }
                    else
                    {
                        salesDataModel.Message = "'" + salesDataModel.SalesFileName + "' file has incorrect data. ";
                        salesDataModel.IsValid = false;
                    }
                    
                }
                else
                {
                    salesDataModel.Message = "'" + salesDataModel.SalesFileName + "' file is not supported. ";
                    salesDataModel.IsValid = false;
                }

                return View("View", salesDataModel);
            }
            catch
            {
                return View("View");
            }
        }
     
    }
}