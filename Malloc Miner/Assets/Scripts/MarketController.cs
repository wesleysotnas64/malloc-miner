using System.Collections.Generic;
using UnityEngine;

public class MarketController : MonoBehaviour
{
    [SerializeField] private List<APEXProbeScriptable> apexProbeList;
    [SerializeField] private GameObject marketItemPrefab;
    [SerializeField] private GameObject marketContent;

    void Start()
    {
        InitMarket();
    }

    public void InitMarket()
    {
        //Limpa o mercado antes de listar os itens
        foreach (Transform child in marketContent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (APEXProbeScriptable probe in apexProbeList)
        {
            // Instancia o prefab como filho do content do ScrollView
            GameObject go = Instantiate(marketItemPrefab, marketContent.transform);

            // Pega a referência do seu script MarketItem
            MarketItem itemScript = go.GetComponent<MarketItem>();

            if (itemScript != null)
            {
                // Chama o seu método SetItem
                itemScript.SetItem(probe.probeName, probe.description, probe.value);
            }
        }
    }
}
