using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnimationButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (CharController.dead == false && ButtonsScript.PauseActive == false)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 6f);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (CharController.dead == false && ButtonsScript.PauseActive == false)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 6f);
        }
    }
    public void Update()
    {
        if(ButtonsScript.PauseActive == true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }
}
