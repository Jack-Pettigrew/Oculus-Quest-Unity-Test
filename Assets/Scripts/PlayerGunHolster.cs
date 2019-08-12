using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunHolster : MonoBehaviour
{
	public Transform followTarget;
	public Transform[] weaponHolsters;
	private List<Vector3> holsterOffsetPos;

	private void Awake()
	{
		if(!followTarget)
		{
			Debug.LogWarning("No follow target, defaulting to this GameObject: " + gameObject.name);
			followTarget = this.transform;
		}

		holsterOffsetPos = new List<Vector3>();

		for (int i = 0; i < weaponHolsters.Length; i++)
		{
			holsterOffsetPos.Add(new Vector3(weaponHolsters[i].position.x, 0, weaponHolsters[i].position.z));
		}
	}

	private void LateUpdate()
	{
		for (int i = 0; i < weaponHolsters.Length; i++)
		{
			weaponHolsters[i].position = followTarget.position + holsterOffsetPos[i];
			weaponHolsters[i].rotation = followTarget.rotation;
		}
	}
}
