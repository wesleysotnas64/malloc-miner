using UnityEngine;

[CreateAssetMenu(fileName = "APEXProbeScriptable", menuName = "Probe/New APEX")]
public class APEXProbeScriptable : ScriptableObject
{
    public int id;
    public string probeName;
    public int yieldAmount;
    public float extractionInterval;
}
