/**  版本信息模板在安装目录下，可自行修改。
* TableColumns.cs
*
* 功 能： N/A
* 类 名： TableColumns
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/10/11 16:09:13   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace DAL
{
	/// <summary>
	/// 数据访问类:TableColumns
	/// </summary>
	public partial class TableColumns
	{
		public TableColumns()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("tcId", "TableColumns"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int tcId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TableColumns");
			strSql.Append(" where tcId=@tcId");
			SqlParameter[] parameters = {
					new SqlParameter("@tcId", SqlDbType.Int,4)
			};
			parameters[0].Value = tcId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.TableColumns model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TableColumns(");
			strSql.Append("dbId,ColumnName,ColumnType,Statue,CreateDate)");
			strSql.Append(" values (");
			strSql.Append("@dbId,@ColumnName,@ColumnType,@Statue,@CreateDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@dbId", SqlDbType.Int,4),
					new SqlParameter("@ColumnName", SqlDbType.VarChar,100),
					new SqlParameter("@ColumnType", SqlDbType.VarChar,50),
					new SqlParameter("@Statue", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = model.dbId;
			parameters[1].Value = model.ColumnName;
			parameters[2].Value = model.ColumnType;
			parameters[3].Value = model.Statue;
			parameters[4].Value = model.CreateDate;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.TableColumns model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TableColumns set ");
			strSql.Append("dbId=@dbId,");
			strSql.Append("ColumnName=@ColumnName,");
			strSql.Append("ColumnType=@ColumnType,");
			strSql.Append("Statue=@Statue,");
			strSql.Append("CreateDate=@CreateDate");
			strSql.Append(" where tcId=@tcId");
			SqlParameter[] parameters = {
					new SqlParameter("@dbId", SqlDbType.Int,4),
					new SqlParameter("@ColumnName", SqlDbType.VarChar,100),
					new SqlParameter("@ColumnType", SqlDbType.VarChar,50),
					new SqlParameter("@Statue", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@tcId", SqlDbType.Int,4)};
			parameters[0].Value = model.dbId;
			parameters[1].Value = model.ColumnName;
			parameters[2].Value = model.ColumnType;
			parameters[3].Value = model.Statue;
			parameters[4].Value = model.CreateDate;
			parameters[5].Value = model.tcId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int tcId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TableColumns ");
			strSql.Append(" where tcId=@tcId");
			SqlParameter[] parameters = {
					new SqlParameter("@tcId", SqlDbType.Int,4)
			};
			parameters[0].Value = tcId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string tcIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TableColumns ");
			strSql.Append(" where tcId in ("+tcIdlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.TableColumns GetModel(int tcId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 tcId,dbId,ColumnName,ColumnType,Statue,CreateDate from TableColumns ");
			strSql.Append(" where tcId=@tcId");
			SqlParameter[] parameters = {
					new SqlParameter("@tcId", SqlDbType.Int,4)
			};
			parameters[0].Value = tcId;

			Model.TableColumns model=new Model.TableColumns();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.TableColumns DataRowToModel(DataRow row)
		{
			Model.TableColumns model=new Model.TableColumns();
			if (row != null)
			{
				if(row["tcId"]!=null && row["tcId"].ToString()!="")
				{
					model.tcId=int.Parse(row["tcId"].ToString());
				}
				if(row["dbId"]!=null && row["dbId"].ToString()!="")
				{
					model.dbId=int.Parse(row["dbId"].ToString());
				}
				if(row["ColumnName"]!=null)
				{
					model.ColumnName=row["ColumnName"].ToString();
				}
				if(row["ColumnType"]!=null)
				{
					model.ColumnType=row["ColumnType"].ToString();
				}
				if(row["Statue"]!=null && row["Statue"].ToString()!="")
				{
					model.Statue=int.Parse(row["Statue"].ToString());
				}
				if(row["CreateDate"]!=null && row["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(row["CreateDate"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select tcId,dbId,ColumnName,ColumnType,Statue,CreateDate ");
			strSql.Append(" FROM TableColumns ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" tcId,dbId,ColumnName,ColumnType,Statue,CreateDate ");
			strSql.Append(" FROM TableColumns ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM TableColumns ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.tcId desc");
			}
			strSql.Append(")AS Row, T.*  from TableColumns T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "TableColumns";
			parameters[1].Value = "tcId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

