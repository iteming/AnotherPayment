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
    public partial class SearchAttach : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback)
            {
                GvDataBind();
                ComClass.IsLoading(this.Page);

                SelectNoUsedCode("");                
            }
        }

        private bool SelectNoUsedCode(string isNext)
        {
            CouponDB couponDb = new CouponDB();
            int intNoUsedCoupon = couponDb.GetCouponNoUsed();
            if (intNoUsedCoupon == -1)
            {
                this.lblCouponCode.Text = "获取优惠码失败！";
                return false;
            }
            else if (intNoUsedCoupon == 0)
            {
                this.lblCouponCode.Text = "优惠码已经使用完！请重新添加优惠码！";
                return false;
            }
            else
            {
                if (isNext == "NEXT")
                {
                    this.lblCouponCode.Text = "优惠码剩余 " + (intNoUsedCoupon - 1) + " 张！";
                }
                else
                {
                    this.lblCouponCode.Text = "优惠码剩余 " + intNoUsedCoupon + " 张！";
                }
                return true;
            }
        }
        private void GvDataBind()
        {
            string uploadUser = this.txtUploadUser.Text.Trim();
            string state = this.dllState.SelectedValue;

            try
            {
                AttachManageDB db = new AttachManageDB();
                DataSet ds = db.GetPhoneList(uploadUser, state);
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
                //修改附件审批状态，并且发放优惠码
                UpdateStateAndGrantCoupon(((Label)this.gvPhone.Rows[e.RowIndex].FindControl("lblUploadUser")).Text,ID);
            }
            catch (Exception)
            {
                this.lblMsg.Text = "发放失败！";
            }
            GvDataBind();
        }

        private void UpdateStateAndGrantCoupon(string UploadUser,string ID)
        {
            //获取一枚优惠码
            CouponDB couponDb = new CouponDB();
            string strCoupon = "";
            if (IsHaveCoupon(UploadUser))
            {
                lblMsg.Text = "该用户已经有优惠码了，不可再次发放！";
            }
            else
            {
                if (!SelectNoUsedCode("NEXT")) 
                {
                    return;
                }

                strCoupon = couponDb.GetACoupon();

                if (!string.IsNullOrEmpty(strCoupon))
                {
                    //进行发放
                    UserManageDB userDb = new UserManageDB();
                    if (userDb.UpdateCoupon(UploadUser, strCoupon))
                    {
                        this.lblMsg.Text = "发放成功！";
                    }
                    else
                    {
                        this.lblMsg.Text = "发放失败！";
                    }
                }
                else
                {
                    this.lblMsg.Text = "获取优惠码失败！";
                    return;
                }
            }

            //修改附件审批成功
            AttachManageDB db = new AttachManageDB();
            string state = "1";
            db.UpdateState(ID, state);
        }

        private bool IsHaveCoupon(string UploadUser)
        {
            UserManageDB db = new UserManageDB();
            DataSet ds = db.GetCouponByUserName(UploadUser);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string coupon =Convert.ToString(ds.Tables[0].Rows[0]["Coupon"]).Trim();
                    if (!string.IsNullOrEmpty(coupon))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage.aspx");
        }

        public string VerifyState(object obj)
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

        public string GetPath(object obj)
        {
            if (obj != null)
            {
                string UploadPath = Convert.ToString(obj);

                string serPath1 = ConfigurationSettings.AppSettings["serPathServer"];

                return serPath1 + UploadPath;
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
    }
}