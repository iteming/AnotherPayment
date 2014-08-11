using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AnotherPayment.DB
{
    public class UserManageDB
    {

        private ConnDataBase cdb;

        public bool VerifiLogin(string userName)
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "SELECT COUNT(ID) FROM UserInfo WHERE UserName='" + userName + "' ";
            DataSet ds = cdb.GetData(strSql, "UserInfo");

            if (ds != null && ds.Tables[0].Rows.Count>0)
            {
                int intRow = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                if (intRow > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void CreateUser(string UserName, string password)
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "INSERT INTO UserInfo Values ('" + UserName + "','" + password + "',NULL,'1')";
            cdb.ExecuteSQL(strSql);
        }

        public bool UpdateCoupon(string UploadUser, string strCoupon)
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "UPDATE UserInfo SET COUPON = '" + strCoupon + "'  WHERE UserName='" + UploadUser + "' ";
            return(cdb.ExecuteSQL(strSql));
        }

        public DataSet GetCouponByUserName(string UserName)
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "SELECT Coupon,State FROM UserInfo WHERE UserName='" + UserName + "' ";
            DataSet ds = cdb.GetData(strSql, "UserInfo");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
    }
}