const isHiddenPanel = el => el.getAttribute('name') === "hiddenPanel"

export const showHiddenPanel = id => {
    let _id = id || lemon.form.id || 'id'
    let rows = document.getElementById(_id) && document.getElementById(_id).childNodes
    for (let row of rows) {
        if (isHiddenPanel(row) && lemon.form.isHidden) {
            row.removeAttribute("hidden")
            document.getElementById("hiddenPanelBtn").innerHTML = "隐藏更多条件"
            lemon.form.isHidden = !lemon.form.isHidden
            continue
        }
        if (isHiddenPanel(row) && !lemon.form.isHidden) {
            row.setAttribute('hidden', '')
            document.getElementById("hiddenPanelBtn").innerHTML = "显示更多条件"
            lemon.form.isHidden = !lemon.form.isHidden
            continue
        }
    }
}

export const initHiddenPanel = () => {
    let hiddenPanel = document.getElementsByName("hiddenPanel")[0]
    if (hiddenPanel) {
        hiddenPanel.outerHTML += `<button class="layui-btn" type="button" id="hiddenPanelBtn" onclick="lemon.showHiddenPanel()">显示更多条件</button>` 
    }
}