class SQTagNode {
    constructor(tagName, id) {
        this.tagName = tagName;
        this.id = id;
        this.children = null;
    }

    static getFirstLayerTags(tagName, element, isFirst) {
        let childNodes = [];
        if (!isFirst && element.tagName.toUpperCase() == tagName) {
            childNodes.push(new SQTagNode(tagName, element.id));
            return childNodes;
        }

        let children = element.children;
        if (children == null || children.length == 0)
            return childNodes;

        for (let i = 0; i < children.length; i++) {
            let childNodesOfChildren = this.getFirstLayerTags(tagName, children[i], false);
            childNodes = childNodes.concat(childNodesOfChildren);
        }
        return childNodes;
    }

    static getTagsAvoidTag(tagName, avoidTagName, element, isFirst) {
        if (!isFirst && element.tagName.toUpperCase() == avoidTagName)
            return null;
        let childNodes = [];
        if (element.tagName == tagName.toUpperCase()) {
            childNodes.push(new SQTagNode(tagName, element.id));
            return childNodes;
        }

        let children = element.children;
        if (children == null || children.length == 0)
            return childNodes;

        for (let i = 0; i < children.length; i++) {
            let childNodesOfChildren = this.getTagsAvoidTag(tagName, avoidTagName, children[i], false);
            if (childNodesOfChildren != null)
                childNodes = childNodes.concat(childNodesOfChildren);
        }

        return childNodes;
    }
}