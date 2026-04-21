using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllStationController : MonoBehaviour
{
    [Header("Resources Info")]
    [SerializeField] private long totalResources; // 'minerio' -> totalResources (long para suportar valores altos)
    [SerializeField] private float currentFlow;    // 'fluxoMineracao' -> currentFlow
    
    [Header("Deployment Info")]
    [SerializeField] private int activeProbesCount;
    [SerializeField] private int totalAvailableSlots;

    [Header("UI References")]
    public TMP_Text textResources;
    public TMP_Text textFlow;
    public TMP_Text textProbes;

    // Dicionário para rastrear a produção de cada sonda individualmente e calcular o fluxo total
    private Dictionary<int, float> probeProductionRates = new Dictionary<int, float>();

    void Start()
    {
        if (GameEvents.Instance != null) GameEvents.Instance.OnAddResource += HandleResourceReceived;

        UpdatePanel();
    }
    private void OnDestroy()
    {
        if (GameEvents.Instance != null) GameEvents.Instance.OnAddResource -= HandleResourceReceived;
    }

    private void HandleResourceReceived(ResourceEventData data)
    {
        // 1. Atualiza o montante total
        totalResources += data.amount;

        // 2. Calcula a taxa de produção desta sonda específica (u/s)
        // Se a sonda envia 10 minérios a cada 2 segundos, ela contribui com 5 u/s.
        float ratePerSecond = data.amount / data.interval;
        
        // Armazena ou atualiza a taxa no dicionário usando o ID da sonda
        probeProductionRates[data.id] = ratePerSecond;

        // 3. Recalcula o fluxo total da estação
        CalculateTotalFlow();

        // 4. Atualiza a interface
        UpdatePanel();
    }

    private void CalculateTotalFlow()
    {
        float total = 0;
        foreach (var rate in probeProductionRates.Values)
        {
            total += rate;
        }
        currentFlow = total;
    }

    private void UpdatePanel() 
    {
        // :N0 formata com separadores de milhar. :F1 formata com 1 casa decimal.
        textResources.text = $"RESOURCE: {totalResources:N0} / 1.000.000";
        textFlow.text = $"FLOW: {currentFlow:F1} u/s";
        textProbes.text = $"FIELD DATA: {activeProbesCount} / {totalAvailableSlots} [ACTIVE PROBES / TOTAL SLOTS]";
    }

    // Método para ser chamado quando o jogador comprar/instanciar uma sonda
    public void RegisterNewProbe()
    {
        activeProbesCount++;
        UpdatePanel();
    }
}