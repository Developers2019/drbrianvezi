using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using ClinicLogist.Helpers;
using ClinicLogist.Models;
using ClinicLogist.Service.Email_Management;
using drbrianvezi_cms.Helpers;
using Microsoft.Reporting.WebForms;

namespace drbrianvezi_cms.Service.RDLC_Management
{
    public class Reports
    {

        public static void MedicalCertificateEmail(int medicalCertificateId)
        {
            string emailStatus = "";
            var personalInformation = MedicalCertificateViewModel.GetById(medicalCertificateId).Client_ID;
            var y = ClientDataViewModel.GetClient(personalInformation);

            string mailTo = y.Client_Email;

            string bccMailTo = ConfigurationManager.AppSettings["DeveloperEmail"].ToString();


            //StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Email/Student/StudentMailer.html"));
            string body = "Medical Certificate test";
            //sr.Close();
            //body = body.Replace("[StudentName]", personalInformation.FirstName + " " + personalInformation.Surname);
            string subject = "Medical Certificate of - " + y.FullName;
            string attachmentName = "";
            MemoryStream attachmentPdf = new MemoryStream();

            attachmentName = "Medical Certificate " + y.FullName + ".pdf";

            LocalReport report = new LocalReport();
            report.ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/MedicalCertificateReport.rdlc");
            var ds = GetMedicalCertificate(medicalCertificateId);
            report.DataSources.Add(new ReportDataSource("ds_DrBrianVezi", ds.Tables[0]));

            string deviceInfo =
          "<DeviceInfo>" +
          //"  <OutputFormat>EMF</OutputFormat>" +
          "  <PageWidth>8.2677in</PageWidth>" +
          "  <PageHeight>11.69in</PageHeight>" +
          "  <MarginTop>0.78in</MarginTop>" +
          "  <MarginLeft>0.59in</MarginLeft>" +
          "  <MarginRight>0.79in</MarginRight>" +
          "  <MarginBottom>0.78in</MarginBottom>" +
          "</DeviceInfo>";
            Warning[] warnings;
           
            byte[] bytes = report.Render("PDF");
            attachmentPdf = new MemoryStream(bytes);


            Email email = new Email();
            List<Email.EmailAttachment> emailAttachment = new List<Email.EmailAttachment>();
            Email.EmailAttachment rEgContract = new Email.EmailAttachment
            {
                Name = attachmentName,
                Stream = attachmentPdf,
                Type = "application/pdf"
            };
            emailAttachment.Add(rEgContract);

            emailStatus = email.SendEmail(mailTo, subject, body, bccMailTo, emailAttachment);

           EmailLogService.InsertLog(mailTo,emailStatus,"");
        }
        public static void PrescritionNoteEmail(int prescriptionId)
        {
            string emailStatus = "";
            var personalInformation = PrescriptionViewModel.GetById(prescriptionId).ClientID;
            if (personalInformation != null)
            {
                var y = ClientDataViewModel.GetClient((int)personalInformation);

                string mailTo = y.Client_Email;

                string bccMailTo = ConfigurationManager.AppSettings["DeveloperEmail"].ToString();


                //StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Email/Student/StudentMailer.html"));
                string body = "Medical Certificate test";
                //sr.Close();
                //body = body.Replace("[StudentName]", personalInformation.FirstName + " " + personalInformation.Surname);
                string subject = "Prescription Note - " + y.FullName;
                string attachmentName = "";
                MemoryStream attachmentPdf = new MemoryStream();

                attachmentName = "Prescription Note - " + y.FullName + ".pdf";

                LocalReport report = new LocalReport
                {
                    ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/PrescritionNote.rdlc")
                };
                var ds = GetPrescriptionNote(prescriptionId);
                report.DataSources.Add(new ReportDataSource("ds_Prescription", ds.Tables[0]));

                string deviceInfo =
                    "<DeviceInfo>" +
                    //"  <OutputFormat>EMF</OutputFormat>" +
                    "  <PageWidth>8.2677in</PageWidth>" +
                    "  <PageHeight>11.69in</PageHeight>" +
                    "  <MarginTop>0.78in</MarginTop>" +
                    "  <MarginLeft>0.59in</MarginLeft>" +
                    "  <MarginRight>0.79in</MarginRight>" +
                    "  <MarginBottom>0.78in</MarginBottom>" +
                    "</DeviceInfo>";
                Warning[] warnings;
              
                byte[] bytes = report.Render("PDF");
                attachmentPdf = new MemoryStream(bytes);


                Email email = new Email();
                List<Email.EmailAttachment> emailAttachment = new List<Email.EmailAttachment>();
                Email.EmailAttachment rEgContract = new Email.EmailAttachment
                {
                    Name = attachmentName,
                    Stream = attachmentPdf,
                    Type = "application/pdf"
                };
                emailAttachment.Add(rEgContract);

                emailStatus = email.SendEmail(mailTo, subject, body, bccMailTo, emailAttachment);

                EmailLogService.InsertLog(mailTo,emailStatus,"");
            }
        }
        public static void MedicalCertificateReport(int medicalCertificateId)
        {
            int clientId = MedicalCertificateViewModel.GetById(medicalCertificateId).Client_ID;
            ClientDataViewModel client = ClientDataViewModel.GetClient(clientId);

            var report = new LocalReport
            {
                ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/MedicalCertificateReport.rdlc")
            };
            DataSet ds = GetMedicalCertificate(medicalCertificateId);
            report.DataSources.Add(new ReportDataSource("ds_DrBrianVezi", ds.Tables[0]));

            
            string deviceInfo =
              "<DeviceInfo>" +

              //"  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>8.2677in</PageWidth>" +
              "  <PageHeight>11.69in</PageHeight>" +
              "  <MarginTop>0.78in</MarginTop>" +
              "  <MarginLeft>0.59in</MarginLeft>" +
              "  <MarginRight>0.79in</MarginRight>" +
              "  <MarginBottom>0.78in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            //m_streams = new List<Stream>();
            //report.Render("Image", deviceInfo, CreateStream, out warnings);

            //byte[] Bytes = report.Render(format: "PDF", deviceInfo: DeviceInfo);


            byte[] render = report.Render("PDF");

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "Medical Certificate " + client.FullName + ".pdf");
            HttpContext.Current.Response.BinaryWrite(render);
            HttpContext.Current.Response.Flush();
        }
        public static void PrescriptionNoteReport(int? prescriptionId)
        {
            if (prescriptionId != null)
            {
                int clientId = (int)PrescriptionViewModel.GetById((int)prescriptionId).ClientID;
                ClientDataViewModel client = ClientDataViewModel.GetClient(clientId);

                var report = new LocalReport
                {
                    ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/PrescritionNote.rdlc")
                };
                DataSet ds = GetPrescriptionNote((int)prescriptionId);
                report.DataSources.Add(new ReportDataSource("ds_Prescription", ds.Tables[0]));

            
                string deviceInfo =
                    "<DeviceInfo>" +

                    //"  <OutputFormat>EMF</OutputFormat>" +
                    "  <PageWidth>8.2677in</PageWidth>" +
                    "  <PageHeight>11.69in</PageHeight>" +
                    "  <MarginTop>0.78in</MarginTop>" +
                    "  <MarginLeft>0.59in</MarginLeft>" +
                    "  <MarginRight>0.79in</MarginRight>" +
                    "  <MarginBottom>0.78in</MarginBottom>" +
                    "</DeviceInfo>";
                Warning[] warnings;
                //m_streams = new List<Stream>();
                //report.Render("Image", deviceInfo, CreateStream, out warnings);

                //byte[] Bytes = report.Render(format: "PDF", deviceInfo: DeviceInfo);


                byte[] render = report.Render("PDF");

                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "Prescription Note - " + client.FullName + ".pdf");
                HttpContext.Current.Response.BinaryWrite(render);
            }
            HttpContext.Current.Response.Flush();
        }
        private static DataSet GetMedicalCertificate(int medicalCertificateId)
        {
            using (var con = new SqlConnection(CmsConfig.Connection))
            {
                con.Open();
                using (var cmd = new SqlCommand("sp_GetMedicalCertificate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MedicalCertificateID", SqlDbType.Int).Value = medicalCertificateId;

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var ds = new DataSet();
                        da.Fill(ds);
                        ds.Tables[0].TableName = "test";
                        return ds;
                    }
                }
            }
        }
        private static DataSet GetPrescriptionNote(int prescriptionNoteId)
        {
            using (var con = new SqlConnection(CmsConfig.Connection))
            {
                con.Open();
                using (var cmd = new SqlCommand("GetPrescriptionNote", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PrescriptionID", SqlDbType.Int).Value = prescriptionNoteId;

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var ds = new DataSet();
                        da.Fill(ds);
                        ds.Tables[0].TableName = "test";
                        return ds;
                    }
                }
            }
        }

    }
}