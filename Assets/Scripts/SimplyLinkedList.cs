using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SimplyLinkedList<T> : ISerializationCallbackReceiver
{
    [SerializeField]
    private List<T> serializedList = new List<T>();

    private class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }

    [NonSerialized]
    private Node head;

    [NonSerialized]
    private int count;

    public int Count => count;

    public void OnBeforeSerialize()
    {
        serializedList.Clear();
        Node tmp = head;
        while (tmp != null)
        {
            serializedList.Add(tmp.Value);
            tmp = tmp.Next;
        }
    }

    public void OnAfterDeserialize()
    {
        Clear();
        foreach (T value in serializedList)
        {
            AddNodeAtEnd(value);
        }
    }

    public void AddNodeAtStart(T value)
    {
        Node newNode = new Node(value);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            newNode.Next = head;
            head = newNode;
        }

        count++;
    }

    public void Add(T value)
    {
        AddNodeAtStart(value);
    }

    public T RemoveFirst()
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty");
        }

        T value = head.Value;
        head = head.Next;
        count--;
        return value;
    }

    public T GetFirst()
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty");
        }

        return head.Value;
    }

    public void AddNodeAtEnd(T value)
    {
        Node newNode = new Node(value);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node tmp = head;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            tmp.Next = newNode;
        }

        count++;
    }

    public void AddNodeAtPosition(T value, int position)
    {
        if (position == 0)
        {
            AddNodeAtStart(value);
        }
        else if (position == count)
        {
            AddNodeAtEnd(value);
        }
        else if (position > count)
        {
            // Console.WriteLine("No se puede");
        }
        else
        {
            int currentPosition = 0;
            Node tmp = head;

            while (currentPosition < position - 1)
            {
                tmp = tmp.Next;
                currentPosition++;
            }

            Node nodeAtPosition = tmp.Next;
            Node newNode = new Node(value);
            tmp.Next = newNode;
            newNode.Next = nodeAtPosition;

            count++;
        }
    }

    public void ModifyAtStart(T value)
    {
        if (head != null)
        {
            head.Value = value;
        }
    }

    public void ModifyAtEnd(T value)
    {
        Node tmp = head;
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
        else if (position == count - 1)
        {
            ModifyAtEnd(value);
        }
        else if (position >= count)
        {
            // Console.WriteLine("No se puede");
        }
        else
        {
            int iterator = 0;
            Node tmp = head;

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
        Node tmp = head;
        while (tmp != null)
        {
            Debug.Log(tmp.Value + " ");
            tmp = tmp.Next;
        }
    }

    public int GetCapacity()
    {
        return count;
    }

    public T GetNextValue(T value)
    {
        Node tmp = head;
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
        Node tmp = head;
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
        if (head == null)
        {
            return;
        }

        if (head.Value.Equals(value))
        {
            head = head.Next;
            count--;
            return;
        }

        Node tmp = head;
        while (tmp.Next != null && !tmp.Next.Value.Equals(value))
        {
            tmp = tmp.Next;
        }

        if (tmp.Next != null)
        {
            tmp.Next = tmp.Next.Next;
            count--;
        }
    }
    public T Get(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new ArgumentOutOfRangeException("index");
        }

        Node tmp = head;
        for (int i = 0; i < index; i++)
        {
            tmp = tmp.Next;
        }

        return tmp.Value;
    }

    public void Clear()
    {
        head = null;
        count = 0;
    }
}
