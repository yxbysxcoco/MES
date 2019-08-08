const pushData = (options) => {
    $.ajax({
        type: options.method,
        url: options.url,
        data: options.data,
        dataType: 'JSON',
        success: res => {
        },
        error: err => {
            console.log(err)
            if (err.status) {
                layer.open({
                    title: "请求发生错误",
                    content: err.responseText
                })
            }
        }
    })
}

const getData = (options, fn1) => {
    $.ajax({
        type: options.method,
        url: options.url,
        data: options.data,
        dataType: 'JSON',
        success: res => {
            fn1(res)
        },
        error: err => {
            console.log(err)
            if (err.status) {
                layer.open({
                    title: "请求发生错误",
                    content: err.responseText
                })
            }
        }
    })
}

function getFormData (formId) {
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