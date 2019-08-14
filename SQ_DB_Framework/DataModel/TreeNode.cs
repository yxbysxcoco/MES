using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SQ_DB_Framework.DataModel
{
    public class TreeNode
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public List<TreeNode> Children { get; set; }

        public TreeNode(string title, string id)
        {
            Title = title;
            Id = id;
        }


        public static TreeNode GetTree<TEntity>(TEntity entity, Expression<Func<TEntity, IEnumerable<TEntity>>> childrenPropExp,
            Expression<Func<TEntity, string>> titlePropExp,
            Expression<Func<TEntity, string>> idPropExp) where TEntity:EntityBase
        {
            var currentNode = new TreeNode(titlePropExp.Compile()(entity), idPropExp.Compile()(entity));

            if (childrenPropExp.Compile().Invoke(entity) == null)
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
