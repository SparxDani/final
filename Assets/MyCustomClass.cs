using System.Collections.Generic;
using UnityEngine;

public class MyCustomClass
{
    private List<GameObject> myList;
    private Queue<GameObject> myQueue;
    private Stack<GameObject> myStack;

    public MyCustomClass()
    {
        myList = new List<GameObject>();
        myQueue = new Queue<GameObject>();
        myStack = new Stack<GameObject>();
    }

    public void AddToList(GameObject obj)
    {
        myList.Add(obj);
    }

    public void EnqueueObject(GameObject obj)
    {
        myQueue.Enqueue(obj);
    }

    public GameObject DequeueObject()
    {
        return myQueue.Dequeue();
    }

    public void PushObject(GameObject obj)
    {
        myStack.Push(obj);
    }

    public GameObject PopObject()
    {
        return myStack.Pop();
    }
}
