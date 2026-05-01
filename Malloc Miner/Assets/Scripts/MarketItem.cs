using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketItem : MonoBehaviour
{
    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textDescription;
    [SerializeField] private TMP_Text textValue;

    public void SetItem(string name, string description, int value)
    {
        textName.text = name;
        textDescription.text = description;
        textValue.text = $"M$ {value:N0}";
    }

}
