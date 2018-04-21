using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using NNBlog.Model;

namespace NNBlog.DAL
{
    public class AdminDAL
    {
        /// <summary>
        /// 数据库连接字符串，从Web层注入
        /// </summary>
        public string ConnStr { get; set; }

        #region 新增
        public int Insert(Model.Admin model)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                int resid = connection.Query<int>(@"INSERT INTO [Admin]([UserName],[Password],[Remark]) VALUES (@UserName,@Password ,@Remark);SELECT @@IDENTITY;",model).First();
                return resid;
            }
        }
        #endregion

        #region 删除
        public bool Delete(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                int resid = connection.Execute(@"DELETE FROM Admin WHERE Id = @Id", new { Id = id });
                return resid > 0;
            }
        }
        #endregion

        #region 修改

        public bool Update(Model.Admin model)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                int res = connection.Execute(@"UPDATE [Admin] SET [UserName] = @Username,[Password] = @Password,[Remark] = @Remark  WHERE Id = @Id", model);
                return res > 0;
            }
        }

        #endregion

        #region 查询

        public List<Model.Admin> GetList(string condition)
        {
            string sql = @"SELECT * FROM Admin ";
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                if (!string.IsNullOrEmpty(condition))
                    sql += $" where {condition}";
                var list = connection.Query<Model.Admin>(sql).ToList();
                return list;
            }
        }

        public Model.Admin GetModel(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                var model = connection.Query<Model.Admin>("SELECT * FROM Admin WHERE Id = @Id", new { Id = id }).First();
                return model;
            }
        }

        public Admin GetModel(string username, string password)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                try
                {
                    var model = connection.Query<Model.Admin>("SELECT * FROM Admin WHERE UserName = @UserName AND Password = @Password", new { UserName = username, Password = password }).First();
                    return model;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion
    }
}