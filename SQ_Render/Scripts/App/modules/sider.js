export const initSider = () => layui.use('element', function () {
    layui.element.on('nav(sider)', function (elem) {
        layer.msg(elem.text())
    })
    layui.element.init()
    layui.element.render('nav')
})