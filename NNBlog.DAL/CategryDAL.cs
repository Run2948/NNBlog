using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace NNBlog.DAL
{
    public class CategryDAL
    {
        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public int Insert()
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                int resid = connection.Query<int>(
                    @"INSERT INTO Category(CateName,CateNo,ParentCate,Remark) values(@CateName,@CateNo,@ParentCate,@Remark);SELECT @@IDENTITY;",
                new { catename = "新分类", cateno = "222", parentcate = "0", remark = "" }).First();
                return resid;
            }
        }

        public int Insert(Model.Category model)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                int resid = connection.Query<int>(
                    @"INSERT INTO Category(CateName,CateNo,ParentCate,Remark) values(@CateName,@CateNo,@ParentCate,@Remark);SELECT @@IDENTITY;",
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
                int resid = connection.Execute(@"DELETE FROM Category WHERE Id = @Id", new { Id = id });
                return resid > 0;
            }
        }
        #endregion

        #region 修改

        public bool Update(Model.Category model)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
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
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                if (!string.IsNullOrEmpty(condition))
                    sql += $" where {condition}";
                var list = connection.Query<Model.Category>(sql).ToList();
                return list;
            }
        }

        public Model.Category GetModel(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                var model = connection.Query<Model.Category>("SELECT * FROM Category WHERE Id = @Id", new { Id = id }).First();
                return model;
            }
        }

        #endregion
    }
}