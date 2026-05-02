using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllStationController : MonoBehaviour
{
    public static ControllStationController StationInstance;

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

    void Awake()
    {
        StationInstance = this;
    }

    void Start()
    {
        totalResources = 100;
        
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnAddResource += HandleResourceReceived;
            GameEvents.Instance.OnBuyProbe += HandleRegisterNewProbe;
        } 

        UpdatePanel();
    }
    private void OnDestroy()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnAddResource -= HandleResourceReceived;
            GameEvents.Instance.OnBuyProbe -= HandleRegisterNewProbe;
        } 
    }

    private void HandleResourceReceived(ResourceEventData data)
    {
        totalResources += data.amount;
        float ratePerSecond = data.amount / data.interval;
        probeProductionRates[data.id] = ratePerSecond;
        CalculateTotalFlow();
        UpdatePanel();
    }

    public void HandleRegisterNewProbe(APEXProbeScriptable data)
    {
        activeProbesCount++;
        totalResources -= data.value;
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

    public long GetTotalResources()
    {
        return totalResources;
    }
}