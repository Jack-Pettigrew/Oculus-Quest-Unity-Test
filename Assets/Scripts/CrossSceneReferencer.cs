using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneReferencer : MonoBehaviour
{
	public static CrossSceneReferencer singleton;

	private void Awake()
	{
		if (singleton != this)
			singleton = this;
	}

	public Transform player;
}
