using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace POSApp.Services
{
    public static class ImportService
    {
        public static DataTable GetExcelData(HttpPostedFileBase file)
        {
            try
            {
                DataTable dt = new DataTable();
                string filepath = string.Empty;
                if (file != null)
                {
                    string path = HttpContext.Current.Server.MapPath("~/Content/Uploads/");
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
                        default:
                            throw new Exception("Please Insert Excel File. ("+extension+") Not a Valid Format! Valid Formats are: .xls,.xlsx");
                            break;
                    }

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
                                if (dt.Rows.Count < 1)
                                {
                                    conExcel.Open();
                                    dtExcelSchema = conExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    sheetName = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();
                                    conExcel.Close();
                                    conExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    conExcel.Close();
                                }
                            }

                        }
                    }
                }

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}