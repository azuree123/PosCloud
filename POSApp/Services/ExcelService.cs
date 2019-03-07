using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using POSApp.Core;
using POSApp.Core.Models;

namespace POSApp.Services
{
    public static class ExcelService
    {
        public static int GenerateCrystalReport<T>(List<T> dtList, string reportName, string userId, IUnitOfWork unitOfWork, int storeId, string details, string crystalReportPath,string crystalReportName)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Content/Reports/");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileName = reportName + "_" + userId + "_" + DateTime.Now.ToString("ddd, dd MMM yyy HH-mm-ss ") + ".PDF";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(crystalReportPath, crystalReportName));
            rd.SetDataSource(dtList);
            foreach (ReportDocument reportDocument in rd.Subreports)
            {
                reportDocument.SetDataSource(unitOfWork.ReportsRepository.GenerateSubReportData(details, reportName));
            }
            rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filePath + fileName);
            var report = new ReportsLog
            {
                Name = reportName,
                Path = fileName,
                Status = "Ready",
                Details = details,
                StoreId = storeId

            };
            unitOfWork.ReportsLogRepository.AddReportsLog(report);
            unitOfWork.Complete();
            return report.Id; 
        }

        public static int GenerateEmployeeCrystalReport<T>(List<T> dtList, string reportName, string userId, IUnitOfWork unitOfWork, int storeId, string details, string crystalReportPath, string crystalReportName)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Content/Reports/");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileName = reportName + "_" + userId + "_" + DateTime.Now.ToString("ddd, dd MMM yyy HH-mm-ss ") + ".PDF";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(crystalReportPath, crystalReportName));
            foreach (ReportDocument reportDocument in rd.Subreports)
            {
                reportDocument.SetDataSource(unitOfWork.ReportsRepository.GenerateSubReportData(details, reportName));
            }
            rd.SetDataSource(dtList);
            rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filePath + fileName);
            var report = new ReportsLog
            {
                Name = reportName,
                Path = fileName,
                Status = "Ready",
                Details = details,
                StoreId = storeId

            };
            unitOfWork.ReportsLogRepository.AddReportsLog(report);
            unitOfWork.Complete();
            return report.Id;
        }
        public static void GenerateExcelSheet(DataTable dtList,string reportName,string filePath,string userId,IUnitOfWork unitOfWork,int storeId,string details)
        {
            string fileName = reportName + "_" + userId + "_" + DateTime.Now.ToString("ddd, dd MMM yyy HH-mm-ss ") + ".PDF";
            DataGrid gridDetails=new DataGrid();
            gridDetails.DataSource = dtList;
            gridDetails.DataBind();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            gridDetails.RenderControl(htw);
            string renderedGridView = sw.ToString();
            System.IO.File.WriteAllText(filePath + fileName, renderedGridView);
            unitOfWork.ReportsLogRepository.AddReportsLog(new ReportsLog
            {
                Name = reportName,
                Path = fileName,
                Status = "Ready",
                Details = details,
                StoreId = storeId
            
            });
            unitOfWork.Complete();
        }
        public static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}