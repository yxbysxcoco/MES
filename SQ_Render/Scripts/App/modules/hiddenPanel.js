const isHiddenPanel = el => el.getAttribute('name') === "hiddenPanel"

export const showHiddenPanel = (id, formId) => {
    let f = getFormElById(formId)
    let el = document.getElementById(id)
    if (f.isHidden) {
        el.removeAttribute("hidden")
        document.getElementById(id + "btn").innerHTML = "隐藏更多条件"
        f.isHidden = !f.isHidden
    }else {
        el.setAttribute('hidden', '')
        document.getElementById(id + "btn").innerHTML = "显示更多条件"
        f.isHidden = !f.isHidden
    }
}

export const initHiddenPanel = id => {
    let hiddenPanel = document.getElementById(id)
    if (hiddenPanel) {
        if (document.getElementById(id + "btn")) return
        hiddenPanel.outerHTML +=
            `<button class="layui-btn" type="button" id="${id}btn" onclick="lemon.showHiddenPanel('${id}', '${hiddenPanel.getAttribute("formId")}')">显示更多条件</button>`
    }
}

const getFormElById = id => lemon.form.find(f => f.id === id)