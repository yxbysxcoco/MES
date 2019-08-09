const isHiddenPanel = el => el.getAttribute('name') === "hiddenPanel"

export const showPanel = id => {
    let _id = id || lemon.form.id || 'id'
    let showRows = document.getElementById(_id).childNodes
    for (let showRow of showRows) {
        isHiddenPanel(showRow) && showRow.removeAttribute("hidden")
    }
}

export const hiddenPanel = id => {
    let _id = id || lemon.form.id || 'id'
    let hiddenRows = document.getElementById(_id) && document.getElementById(_id).childNodes
    for (let hiddenRow of hiddenRows) {
        isHiddenPanel(hiddenRow) && hiddenRow.setAttribute('hidden', '')
    }
}