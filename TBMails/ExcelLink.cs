using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
//using System.Windows.Forms;


namespace TBMails
{
    class ExcelLink
    {
        static private string _FileName;
        static private Dictionary<string, DateTime > _conns;
        object NullValue = System.Reflection.Missing.Value;
        Application xlApp;
        private Workbook _xlWorkbook;
        public Workbook xlWorkbook  { get {return _xlWorkbook;} }

        public Worksheet sheet { get; set; }
        public ExcelLink(string filename)
        {
            _FileName = filename;
            xlApp = new Application();
            try
            {

         
            _xlWorkbook = xlApp.Workbooks.Open(@_FileName, NullValue, false, NullValue, NullValue,
               NullValue, NullValue, NullValue, NullValue, NullValue,
               NullValue, NullValue, NullValue, NullValue, NullValue);
                this.sheet = _xlWorkbook.Worksheets.get_Item(1);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Range GetRange(string cell1, string cell2)
        {
            string range = cell1 + (cell2 == "" ? "" : ":" + cell2);
            Range rang = sheet.get_Range(range, Type.Missing);
            return rang;
        }

        public Image CopyImage(string cell1, string cell2 = "")
        {
            System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();
            this.GetRange(cell1, cell2).CopyPicture(XlPictureAppearance.xlScreen, XlCopyPictureFormat.xlBitmap);
            Image image = (Image)data.GetData(System.Windows.Forms.DataFormats.Bitmap, true);
            return image;
        }

        public Image copyChart()
        {

            // ChartObjects chartObjects = sheet.ChartObjects();
            //xlWorkbook.Charts.Select("Retenção_PDS");
            //System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();
            //xlWorkbook.ActiveChart.CopyPicture(XlPictureAppearance.xlScreen, XlCopyPictureFormat.xlBitmap);
            //
            ChartObjects charts = (ChartObjects)sheet.ChartObjects(Type.Missing);
            charts.Select("Retenção_PDS");
            ChartObject chart = charts.Item(1);
            
            System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();
            chart.Chart.CopyPicture(XlPictureAppearance.xlScreen, XlCopyPictureFormat.xlBitmap);
            Image t = (Image)data.GetData(System.Windows.Forms.DataFormats.Bitmap, true);
            return t;
        }
        public Image CopyImage(Range ran,string cell1, string cell2)
        {
            System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();

             ran.Range[cell1,cell2].CopyPicture(XlPictureAppearance.xlScreen, XlCopyPictureFormat.xlBitmap);
            Image image = (Image)data.GetData(System.Windows.Forms.DataFormats.Bitmap, true);
            return image;
        }
        public Image CopyImage(Range ran, string cells)
        {
            System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();
            string[] r = cells.Split(':');
            ran.Range[r[0], r[1]].CopyPicture(XlPictureAppearance.xlScreen, XlCopyPictureFormat.xlBitmap);
            Image image = (Image)data.GetData(System.Windows.Forms.DataFormats.Bitmap, true);
            return image;
        }
       //public void RefreshAll()
       //{

           

       //    //xlWorkbook.RefreshAll();
       //    refreshConnection(xlWorkbook);
       //    refreshQueryTables(xlWorkbook);
       //    refreshPivots(xlWorkbook);
       //    xlWorkbook.Save();
       //    closeFile( true);
       //}
       //private static void refreshQueryTables(Workbook theWorkbook)
       //{
       //    Sheets oSheets = (Sheets)theWorkbook.Worksheets;

       //    foreach (Worksheet oWorkSheet in oSheets)
       //    {
       //        Console.WriteLine(" {0}", oWorkSheet.Name);
       //        foreach (QueryTable qt in oWorkSheet.QueryTables)
       //        {
       //            Console.WriteLine("--qt:{0}", qt.Name);
       //            qt.EnableRefresh = true;
       //            qt.FieldNames = false;
       //            qt.RowNumbers = false;
       //            qt.SavePassword = false;
       //            qt.SaveData = true;
       //            qt.PreserveColumnInfo = true;
       //            qt.Refresh(false);
       //        }
       //    }

       //    return;
       //}
       //private static void refreshPivots(Workbook theWorkbook)
       //{

       //    Console.WriteLine("WorkSheets:");
       //    Sheets oSheets = (Sheets)theWorkbook.Worksheets;

       //    foreach (Worksheet oWorkSheet in oSheets)
       //    {
       //        Console.WriteLine(" {0}", oWorkSheet.Name);
       //        PivotTables pivotTables1 =
       //            (PivotTables)oWorkSheet.PivotTables();

       //        if (pivotTables1.Count > 0)
       //        {
       //            for (int i = 1; i <= pivotTables1.Count; i++)
       //            {
       //                Console.WriteLine("  PivoteTable Refresh: {0}", pivotTables1.Item(i).Name);
       //                pivotTables1.Item(i).RefreshTable();

       //            }
       //        }
       //        else
       //        {
       //            Console.WriteLine("  !This worksheet contains no pivot tables.");
       //        }

       //    }

       //}
       //private static void refreshConnection(Workbook theWorkbook)
       //{
       //    foreach (WorkbookConnection i in theWorkbook.Connections)
       //    {
       //        Console.WriteLine("Connection refresh: {0}", i.Name);
       //        i.OLEDBConnection.BackgroundQuery = false;
       //        i.Refresh();
       //    }
       //}
       public void closeFile( Boolean gravar)
       {
           if(gravar) xlWorkbook.Save();
           xlWorkbook.Close(0);
           xlApp.Quit();
           System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
           System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
       }
      
    }
}
