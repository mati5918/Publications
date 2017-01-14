using System;
using System.IO;
using OfficeOpenXml;
using Publications.Models.Statistisc;

namespace Publications.Helpers
{
    public class FileFormatHelper
    {
        public static string GenerateXlsx(StatisticsViewModel data)
        {

            var name = Guid.NewGuid() + ".xlsx";
            
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(name);
            }
            using (ExcelPackage package = new ExcelPackage(file))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee");
                int i = 1;



                if (!string.IsNullOrEmpty(data.AuthorName))
                {
                    worksheet.Cells[1, i].Value = "Autor";
                    i++;
                }

                if (!string.IsNullOrEmpty(data.KnowledgeBranchName))
                {
                    worksheet.Cells[1, i].Value = "Dziedzina Wiedzy";
                    i++;
                }
                if (!string.IsNullOrEmpty(data.TimeAmount))
                {
                    worksheet.Cells[1, i].Value = "Okres czasu";
                }
                worksheet.Cells[1, i+1].Value = "Ilość publikacji";
                worksheet.Cells[1, i+2].Value = "Procent wszystkich publikacji";
                if (data.PublicationsPerKonwledgeBranch != null)
                    worksheet.Cells[1, i+3].Value = "Publikacje/dziedzina wiedzy";

                i = 1;



                if (!string.IsNullOrEmpty(data.AuthorName))
                {
                    worksheet.Cells[2, i].Value = data.AuthorName;
                    i++;
                }

                if (!string.IsNullOrEmpty(data.KnowledgeBranchName))
                {
                    worksheet.Cells[2, i].Value = data.KnowledgeBranchName;
                    i++;
                }
                if (!string.IsNullOrEmpty(data.TimeAmount))
                {
                    worksheet.Cells[2, i].Value = data.TimeAmount;
                }
                worksheet.Cells[2, i + 1].Value = data.PublicationsCount;
                worksheet.Cells[2, i + 2].Value = data.PercentOfAllPublications;
                if (data.PublicationsPerKonwledgeBranch != null)
                {
                    for (int j = 0; j < data.PublicationsPerKonwledgeBranch.Count; j++)
                    {
                        var item = data.PublicationsPerKonwledgeBranch[j];
                        worksheet.Cells[2+j, i + 3].Value = $"{item.KnowledgeBranchName} / {item.PublicationsCount} ({item.PublicationsPercentage}%)";
                    }
                }

                package.Save();
            }
            return name;
        }
    }

}
