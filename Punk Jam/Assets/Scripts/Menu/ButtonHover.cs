using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler
{
	[SerializeField] private ClockArrays clockArrays;
	public int index = 0;

	public void OnPointerEnter(PointerEventData eventData)
	{
		clockArrays.Button0(index);
	}
}
