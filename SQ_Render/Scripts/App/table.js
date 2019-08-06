var table = null
var tableId = null
var tableData = []
var checkBox = new Map()
var getFirstProp = v => v[Object.keys(v)[0]]
var initTable = (id, dataTable) => {
    var res = JSON.parse(dataTable);
    console.log(res)
    var tableHeader = [];
    for (var column of res.Columns) {
        let arr = []
        for (var field of column) {
            if (field.Id === null) {
                arr.push({
                    title: field.Alais,
                    field: field.Name,
                    sort: field.IsSortable,
                    colspan: field.Colspan === 0 ? 1 : field.Colspan,
                    rowspan: field.Rowspan === 0 ? 1 : field.Rowspan,
                })
            } else {
                arr.push({
                    fixed: "right",
                    toolbar: "#" + field.Id,
                    title: field.Alais,
                    colspan: field.Colspan === 0 ? 1 : field.Colspan,
                    rowspan: field.Rowspan === 0 ? 1 : field.Rowspan,
                })
            }
        }
        tableHeader.push(arr)
    }
    tableHeader[0].unshift({
        type: "checkbox",
        fixed: "left",
        colspan: 1,
        rowspan: 2
    })
    tableId = id
    table = layui.table
    tableData = res.Rows

    table.render({
        id: tableId,
        elem: '#' + id,
        toolbar: '#batchOperation',
        title: res.TableName,
        cols: tableHeader,
        data: res.Rows,
        page: true,
        limits: res.Limits,
        cellMinWidth: 80,
        limit: res.PageSize,
    });
    table.on('checkbox(table)', function (obj) {
        if (Object.keys(obj.data).length === 0 && obj.checked) {
            // 这样可以拿到全选数据
            for (var val of table.cache.t1) {
                checkBox.set(getFirstProp(val))
            }
        } else if (Object.keys(obj.data).length !== 0 && obj.checked) {
            checkBox.set(getFirstProp(obj.data))
        } else if (Object.keys(obj.data).length !== 0 && !obj.checked) {
            checkBox.delete(getFirstProp(obj.data))
        } else {
            for (var val of table.cache.t1) {
                checkBox.delete(getFirstProp(val))
            }
        }
        console.log(checkBox)
        //layer.alert(JSON.stringify(data));
    });

    var $ = layui.$, active = {
        getCheckData: function () { //获取选中数据
            var checkStatus = table.checkStatus('idTest')
                , data = checkStatus.data;
            layer.alert(JSON.stringify(data));
        }
        , getCheckLength: function () { //获取选中数目
            var checkStatus = table.checkStatus('idTest')
                , data = checkStatus.data;
            layer.msg('选中了：' + data.length + ' 个');
        }
        , isAll: function () { //验证是否全选
            var checkStatus = table.checkStatus('idTest');
            layer.msg(checkStatus.isAll ? '全选' : '未全选')
        }
    };
}
var handleEdit = (e, url) => {
    let id = e.parentNode.parentNode.parentNode.firstChild.firstChild.innerHTML
    console.log(id)
    //handle url
    //var index = e.parentNode.parentNode.parentNode.attributes['data-index'].value
    //let row = tableData[index]
    //let getFirstKey = row => row[Object.keys(row)[0]];
    //console.log(getFirstKey(row))
}
var handleDel = (e, url) => {
    let id = e.parentNode.parentNode.parentNode.firstChild.firstChild.innerHTML
    console.log(id)
    //handle url
    //var index = e.parentNode.parentNode.parentNode.attributes['data-index'].value
    //let row = tableData[index]
    //let getFirstKey = row => row[Object.keys(row)[0]];
    //console.log(getFirstKey(row))
}
var batchDel = (e, url) => {
    console.log(e)
    console.log(url)
}
var handleShow = (e) => {
    console.log(e)
}