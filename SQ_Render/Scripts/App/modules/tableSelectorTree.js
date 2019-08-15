import { getRecursionChildren, getRenderObj } from "./tree.js";

export const treeFilterTable = obj => {

    let allChildren = [];
    getRecursionChildren(allChildren, obj.data);

    let titalsOfAllChildren = allChildren.map(node => node.title); 

    let tableId = obj.data.tableId;
    let fieldName = obj.data.fieldName;

    let table = lemon.table.find(t => t.id == tableId);
    if (table == null)
        return;

    let dataFilterd = table.data.Rows.filter(row => titalsOfAllChildren.some(tital => tital == row[fieldName]))

    layui.table.reload(tableId, {
        page: {
            curr: 1
        },
        data: dataFilterd
    });
}

export const initTableSelectorTree = tsTreeData => {
    let data = [];
    for (let treeNode of tsTreeData.Nodes) {
        data.push(initTreeData(treeNode, tsTreeData._tableId, tsTreeData._fieldName));
    }
    let renderObj = getRenderObj(tsTreeData);
    renderObj.data = data;
    layui.tree.render(renderObj);
}

const initTreeData = (treeNode, tableId, fieldName) => {
    let data = {
        id: treeNode.Id,
        title: treeNode.Title,
        tableId: 0,
        href: treeNode.Href,
        tableId: tableId,
        fieldName: fieldName,

        spread: treeNode.Spread,
        checked: treeNode.Checked,
        dsiabled: treeNode.Disabled
    };

    if (treeNode.Children === null)
        return data;

    let children = [];
    for (let child of treeNode.Children) {
        children.push(initTreeData(child, tableId, fieldName));
    }
    data.children = children;

    return data;
}