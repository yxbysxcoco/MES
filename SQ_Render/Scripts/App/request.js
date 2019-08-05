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

const getFormData = (formId) => {
    let res = []
    let inputList = $("#" + formId + " input")
    for (let i = inputList.length; i--;) {
        res.push({
            name: inputList[i].parentNode.getAttribute("id"),
            value: inputList[i].value
        })
    }
    console.log(res)
    return res
}