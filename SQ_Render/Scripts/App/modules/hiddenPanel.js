const isHiddenPanel = el => el.getAttribute('name') === "hiddenPanel"

export const showHiddenPanel = id => {
    let _id = id || lemon.form.id || 'id'
    let rows = document.getElementById(_id) && document.getElementById(_id).childNodes
    for (let row of rows) {
        if (isHiddenPanel(row) && lemon.form.isHidden) {
            row.removeAttribute("hidden")
            lemon.form.isHidden = !lemon.form.isHidden
            continue
        }
        if (isHiddenPanel(row) && !lemon.form.isHidden) {
            row.setAttribute('hidden', '')
            lemon.form.isHidden = !lemon.form.isHidden
            continue
        }
    }
}