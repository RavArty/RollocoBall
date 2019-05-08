using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScrollHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler,
IBeginDragHandler, IEndDragHandler, IScrollHandler{ 

	private ScrollRect MainScroll;
	private Animator anim;
	private bool exit = false;
	private bool pressed = false;

	void Awake(){
		anim = GetComponent<Animator> ();
		MainScroll = GameObject.Find ("ScrollViewShop").GetComponent<ScrollRect> ();
	}

	public void OnPointerDown (PointerEventData eventData) {

		exit = false;
		pressed = true;
		anim.SetTrigger ("Pressed");
	}
	 

	public void OnPointerExit (PointerEventData eventData) {
        
		if (pressed) {
			pressed = false;
			anim.SetTrigger ("Exit");

			exit = true;
		}
	}

	public void OnPointerUp (PointerEventData eventData) {
        
		if (!exit) {
			pressed = false;
			anim.SetTrigger ("Up");

		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		MainScroll.OnBeginDrag(eventData);
	}


	public void OnDrag(PointerEventData eventData)
	{
		MainScroll.OnDrag(eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		MainScroll.OnEndDrag(eventData);
	}


	public void OnScroll(PointerEventData data)
	{
		MainScroll.OnScroll(data);
	}


}
