﻿export const getFormVal = id => {
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

export const resetForm = () => {
    let inputsOfForm = $("#" + lemon.form.id + " input")
    let selectsOfForm = $("#" + lemon.form.id + " select")
    for (let i = inputsOfForm.length; i--;) {
        inputsOfForm[i].value = ""
    }
    for (let i = selectsOfForm.length; i--;) {
        selectsOfForm[i].value = ""
    }
    lemon.fliterTable()
}

export const initForm = () => layui.form.render()

// 去除laykey并且添加trigger为click可以解决闪退的bug
export const initDatePicker = (id, isRange) => {
    var laydate = layui.laydate;
    document.getElementById(id).removeAttribute('lay-key');
    laydate.render({
        elem: '#' + id,
        type: 'datetime',
        range: isRange === "False" ? false : true,
        trigger: 'click'
    });
}