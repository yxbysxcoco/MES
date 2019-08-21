export default function Table(id, tableAttr) {
    this.elem = '#' + id
    this.id = id
    this.cols = []
    this.toolbar = ''
    this.width = -1
    this.height = -1
    this.cellMinWidth = 80
    this.done = obj => { }
    this.data = tableAttr.Rows || []
    this.page = true
    this.limit = tableAttr.PageSize || 10
    this.limits = tableAttr.Limits || [5, 10, 20, 50]
    this.title = tableAttr.TableName || '表格'
    this.text = '暂无数据'
    this.autoSort = false
    this.skin = 'line'
    this.even = true
    this.size = 'sm'

    this.sortDup = []
    this.qrCodes = []
}

Table.prototype.render = function () {
    layui.table.render({
        id: this.id,
        elem: this.elem,
        autoSort: this.autoSort,
        toolbar: this.toolbar,
        title: this.title,
        cols: this.initCols(),
        data: this.data,
        page: this.page,
        limits: this.limits,
        cellMinWidth: this.cellMinWidth,
        limit: this.limit,
        done: this.done
    })
    this.bindCheckBoxEvent()
    this.bindSortEvent()
}

Table.prototype.initCols = function (cols) {
    let res = []

    for (let col of cols) {
        let field = []

        for (let fieldAttribute of col) {
            fieldAttribute.HasQRCode && (this.qrCodes.push(fieldAttribute.Name))
            field.push({
                fixed: fieldAttr.Fixed || '',
                title: fieldAttr.Alais,
                field: fieldAttr.Name,
                sort: fieldAttr.IsSortable,
                colspan: fieldAttr.Colspan === 0 ? 1 : fieldAttr.Colspan,
                rowspan: fieldAttr.Rowspan === 0 ? 1 : fieldAttr.Rowspan,
                width: fieldAttr.Width
            })
        }
        res.push(field)
    }
    return res
}

Table.prototype.bindEditEvent = function () {
    layui.table.on(`edit(${this.id})`, function (obj) {
        console.log(obj.value)
        console.log(obj.field)
        console.log(obj.data)  
    });
}

Table.prototype.bindCheckBoxEvent = function () {
    layui.table.on(`checkbox(${this.id})`, function (obj) {
        console.log(obj)
    })
}

Table.prototype.bindSortEvent = function () {
    layui.table.on(`sort(${this.id})`, function (obj) {
        this.sortTable(obj.field, obj.type)
        layui.table.reload(t.id, {
            initSort: obj,
            data: t.data
        })
    })
}

Table.prototype.deleteRow = function (row) {

}

Table.prototype.sortTable = function (field, type) {
    if (type === null) {
        if (this.sortDup.length === 0) { }
        else this.data = this.sortDup.slice()
        return
    }
    if (this.sortDup.length === 0) this.sortDup = this.data.slice()
    if (type === "desc") {
        this.data = this.data.sort((a, b) => {
            if (typeof b[field] === "string") return b[field].localeCompare(a[field])
            else return b[field] - a[field]
        })
    } else {
        this.data = this.data.sort((a, b) => {
            if (typeof b[field] === "string") return a[field].localeCompare(b[field])
            else return a[field] - b[field]
        })
    }
}

Table.prototype.createCode = function (field) {
    for (let el of document.getElementsByTagName("td")) {
        if (el.getAttribute("data-field") === field) {
            let qrtext = el.childNodes[0].innerHTML
            el.childNodes[0].innerHTML += `<img id="${qrtext}img" style="display: inline; margin-left: 5px; width: 20px; height: 20px;" src="../../../Content/imgs/qrcode.jpg" />`
            let div = document.createElement("div");
            div.setAttribute("id", qrtext)
            var qrcode = new QRCode(div, {
                width: 80,
                height: 80,
            });
            qrcode.makeCode(qrtext);
            document.getElementById(qrtext + 'img').addEventListener("click", function () {
                layer.tips(div.innerHTML, `#${qrtext}img`, {
                    tips: [3, '#fff'],
                    closeBtn: 1,
                    time: 0
                });
                event.stopPropagation();
            })
        }
    }
}

export const createCode = (field) => {
    for (let el of document.getElementsByTagName("td")) {
        if (el.getAttribute("data-field") === field) {
            let qrtext = el.childNodes[0].innerHTML
            el.childNodes[0].innerHTML += `<img id="${qrtext}img" style="display: inline; margin-left: 5px; width: 20px; height: 20px;" src="../../../Content/imgs/qrcode.jpg" />`
            let div = document.createElement("div");
            div.setAttribute("id", qrtext)
            var qrcode = new QRCode(div, {
                width: 80,
                height: 80,
            });
            qrcode.makeCode(qrtext);
            document.getElementById(qrtext + 'img').addEventListener("click", function () {
                layer.tips(div.innerHTML, `#${qrtext}img`, {
                    tips: [3, '#fff'],
                    closeBtn: 1,
                    time: 0
                });
                event.stopPropagation();
            })
        }
    }
}

function done(res, curr, count) {
    let t = getTableElById(id)
    for (let codeCol of t.codeList) {
        createCode(codeCol)
    }
}