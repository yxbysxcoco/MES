
export const initTree = (id, treeData) => {
    let data = [];
    for (let treeNode of treeData) {
        data.push(initTreeData(treeNode));
    }
    layui.tree.render({
        elem: '#' + id,
        data: data,
        onlyIconControl: true,
        click: obj => layer.msg(JSON.stringify(obj))
    });
    console.log(JSON.stringify(data));
}
const initTreeData = (treeData) => {
    let data = {
        id: treeData.Id,
        title: treeData.Title
    };

    if (treeData.Children === null)
        return data;

    let children = [];
    for (let child of treeData.Children) {
        children.push(initTreeData(child));
    }
    data.children = children;

    return data;
}