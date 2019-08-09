import { hiddenPanel, showPanel } from './hiddenPanel.js'
import { fliterTable, initTable } from './table.js'
import { resetForm, initForm } from './form.js'
import { initSider } from './sider.js'
import { handleFullscreen } from './util.js'

window.lemon = (function () {
    let table = {
        id: '',
        data: [],
        checkBox: new Map()
    }
    let form = {
        id: ''
    }
    let app = {
        isFullscreen: false
    }
    return {
        table,
        form,
        app,
        hiddenPanel,
        showPanel,
        fliterTable,
        initTable,
        resetForm,
        initForm,
        initSider,
        handleFullscreen
    }
})()

// 初始化
lemon.initSider()
lemon.initForm()
console.log(lemon)