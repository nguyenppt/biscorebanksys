using System;
using System.Data;
using System.Web;

namespace BankProject.Controls
{
    public class Reports
    {
        public static void createFileDownload(string reportTemplate, DataSet reportData, string saveName, Aspose.Words.SaveFormat saveFormat, Aspose.Words.SaveType saveType, HttpResponse Response)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open the template document
            Aspose.Words.Document reportDoc = new Aspose.Words.Document(reportTemplate);

            // Fill the fields in the document with user data.
            reportDoc.MailMerge.ExecuteWithRegions(reportData);

            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            reportDoc.Save(saveName, saveFormat, saveType, Response);
        }
    }
}