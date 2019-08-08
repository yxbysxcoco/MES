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
                    fixed: field.Fixed || "",
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
function compareDate(date1, date2) {
    var oDate1 = new Date(date1);
    var oDate2 = new Date(date2);
    if (oDate1.getTime() > oDate2.getTime()) {
        return true; //第一个大
    } else {
        return false; //第二个大
    }
}
// 根据选项框选项查找行
function findObjArrByOption(str, arr, key) {
    let _arr = []
    for (var obj of arr) {
        if (obj[key].toLowerCase() == str.toLowerCase()) {
            _arr.push(obj)
        }
    }
    return _arr
}
// 根据时间区间查找行
function findObjArrByDate (str1, str2, arr, key) {
    let _arr = []
    for (var obj of arr) {
        var k = obj[key].split("T")
        k = k.join(" ")
        if (compareDate(k, str1) && compareDate(str2, k)) {
            _arr.push(obj)
        }
    }
    return _arr
}
// 根据多个条件查找行
function findTableCol(formData) {
    var arr = []
    arr = tableData.slice()
    for (var val of formData) {
        if (val.value.length === 0) continue
        if (val.type === "string") {
            arr = findObjArr(val.value, arr, val.name)
        } else if (val.type === "date") {
            var str = val.value.split("-")
            var str1 = str[0] + '-' + str[1] + '-' + str[2]
            var str2 = str[3] + '-' + str[4] + '-' + str[5]
            arr = findObjArrByDate(str1, str2, arr, val.name)
        } else if (val.type === "select") {
            arr = findObjArrByOption(val.value, arr, val.name)
        }
    }
    console.log(arr)
    return arr
}
// 根据字符串查找行
function findObjArr (str, arr, key) {
    let _arr = []
    for (var obj of arr) {
        if (String(obj[key]).toLowerCase().indexOf(str.toLowerCase()) > -1) {
            _arr.push(obj)
        }
    }
    return _arr
}
// 根据表单过滤表格
function fliterTable() {
    const formData = getFormData("SearchForm");
    var form = []
    for (var inputData of formData) {
        form.push(inputData)
    }
    var res = findTableCol(form)
    table.reload(tableId, {
        page: {
            curr: 1
        },
        data: res
    });
}
// 重置表格
function resetTable() {
    var inputList = $("#SearchForm input")
    var selectList = $("#SearchForm select")
    for (let i = inputList.length; i--;) {
        inputList[i].value = ""
    }
    for (let i = selectList.length; i--;) {
        selectList[i].value = ""
    }
    fliterTable()
}
// 获取指定id表单的数据
function getFormData(formId) {
    var res = []
    var inputList = $("#" + formId + " input")
    var selectList = $("#" + formId + " select")
    for (let i = inputList.length; i--;) {
        if (inputList[i].getAttribute("id") === null) {
            continue
        }
        res.push({
            name: inputList[i].getAttribute("id"),
            value: inputList[i].value,
            type: inputList[i].getAttribute("datepicker") === "true" ? "date" : "string"
        })
    }
    for (let i = selectList.length; i--;) {
        res.push({
            name: selectList[i].getAttribute("id"),
            value: selectList[i].value,
            type: "select"
        })
    }
    console.log(res)
    return res
}