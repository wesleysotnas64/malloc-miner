using System.Collections;
using UnityEngine;

public class APEXProbe : MonoBehaviour
{
    [Header("Probe Settings")]
    [SerializeField] private int id;
    [SerializeField] private string probeName;
    
    [Header("Mining Stats")]
    [SerializeField] private int yieldAmount;
    [SerializeField] private float extractionInterval;

    public float ExtractionProgress { get; private set; }

    void Start()
    {
        StartCoroutine(ExtractResourcesRoutine());
    }

    private IEnumerator ExtractResourcesRoutine()
    {
        float timer = 0.0f;

        while (true)
        {
            timer += Time.deltaTime;
            
            ExtractionProgress = Mathf.Clamp01(timer / extractionInterval);

            if (timer >= extractionInterval)
            {
                timer = 0.0f;
                ExtractionProgress = 0.0f;

                Debug.Log($"[APEX Probe - {probeName}]: {yieldAmount:N0} units extracted.");
                
                // O evento de adicionar minério ao GameManager entrará aqui
                ResourceEventData data = new (id, probeName, yieldAmount, extractionInterval);
                GameEvents.Instance.AddResource(data);
            }

            yield return null;
        }
    }
}