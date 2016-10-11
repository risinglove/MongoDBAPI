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
namespace Model
{
	/// <summary>
	/// TableColumns:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TableColumns
	{
		public TableColumns()
		{}
		#region Model
		private int _tcid;
		private int _dbid;
		private string _columnname;
		private string _columntype;
		private int _statue;
		private DateTime _createdate;
		/// <summary>
		/// 
		/// </summary>
		public int tcId
		{
			set{ _tcid=value;}
			get{return _tcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int dbId
		{
			set{ _dbid=value;}
			get{return _dbid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ColumnName
		{
			set{ _columnname=value;}
			get{return _columnname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ColumnType
		{
			set{ _columntype=value;}
			get{return _columntype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Statue
		{
			set{ _statue=value;}
			get{return _statue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		#endregion Model

	}
}

