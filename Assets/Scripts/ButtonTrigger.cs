using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour
{
	public string collisionTag;
	public UnityEvent onButtonTrigger;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == collisionTag)
		{
			onButtonTrigger.Invoke();
		}
	}

}
