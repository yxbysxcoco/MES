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
export const getFormData = id => {
    let res = {};
    let inputList = $("#" + id + " input")
    console.log(inputList)

    for (let i = inputList.length; i--;) {
        if (inputList[i].getAttribute("id") === null) { continue }

        res[inputList[i].getAttribute("id")] = inputList[i].value;
    }

    let selectList = Array.from(document.querySelectorAll("#" + id + " select")).filter(node => node.getAttribute("id") !== null);

    console.log(selectList)

    for (let i = selectList.length; i--;) {
        res[selectList[i].getAttribute("id")] = selectList[i].value;
    }
    let tablesOfForm = Array.from(document.getElementById(id).querySelectorAll("table"));
    let tables = lemon.table.filter(t => tablesOfForm.map(t => t.getAttribute("id")).includes(t.id));

    for (let t of tables) {
        res[t.id] = layui.table.cache[t.id]
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

export const initDatePicker = (id, isRange) => {
    var datepickerList = document.getElementsByClassName('mydatepicker')
    for (let datepicker of datepickerList) {
        datepicker.removeAttribute('lay-key')
        let p = datepicker.parentNode
        p.removeChild(datepicker)
        p.appendChild(datepicker)
    }
    
    lay('.mydatepicker').each(function () {
        layui.laydate.render({
            elem: this,
            type: 'datetime',
            event: 'click',
            range: isRange === "False" ? false : true,
            trigger: 'click'
        })
    })
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