using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnotherPayment.CommTools;
using AnotherPayment.DB;

namespace AnotherPayment
{
    public partial class AttachUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback)
            {
                ComClass.IsLoading(this.Page);
                //判断是否存在 未审批的附件信息
                IsExistAttachByNotExamine();
            }
        }

        private void IsExistAttachByNotExamine()
        {
            if (ComClass.IsExistAttachByNotExamine(this.Page)) 
            {
                this.btnUpload.Visible = false;
            }
            else
            {
                this.btnUpload.Visible = true;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ComClass.IsLoading(this.Page);
            UploadClick();
            //判断是否存在 未审批的附件信息
            IsExistAttachByNotExamine();
        }

        protected void UploadClick()
        {
            try
            {
                if (this.FileUpload1.PostedFile.FileName.Length > 0)
                {
                    if (FileUpload1.PostedFile != null)
                    {
                        //获取上传文件类型//
                        string FileType = FileUpload1.PostedFile.ContentType.ToString();

                        //获取文件名//
                        string FilePath = FileUpload1.PostedFile.FileName.ToString();

                        int iPoint = FilePath.LastIndexOf(".");
                        //获取扩展名//
                        string FileExt = FilePath.Substring(iPoint);
                        //选择文件格式
                        if (FileExt == ".bmp" || FileExt == ".jpg" || FileExt == "gif" || FileExt == ".jpeg" || FileExt == ".png" || FileExt == ".BMP" || FileExt == ".JPG" || FileExt == "GIF" || FileExt == ".JPEG" || FileExt == ".PNG")
                        {
                            //重新设置一个文件名//
                            string FrontFileName = Convert.ToString(Session["USERNAME"]);
                            if (string.IsNullOrEmpty(FrontFileName))
                            {
                                DateTime now = DateTime.Now;
                                FrontFileName = now.ToFileTimeUtc() + FileUpload1.PostedFile.ContentLength.ToString();
                            }
                            string UploadPath = FrontFileName + FileExt;
                            //string serPath = ConfigurationSettings.AppSettings["serPath"];
                            string serPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "upLoad\\";

                            string serPath1 = ConfigurationSettings.AppSettings["serPathServer"];

                            //创建目录的对象http://192.168.1.122/upLoad
                            System.IO.DirectoryInfo dire = new System.IO.DirectoryInfo(serPath);


                            //判断目录是否存在
                            if (!dire.Exists)
                            {
                                //不存在创建文件
                                dire.Create();
                            }
                            //文件保存
                            FileUpload1.PostedFile.SaveAs(serPath + UploadPath);


                            int iBars = FilePath.LastIndexOf(@"\") + 1;
                            string FileName = FilePath.Substring(iBars, iPoint - iBars);
                            //服务器文件路径（显示用）
                            this.hlFile1.NavigateUrl = serPath1 + UploadPath;
                            this.hlFile1.Text = FileName;

                            SaveToDB(Convert.ToString(Session["USERNAME"]), UploadPath, FileName);
                            this.lblMsg.Text = "上传成功，等待管理员审核，审核完毕，再用您提交的手机号码提取优惠码！";
                            Response.Redirect("AddAttach.aspx?action=upload");                            
                        }
                        else
                        {
                            this.lblMsg.Text = "请选择正确的文件类型，文件类型可为：BMP,JPG,GIF,JPEG,PNG,BMP,JPG,GIF,JPEG,PNG！";
                        }

                    }
                    else
                    {
                    }
                }
            }
            catch {
                lblMsg.Text = "上传文件发生异常！";
            }
            finally { }


        }

        private void SaveToDB(string UserName, string UploadPath, string FileName)
        {
            try 
	        {	        
                AttachManageDB db = new AttachManageDB();
                DataSet ds = db.GetPhoneList(UserName, "");
                if (ds!= null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return;
                    }
                }

                db.CreateAttach(UserName, UploadPath, FileName);
	        }
	        catch (Exception)
	        {
		        this.lblMsg.Text += "新增附件失败！";
	        }
        }
    }
}