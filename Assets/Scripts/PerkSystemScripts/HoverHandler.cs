using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PerkInfo perkInfo { get; set; }

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    void Update()
    {
        title.text = perkInfo.title;  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.text = perkInfo.description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.text = "";
    }

}
