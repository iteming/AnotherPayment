using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AnotherPayment.DB
{
    public class TelePhoneDB
    {


        private ConnDataBase cdb;

        public bool IsExist(string telePhone)
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "SELECT COUNT(ID) FROM TelePhone WHERE Phone='" + telePhone + "'";
            DataSet ds = cdb.GetData(strSql, "TelePhone");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int intRow = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                if (intRow > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AddTelePhone(string telePhone,string State)
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "INSERT INTO TelePhone Values ('" + telePhone + "','"+ State +"','" + DateTime.Now + "')";
            
            return cdb.ExecuteSQL(strSql);
        }


        public DataSet GetPhoneList(string telePhone, string state)
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "SELECT * FROM TelePhone WHERE 1=1 ";
            if (!string.IsNullOrEmpty(telePhone.Trim()))
            {
                strSql += " AND Phone='" + telePhone + "' ";
            }
            if (!string.IsNullOrEmpty(state.Trim()))
            {
                strSql += " AND State='" + state + "' ";
            }

            DataSet ds = cdb.GetData(strSql, "TelePhone");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }

            return null;
        }

        public bool UpdateState(string ID, string state)
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "UPDATE TelePhone Set state= '" + state + "' where id='" + ID + "' ";
            return cdb.ExecuteSQL(strSql);
        }
    }
}