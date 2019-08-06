layui.use('element', function () {
    var element = layui.element;
    element.on('nav(sider)', function (elem) {
        layer.msg(elem.text());
    });
    element.init();
    element.render('nav');
});