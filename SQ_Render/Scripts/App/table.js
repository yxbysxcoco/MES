// 下面这四个方法后面有时间重写
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
