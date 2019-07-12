```
����Config �����ļ���
����Const �����ļ���
����Content
��  ����App ǰ���ⲿCss���ļ���
��  ����Spread �������ѳ�Excel��ʽ���ļ���
����Controllers ������
����Dll ���õ�dll�ļ�
����fonts ����Ŀ¼
����Models ģ��
����Scripts 
��  ����App ǰ���ⲿJs���ļ���
��  ����Spread �������ѳ�Js�ļ���
����Table ���ļ���
����Tools ������Ŀ¼
����menu.json ����������ļ�
����Views ��ͼ��
    ����Home ��ҳ
    ����Shared ����
    ����Tool ��װ����
```

## ����׼��
��Ҫgit���� 
>ʹ���Ա���������[git](https://npm.taobao.org/mirrors/git-for-windows/)���������֮���cmd������git --version��ʾ�汾��Ϣ��Ϊ���سɹ�
��Ҫvisual studio��װgit����չ��
>[·��](https://marketplace.visualstudio.com/items?itemName=GitHub.GitHubExtensionforVisualStudio)���������֮������visual studio�������ͼ���������ڣ�����github��Ϊ�ɹ�
��װ���֮������ͼ-->��������-->Github-->��½git
Ȼ������ͼ-->�Ŷ���Դ������-->����-->�洢�����ã����칤����ϲ����߶�ѡvisual studio

## ���ڲ���
���ֵ�ģ��ȫ��������/Views/Shared/Ŀ¼�£�_Layout.cshtmlΪ������ҳ�����е���ͼҳ�涼��Ĭ�ϼ̳����ҳ��������ⲿ�ļ���
���У�
- Toolbar ����������
- Tagbar ����Logo��
- MenuLayout �ɹ��̳еĴ�������Ĳ��֣�ѡ��̳иò��ֽ��������������̳и�����ҳ
- Footer ��ҳ�ײ�������Ϣ
- Error �����������ʱ���ص�ҳ��

## ���ҳ��
���һ����ͼҳ��Ҫ�������²���
1.��views��ѡ��һ���ļ��л��½�һ���ļ��У�ʹ���µĿ����������ڸ��ļ����´���name.cshtml��ɾ��ԭ�д��룬�������д���
```html
<div>�����´�������ͼҳ</div>
```
2.����������ļ����´�����ͼ������controllersĿ¼�����һ����������ɾ��ԭ�д��룬�������д���
```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MES.Models;

namespace MES.Controllers
{
    public class ����������Controller : Controller
    {
        public List<Sider> menu
        {
            get { return new JsonTool("menu.json").GetValueList<Sider>("Menu").Where(m => m.ControllerName == "����������").ToList(); }
        }
        public ActionResult ҳ������()
        {
            return View(menu);
        }
    }
}
```
3.�ڸ�Ŀ¼��menu.json�����һ���������������


## ���Эͬ����
��ȡԶ�ֿ̲���룬�����Ŷ���Դ�����������ͬ��Ŀ¼�µĴ����ύ����ȡ���ٵ����֧master�Ե�ͬ����
�ϴ����ش��룬�����Ŷ���Դ����������ִ������Ļ�ȡԶ�ֿ̲���벽�裬Ȼ��������Ŀ¼�������ϴ���Ϣ�����ȫ���ύ��Ȼ�����ͬ��Ŀ¼�µ����֧master�Ե�ͬ����

## ����ҳ������
ÿ��ҳ�涼����һ��vueʾ������ʼ��ҳ��ģ������
```
<div id="app">
	<i-button v-on:click="handleClick"></i-button><!--ʹ��v-on:click�󶨵���¼�-->
	{{ msg }} <!--չʾģ���ַ���-->
	<i-table border  width="100%" :columns="columns" :data="dataList"></i-table>
</div>
<script>
	var vm = new Vue({
		el: "#app",
		data: {
			// �����˫�������
			msg: "����һ��ģ���ַ���", // �޸�data�������ҳ��Ҳ����Ӧ����
			dataList: [],
			columns: [
				{
                    title: '���', // ��ͷ����
                    key: 'id', //��
                    fixed: 'left', // �Ƿ�̶������
                    width: 80 // ���
                },
                {
                    title: '����',
                    key: 'code',
                    width: 120
                },
			]
		},
		methods: {
			// ����д����
			handleClick() {
				console.log("�����˰�ť")
			},
			handleAddList() {
				this.dataList.push() // ���������������
			},
			handleDeleteList(index) {
				this.dataList.splice(index, 1) // ��������ɾ�����ݣ�indexΪɾ�����ݵ��±�
			},
			handleChangeList(index) {
				this.$set(this.dataList(index), "�޸Ķ���������", "�޸Ķ����ֵ") // �޸Ķ���������ĳ���������
			},
			// �����������
			handleGetData() {
				 axios({
                    method: 'post',
                    url: "http://localhost:51847/ToolEquipment/GetData",
                    data: {
                        userName: "504",
                        password: "1213"
                    }
                }).then((res) => {
				// �������õ������ֵ��ֵ��data�������
                    this.dataList = res.data
                }).catch( (err) => {
				//�������
                    alert(err);
                });
			}
		}
	})
</script>
```