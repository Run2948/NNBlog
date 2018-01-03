using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using NNBlog.Model;

namespace NNBlog.DAL
{
    public class BlogDAL
    {
        #region 新增
        public int Insert(Model.Blog model)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                int resid = connection.Query<int>(
                    @"INSERT INTO Blog ([Title] ,[Body],[Body_md],[VisitNo],[CateNo],[CateName],[Remark],[Sort]) VALUES(@Title,@Body,@Body_md,@VisitNo,@CateNo,@CateName,@Remark,@Sort);SELECT @@IDENTITY;",
                    model).First();
                return resid;
            }
        }
        #endregion

        #region 删除
        public bool Delete(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                int resid = connection.Execute(@"DELETE FROM Blog WHERE Id = @Id", new { Id = id });
                return resid > 0;
            }
        }
        #endregion

        #region 修改

        public bool Update(Model.Blog model)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                int res = connection.Execute(
                    @"UPDATE Blog SET [Title] = @Title,[Body] = @Body,[Body_md] = @Body_md,[VisitNo] = @VisitNo,[CateNo] = @CateNo,[CateName] = @CateName,[Remark] = @Remark,[Sort] = @Sort WHERE Id = @Id", model);
                return res > 0;
            }
        }

        #endregion

        #region 查询

        public List<Model.Blog> GetList(string condition)
        {
            string sql = @"SELECT * FROM Blog ";
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                if (!string.IsNullOrEmpty(condition))
                    sql += $" where {condition}";
                var list = connection.Query<Model.Blog>(sql).ToList();
                return list;
            }
        }

        public Model.Blog GetModel(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                var model = connection.Query<Model.Blog>("SELECT * FROM Blog WHERE Id = @Id", new { Id = id }).First();
                return model;
            }
        }

        public List<Blog> GetList(string orderstr, int pagesize, int pageindex, string condition)
        {
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
            }
            string sql = $"SELECT * FROM [Blog] {condition} ORDER BY {orderstr} OFFSET {(pageindex - 1) * pagesize} ROWS FETCH NEXT {pagesize} ROWS ONLY";
            List<Model.Blog> list;
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                list = connection.Query<Model.Blog>(sql).ToList();
            }
            return list;
        }

        #endregion

        /// <summary>
        /// 计算记录数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int CalcCount(string condition)
        {

            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                string sql = "SELECT COUNT(1) FROM Blog ";
                if (!string.IsNullOrEmpty(condition))
                {
                    sql += $" where {condition}";
                }
                return connection.ExecuteScalar<int>(sql);
            }

        }
    }
}