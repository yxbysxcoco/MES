export const compareDate = (date1, date2) => {
    let _Date1 = new Date(date1),
        _Date2 = new Date(date2)
    if (_Date1.getTime() > _Date2.getTime()) return true
    return false
}

export const getObjFirstProp = obj => obj[Object.keys(obj)[0]]

export const serializeDateRange = dateRange => {
    let res = []
    let _arr = dateRange.split("-")
    res[0] = _arr[0] + '-' + _arr[1] + '-' + _arr[2]
    res[1] = _arr[3] + '-' + _arr[4] + '-' + _arr[5]
    return res
}

export const handleFullscreen = () => {
    let main = document.body
    if (lemon.app.isFullscreen) {
        if (document.exitFullscreen) {
            document.exitFullscreen()
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen()
        } else if (document.webkitCancelFullScreen) {
            document.webkitCancelFullScreen()
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen()
        }
    } else {
        if (main.requestFullscreen) {
            main.requestFullscreen()
        } else if (main.mozRequestFullScreen) {
            main.mozRequestFullScreen()
        } else if (main.webkitRequestFullScreen) {
            main.webkitRequestFullScreen()
        } else if (main.msRequestFullscreen) {
            main.msRequestFullscreen()
        }
    }
    lemon.app.isFullscreen = !lemon.app.isFullscreen
}

export const previewPrint = oper => {
    if (oper < 10) {
        let bdhtml = window.document.body.innerHTML
        let sprnstr = "<!--startprint" + oper + "-->"
        let eprnstr = "<!--endprint" + oper + "-->"
        let prnhtml = bdhtml.substring(bdhtml.indexOf(sprnstr) + 18)

        prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
        window.document.body.innerHTML = prnhtml;
        window.print();
        window.document.body.innerHTML = bdhtml
    } else {
        window.print()
    }
} 