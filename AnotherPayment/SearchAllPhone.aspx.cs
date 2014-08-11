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
    public partial class SearchAllPhone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback)
            {
                GvDataBind();
                ComClass.IsLoading(this.Page);
            }
        }

        private void GvDataBind() 
        {
            string telePhone = this.txtTelePhone.Text.Trim();
            string state = this.dllState.SelectedValue;

            try
            {
                TelePhoneDB db = new TelePhoneDB();
                DataSet ds = db.GetPhoneList(telePhone,state);
                this.gvPhone.DataSource = ds;
                this.gvPhone.DataKeyNames = new string[] { "ID" };
                this.gvPhone.DataBind();
            }
            catch (Exception)
            {
                this.lblMsg.Text = "绑定列表失败！";
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPhone.PageIndex = e.NewPageIndex;
            GvDataBind(); //重新绑定GridView数据的函数
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GvDataBind();
        }

        protected void gvPhone_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = Convert.ToString(this.gvPhone.DataKeys[e.RowIndex].Value);

            try
            {
                TelePhoneDB db = new TelePhoneDB();
                string state = "1";
                if (db.UpdateState(ID, state))
                {
                    this.lblMsg.Text = "审批通过！";
                    UserManageDB userDb = new UserManageDB();
                    string strPhone = ((Label)this.gvPhone.Rows[e.RowIndex].FindControl("lblPhone")).Text;
                    userDb.CreateUser(strPhone, "123");
                }
            }
            catch (Exception)
            {
                this.lblMsg.Text = "审批失败！";
            }
            GvDataBind();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage.aspx");
        }


        public string VerifyPhone(object obj)
        {
            if (obj != null)
            {
                int intState = Convert.ToInt32(obj);
                if (intState == 0)
                {
                    return "未审批";
                }
                else
                {
                    return "已审批";
                }
            }
            else
            {
                return "--";
            }
        }


        public bool VerifyEnable(object obj)
        {
            if (obj != null)
            {
                int intState = Convert.ToInt32(obj);
                if (intState == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        protected void btnAuto_Click(object sender, EventArgs e)
        {
            XmlNode xn = null;
            XmlDocument XmlDoc = XmlTools.GetXMLNode("isAuto",ref xn);
            
            if (xn != null)
            {
                if (Convert.ToBoolean(xn.InnerXml))
                {
                    this.lblMsg.Text = "设置手动审核成功！";
                    xn.InnerXml = "false";
                    XmlDoc.Save(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "XmlDoc\\XmlDocHelper.xml");
                    //ModifyAppSettings("isAuto", "false");
                }
                else
                {
                    this.lblMsg.Text = "设置自动审核成功！";
                    xn.InnerXml = "true";
                    XmlDoc.Save(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "XmlDoc\\XmlDocHelper.xml");
                    //ModifyAppSettings("isAuto", "true");
                }
            }

        }


        static void DisplayAppSettings()
        {
            // Get the configuration file.
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //Get the appSettings section.
            AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");

            if (appSettings != null)
            {
                foreach (string key in appSettings.Settings.AllKeys)
                {
                    string value = appSettings.Settings[key].Value;
                    Console.WriteLine("Key: {0} Value: {1}", key, value);
                }
            }
        }

        /// <summary>        
        /// 修改配置文件(appSettings)
        /// </summary>    
        /// <param name="key"></param>  
        /// <param name="value"></param>  
        private static void ModifyAppSettings(string key, string value)
        {
            //--修改RunTime(内存)中的App.config
            // Open App.Config of executable
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Add an Application Setting.
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);

            // Save the configuration file.         
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section.           
            ConfigurationManager.RefreshSection("appSettings");

            //--修改App.config文件    
            XmlDocument xDoc = new XmlDocument();

            //获取可执行文件的路径和名称            
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

            XmlNode xNode;
            XmlElement xElem1;
            XmlElement xElem2;

            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key=" + key + "]");
            if (xElem1 != null)
                xElem1.SetAttribute("value", value);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", key);
                xElem2.SetAttribute("value", value);
                xNode.AppendChild(xElem2);
            }

            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");

        }
    }
}