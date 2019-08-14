export const initTree = (id, treeData) => {
    let data = initTreeData(treeData)
    layui.tree.render({
        elem: '#' + id,
        data: data
    });
    console.log(data);
}
const initTreeData = (treeData) => {
    let data = {
        id: treeData.Id,
        title : treeData.Title
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