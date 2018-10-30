using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;

namespace POSApp.Controllers
{
    public class ExcelImportController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public ExcelImportController()
        {

        }

        public ExcelImportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: ExcelImport
        public ActionResult StateExcelImport()
        {
            ViewBag.edit = "StateExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult StateExcelImport(HttpPostedFileBase file)
        {
            string filepath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Content/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filepath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filepath);

                string conString = string.Empty;
                switch (extension)
                {
                    case ".xls":
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx":
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filepath);

                using (OleDbConnection conExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = conExcel;
                            conExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = conExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            conExcel.Close();
                            conExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            conExcel.Close();
                        }

                    }
                }

                //var userid = User.Identity.GetUserId();
                //var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    State NewModel = new State();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                    NewModel.Name = dr["Name"].ToString();
                    _unitOfWork.StateRepository.AddState(NewModel);
                    }
                }
                _unitOfWork.Complete();

            }
                return RedirectToAction("StateList", "Setup");
        }
        [HttpGet]
        public ActionResult CityExcelImport()
        {
            ViewBag.edit = "CityExcelImport";
            return View();
        }
        [HttpPost]
        public ActionResult CityExcelImport(HttpPostedFileBase file)
        {
            string filepath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Content/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filepath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filepath);

                string conString = string.Empty;
                switch (extension)
                {
                    case ".xls":
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx":
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filepath);

                using (OleDbConnection conExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = conExcel;
                            conExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = conExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            conExcel.Close();
                            conExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            conExcel.Close();
                        }

                    }
                }

                //var userid = User.Identity.GetUserId();
                //var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    City NewModel = new City();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.StateId = Int32.Parse(dr["StateId"].ToString());
                        _unitOfWork.CityRepository.AddCity(NewModel);
                    }
                }
                _unitOfWork.Complete();

            }
            return RedirectToAction("CityList", "Setup");
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
    }
}