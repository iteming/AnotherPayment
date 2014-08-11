using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AnotherPayment.DB
{
    public class CouponDB
    {
        private ConnDataBase cdb;

        public string GetACoupon()
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "SELECT ID,CouponCode FROM CouponCode WHERE state=0 ORDER BY id";
            DataSet ds = cdb.GetData(strSql, "CouponCode");

            if (ds != null && ds.Tables[0].Rows.Count>0)
            {
                string Coupon = Convert.ToString(ds.Tables[0].Rows[0]["CouponCode"]);
                string ID = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);

                string sql = "UPDATE CouponCode SET state=1 WHERE ID='" + ID + "'";
                if (cdb.ExecuteSQL(sql))
                {
                    return Coupon;
                }
            }

            return "";
        }

        public int GetCouponNoUsed() 
        {
            cdb = new ConnDataBase();
            //取得剩余密码券数量
            string strSql = "SELECT count(ID) FROM CouponCode WHERE state=0";
            DataSet ds = cdb.GetData(strSql, "CouponCode");
            if (ds != null && ds.Tables.Count>0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            return -1;
        }

        public bool CreateCoupon(List<string> strList)
        {
            cdb = new ConnDataBase();
            ArrayList myArray = new ArrayList();
            foreach (string item in strList)
            {
                string strSql = "INSERT INTO CouponCode Values ('" + item + "','0')";
                myArray.Add(strSql);
            }
            //取得表结构
            int rows = cdb.ExecuteSQL((string[])myArray.ToArray(typeof(string)));
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}