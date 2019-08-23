import { compareDate, serializeDateRange } from "./util.js"

export const getFormVal = id => {
    let res = []
    let inputList = $("#" + id + " input")
    for (let i = inputList.length; i--;) {
        if (inputList[i].getAttribute("id") === null) { continue }
        res.push({
            name: inputList[i].getAttribute("id"),
            value: inputList[i].value,
            type: inputList[i].getAttribute("datepicker") === "true" ? "date" : "string"
        })
    }
    let selectList = $("#" + id + " select")
    for (let i = selectList.length; i--;) {
        res.push({
            name: selectList[i].getAttribute("id"),
            value: selectList[i].value,
            type: "select"
        })
    }
    return res
}

export const resetForm = id => {
    let inputsOfForm = $("#" + id + " input")
    let selectsOfForm = $("#" + id + " select")
    for (let i = inputsOfForm.length; i--;) {
        inputsOfForm[i].value = ""
    }
    for (let i = selectsOfForm.length; i--;) {
        selectsOfForm[i].value = ""
    }
    lemon.fliterTable(id)
}

export const initForm = () => {
    if (lemon) {
        layui.form.render()
    } else {
        setTimeout(() => {
            layui.form.render()
        }, 0)
    }
}

// 去除laykey并且添加trigger为click可以解决闪退的bug
export const initDatePicker = (id, isRange) => {
    document.getElementById(id).removeAttribute('lay-key');
    layui.laydate.render({
        elem: '#' + id,
        type: 'datetime',
        range: isRange === "False" ? false : true,
        trigger: 'click'
    });
}

export const bindTableIdToForm = (tableId, formId) => {
    if (lemon.table.filter(t => t.id === tableId)[0]) {
        lemon.table.filter(t => t.id === tableId)[0].formId = formId
    } else {
        setTimeout(() => {
            lemon.table.filter(t => t.id === tableId)[0] && (lemon.table.filter(t => t.id === tableId)[0].formId = formId)
        }, 0)
    }
}

const findRowByOption = (option, rows, field) => {
    let res = []
    for (let row of rows) {
        (row[field].toString().toLowerCase() === option.toString().toLowerCase()) && res.push(row)
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
    let rows = findRow(t, formVal)
    layui.table.reload(t.id, {
        page: {
            curr: 1
        },
        data: rows
    });
}

const getTableElByFormId = formId => lemon.table.find(t => t.formId === formId)