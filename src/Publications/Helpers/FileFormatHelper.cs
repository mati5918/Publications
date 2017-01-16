using System;
using System.IO;
using System.Linq;
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

                worksheet.Cells[1, i].Value = "Autor";
                i++;

                worksheet.Cells[1, i].Value = "Dziedzina Wiedzy";
                i++;

                worksheet.Cells[1, i].Value = "Okres czasu";

                worksheet.Cells[1, i + 1].Value = "Ilość publikacji";

                if (data.PercentOfAllPublications != -1)
                {
                    worksheet.Cells[1, i+2].Value = "Procent wszystkich publikacji";
                    i++;
                }

                if (data.PublicationsPerKonwledgeBranch != null)
                    worksheet.Cells[1, i + 2].Value = "Publikacje/dziedzina wiedzy";

                i = 1;

                if (!string.IsNullOrEmpty(data.AuthorName))
                {
                    worksheet.Cells[2, i].Value = data.AuthorName;
                    i++;
                }
                else
                {
                    worksheet.Cells[2, i].Value = "Wszyscy autorzy";
                    i++;
                }

                if (!string.IsNullOrEmpty(data.KnowledgeBranchName))
                {
                    worksheet.Cells[2, i].Value = data.KnowledgeBranchName;
                    i++;
                }
                else
                {
                    worksheet.Cells[2, i].Value = "Wszystie dziedziny";
                    i++;
                }
                if (!string.IsNullOrEmpty(data.TimeAmount))
                {
                    worksheet.Cells[2, i].Value = data.TimeAmount;
                }
                else
                {
                    worksheet.Cells[2, i].Value = "Brak (statystyki ogólne)";
                }
                worksheet.Cells[2, i + 1].Value = data.PublicationsCount;
                if (data.PercentOfAllPublications != -1)
                {
                    worksheet.Cells[2, i + 2].Value = data.PercentOfAllPublications;
                    i++;
                }
                if (data.PublicationsPerKonwledgeBranch.Any())
                {
                    for (int j = 0; j < data.PublicationsPerKonwledgeBranch.Count; j++)
                    {
                        var item = data.PublicationsPerKonwledgeBranch[j];
                        worksheet.Cells[2 + j, i + 2].Value = $"{item.KnowledgeBranchName} / {item.PublicationsCount} ({item.PublicationsPercentage}%)";
                    }
                }

                package.Save();
            }
            return name;
        }
    }
}