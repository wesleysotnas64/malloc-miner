using TMPro;
using UnityEngine;

public class MarketItem : MonoBehaviour
{
    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textDescription;
    [SerializeField] private TMP_Text textValue;
    private APEXProbeScriptable itemScriptable;

    public void SetItem(APEXProbeScriptable apexScriptable)
    {
        itemScriptable = apexScriptable;
        textName.text = itemScriptable.name;
        textDescription.text = $"{itemScriptable.yieldAmount}U in {itemScriptable.extractionInterval}s ({(float)itemScriptable.yieldAmount / itemScriptable.extractionInterval:F2}U/s)";
        textValue.text = $"M$ {itemScriptable.value:N0}";
    }
    
    public void BuyItem()
    {
        // Verifica se há espaço no campo de mineração
        if (!MiningFieldController.MiningFieldInstance.HasFreeSpace())
        {
            // Se não houver espaço, lança um erro no terminal
            // [ERROR]: Não há slots de mineração disponíveis 
            return;
        }
        
        // Verifica se há o minério necessário
        if (ControllStationController.StationInstance.GetTotalResources() < itemScriptable.value)
        {
            // Se não houver minério o suficiente, lança um erro no terminal
            // [ERROR]: Não há minério suficiente
            return;
        }
        
        // Compra o item e aloca no primeiro espaço livre do campo
        GameEvents.Instance.BuyProbe(itemScriptable);
    }

}
