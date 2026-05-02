using System.Collections.Generic;
using UnityEngine;

public class MiningFieldController : MonoBehaviour
{
    public static MiningFieldController MiningFieldInstance;
    public List<MiningSlot> miningSlots;
    public GameObject apexProbePrefab;

    void Awake()
    {
        MiningFieldInstance = this;
    }

    void Start()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnBuyProbe += HandleAddProbe;
        }
    }

    void OnDestroy()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnBuyProbe -= HandleAddProbe;
        }
    }

    private void HandleAddProbe(APEXProbeScriptable data)
    {
        int index = miningSlots.FindIndex(slot => slot.IsFree());
        GameObject probeInstance = Instantiate(apexProbePrefab);
        probeInstance.transform.position = miningSlots[index].transform.position;
        probeInstance.GetComponent<APEXProbe>().InitProbe(data);
        miningSlots[index].IsFree(false);
    }

    public bool HasFreeSpace()
    {
        foreach (MiningSlot slot in miningSlots)
        {
            if(slot.IsFree()) return true;
        }

        return false;
    }
}
