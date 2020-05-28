using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour
{
	public ClickEvents onClick;

	void OnMouseDown()
	{
		// Debug.Log("Mouse Down!");
    onClick.Invoke();
	}
}
