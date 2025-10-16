using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public ItemData item;
    public int amount;

    [Header("UI Refernece")]
    public Image itemIcon;
    public Text amountText;
    public GameObject emptySlotImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
