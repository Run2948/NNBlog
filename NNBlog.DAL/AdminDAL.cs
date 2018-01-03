using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using NNBlog.Model;

namespace NNBlog.DAL
{
    public class AdminDAL
    {
        #region 新增
        public int Insert(Model.Admin model)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                int resid = connection.Query<int>(@"INSERT INTO [Admin]([UserName],[Password],[Remark]) VALUES (@UserName,@Password ,@Remark);SELECT @@IDENTITY;",model).First();
                return resid;
            }
        }
        #endregion

        #region 删除
        public bool Delete(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                int resid = connection.Execute(@"DELETE FROM Admin WHERE Id = @Id", new { Id = id });
                return resid > 0;
            }
        }
        #endregion

        #region 修改

        public bool Update(Model.Admin model)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
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
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                if (!string.IsNullOrEmpty(condition))
                    sql += $" where {condition}";
                var list = connection.Query<Model.Admin>(sql).ToList();
                return list;
            }
        }

        public Model.Admin GetModel(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                var model = connection.Query<Model.Admin>("SELECT * FROM Admin WHERE Id = @Id", new { Id = id }).First();
                return model;
            }
        }

        public Admin GetModel(string username, string password)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                var model = connection.Query<Model.Admin>("SELECT * FROM Admin WHERE UserName = @UserName AND Password = @Password", new { UserName = username,Password = password }).First();
                return model;
            }
        }

        #endregion
    }
}