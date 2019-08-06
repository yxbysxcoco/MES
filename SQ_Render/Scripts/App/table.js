var table = null
var tableId = null
var tableData = []
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
    tableId = id
    table = layui.table
    tableData = res.Rows

    table.render({
        id: tableId,
        elem: '#' + id,
        toolbar: '#toolbar',
        title: res.TableName,
        cols: tableHeader,
        data: res.Rows,
        page: true,
        limits: res.Limits,
        cellMinWidth: 80,
        limit: res.PageSize,
    });
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