using lab02.Interfaces;
using lab02.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab02.Tree
{
    public class KdTree<T> where T: ISplitable<T>
    {
        private const int MinNumberOfTriangles = 8;
        private const int MaxDepth = 32;

        public KdNode<T> Root { get; init; }

        public KdTree(T value)
        {
            Root = BuildTree(value, 0);
        }

        private KdNode<T> BuildTree(T value, int depth)
        {
            var node = new KdNode<T>(value);
            //return triangles.Count <= MinNumberOfTriangles || 
            return depth >= MaxDepth ? node : Split(node, depth);
        }

        private KdNode<T> Split(KdNode<T> node, int depth)
        {
            var splitted = node.Value.Split(depth, out T left, out T right, out T middle);
            if (!splitted)
            {
                return node;
            }
            var leftNode = BuildTree(left, depth + 1);
            var middleNode = BuildTree(middle, depth + 1);
            var rightNode = BuildTree(right, depth + 1);
            node.Children.Add(leftNode);
            node.Children.Add(middleNode);
            node.Children.Add(rightNode);
            return node;
        }

        public T1 Perform<T1>(Func<T, T1> operation)
        {
            return Root.Perform(operation);
        }

    }
}
