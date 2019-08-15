﻿import { showHiddenPanel, initHiddenPanel } from './hiddenPanel.js'
import { fliterTable, initTable, createCode } from './table.js'
import { resetForm, initForm, initDatePicker } from './form.js'
import { initSider } from './sider.js'
import { handleFullscreen, previewPrint } from './util.js'

window.lemon = (function () {
    let table = [{
        id: '',
        formId: '',
        data: [],
        checkBox: new Map(),
        sortDup: [],
        codeList: []
    }]
    let form = {
        id: [],
        isHidden: true,
        datePickerId: ''
    }
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
        initDatePicker,
        initSider,
        handleFullscreen,
        previewPrint
    }
})()

// 初始化
lemon.initSider()
lemon.initForm()
lemon.initHiddenPanel()
console.log(lemon)