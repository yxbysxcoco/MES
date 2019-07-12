```
├─Config 配置文件夹
├─Const 常量文件夹
├─Content
│  ├─App 前端外部Css根文件夹
│  └─Spread 引入葡萄城Excel样式根文件夹
├─Controllers 控制器
├─Dll 引用的dll文件
├─fonts 字体目录
├─Models 模型
├─Scripts 
│  ├─App 前端外部Js根文件夹
│  └─Spread 引入葡萄城Js文件夹
├─Table 表文件夹
├─Tools 工具类目录
├─menu.json 侧边栏配置文件
└─Views 视图层
    ├─Home 首页
    ├─Shared 布局
    └─Tool 工装管理
```

## 环境准备
需要git环境 
>使用淘宝镜像下载[git](https://npm.taobao.org/mirrors/git-for-windows/)，下载完成之后打开cmd，输入git --version提示版本信息即为下载成功
需要visual studio安装git的拓展包
>[路径](https://marketplace.visualstudio.com/items?itemName=GitHub.GitHubExtensionforVisualStudio)，下载完成之后重启visual studio，点击视图，其他窗口，出现github即为成功
安装完成之后点击视图-->其他窗口-->Github-->登陆git
然后点击视图-->团队资源管理器-->设置-->存储库设置，差异工具与合并工具都选visual studio

## 关于布局
布局的模板全部放在了/Views/Shared/目录下，_Layout.cshtml为根布局页，所有的视图页面都会默认继承这个页面引入的外部文件。
其中：
- Toolbar 顶部导航栏
- Tagbar 顶部Logo栏
- MenuLayout 可供继承的带侧边栏的布局，选择继承该布局将带侧边栏，空则继承根布局页
- Footer 网页底部其他信息
- Error 处理请求出错时返回的页面

## 添加页面
添加一个视图页需要进行如下操作
1.在views下选择一个文件夹或新建一个文件夹（使用新的控制器），在该文件夹下创建name.cshtml，删除原有代码，拷贝下列代码
```html
<div>这是新创建的视图页</div>
```
2.如果是在新文件夹下创建视图，则在controllers目录下添加一个控制器，删除原有代码，拷贝下列代码
```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MES.Models;

namespace MES.Controllers
{
    public class 控制器名称Controller : Controller
    {
        public List<Sider> menu
        {
            get { return new JsonTool("menu.json").GetValueList<Sider>("Menu").Where(m => m.ControllerName == "控制器名称").ToList(); }
        }
        public ActionResult 页面名称()
        {
            return View(menu);
        }
    }
}
```
3.在根目录下menu.json中添加一个侧边栏导航对象


## 如何协同开发
获取远程仓库代码，进入团队资源管理器，点击同步目录下的传入提交的拉取，再点击分支master旁的同步。
上传本地代码，进入团队资源管理器，先执行上面的获取远程仓库代码步骤，然后进入更改目录下输入上传信息，点击全部提交，然后进入同步目录下点击分支master旁的同步。

## 关于页面内容
每个页面都包含一个vue示例，初始化页面模板如下
```
<div id="app">
	<i-button v-on:click="handleClick"></i-button><!--使用v-on:click绑定点击事件-->
	{{ msg }} <!--展示模板字符串-->
	<i-table border  width="100%" :columns="columns" :data="dataList"></i-table>
</div>
<script>
	var vm = new Vue({
		el: "#app",
		data: {
			// 这里放双向绑定数据
			msg: "这是一个模板字符串", // 修改data里的数据页面也会响应更新
			dataList: [],
			columns: [
				{
                    title: '序号', // 表头名字
                    key: 'id', //键
                    fixed: 'left', // 是否固定在左侧
                    width: 80 // 宽度
                },
                {
                    title: '编码',
                    key: 'code',
                    width: 120
                },
			]
		},
		methods: {
			// 这里写方法
			handleClick() {
				console.log("你点击了按钮")
			},
			handleAddList() {
				this.dataList.push() // 往数组中添加数据
			},
			handleDeleteList(index) {
				this.dataList.splice(index, 1) // 往数组中删除数据，index为删除数据的下标
			},
			handleChangeList(index) {
				this.$set(this.dataList(index), "修改对象属性名", "修改对象的值") // 修改对象数组中某对象的属性
			},
			// 如何请求数据
			handleGetData() {
				 axios({
                    method: 'post',
                    url: "http://localhost:51847/ToolEquipment/GetData",
                    data: {
                        userName: "504",
                        password: "1213"
                    }
                }).then((res) => {
				// 在这里拿到请求的值赋值给data里的数据
                    this.dataList = res.data
                }).catch( (err) => {
				//处理错误
                    alert(err);
                });
			}
		}
	})
</script>
```