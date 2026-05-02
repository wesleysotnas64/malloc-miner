using UnityEngine;

public class MiningSlot : MonoBehaviour
{
    [SerializeField] private bool isBlocked;
    [SerializeField] private bool isFree;
    
    public bool IsFree()
    {
        return isFree;
    }

    public void IsFree(bool free)
    {
        isFree = free;
    }

    public bool IsBlocked()
    {
        return isBlocked;
    }

    public void IsBlocked(bool blocked)
    {
        isBlocked = blocked;
    }
}
