using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimpleList<T> :ISerializationCallbackReceiver
{
    [SerializeField]
    private T[] array;
    [SerializeField]
    private int count;

    public SimpleList()
    {
        array = new T[4]; // Tama�o inicial del arreglo
        count = 0;
    }

    public void Add(T item)
    {
        if (count == array.Length)
        {
            // Si el arreglo est� lleno, duplicamos su tama�o
            Array.Resize(ref array, array.Length * 2);
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

        if (count <= array.Length / 4)
        {
            // Si el arreglo est� menos de 1/4 lleno, reducimos su tama�o a la mitad
            Array.Resize(ref array, array.Length / 2);
        }
    }

    public T Get(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException();
        }

        return array[index];
    }

    // Resto de los m�todos omitidos por brevedad

    public int Count
    {
        get { return count; }
    }

    public void OnBeforeSerialize()
    {
        // No se requiere implementaci�n adicional para la serializaci�n
    }

    public void OnAfterDeserialize()
    {
        // Actualizar el recuento y la capacidad despu�s de la deserializaci�n
        count = array.Length;
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
}
