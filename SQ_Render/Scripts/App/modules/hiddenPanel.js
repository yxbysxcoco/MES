const isHiddenPanel = el => el.getAttribute('name') === "hiddenPanel"

export const showHiddenPanel = (id, formId) => {
    let f = getFormElById(formId)
    let el = document.getElementById(id)
    if (isHiddenPanel(el) && f.isHidden) {
        el.removeAttribute("hidden")
        document.getElementById(id + "btn").innerHTML = "隐藏更多条件"
        f.isHidden = !f.isHidden
    }else if (isHiddenPanel(el) && !f.isHidden) {
        el.setAttribute('hidden', '')
        document.getElementById(id + "btn").innerHTML = "显示更多条件"
        f.isHidden = !f.isHidden
    }
}

export const initHiddenPanel = () => {
    for (let hiddenPanel of document.getElementsByName("hiddenPanel")) {
        if (hiddenPanel) {
            hiddenPanel.outerHTML +=
                `<button class="layui-btn" type="button" id="${hiddenPanel.getAttribute("id")}btn" onclick="lemon.showHiddenPanel('${hiddenPanel.getAttribute("id")}', '${hiddenPanel.getAttribute("formId")}')">显示更多条件</button>`
        }
    }
}

const getFormElById = id => lemon.form.filter(f => f.id === id)[0]