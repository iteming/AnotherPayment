using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnotherPayment.DB;

namespace AnotherPayment
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsCallback)
            {
                //初次加载
                Session.Remove("USERNAME");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = this.UserName.Text.Trim();
            try
            {
                
                UserManageDB db = new UserManageDB();
                if (db.VerifiLogin(userName.ToLower()))
                {
                    //登陆成功
                    lblMsg.Text = "登陆成功！";
                    Session.Add("USERNAME", userName);

                    string defultUserName = ConfigurationSettings.AppSettings["defultUserName"];
                    if (defultUserName.ToLower() == userName.ToLower())
                    {
                        Response.Redirect("AddTelePhone.aspx");
                    }
                    else
                    {
                        Response.Redirect("AddAttach.aspx");
                    }
                }
                else
                {
                    //登陆失败
                    lblMsg.Text = "登陆失败，账号未通过审核，等待管理员审核完毕以后，使用您提交的手机号码登录！";
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "数据库连接失败！";
            }
        }

        protected void btnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

    }
}