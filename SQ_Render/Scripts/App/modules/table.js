import { compareDate, getObjFirstProp, serializeDateRange } from "./util.js"
import { getFormVal } from "./form.js"


const findRowByOption = (option, rows, field) => {
    let res = []
    for (let row of rows) {
        (row[field].toLowerCase() === option.toLowerCase()) && res.push(row)
    }
    return res
}

const findRowByDate = (fDate, lDate, rows, field) => {
    let res = []
    for (let row of rows) {
        let rowDate = row[field].split("T").join(" ")
        if (compareDate(rowDate, fDate) && compareDate(lDate, rowDate)) {
            res.push(row)
        }
    }
    return res
}

const findRowByStr = (str, rows, field) => {
    let res = []
    for (let row of rows) {
        (String(row[field]).toLowerCase().indexOf(str.toLowerCase()) > -1) && res.push(row)
    }
    return res
}

const findRow = (t, formVal) => {
    let res = t.data.Rows.slice()
    for (let fieldVal of formVal) {
        if (fieldVal.value.length === 0) continue
        switch (fieldVal.type) {
            case "string": {
                res = findRowByStr(fieldVal.value, res, fieldVal.name)
            } break
            case "date": {
                let dateRange = serializeDateRange(fieldVal.value)
                res = findRowByDate(dateRange[0], dateRange[1], res, fieldVal.name)
            } break
            case "select": {
                res = findRowByOption(fieldVal.value, res, fieldVal.name)
            } break
        }
    }
    return res
}

export const fliterTable = id => {
    const formVal = getFormVal(id);
    let t = getTableElByFormId(id)
    console.log(t)
    let rows = findRow(t, formVal)
    layui.table.reload(t.id, {
        page: {
            curr: 1
        },
        data: rows
    });
}

const initTableCols = (id, tableData) => {
    let cols = []
    let t = getTableElById(id)
    for (let col of tableData.Columns) {
        let field = []
        for (let fieldAttr of col) {
            if (fieldAttr.HasQRCode) {
                console.log(t)
                t.codeList.push(fieldAttr.Name)
            }
            field.push({
                fixed: fieldAttr.Fixed || "",
                title: fieldAttr.Alais,
                field: fieldAttr.Name,
                sort: fieldAttr.IsSortable,
                colspan: fieldAttr.Colspan === 0 ? 1 : fieldAttr.Colspan,
                rowspan: fieldAttr.Rowspan === 0 ? 1 : fieldAttr.Rowspan,
                width: fieldAttr.Width
            })
        }
        cols.push(field)
    }
    if (document.getElementsByName("tableHandle")) {
        cols[0].push({
            fixed: "right",
            toolbar: "#" + document.getElementsByName("tableHandle")[0].getAttribute("id"),
            title: "操作",
            width: 200,
        })
    }
    cols[0].unshift({
        type: "checkbox"
    })
    return cols
}

export const initTable = (id, formId, tableData) => {
    lemon.table.push({
        data: tableData || {},
        id: id || "",
        formId: formId || "",
        checkBox: new Map(),
        sortDup: [],
        codeList: []
    })
    let cols = initTableCols(id, tableData)
    layui.table.render({
        id: id,
        elem: '#' + id,
        autoSort: false,
        toolbar: '#batchOperation',
        title: tableData.TableName,
        cols: cols,
        data: tableData.Rows,
        page: true,
        limits: tableData.Limits,
        cellMinWidth: 60,
        limit: tableData.PageSize,
        done: function (res, curr, count) {
            let t = getTableElById(id)
            for (let codeCol of t.codeList) {
                createCode(codeCol)
            }
        }
    });
    bindCheckBoxEvent()
    bindSortEvent()
}

// 目前多选框只能针对分页的当前页
const bindCheckBoxEvent = () => {
    for (let t of lemon.table) {
        if (t.id === null) continue
        layui.table.on("checkbox(layui-" + t.id + ")", function (obj) {
            if (Object.keys(obj.data).length === 0 && obj.checked) {
                for (let row of layui.table.cache.t1) {
                    t.checkBox.set(getObjFirstProp(row))
                }
            } else if (Object.keys(obj.data).length !== 0 && obj.checked) {
                t.checkBox.set(getObjFirstProp(obj.data))
            } else if (Object.keys(obj.data).length !== 0 && !obj.checked) {
                t.checkBox.delete(getObjFirstProp(obj.data))
            } else {
                for (let row of layui.table.cache.t1) {
                    t.checkBox.delete(getObjFirstProp(row))
                }
            }
            console.log(t.checkBox)
        })
    }
}

const bindSortEvent = () => {
    for (let t of lemon.table) {
        if(t.id === null) continue
        layui.table.on("sort(layui-" + t.id + ")", function (obj) {
            sortTable(t, obj.field, obj.type)
            layui.table.reload(t.id, {
                initSort: obj,
                data: t.data.Rows
            })
        })
    }
}

const sortTable = (t, field, type) => {
    if (type === null) {
        if (t.sortDup.length === 0) { }
        else t.data.Rows = t.sortDup.slice()
        return
    }
    if (t.sortDup.length === 0) t.sortDup = t.data.Rows.slice()
    if (type === "desc") {
        t.data.Rows = t.data.Rows.sort((a, b) => {
            if (typeof b[field] === "string") return b[field].localeCompare(a[field])
            else return b[field] - a[field]
        })
    } else {
        t.data.Rows = t.data.Rows.sort((a, b) => {
            if (typeof b[field] === "string") return a[field].localeCompare(b[field])
            else return a[field] - b[field]
        })
    }
}

// 多个表可能出错
export const createCode = (field) => {
    for (let el of document.getElementsByTagName("td")) {
        if (el.getAttribute("data-field") === field) {
            let qrtext = el.childNodes[0].innerHTML
            el.childNodes[0].innerHTML += `<img id="${qrtext}img" style="display: inline; margin-left: 5px; width: 20px; height: 20px;" src="../../../Content/imgs/qrcode.jpg" />`
            document.getElementById(qrtext + 'img').addEventListener("click", function () {
                document.getElementById(qrtext) && document.getElementById(qrtext).removeAttribute("hidden")
                event.stopPropagation();
            })
            document.addEventListener("click", function () {
                document.getElementById(qrtext)&& document.getElementById(qrtext).setAttribute("hidden", "")
            })
            let div = document.createElement("div");
            div.setAttribute("hidden", "")
            div.setAttribute("id", qrtext)
            el.appendChild(div)
            var qrcode = new QRCode(div, {
                width: 80,
                height: 80,
            });
            qrcode.makeCode(qrtext);
            // document.getElementById(qrtext).className += " tooltip"
        }
    }
}

const getTableElById = id => lemon.table.filter(t => t.id === id)[0]

const getTableElByFormId = formId => lemon.table.filter(t => t.formId === formId)[0]