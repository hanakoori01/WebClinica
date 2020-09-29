using System;
using System.Collections.Generic;
using cm = System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Svg.Renderers.Impl;
using iText.IO.Image;
using iText.Layout.Properties;
using OfficeOpenXml;

namespace Clinica.Models
{
    public class Utilitarios
    {
        public byte[] ExportarPDFDatos<T>(String[] nombrePropiedades, List<T> lista, string titulo)
        {
            if (lista == null)
            {
                //No crear pdf
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Dictionary<string, string> diccionario =
                    cm.TypeDescriptor.GetProperties(typeof(T))
                    .Cast<cm.PropertyDescriptor>().ToDictionary(p => p.Name, p => p.DisplayName);

                PdfWriter writer = new PdfWriter(ms);
                using (var pdfDoc = new PdfDocument(writer))
                {
                    Document doc = new Document(pdfDoc);
                    //Text Texto = new Text("Clínica ACME")
                    //.SetFontColor(ColorConstants.RED)
                    //.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                    //Paragraph p0 = new Paragraph(Texto);
                    //p0.SetFontSize(24);

                    Paragraph p1 = new Paragraph(titulo);
                    p1.SetFontSize(20);
                    p1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    Image img = new Image(ImageDataFactory
                   .Create(@"D:\Clinica\wwwroot\img\ClinicaAcme.png"))
                   .SetTextAlignment(TextAlignment.LEFT);

                    doc.Add(img);
                    doc.Add(p1);
                    //crearemos la tabla
                    Table table = new Table(nombrePropiedades.Length);
                    Cell celda;
                    for (int i = 0; i < nombrePropiedades.Length; i++)
                    {
                        celda = new Cell();
                        celda.Add(new Paragraph(diccionario[nombrePropiedades[i]]));
                        //celda.Add(new Paragraph(nombrePropiedades[i]));
                        table.AddHeaderCell(celda);
                    }
                    foreach (object item in lista)
                    {
                        foreach (string propiedad in nombrePropiedades)
                        {
                            celda = new Cell();
                            celda.Add(new Paragraph(
                            item.GetType().GetProperty(propiedad).GetValue(item).ToString()));
                            table.AddCell(celda);
                        }
                    }
                    doc.Add(table);
                    doc.Close();
                    writer.Close();
                }
                return ms.ToArray();
            }
        }
        //nos genera el array de bytes
        public byte[] generarExcel<T>(string[] cabeceras, string[] nombrePropiedades, List<T> lista)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage ep = new ExcelPackage())
                {
                    ep.Workbook.Worksheets.Add("Hoja");
                    ExcelWorksheet ew = ep.Workbook.Worksheets[0];
                    for (int i = 0; i < cabeceras.Length; i++)
                    {
                        ew.Cells[1, i + 1].Value = cabeceras[i];
                        ew.Column(i + 1).Width = 30;
                    }
                    int row = 2;
                    int col = 1;
                    foreach (object item in lista)
                    {
                        col = 1;
                        foreach (string propiedad in nombrePropiedades)
                        {
                            ew.Cells[row, col].Value =
                             item.GetType().GetProperty(propiedad).GetValue(item).ToString();
                            col++;
                        }
                        row++;
                    }
                    ep.SaveAs(ms);
                    byte[] buffer = ms.ToArray();
                    return buffer;
                }
            }
        }
    }
}
