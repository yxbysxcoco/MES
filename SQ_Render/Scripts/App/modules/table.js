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

const findRow = formVal => {
    let res = lemon.table.data.Rows.slice()
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

export const fliterTable = () => {
    const formVal = getFormVal(lemon.form.id);
    let rows = findRow(formVal)
    layui.table.reload(lemon.table.id, {
        page: {
            curr: 1
        },
        data: rows
    });
}

const initTableCols = tableData => {
    let cols = []
    for (let col of tableData.Columns) {
        let field = []
        for (let fieldAttr of col) {
            if (fieldAttr.Id === null) {
                field.push({
                    fixed: fieldAttr.Fixed || "",
                    title: fieldAttr.Alais,
                    field: fieldAttr.Name,
                    sort: fieldAttr.IsSortable,
                    colspan: fieldAttr.Colspan === 0 ? 1 : fieldAttr.Colspan,
                    rowspan: fieldAttr.Rowspan === 0 ? 1 : fieldAttr.Rowspan,
                    width: fieldAttr.Width
                })
            } else {
                field.push({
                    fixed: "right",
                    toolbar: "#" + fieldAttr.Id,
                    title: fieldAttr.Alais,
                    colspan: fieldAttr.Colspan === 0 ? 1 : fieldAttr.Colspan,
                    rowspan: fieldAttr.Rowspan === 0 ? 1 : fieldAttr.Rowspan,
                    width: fieldAttr.Width
                })
            }
        }
        cols.push(field)
    }
    cols[0].unshift({
        type: "checkbox",
        fixed: "left"
    })
    return cols
}

export const initTable = (id, tableData) => {
    lemon.table.data = tableData || []
    lemon.table.id = id || ""
    lemon.form.id = id || ""
    let cols = initTableCols(tableData)

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
    });

    layui.table.on("checkbox(table)", function (obj) {
        console.log(obj)
    })
}

