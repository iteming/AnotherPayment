using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnotherPayment.CommTools;
using AnotherPayment.DB;

namespace AnotherPayment
{
    public partial class AddCoupon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback)
            {
                ComClass.IsLoading(this.Page);
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            int intNum = Convert.ToInt32(this.txtNun.Text.Trim());
            //生成优惠券规则
            List<string> strList = ComClass.GetCoupon(intNum);

            try
            {
                CouponDB db = new CouponDB();
                if (db.CreateCoupon(strList)) 
                {
                    //生成优惠券成功
                    ListBox1.DataSource = strList;
                    ListBox1.DataBind();
                }
            }
            catch (Exception)
            {
                //生产优惠券失败
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage.aspx");
        }
    }
}