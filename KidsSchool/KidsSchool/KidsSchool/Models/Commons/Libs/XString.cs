using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using KidsSchool.Models;
using KidsSchool.Models.DB;

static public class Xstring
{
    #region Chuyen so sang chu
    public static String DocSo(double n)
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
    public static String DonVi(int n)
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
    public static String Doc3(string n)
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
    public static String So(double n)
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

    static public string ReplaceInsensitive(this String str, String from, String to)
    {
        str = Regex.Replace(str, from, to, RegexOptions.IgnoreCase);
        return str;
    }

    public static Double? KRound(this Double num)
    {
        if (num < 1000)
        {
            return num;
        }

        return num - (num % 1000);
    }
    public static String Catchu(this String s,int kytu)

    {
        if(s==null)
        {
            return "";
        }
      if(s.Length> kytu)
        {
            return s.Substring(0, kytu) + "...";
        }else
        {
            return s;
        }
    }
    public static String ToAscii(this String s)//chuyển sang ko dấu dang link
    {
        if (s == null) return "";

        String[][] symbols = { 
                                 new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]", "a" }, 
                                 new String[] { "[đ]", "d" },
                                 new String[] { "[éèẻẽẹêếềểễệ]", "e" },
                                 new String[] { "[íìỉĩị]", "i" },
                                 new String[] { "[óòỏõọôốồổỗộơớờởỡợ]", "o" },
                                 new String[] { "[úùủũụưứừửữự]", "u" },
                                 new String[] { "[ýỳỷỹỵ]", "y" },
                                 new String[] { "[&+?.,)(]@%", "-" },
                                 new String[] { "[\\s'\"| ]", "-" },
                                 new String[] { "-{2,10}", "-" }
                             };
        s = s.ToLower();
        foreach (var ss in symbols)
        {
            s = Regex.Replace(s, ss[0], ss[1]);
        }
        return s.Replace("--","-");
    }
    public static String ToAscii2(this String s)//chuyển sang ko dấu
    {
        if (s == null) return "";

        String[][] symbols = {
                                 new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]", "a" },
                                 new String[] { "[đ]", "d" },
                                 new String[] { "[éèẻẽẹêếềểễệ]", "e" },
                                 new String[] { "[íìỉĩị]", "i" },
                                 new String[] { "[óòỏõọôốồổỗộơớờởỡợ]", "o" },
                                 new String[] { "[úùủũụưứừửữự]", "u" },
                                 new String[] { "[ýỳỷỹỵ]", "y" },
                                 new String[] { "[&+?.,)(]", " " },
                                 new String[] { "[\\s'\" ]", " " },
                                 new String[] { "-{2,10}", " " }
                             };
        s = s.ToLower();
        foreach (var ss in symbols)
        {
            s = Regex.Replace(s, ss[0], ss[1]);
        }
        return s;
    }
    public static String ToUniqueUrl(this String s, Entities db)
    {
        if (s == null) return "";

        String[][] symbols = {
        // ... Các luật chuyển đổi URL như bạn đã định nghĩa ...
    };

        s = s.ToAscii();

        string originalUrl = s;
        int counter = 1;

        while (true)
        {
            // Kiểm tra xem URL đã tồn tại trong cơ sở dữ liệu hay chưa
            bool urlExists = db.SeoUrlRecords.Any(entity => entity.url == s);

            if (!urlExists)
            {
                return s;
            }

            // Nếu URL đã tồn tại, thêm số đằng sau và kiểm tra lại
            s = $"{originalUrl}-{counter}";
            counter++;
        }
    }

    public static bool hasSpecialCharUrl(this string input)
    {
        string specialChar = @"\|!#$%&/()=?»«@£§€{}.;'<>_,";
        foreach (var item in specialChar)
        {
            if (input.Contains(item)) return true;
        }

        return false;
    }


    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source.IndexOf(toCheck, comp) >= 0;
    }

    public static String HighlightKeywords(this String input, String keywords)
    {
        if (input == string.Empty || keywords == string.Empty)
        {
            return input;
        }

        string[] sKeywords = keywords.Split(' ');
        foreach (string sKeyword in sKeywords)
        {
            try
            {
                input = Regex.Replace(input, sKeyword, string.Format("<span class=\"highlighted\">{0}</span>", "$0"), RegexOptions.IgnoreCase);
            }
            catch
            {
                //
            }
        }
        return input;
    }

    private static string GetKeyword(string strU)
    {
        Regex regKeyword = new Regex("(p|q)=(?<keyword>.*?)&", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        Regex regSnippet = new Regex("forgetcode.com.*%2F(?<id>\\d+)%2F", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        Match match = regKeyword.Match(strU);
        string keyword = match.Groups["keyword"].ToString();
        // Get the decoded URL
        string result = HttpUtility.UrlDecode(keyword);
        // Get the HTML representation
        result = HttpUtility.HtmlEncode(result);

        return result;

    }

    /// <summary>
    /// Remove HTML from string with Regex.
    /// </summary>
    public static string StripTagsRegex(this string source)//xóa tag
    {
        return Regex.Replace(source, "<.*?>", string.Empty);
    }

    public static string Remove_Html_Tags(this string Html)//xóa html
    {
        string Only_Text = "";
        if (!string.IsNullOrEmpty(Html))
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = Html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);
            Only_Text = text;
        }

        return Only_Text;
    }

    public static string LimitLength(this string orgText, int maxLength, string append)
    {
        if (orgText == null) return null;
        if (orgText.Length <= maxLength) return orgText;
        orgText = HttpContext.Current.Server.HtmlDecode(orgText);
        var words = orgText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var sb = new StringBuilder();
        foreach (var word in words)
        {
            if ((sb + word).Length > maxLength)
                return string.Format("{0}{1}", sb.ToString().TrimEnd(' '), append);
            sb.Append(word + " ");
        }
        return string.Format("{0}{1}", sb.ToString().TrimEnd(' '), append);

        //return string.Format("{0}{1}", orgText.Substring(0, maxLength), append);
    }

    public static string ResolveServerUrl(this string serverUrl, bool forceHttps = false)
    {
        if (serverUrl.IndexOf("://") > -1)
            return serverUrl;

        string newUrl = serverUrl;
        Uri originalUri = System.Web.HttpContext.Current.Request.Url;
        newUrl = (forceHttps ? "https" : originalUri.Scheme) +
            "://" + originalUri.Authority + newUrl;
        return newUrl;
    }


    public static string GeneratePassword()
    {
        //Since I'm big on security, I wanted to generate passwords that contained numbers, letters and special
        //characters - and not easily hacked.

        //I started with creating three string variables.
        //This one tells you how many characters the string will contain.
        string PasswordLength = "12";
        //This one, is empty for now - but will ultimately hold the finised randomly generated password
        string NewPassword = "";

        //This one tells you which characters are allowed in this new password
        string allowedChars = "";
        allowedChars = "1,2,3,4,5,6,7,8,9,0";
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        allowedChars += "~,!,@,#,$,%,^,&,*,+,?";

        //Then working with an array...

        char[] sep = { ',' };
        string[] arr = allowedChars.Split(sep);

        string IDString = "";
        string temp = "";

        //utilize the "random" class
        Random rand = new Random();

        //and lastly - loop through the generation process...
        for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            IDString += temp;
            NewPassword = IDString;

            //For Testing purposes, I used a label on the front end to show me the generated password.
            //lblProduct.Text = IDString;
        }

        return NewPassword;
    }

    public static List<WebsiteData> ConvertPageIsSpecial(this string htmlContent)//xóa html
    {
        HtmlDocument document = new HtmlDocument();
        document.LoadHtml(htmlContent);

        List<WebsiteData> websiteDataList = new List<WebsiteData>();
        WebsiteData currentData = new WebsiteData();
        int stt = 1;

        foreach (HtmlNode node in document.DocumentNode.Descendants())
        {
            if (node.Name == "img")
            {
                if (!string.IsNullOrEmpty(currentData.Images) ||
                    currentData.H1.Count > 0 ||
                    currentData.H2.Count > 0 ||
                    currentData.H3.Count > 0 ||
                    currentData.H4.Count > 0 ||
                    currentData.H5.Count > 0 ||
                    currentData.OtherTags.Count > 0)
                {
                    currentData.Stt = stt++;
                    websiteDataList.Add(currentData);
                    currentData = new WebsiteData();
                }

                currentData.Images = node.GetAttributeValue("src", "");
            }
            else if (node.Name.StartsWith("h") && node.Name.Length == 2 && char.IsDigit(node.Name[1]))
            {
                var headerText = GetInnerText(node);
                if (!string.IsNullOrWhiteSpace(headerText))
                {
                    switch (node.Name)
                    {
                        case "h1":
                            currentData.H1.Add(headerText);
                            break;
                        case "h2":
                            currentData.H2.Add(headerText);
                            break;
                        case "h3":
                            currentData.H3.Add(headerText);
                            break;
                        case "h4":
                            currentData.H4.Add(headerText);
                            break;
                        case "h5":
                            currentData.H5.Add(headerText);
                            break;
                    }
                }
            }
            else if (node.Name == "p" || node.Name == "li")
            {
                var innerText = GetInnerText(node);
                if (!string.IsNullOrWhiteSpace(innerText))
                {
                    currentData.OtherTags.Add(innerText);
                }
            }
        }

        // Thêm dữ liệu từ phần tử cuối cùng vào danh sách
        if (!string.IsNullOrEmpty(currentData.Images) ||
            currentData.H1.Count > 0 ||
            currentData.H2.Count > 0 ||
            currentData.H3.Count > 0 ||
            currentData.H4.Count > 0 ||
            currentData.H5.Count > 0 ||
            currentData.OtherTags.Count > 0)
        {
            currentData.Stt = stt++;
            websiteDataList.Add(currentData);
        }


        return websiteDataList;
    }

    static string GetInnerText(HtmlNode node)
    {
        if (node.HasChildNodes)
        {
            var innerText = "";
            foreach (var childNode in node.ChildNodes)
            {
                if (childNode.NodeType == HtmlNodeType.Text)
                {
                    innerText += childNode.InnerText;
                }
                else if (childNode.NodeType == HtmlNodeType.Element)
                {
                    innerText += GetInnerText(childNode);
                }
            }
            return innerText.Trim();
        }
        return node.InnerText.Trim();
    }

}
