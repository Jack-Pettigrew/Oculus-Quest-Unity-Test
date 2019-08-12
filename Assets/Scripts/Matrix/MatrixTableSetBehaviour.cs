using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTableSetBehaviour : MonoBehaviour
{
	private Transform player;

	private void Start()
	{
		player = CrossSceneReferencer.singleton.player;
	}

	private void Update()
	{
		if(transform.position.z <= player.position.z)
			MatrixController.singleton.StopScroll = true;
	}
}
