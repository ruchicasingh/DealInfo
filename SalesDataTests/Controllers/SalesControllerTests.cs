using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesData.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SalesData.Models;
using System.Web;
using System.IO;

namespace SalesData.Controllers.Tests
{
    [TestClass()]
    public class SalesControllerTests
    {
        SalesController controller = new SalesController();

        [TestMethod()]
        public void IndexTest()
        {
            var result = controller.Index() as ViewResult ;
            Assert.AreEqual(result.ViewName, "View");
        }

        [TestMethod()]
        public void ViewTest()
        {
            var result = controller.view() as ViewResult;
            Assert.AreEqual(result.ViewName, "View");
        }

        [TestMethod()]
        public void UploadNullFileTest()
        {
            var result = controller.UploadFile(null) as ViewResult;
            Assert.AreEqual(result.ViewName, "View");
            Assert.AreEqual(result.Model,null);
        }

        [TestMethod()]
        public void UploadFileDataTest()
        {
            SalesDataModel salesDataModel = new SalesDataModel();
            FileStream fs = System.IO.File.OpenRead(@"..\..\" + "\\testfiles\\Dealertrack-CSV-Data.csv");

            MyTestPostedFileBase fileBase = new MyTestPostedFileBase(fs, "csv", "Dealertrack-CSV-Data.csv");

            salesDataModel.SalesFile = fileBase;

            var result = controller.UploadFile(salesDataModel) as ViewResult;
            SalesDataModel resultSalesDataModel = (SalesDataModel)result.Model;

            Assert.AreEqual(resultSalesDataModel.IsValid, false);
            Assert.AreEqual(resultSalesDataModel.Message, "'Dealertrack-CSV-Data.csv' file has incorrect data.");
            Assert.AreEqual(result.ViewName, "View");
           
        }

        [TestMethod()]
        public void UploadFileTest()
        {
            SalesDataModel salesDataModel = new SalesDataModel();
            FileStream fs = System.IO.File.OpenRead(@"..\..\" + "\\testfiles\\Dealertrack-CSV-Example.csv");

            MyTestPostedFileBase fileBase = new MyTestPostedFileBase(fs, "csv", "Dealertrack-CSV-Example.csv");

            salesDataModel.SalesFile = fileBase;

            var result = controller.UploadFile(salesDataModel) as ViewResult;
            SalesDataModel resultSalesDataModel = (SalesDataModel)result.Model;

            Assert.AreEqual(resultSalesDataModel.IsValid, true);

            Assert.AreEqual(result.ViewName, "View");
        }

        [TestMethod()]
        public void UploadFileHeaderTest()
        {
            SalesDataModel salesDataModel = new SalesDataModel();
            FileStream fs = System.IO.File.OpenRead(@"..\..\" + "\\testfiles\\Dealertrack-CSV-header.csv");

            MyTestPostedFileBase fileBase = new MyTestPostedFileBase(fs, "csv", "Dealertrack-CSV-header.csv");

            salesDataModel.SalesFile = fileBase;

            var result = controller.UploadFile(salesDataModel) as ViewResult;
            SalesDataModel resultSalesDataModel = (SalesDataModel)result.Model;

            Assert.AreEqual(resultSalesDataModel.IsValid, false);
            Assert.AreEqual(resultSalesDataModel.Message, "'Dealertrack-CSV-header.csv' file has incorrect data. ");
            Assert.AreEqual(result.ViewName, "View");
        }

        [TestMethod()]
        public void UploadFiletxtTest()
        {
            SalesDataModel salesDataModel = new SalesDataModel();
            FileStream fs = System.IO.File.OpenRead(@"..\..\" + "\\testfiles\\test.txt");

            MyTestPostedFileBase fileBase = new MyTestPostedFileBase(fs, "txt", "test.txt");

            salesDataModel.SalesFile = fileBase;

            var result = controller.UploadFile(salesDataModel) as ViewResult;
            SalesDataModel resultSalesDataModel = (SalesDataModel)result.Model;

            Assert.AreEqual(resultSalesDataModel.IsValid, false);
            Assert.AreEqual(resultSalesDataModel.Message, "'test.txt' file has incorrect data. ");
            Assert.AreEqual(result.ViewName, "View");
        }

    }
}