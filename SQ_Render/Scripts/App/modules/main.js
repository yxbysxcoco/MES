import { showHiddenPanel, initHiddenPanel } from './hiddenPanel.js'
import { initTable, createCode } from './table.js'
import { fliterTable, resetForm, initForm, initDatePicker, bindTableIdToForm } from './form.js'
import { initSider } from './sider.js'
import { handleFullscreen, previewPrint } from './util.js'
import { initTree } from './tree.js'

window.lemon = (function () {
    let table = [{
        id: '',
        formId: '',
        data: [],
        checkBox: new Map(),
        sortDup: [],
        codeList: []
    }]
    let form = [{
        id: "",
        isHidden: true,
        datePickerId: ''
    }]
    let app = {
        isFullscreen: false
    }
    return {
        table,
        form,
        app,
        showHiddenPanel,
        initHiddenPanel,
        fliterTable,
        initTable,
        createCode,
        resetForm,
        initForm,
        bindTableIdToForm,
        initDatePicker,
        initSider,
        handleFullscreen,
        previewPrint,
        initTree
    }
})()

// 初始化
lemon.initSider()
lemon.initForm()
lemon.initHiddenPanel()
console.log(lemon)