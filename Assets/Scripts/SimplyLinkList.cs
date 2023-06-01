using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplyLinkList<T>
{
    class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }
        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
    Node Head { get; set; }
    int Count = 0;
    public void AddNodeAtStart(T value)
    {
        Node newNode = new Node(value);
        if (Head == null)
        {
            Head = newNode;
            Count = Count + 1;
        }
        else if (Head != null)
        {
            Node tmp = Head;
            Head = newNode;
            Head.Next = tmp;
            Count = Count + 1;
        }
    }

    internal void Add(GameObject newProjectile)
    {
        throw new NotImplementedException();
    }

    public void AddNodeAtEnd(T value)
    {
        if (Head == null)
        {
            AddNodeAtStart(value);
        }
        else if (Head != null)
        {
            Node tmp = Head;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            Node newNode = new Node(value);
            tmp.Next = newNode;
            Count = Count + 1;
        }
    }
    public void AddNodeAtPosition(T value, int position)
    {
        if (position == 0)
        {
            AddNodeAtStart(value);
        }
        else if (position == Count)
        {
            AddNodeAtEnd(value);
        }
        else if (position > Count)
        {
            //Console.WriteLine("No se puede");
        }
        else
        {
            int currentPosition = 0;
            Node tmp = Head;
            while (currentPosition < position - 1)
            {
                tmp = tmp.Next;
                currentPosition = currentPosition + 1;
            }
            Node nodeAtPosition = tmp.Next;
            Node newNode = new Node(value);
            tmp.Next = newNode;
            newNode.Next = nodeAtPosition;
            Count = Count + 1;
        }
    }
    public void ModifyAtStart(T value)
    {
        if (Head == null)
        {
            //Console.WriteLine("No se puede");
        }
        else
        {
            Head.Value = value;
        }
    }
    public void ModifyAtEnd(T value)
    {
        Node tmp = Head;
        while (tmp.Next != null)
        {
            tmp = tmp.Next;
        }
        tmp.Value = value;
    }
    public void ModifyAtPosition(T value, int position)
    {
        if (position == 0)
        {
            ModifyAtStart(value);
        }
        else if (position == Count - 2)
        {
            ModifyAtEnd(value);
        }
        else if (position > Count)
        {
            //Console.WriteLine("No se puede");
        }
        else
        {
            int iterator = 0;
            Node tmp = Head;
            while (iterator < Count)
            {
                iterator = iterator + 1;
                tmp = tmp.Next;
            }
            tmp.Value = value;
        }
    }
    public void PrintAllNodes()
    {
        Node tmp = Head;
        while (tmp != null)
        {
           Debug.Log(tmp.Value + " ");
            tmp = tmp.Next;
        }
        //Console.WriteLine();
    }

    public int GetCapacity()
    {
        return Count;
    }

    public T GetNextValue(T value)
    {
        Node tmp = Head;
        while (tmp != null && !tmp.Value.Equals(value))
        {
            tmp = tmp.Next;
        }
        if (tmp != null && tmp.Next != null)
        {
            return tmp.Next.Value;
        }
        else
        {
            return default(T);
        }
    }
    public bool FindValue(T value)
    {
        Node tmp = Head;
        while (tmp != null && !tmp.Value.Equals(value))
        {
            tmp = tmp.Next;
        }
        if (tmp != null)
        {
           // Console.WriteLine("Se encontró el elemento");
            return true;
        }
        else
        {
            //Console.WriteLine("No se encontró el elemento");
            return false;
        }
    }
}
