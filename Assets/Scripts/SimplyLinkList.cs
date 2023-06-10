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
    public int Count { get; private set; }

    public void AddNodeAtStart(T value)
    {
        Node newNode = new Node(value);

        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            newNode.Next = Head;
            Head = newNode;
        }

        Count++;
    }

    public void Add(T value)
    {
        AddNodeAtStart(value);
    }

    public T RemoveFirst()
    {
        if (Head == null)
        {
            throw new InvalidOperationException("List is empty");
        }

        T value = Head.Value;
        Head = Head.Next;
        Count--;
        return value;
    }

    public T GetFirst()
    {
        if (Head == null)
        {
            throw new InvalidOperationException("List is empty");
        }

        return Head.Value;
    }

    public void AddNodeAtEnd(T value)
    {
        Node newNode = new Node(value);

        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            Node tmp = Head;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            tmp.Next = newNode;
        }

        Count++;
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
            // Console.WriteLine("No se puede");
        }
        else
        {
            int currentPosition = 0;
            Node tmp = Head;

            while (currentPosition < position - 1)
            {
                tmp = tmp.Next;
                currentPosition++;
            }

            Node nodeAtPosition = tmp.Next;
            Node newNode = new Node(value);
            tmp.Next = newNode;
            newNode.Next = nodeAtPosition;

            Count++;
        }
    }

    public void ModifyAtStart(T value)
    {
        if (Head != null)
        {
            Head.Value = value;
        }
    }

    public void ModifyAtEnd(T value)
    {
        Node tmp = Head;
        while (tmp != null && tmp.Next != null)
        {
            tmp = tmp.Next;
        }

        if (tmp != null)
        {
            tmp.Value = value;
        }
    }

    public void ModifyAtPosition(T value, int position)
    {
        if (position == 0)
        {
            ModifyAtStart(value);
        }
        else if (position == Count - 1)
        {
            ModifyAtEnd(value);
        }
        else if (position >= Count)
        {
            // Console.WriteLine("No se puede");
        }
        else
        {
            int iterator = 0;
            Node tmp = Head;

            while (iterator < position && tmp != null)
            {
                tmp = tmp.Next;
                iterator++;
            }

            if (tmp != null)
            {
                tmp.Value = value;
            }
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
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Remove(T value)
    {
        if (Head == null)
        {
            return;
        }

        if (Head.Value.Equals(value))
        {
            Head = Head.Next;
            Count--;
            return;
        }

        Node tmp = Head;
        while (tmp.Next != null && !tmp.Next.Value.Equals(value))
        {
            tmp = tmp.Next;
        }

        if (tmp.Next != null)
        {
            tmp.Next = tmp.Next.Next;
            Count--;
        }
    }

    public void Clear()
    {
        Head = null;
        Count = 0;
    }
}
