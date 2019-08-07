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
                    width: field.Width
                })
            } else {
                arr.push({
                    fixed: "right",
                    toolbar: "#" + field.Id,
                    title: field.Alais,
                    colspan: field.Colspan === 0 ? 1 : field.Colspan,
                    rowspan: field.Rowspan === 0 ? 1 : field.Rowspan,
                    width: field.Width
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
        cellMinWidth: 60,
        limit: res.PageSize,
    });
    table.on('checkbox(table)', function (obj) {
        if (Object.keys(obj.data).length === 0 && obj.checked) {
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
}
var handleEdit = (e, url) => {
    let id = e.parentNode.parentNode.parentNode.firstChild.firstChild.innerHTML
    console.log(id)
}
var handleDel = (e, url) => {
    let id = e.parentNode.parentNode.parentNode.firstChild.firstChild.innerHTML
    console.log(id)
}
var batchDel = (url) => {
    console.log(checkBox)
    console.log(url)
}
var handleShow = (e) => {
    console.log(e)
}