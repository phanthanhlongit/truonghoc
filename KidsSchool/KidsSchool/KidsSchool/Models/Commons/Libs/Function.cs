using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace KidsSchool.Models.Commons.Libs
{
    public  class Function
    {
        public static Function Instance = new Function();

        #region Chuyen so sang chu
        public String DocSo(double n)
        {

            string strSo = n.ToString();
            string str = "";

            try
            {
                int len = (int)strSo.Length / 3;

                if (len * 3 < strSo.Length) len++;

                string[] t = new string[len];

                int i = 0;

                while (strSo != "")
                {

                    if (strSo.Length > 3)
                    {

                        t[i] = strSo.Substring(strSo.Length - 3, 3);

                        strSo = strSo.Substring(0, strSo.Length - 3);

                    }

                    else
                    {

                        t[i] = strSo;

                        strSo = "";

                    }

                    i++;

                }



                string temp;

                for (i = t.Length - 1; i >= 0; i--)
                {

                    temp = Doc3(t[i]);

                    if (temp.Trim() != "")
                    {

                        str += temp.Trim() + " " + DonVi(i + 1) + " ";

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;

        }
        public String DonVi(int n)
        {
            string str = "";
            switch (n.ToString())
            {
                case "0":
                    str = "VN";
                    break;
                case "1":
                    str = "đồng VN";
                    break;
                case "2":
                    str = "ngàn";
                    break;
                case "3":
                    str = "triệu";
                    break;
                case "4":
                    str = "tỷ";
                    break;
            }

            return str;

        }
        public String Doc3(string n)
        {
            string tram = string.Empty, chuc = string.Empty, dv = string.Empty;
            if (n.Length == 3)
            {
                tram = n[0].ToString();
                chuc = n[1].ToString();
                dv = n[2].ToString();
            }
            if (n.Length == 2)
            {
                chuc = n[0].ToString();
                dv = n[1].ToString();
            }
            if (n.Length == 1)
            {
                dv = n[0].ToString();
            }
            if (n != "000")
            {
                if (tram != "") tram = So(int.Parse(tram)) + " trăm";
                if (chuc != "")
                {
                    switch (chuc)
                    {
                        case "0":
                            if (dv != "0")
                            {
                                chuc = "lẻ"; dv = So(int.Parse(dv));
                            }
                            else
                            {
                                dv = "";
                                chuc = "";
                            }
                            break;
                        case "1":
                            chuc = " mười";
                            if (dv != "0")
                            {
                                if (dv == "5")
                                {
                                    dv = "lăm";
                                }
                                else
                                {
                                    dv = So(double.Parse(dv));
                                }
                            }
                            else
                            {
                                dv = "";
                            }
                            break;
                        default:
                            chuc = So(int.Parse(chuc)) + " mươi";
                            if (dv == "5")
                            {
                                dv = "lăm";
                            }
                            else
                            {
                                if (dv != "0") dv = So(int.Parse(dv));
                                else dv = "";
                            }
                            //dv = So(int.Parse(dv));
                            break;
                    }
                }
                else
                {
                    if (chuc != "")
                    {
                        switch (chuc)
                        {
                            case "1":
                                chuc = " mươi";
                                if (dv != "0")
                                {
                                    if (dv == "5")
                                    {
                                        dv = "lăm";
                                    }
                                    else
                                    {
                                        dv = So(int.Parse(dv));
                                    }
                                }
                                break;
                            default:
                                chuc = So(int.Parse(chuc)) + " mươi";
                                if (dv == "5")
                                {
                                    dv = "lăm";
                                }
                                else
                                {
                                    dv = So(int.Parse(dv));
                                }
                                break;
                        }
                    }
                    else
                    {
                        dv = So(int.Parse(dv));
                    }
                }
            }
            else
            {
                tram = "";
                chuc = "";
                dv = "";
            }
            return tram + " " + chuc + " " + dv;
        }
        public String So(double n)
        {
            string str = "";
            switch (n.ToString())
            {
                case "0":
                    str = "không";
                    break;
                case "1":
                    str = "một";
                    break;
                case "2":
                    str = "hai";
                    break;
                case "3":
                    str = "ba";
                    break;
                case "4":
                    str = "bốn";
                    break;
                case "5":
                    str = "năm";
                    break;
                case "6":
                    str = "sáu";
                    break;
                case "7":
                    str = "bảy";
                    break;
                case "8":
                    str = "tám";
                    break;
                case "9":
                    str = "chín";
                    break;
            }
            return str;
        }
        #endregion

        #region dấu phân cách phần trăm
        public string ChangeTypeMoney(string str)
        {
            int count = str.Length % 3;
            int flg = 0;
            int number = str.Length / 3;
            if (count == 0)
                number = number - 1;
            for (int z = 1; z <= number; z++)
            {
                if (str.Length - 3 * z - flg >= count)
                {
                    str = str.Insert(str.Length - 3 * z - flg, ".");
                    flg = flg + 1;
                }
            }


            return str;
        }
        #endregion

        #region AddFieldSttToDataTable
        /// <summary>
        /// Thêm trường số thứ tự (STT) vào trong DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable AddSTTToDataTable(DataTable dt)
        {
            int intPos;
            intPos = 0;
            DataTable dtReturn = new DataTable();
            try
            {

                dtReturn = dt;

                dtReturn.Columns.Add("STT");

                if (dtReturn.DefaultView.Count > 0)
                {
                    foreach (DataRow dr in dtReturn.Rows)
                    {

                        dr["STT"] = intPos + 1;

                        intPos += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtReturn;
        }
        #endregion

        #region HasKyTuLa
        /// <summary>
        /// Kiểm tra một String có chứa ký tự lạ hay không
        /// </summary>
        /// <param name="strTest"></param>
        /// <returns></returns>
        public bool HasKyTuLa(string strTest)
        {
            bool bTest;
            bTest = true;
            try
            {
                if (strTest.IndexOf(' ') > 0)
                {
                    bTest = false;
                }
                else
                {
                    for (int i = 0; i < strTest.Length; i++)
                    {
                        if (((strTest[i] < 'A') || (strTest[i] > 'z')) && ((strTest[i] < '0') || (strTest[i] > '9')))
                            bTest = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bTest;
        }
        #endregion

        /// <summary>
        /// Kiểm tra xem một chuỗi có phải là một dữ liệu kiểu số hay không
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool IsNumber(String number)
        {
            bool bTest;
            bTest = true;
            try
            {
                Convert.ToDecimal(number.Trim());
            }
            catch
            {
                bTest = false;
            }
            return bTest;
        }

        #region ExportReportToWord
        /// <summary>
        /// Xuất báo cáo ra file word
        /// </summary>
        /// <param name="page"></param>
        /// <param name="report"></param>
        //public void ExportReportToWord(Page page, ReportClass report)
        //{
        //    MemoryStream oStream; // using System.IO
        //    oStream = (MemoryStream)
        //    report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
        //    page.Response.Clear();
        //    page.Response.Buffer = true;
        //    page.Response.ContentType = "application/msword";
        //    page.Response.BinaryWrite(oStream.ToArray());
        //    page.Response.End();
        //}
        
        #endregion
        #region ExportReportToPDF
        /// <summary>
        /// Xuất báo cáo ra file PDF
        /// </summary>
        /// <param name="page"></param>
        /// <param name="report"></param>
        //public void ExportReportToPDF(Page page, ReportClass report)
        //{
        //    MemoryStream oStream; // using System.IO
        //    oStream = (MemoryStream)
        //    report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    page.Response.Clear();
        //    page.Response.Buffer = true;
        //    page.Response.ContentType = "application/pdf";
        //    page.Response.BinaryWrite(oStream.ToArray());
        //    page.Response.End();
        //}
        #endregion
        

        





    }
}
