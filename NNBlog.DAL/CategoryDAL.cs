using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using NNBlog.Model;

namespace NNBlog.DAL
{
    public class CategoryDAL
    {
        /// <summary>
        /// 数据库连接字符串，从Web层注入
        /// </summary>
        public string ConnStr { get; set; }

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        //        public int Insert()
        //        {
        //            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
        //            {
        //                int resid = connection.Query<int>(
        //                    @"INSERT INTO Category(CateName,CateNo,ParentCate,Remark) values(@CateName,@CateNo,@ParentCate,@Remark);SELECT @@IDENTITY;",
        //                new { catename = "新分类", cateno = "222", parentcate = "0", remark = "" }).First();
        //                return resid;
        //            }
        //        }

        public int Insert(Model.Category model)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                int resid = connection.Query<int>(
                    @"INSERT INTO Category(CateName,CateNo,ParentCate,Remark) values(@CateName,@CateNo,@ParentCate,@Remark);SELECT @@IDENTITY;",
                    model).First();
                return resid;
            }
        }
        #endregion

        #region 删除
        public int Delete(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                int resid = connection.Execute(@"DELETE FROM Category WHERE Id = @Id", new { Id = id });
                return resid;
            }
        }
        #endregion

        #region 修改

        public bool Update(Model.Category model)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                int res = connection.Execute(
                    @"UPDATE Category SET [CateName] = @CateName ,[CateNo] =@CateNo ,[ParentCate] = @ParentCate ,[Remark] = @Remark WHERE Id = @Id", model);
                return res > 0;
            }
        }

        #endregion

        #region 查询

        public List<Model.Category> GetList(string condition)
        {
            string sql = @"SELECT * FROM Category ";
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                if (!string.IsNullOrEmpty(condition))
                    sql += $" where {condition}";
                var list = connection.Query<Model.Category>(sql).ToList();
                return list;
            }
        }

        public Model.Category GetModel(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                var model = connection.Query<Model.Category>("SELECT * FROM Category WHERE Id = @Id", new { Id = id }).First();
                return model;
            }
        }

        #endregion

        public Category GetModelByNo(string cateNo)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                var model = connection.Query<Model.Category>("SELECT * FROM Category WHERE CateNo = @CateNo", new { CateNo = cateNo }).First();
                return model;
            }
        }

        /// <summary>
        /// 根据ParentCate生成下级的CateNo,自动+1,超过限制则返回文本
        /// </summary>
        /// <param name="parent">父编号</param>
        /// <param name="x">每一级编号的位数</param>
        /// <returns></returns>
        public string GetCateNo(string parent, int x)
        {
            string sql = "SELECT RIGHT(MAX(CateNo)," + x + ") FROM  Category WHERE ParentCate=" + parent;
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                string res = connection.ExecuteScalar(sql).ToString();
                if (string.IsNullOrEmpty(res))
                {
                    int a = 1;
                    if (parent != "0")
                    {
                        return parent + a.ToString("d" + x);
                    }
                    return a.ToString("d" + x);
                }
                else
                {
                    int a = int.Parse(res) + 1;
                    int b = (int)Math.Pow(10, x);
                    if (a >= b)
                    {
                        return "编号超过限制!";
                    }
                    if (parent != "0")
                    {
                        return parent + a.ToString("d" + x);
                    }
                    return a.ToString("d" + x);
                }
            }
        }
    }
}