using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SQ_DB_Framework.DataModel
{
    public class TreeNode
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string Href { get; set; }
        public bool Spread { get; set; }
        public bool Checked { get; set; }
        public bool Disabled { get; set; }
        public List<TreeNode> Children { get; set; }

        private TreeNode(string title, string id)
        {
            Title = title;
            Id = id;
        }

        public static List<TreeNode> GetTreeList<TEntity>(IEnumerable<TEntity> entities,
            Expression<Func<TEntity,TEntity>> parentPropExp,
            Expression<Func<TEntity, IEnumerable<TEntity>>> childrenPropExp,
            Expression<Func<TEntity, string>> titlePropExp,
            Expression<Func<TEntity, string>> idPropExp) where TEntity : EntityBase
        {
            var treeList = new List<TreeNode>();
            var rootEntities = entities.Where(e => parentPropExp.Compile()(e) == null).ToList();
            foreach (var entity in rootEntities)
            {
                treeList.Add(GetTree(entity, childrenPropExp, titlePropExp, idPropExp));
            }

            return treeList;
        }
        public static TreeNode GetTree<TEntity>(TEntity entity, Expression<Func<TEntity, IEnumerable<TEntity>>> childrenPropExp,
            Expression<Func<TEntity, string>> titlePropExp,
            Expression<Func<TEntity, string>> idPropExp) where TEntity:EntityBase
        {
            var currentNode = new TreeNode(titlePropExp.Compile()(entity), idPropExp.Compile()(entity));

            if (childrenPropExp.Compile().Invoke(entity) == null || childrenPropExp.Compile().Invoke(entity).Count() == 0)
                return currentNode;

            currentNode.Children = new List<TreeNode>();
            foreach(var childEntity in childrenPropExp.Compile().Invoke(entity))
            {
                currentNode.Children.Add(GetTree(childEntity, childrenPropExp, titlePropExp, idPropExp));
            }

            return currentNode;
        }
    }
}
