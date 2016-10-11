/**  版本信息模板在安装目录下，可自行修改。
* DataBaseTables.cs
*
* 功 能： N/A
* 类 名： DataBaseTables
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/10/11 16:09:12   N/A    初版
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
	/// DataBaseTables:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DataBaseTables
	{
		public DataBaseTables()
		{}
		#region Model
		private int _dbid;
		private int _uid;
		private string _tablename;
		private int _statue;
		private DateTime _createdate;
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
		public int uId
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
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

