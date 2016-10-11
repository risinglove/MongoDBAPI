/**  版本信息模板在安装目录下，可自行修改。
* ScoreNotesTable.cs
*
* 功 能： N/A
* 类 名： ScoreNotesTable
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
	/// ScoreNotesTable:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ScoreNotesTable
	{
		public ScoreNotesTable()
		{}
		#region Model
		private int _snid;
		private int _uid;
		private int? _usetype;
		private int _score;
		private int _statue;
		private DateTime _createdate;
		/// <summary>
		/// 
		/// </summary>
		public int snId
		{
			set{ _snid=value;}
			get{return _snid;}
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
		public int? UseType
		{
			set{ _usetype=value;}
			get{return _usetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Score
		{
			set{ _score=value;}
			get{return _score;}
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

