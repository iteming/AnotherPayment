using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AnotherPayment.DB
{
    public class AttachManageDB
    {
        private ConnDataBase cdb;

        public bool CreateAttach(string UserName, string FilePath,string FileName)
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "INSERT INTO AttachInfo Values ('" + FileName + "','" + FilePath + "','" + UserName + "','0')";
            return cdb.ExecuteSQL(strSql);
        }

        public DataSet GetPhoneList(string UploadUser, string State)
        {
            cdb = new ConnDataBase();
            //取得表结构
            string strSql = "SELECT * FROM AttachInfo WHERE 1=1 ";
            if (!string.IsNullOrEmpty(UploadUser.Trim()))
            {
                strSql += " AND UploadUser='" + UploadUser + "' ";
            }
            if (!string.IsNullOrEmpty(State.Trim()))
            {
                strSql += " AND State='" + State + "' ";
            }

            DataSet ds = cdb.GetData(strSql, "AttachInfo");

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
            string strSql = "UPDATE AttachInfo Set state= '" + state + "' where id='" + ID + "' ";
            return cdb.ExecuteSQL(strSql);
        }
    }
}