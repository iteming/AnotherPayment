using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using AnotherPayment.CommTools;
using AnotherPayment.DB;
using AnotherPayment.Models;
using Scrape.Core;

namespace AnotherPayment
{
    public partial class AddTelePhone : System.Web.UI.Page
    {
        private static string Referer = "";
        //private static string Referer = "http://211.151.124.231:8091/download.html?c=140506";
        //private static string Url = "http://211.151.124.231:8091/ShortUrlServlet?operate=submit&url=http%3A%2F%2Fcitic.lnk8.cn%2Fpk8og8&mobile=";

        private static string Url1 = ConfigurationManager.AppSettings["Url1"];
        private static string Url2 = ConfigurationManager.AppSettings["Url2"];
        private static string Url3 = ConfigurationManager.AppSettings["Url3"];
        private static string Url4 = ConfigurationManager.AppSettings["Url4"];
        private static string Url5 = ConfigurationManager.AppSettings["Url5"];
        private static string Url6 = ConfigurationManager.AppSettings["Url6"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback)
            {
                ComClass.IsLoading(this.Page);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string telePhone = this.txtTelePhone.Text.Trim();
            
            if (string.IsNullOrEmpty(telePhone))
            {
                lblMsg.Text = "请输入手机号！";
                return;
            }

            try
            {
                ViewState["UserName"] = telePhone;
                TelePhoneDB db = new TelePhoneDB();
                if (!db.IsExist(telePhone)) 
                {
                    if (!VerifyPhone(telePhone))
                    {
                        this.lblMsg.Text = "无效的URI: 指定的端口无效，请联系管理员！";
                        return;
                    }
                    
                    XmlNode xn = null;
                    XmlDocument XmlDoc = XmlTools.GetXMLNode("isAuto",ref xn);
                    bool blIsAuto = false;
                    if (xn != null)
                    {
                        //切换是否自动审核
                        blIsAuto = Convert.ToBoolean(xn.InnerXml);
                    }

                    if (blIsAuto)
	                {
                        //不存在，可以新增，并且审核通过
                        if (db.AddTelePhone(telePhone, "1"))
                        {
                            UserManageDB userDb = new UserManageDB();
                            userDb.CreateUser(telePhone, "123");
                            btnSubmit.Visible = false;
                            this.lblMsg.Text = "新增成功，号码已经审核通过，请点击下一步上传图片！";
                        }
                    }
                    else
                    {
                        //不存在，可以新增
                        if (db.AddTelePhone(telePhone, "0"))
                        {
                            this.lblMsg.Text = "新增成功，等待管理员审核，审核完毕以后，直接用您提交的手机号码登录！";
                        }
                    }
                }
                else
                {
                    //存在，提示
                    this.lblMsg.Text = "号码已经存在！";
                }
            }
            catch (Exception)
            {
                this.lblMsg.Text = "新增失败！";
            }

        }

        private bool VerifyPhone(string telePhone)
        {
            //string Json = ComClass.Get_Method(Url1 + telePhone.Substring(1), Referer);
            //Payment pm = JsonTools.ToObj<Payment>(Json);

            ////如果抓包返回状态不等于 "1"
            //if (pm.status != "1")
            //{
            //    lblMsg.Text = "Error:" + pm.status + ",您提交的手机号码已经注册过异度支付！";
            //}

            try
            {
                string Json1 = ComClass.Get_Method(Url1 + telePhone, Referer);
                string Json2 = ComClass.Get_Method(Url2 + telePhone, Referer);
                string Json3 = ComClass.Get_Method(Url3 + telePhone.Substring(1), Referer);
                string Json4 = ComClass.Get_Method(Url4 + telePhone.Substring(1), Referer);
                string Json5 = ComClass.Get_Method(Url5 + telePhone.Substring(1), Referer);
                string Json6 = ComClass.Get_Method(Url6 + telePhone.Substring(1), Referer);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string userName = Convert.ToString(ViewState["UserName"]);
            if (userName.Trim() != "")
	        {
                UserManageDB db = new UserManageDB();
                if (db.VerifiLogin(userName)) 
                {
                    Session["USERNAME"] = userName;
                    Response.Redirect("AddAttach.aspx");
                }
                else
                {
                    lblMsg.Text = "号码未审核，等待管理员审核完毕以后，直接用您提交的手机号码登录！";
                }
            }
            else
            {
                lblMsg.Text = "请输入手机号！";
            }
        }

    }
}