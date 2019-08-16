
export const initTree = (treeData) => {

    layui.tree.render(getRenderObj(treeData));
}
export function getRenderObj(treeData) {

    let data = [];
    for (let treeNode of treeData.Nodes) {
        data.push(initTreeData(treeNode));
    }
    return {
        id: treeData.Id,
        elem: '#' + treeData.Id,
        data: data,
        showCheckbox: treeData.ShowCheckbox || false,
        onlyIconControl: treeData.OnlyIconControl || false,
        edit: getEditItems(treeData.TreeEditItems),
        accordion: treeData.Accordion || false,
        isJump: treeData.IsJump || false,
        showLine: treeData.ShowLine || false,
        text: { defaultNodeName: treeData.DefaultNodeName || '未命名', none: treeData.None || '无数据' },

        click: lemon[treeData.ClickMethod],
        oncheck: treeData.OnCheckMethod,
        operate: treeData.OperateMethod
    }
}
export const getRecursionChildren = (allChildren, node)=> {
    allChildren.push(node);
    if (node.children == null)
        return;

    for (let child of node.children) {
        getRecursionChildren(allChildren, child);
    }
}

const initTreeData = treeNode => {
    let data = {
        id: treeNode.Id,
        title: treeNode.Title,
        tableId:0,
        href: treeNode.Href,
        spread: treeNode.Spread,
        checked: treeNode.Checked,
        dsiabled:treeNode.Disabled
    };

    if (treeNode.Children === null)
        return data;

    let children = [];
    for (let child of treeNode.Children) {
        children.push(initTreeData(child));
    }
    data.children = children;

    return data;
}

const getEditItems = items => {
    if (items === null)
        return null;
    let edit = []
    for (let item of items) {
        switch (item) {
            case 0:
                edit.push("add");
                break;
            case 1:
                edit.push("del");
                break;
            case 2:
                edit.push("update");
                break;
        }
    }
    return edit;
}