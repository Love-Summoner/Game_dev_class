using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class visualize_inventory : MonoBehaviour
{
    [SerializeField]private Inventory inventory;
    [SerializeField]private Sprite[] sprites = new Sprite[6];

    public Image[] icons = new Image[6];
    public TMP_Text[] numbers = new TMP_Text[6];


    public void change_slot_icon(int i)
    {
        numbers[i].text = inventory.get_item_count(i).ToString();
        icons[i].sprite = sprites[i];
    }
}
