using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AnotherPayment
{
    public class ConnDataBase 
    {
        #region 全局变量
        private SqlDataAdapter dataAdapter;
        private SqlTransaction myTrans;
        private string connectionString = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;

        /// <summary>
        /// 开启数据连接
        /// </summary>
        private void InitConnect()
        {
            try
            {
                this.dataAdapter = new SqlDataAdapter();
                this.dataAdapter.SelectCommand = new SqlCommand();
                this.dataAdapter.SelectCommand.Connection = new SqlConnection();
                string constr = connectionString;
                if (!string.IsNullOrEmpty(constr))
                {
                    this.dataAdapter.SelectCommand.Connection.ConnectionString = constr;
                }
                else
                {
                    //throw new ZHException("配置文件中缺少数据库连接配置信息！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询方法
        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="sqlstr">SQL语句</param>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public DataSet GetData(string sqlstr, string tableName)
        {

            DataSet dataSet = new DataSet();
            if (Information.IsNothing(this.dataAdapter))
            {
                this.InitConnect();
            }
            SqlDataAdapter dataAdapter = this.dataAdapter;
            try
            {
                dataAdapter.SelectCommand.Connection.Open();
                if (dataAdapter.TableMappings.IndexOf(tableName) < 0)
                {
                    dataAdapter.TableMappings.Add(tableName, tableName);
                }
                SqlCommand selectCommand = dataAdapter.SelectCommand;
                selectCommand.CommandType = CommandType.Text;
                selectCommand.CommandText = sqlstr;
                selectCommand = null;

                dataAdapter.Fill(dataSet, tableName);
                dataAdapter.Fill(dataSet);
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            finally
            {
                dataAdapter.SelectCommand.Connection.Close();
            }
            dataAdapter = null;
            return dataSet;
        }
        #endregion

        #region 新增方法
        /// <summary>
        /// 批量执行SQL
        /// </summary>
        /// <param name="sqlstr">Sql语句 集合</param>
        /// <returns>是否成功【是：true，否：false】</returns>
        public int ExecuteSQL(string[] sqlstr)
        {
            //影响行数
            int intRows = 0;
            bool flag;

            if (Information.IsNothing(this.dataAdapter))
            {
                this.InitConnect();
            }
            SqlDataAdapter dataAdapter = this.dataAdapter;
            try
            {
                dataAdapter.SelectCommand.Connection.Open();
                this.myTrans = dataAdapter.SelectCommand.Connection.BeginTransaction();
                dataAdapter.SelectCommand.Transaction = this.myTrans;
                int num2 = sqlstr.Length - 1;
                for (int i = 0; i <= num2; i++)
                {
                    if (!Information.IsNothing(sqlstr[i]) & (StringType.StrCmp(sqlstr[i], "", false) != 0))
                    {
                        dataAdapter.SelectCommand.CommandText = sqlstr[i];
                        dataAdapter.SelectCommand.ExecuteNonQuery();
                        intRows++;
                    }
                }
                this.myTrans.Commit();
                flag = true;
            }
            catch (Exception exception1)
            {
                this.myTrans.Rollback();
                throw exception1;
            }
            finally
            {
                dataAdapter.SelectCommand.Connection.Close();
            }
            dataAdapter = null;
            //return flag;
            return intRows;
        }

        /// <summary>
        /// 批量执行SQL
        /// </summary>
        /// <param name="sqlstr">Sql语句 集合</param>
        /// <param name="ID">新增一条返回一条的ID</param>
        /// <returns>是否成功【是：true，否：false】</returns>
        public int ExecuteSQL(string[] sqlstr, out string ID)
        {
            //影响行数
            int intRows = 0;
            ID = "";
            DataSet dataSet = new DataSet();
            
            if (Information.IsNothing(this.dataAdapter))
            {
                this.InitConnect();
            }
            SqlDataAdapter dataAdapter = this.dataAdapter;
            try
            {
                dataAdapter.SelectCommand.Connection.Open();
                this.myTrans = dataAdapter.SelectCommand.Connection.BeginTransaction();
                dataAdapter.SelectCommand.Transaction = this.myTrans;
                int num2 = sqlstr.Length - 1;
                for (int i = 0; i <= num2; i++)
                {
                    if (!Information.IsNothing(sqlstr[i]) & (StringType.StrCmp(sqlstr[i], "", false) != 0))
                    {
                        dataAdapter.SelectCommand.CommandText = sqlstr[i];
                        //dataAdapter.SelectCommand.ExecuteNonQuery();
                        dataAdapter.Fill(dataSet);
                        if (dataSet != null)
                        {
                            ID = Convert.ToString(dataSet.Tables[0].Rows[0][0]);
                        }
                        intRows++;
                    }
                }
                this.myTrans.Commit();
            }
            catch (Exception exception1)
            {
                this.myTrans.Rollback();
                throw exception1;
            }
            finally
            {
                dataAdapter.SelectCommand.Connection.Close();
            }
            dataAdapter = null;
            //return flag;
            return intRows;
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sqlstr">Sql语句</param>
        /// <returns>是否成功【是：true，否：false】</returns>
        public bool ExecuteSQL(string sqlstr)
        {
            bool flag;
            if (Information.IsNothing(this.dataAdapter))
            {
                this.InitConnect();
            }
            SqlDataAdapter dataAdapter = this.dataAdapter;
            try
            {
                dataAdapter.SelectCommand.Connection.Open();
                this.myTrans = dataAdapter.SelectCommand.Connection.BeginTransaction();

                dataAdapter.SelectCommand.Transaction = this.myTrans;
                dataAdapter.SelectCommand.CommandText = sqlstr;
                dataAdapter.SelectCommand.ExecuteNonQuery();

                this.myTrans.Commit();
                flag = true;
            }
            catch (Exception exception1)
            {
                this.myTrans.Rollback();
                throw exception1;
            }
            finally
            {
                dataAdapter.SelectCommand.Connection.Close();
            }
            dataAdapter = null;
            return flag;
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sqlstr">Sql语句</param>
        /// <returns>是否成功【是：true，否：false】</returns>
        public int ExecuteSQL(string sqlstr,out string ID)
        {
            int intRows = 0;
            ID = "";
            DataSet dataSet = new DataSet();

            if (Information.IsNothing(this.dataAdapter))
            {
                this.InitConnect();
            }
            SqlDataAdapter dataAdapter = this.dataAdapter;
            try
            {
                dataAdapter.SelectCommand.Connection.Open();
                this.myTrans = dataAdapter.SelectCommand.Connection.BeginTransaction();

                dataAdapter.SelectCommand.Transaction = this.myTrans;
                dataAdapter.SelectCommand.CommandText = sqlstr;

                dataAdapter.Fill(dataSet);
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    ID = Convert.ToString(dataSet.Tables[0].Rows[0][0]);
                }

                this.myTrans.Commit();
                intRows++;
            }
            catch (Exception exception1)
            {
                this.myTrans.Rollback();
                throw exception1;
            }
            finally
            {
                dataAdapter.SelectCommand.Connection.Close();
            }
            dataAdapter = null;
            return intRows;
        }
        #endregion
    }

}
