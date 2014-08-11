using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnotherPayment
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnManage_Click(object sender, EventArgs e)
        {
            string UserName = this.UserName.Text.Trim();
            string passWord = this.Password.Text.Trim();

            string adminUserName = ConfigurationSettings.AppSettings["adminUserName"];
            string adminPassword = ConfigurationSettings.AppSettings["adminPassword"];

            if (UserName == adminUserName && passWord == adminPassword)
            {
                Session.Remove("USERNAME");
                Session.Add("USERNAME", UserName);
                Response.Redirect("Manage.aspx");
            }
            else
            {
                this.lblMsg.Text = "超级管理员用户名密码错误！";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}