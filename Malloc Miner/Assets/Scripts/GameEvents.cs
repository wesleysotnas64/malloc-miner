using System;
using UnityEngine;

public struct ResourceEventData 
{
    public int id;
    public string probeName;
    public int amount;
    public float interval;

    public ResourceEventData(int id, string name, int amount, float interval)
    {
        this.id = id;
        this.probeName = name;
        this.amount = amount;
        this.interval = interval;
    }
}

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    void Awake() => Instance = this;

    // Evento usando Generics para passar a struct de dados
    public event Action<ResourceEventData> OnAddResource;

    public void AddResource(ResourceEventData data) => OnAddResource?.Invoke(data);
    
}