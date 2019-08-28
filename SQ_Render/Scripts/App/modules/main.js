import { showHiddenPanel, initHiddenPanel } from './hiddenPanel.js'
import { initTable, createCode } from './table.js'
import { fliterTable, resetForm, initForm, initDatePicker, bindTableIdToForm, getFormData} from './form.js'
import { initSider } from './sider.js'
import { handleFullscreen, previewPrint } from './util.js'
import { initTree } from './tree.js'
import { showModal, initShowModalBtn } from './modal.js'
import { initTableSelectorTree, treeFilterTable } from './tableSelectorTree.js'
import { pushData } from "./req.js"

window.lemon = (function () {
    let table = []
    let form = []
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
        initTree,
        showModal,
        initShowModalBtn,
        initTableSelectorTree,
        treeFilterTable,
        getFormData,
        pushData
    }
})()

// 初始化
lemon.initSider()
