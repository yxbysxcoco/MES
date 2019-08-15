export const showModal = (id, title) => {
    layer.open({
        type: 1,
        skin: 'layui-layer-rim', //加上边框
        area: ['820px', '440px'], //宽高
        title: title || " ",
        content: document.getElementById(id).innerHTML
    });   
}

export const initShowModalBtn = (id, text) => {
    document.getElementById(id).outerHTML += `<button class="layui-btn" onclick="lemon.showModal('${id}', '${text}')">${text}</button>`
} 