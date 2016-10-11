/**  版本信息模板在安装目录下，可自行修改。
* UsersTable.cs
*
* 功 能： N/A
* 类 名： UsersTable
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
	/// UsersTable:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UsersTable
	{
		public UsersTable()
		{}
		#region Model
		private int _uid;
		private string _username;
		private string _password;
		private string _nickname;
		private string _appid;
		private string _appsecret;
		private int _score;
		private int _statue;
		private DateTime _createdate;
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
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NickName
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AppID
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AppSecret
		{
			set{ _appsecret=value;}
			get{return _appsecret;}
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

