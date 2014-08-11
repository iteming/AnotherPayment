using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AnotherPayment.DB;
using Scraper_Page;

namespace AnotherPayment.CommTools
{
    public class ComClass
    {
        public static void IsLoading(System.Web.UI.Page page)
        {
            if (page.Session["USERNAME"] == null)
            {
                //请重新登陆
                page.Response.Redirect("login.aspx");
            }
        }

        public static List<string> GetCoupon(int intNum)
        {
            List<string> strList = new List<string>();
            
            Random RD = new Random();
            for (int i = 0; i < intNum; i++)
            {
                DateTime now = DateTime.Now;
                strList.Add(Convert.ToString(now.ToFileTimeUtc() + RD.Next(100)));
            }
            return strList;
        }

        public static bool IsExistAttachByNotExamine(System.Web.UI.Page page)
        {
            //获取优惠券信息
            string UserName = Convert.ToString(page.Session["USERNAME"]);
            try
            {
                AttachManageDB DB = new AttachManageDB();
                DataSet ds = DB.GetPhoneList(UserName, "0");

                if (ds!= null && ds.Tables.Count> 0)
                {
                    if (ds.Tables[0].Rows.Count > 0) 
                    {
                        //该用户存在附件信息
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static string Get_Method(string url, string referer)
        {
            IDownloadPageSource DPSrc = new PageSource_GetMethod(url, referer) { EncodingName = "UTF-8" };
            return DPSrc.GetPageSource();
        }

        public static string Post_Method(string url,string Postdata ,string referer)
        {
            IDownloadPageSource DPSrc = new PageSource_PostMethod(url, Postdata, referer) { EncodingName = "UTF-8" };
            return DPSrc.GetPageSource();
        }
    }
}