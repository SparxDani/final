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
            // Duplicar el tamaño del array si se alcanza la capacidad máxima
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
        // No se requiere implementación adicional para la serialización
    }

    public void OnAfterDeserialize()
    {
        // Actualizar el recuento y la capacidad después de la deserialización
        count = array.Length;
        capacity = array.Length;
    }
}
