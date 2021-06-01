using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Tree
{
    public class KdNode<T>
    {
        public T Value { get; init; }
        public List<KdNode<T>> Children { get; init; }

        public KdNode(T value)
        {
            Value = value;
            Children = new List<KdNode<T>>();
        }

        public T1 Perform<T1>(Func<T, T1> operation)
        {
            if (Children.Count > 0)
            {
                return PerformOnChildren(operation);
            }
            return operation.Invoke(Value);
        } 

        private T1 PerformOnChildren<T1>(Func<T, T1> operation)
        {
            foreach(var node in Children)
            {
                return operation.Invoke(node.Value);
            }
            return default;
        }
    }
}
