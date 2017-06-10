using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapNode<T>
{
    T[] nodes;
    int currentNodeCount;

    public int Count
    {
        get
        {
            return currentNodeCount;
        }
    }

    public Heap(int maxNodeCount)
    {
        nodes = new T[maxNodeCount];
    }

    public bool Contains(T node)
    {
        return Equals(nodes[node.HeapIndex], node);
    }

    public void Update(T node)
    {
        SortUp(node);
    }

    public void Add(T node)
    {
        node.HeapIndex = currentNodeCount++;
        nodes[node.HeapIndex] = node;

        SortUp(node);
    }

    private void SortUp(T node)
    {
        while (true)
        {
            int parentIndex = (node.HeapIndex - 1) / 2;
            if (nodes[parentIndex].CompareTo(node) > 0)
            {
                Swap(node, nodes[parentIndex]);
            }
            else
            {
                return;
            }
        }
    }

    public T RemoveTop()
    {
        T heapTop = nodes[0];
        currentNodeCount--;
        nodes[0] = nodes[currentNodeCount];
        nodes[0].HeapIndex = 0;
        SortDown(nodes[0]);

        return heapTop;
    }

    private void SortDown(T node)
    {
        while (true)
        {
            int leftChildIndex = node.HeapIndex * 2 + 1;
            int rightChildIndex = node.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (leftChildIndex < currentNodeCount)
            {
                swapIndex = leftChildIndex;
                if (rightChildIndex < currentNodeCount)
                {
                    if (nodes[leftChildIndex].CompareTo(nodes[rightChildIndex]) > 0)
                    {
                        swapIndex = rightChildIndex;
                    }
                }

                if (node.CompareTo(nodes[swapIndex]) > 0)
                {
                    Swap(node, nodes[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    private void Swap(T nodeA, T nodeB)
    {
        nodes[nodeA.HeapIndex] = nodeB;
        nodes[nodeB.HeapIndex] = nodeA;
        int tempIndex = nodeA.HeapIndex;
        nodeA.HeapIndex = nodeB.HeapIndex;
        nodeB.HeapIndex = tempIndex;
    }
}

public interface IHeapNode<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}