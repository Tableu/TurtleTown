using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Collider2D collider;
    [SerializeField] private GameObject infoPopup;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MapIcon Clicked");
    }
}
