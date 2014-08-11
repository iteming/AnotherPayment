using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnotherPayment.CommTools;
using AnotherPayment.DB;

namespace AnotherPayment
{
    public partial class AddAttach : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback)
            {
                ComClass.IsLoading(this.Page);

                if (Request.Params["action"] != null) 
                {
                    lblCoupon.Text = "上传成功，等待管理员审核，审核完毕，可以在此提取优惠码！";
                }

                //获取优惠券信息
                string UserName = Convert.ToString(Session["USERNAME"]);
                GetCoupon(UserName);
            }
        }

        private void IsExistAttachByNotExamine()
        {
            if (ComClass.IsExistAttachByNotExamine(this.Page))
            {
                //用户有未审批附件的时候，上传按钮不显示
                this.btnUpload.Visible = false;
            }
            else
            {
                this.btnUpload.Visible = true;
            }
        }

        private void GetCoupon(string UserName)
        {
            try
            {

                UserManageDB db = new UserManageDB();
                DataSet ds = db.GetCouponByUserName(UserName);
                string couponCode = "";
                string state = "";

                if (ds != null)
                {
                    couponCode = Convert.ToString(ds.Tables[0].Rows[0]["Coupon"]);
                    state = Convert.ToString(ds.Tables[0].Rows[0]["state"]);
                }

                if (state == "1")
                {
                    //state == 1，则非系统默认用户
                    this.btnUpload.Visible = true;

                    //判断是否存在 未审批的附件信息
                    IsExistAttachByNotExamine();

                    if (!string.IsNullOrEmpty(couponCode))
                    {
                        this.btnUpload.Visible = false;

                        lblCoupon.Text = couponCode;
                    }
                }
                else
                {
                    this.btnUpload.Visible = false;
                }

            }
            catch (Exception)
            {
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Response.Redirect("AttachUpload.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            lblCoupon.Text = "等待管理员审核，审核完毕，可以在此提取优惠码！";
            //获取优惠券信息
            string UserName = Convert.ToString(Session["USERNAME"]);
            GetCoupon(UserName);
        }

    }
}