using UnityEngine;
using System;

[Serializable]
public class SimpleList<T> : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    private T[] array;
    [SerializeField]
    private int count;
    [SerializeField]
    private int capacity;

    public int Count { get { return count; } }
    public int Capacity { get { return capacity; } }

    public SimpleList()
    {
        array = new T[4];
        count = 0;
        capacity = 4;
    }

    public void Add(T item)
    {
        if (count >= capacity)
        {
            // Duplicar el tama�o del array si se alcanza la capacidad m�xima
            T[] newArray = new T[capacity * 2];
            Array.Copy(array, newArray, capacity);
            array = newArray;
            capacity *= 2;
        }

        array[count] = item;
        count++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException();
        }

        for (int i = index; i < count - 1; i++)
        {
            array[i] = array[i + 1];
        }

        count--;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            return array[index];
        }
        set
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            array[index] = value;
        }
    }

    public void OnBeforeSerialize()
    {
        // No se requiere implementaci�n adicional para la serializaci�n
    }

    public void OnAfterDeserialize()
    {
        // Actualizar el recuento y la capacidad despu�s de la deserializaci�n
        count = array.Length;
        capacity = array.Length;
    }
}
