class SQTagNode {
    constructor(tagName, id) {
        this.tagName = tagName;
        this.id = id;
        this.children = null;
    }

    static getFirstLayerTags(tagName, element, isFirst) {
        let tagElements = [];
        if (!isFirst && element.tagName == tagName.toUpperCase()) {
            tagElements.push(element);
            return tagElements;
        }

        let children = element.children;
        if (children == null || children.length == 0)
            return tagElements;

        for (let i = 0; i < children.length; i++) {
            let childNodesOfChildren = this.getFirstLayerTags(tagName, children[i], false);
            tagElements = tagElements.concat(childNodesOfChildren);
        }
        return tagElements;
    }

    static getTagsAvoidTag(tagName, avoidTagName, element, isFirst) {
        if (!isFirst && element.tagName == avoidTagName.toUpperCase())
            return null;
        let tagElements = [];
        if (element.tagName == tagName.toUpperCase()) {
            tagElements.push(element);
            return tagElements;
        }

        let children = element.children;
        if (children == null || children.length == 0)
            return tagElements;

        for (let i = 0; i < children.length; i++) {
            let childNodesOfChildren = this.getTagsAvoidTag(tagName, avoidTagName, children[i], false);
            if (childNodesOfChildren != null)
                tagElements = tagElements.concat(childNodesOfChildren);
        }

        return tagElements;
    }
}