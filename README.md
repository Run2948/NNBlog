# NNBlog
Asp.Net Core 做的博客网站
#### 01.donet core网站部署在CentOS7上
* VS中右键发布项目的时候输入磁盘路径需要是\,如：C:\temp,而不要是C:/temp(出错)
* 部署文档WEB网站：[http://www.cnblogs.com/ants/p/5732337.html#_label4](http://www.cnblogs.com/ants/p/5732337.html#_label4)
* 简单linux命令：ls(查看当前目录下的文件)，pwd(查看当前路径)，mkdir(创建文件夹)，cat(简单查看文件内容)，cd(切换目录)，vi(编辑文件内容，在vi状态下按i进入编辑模式，编辑好后按esc 返回vi命令行，输入 :wq 则为保存退出)，clear(清除屏幕)

#### 02.前期准备及项目搭建
* 技术：ASP.NET CORE + SQL Server 2012 + Dapper
* 网页模板：
	+ 前台 [http://www.jianshu.com/p/e88cccc80a7d](http://www.jianshu.com/p/e88cccc80a7d "博客前台模板")
	+ 后台 [https://www.jianshu.com/p/55493b356dcc](https://www.jianshu.com/p/55493b356dcc "精美后台系统模板")
	+ 后台内页 [http://www.layui.com/doc](http://www.layui.com/doc "layui开发使用文档 - 入门指南 ")
* 源码管理：github   本地GIT软件 TortoiseGit

#### 03.数据库的设计及Dapper的使用
- 博客表 ，分类表 ，管理员表
- Dapper：https://gits.github.com/lancscoder/1829462

#### 04.后台博客文章增删改查
	    VS2015中使用ASP.NET CORE不完善，需要域(Area)需要手动添加，域中每个控制器类顶部都要加上域标识【Area("Admin")】
	还要在Startup.cs中的Configure方法中加上:
		routes.MapRoute(name: "Admin",template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

#### 05.LayUI分页的使用

#### 06.博客查询功能和LayUI编辑器的使用
	上传的时候指定图片保存路径不能使用以前的Server.MapPath方法，而得用【 hostingEnv.WebRootPath 】
	而 hostingEnv 又必须在控制器的构造函数中注入：
	public BlogController( IHostingEnvironment hostingEnv)
	{
		this.hostingEnv = hostingEnv;
	}

#### 07.后台登录及前台整合
* core 中Session的使用方法：
	+ project.json 中的 dependencies 节点中加入 "Microsoft.AspNetCore.Session": "1.0.0",
	+ Startup.cs 中的 ConfigureServices方法 中加入  services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(30); });
	+ Startup.cs 中的 Configure 方法中加入  app.UseSession();
	+ 在控制器重 引入 using Microsoft.AspNetCore.Http;
	+ 设置值：HttpContext.Session.SetString("blog_admin", a.UserName);
	+ 获取值：int? adminid  = HttpContext.Session.GetInt32("adminid");

#### 08.前台整合2
* 头像修改为我的LOGO，站点名称修改为班纳博客，下方加入搜索框，只保留分类和博客月份，相应查询代码。

