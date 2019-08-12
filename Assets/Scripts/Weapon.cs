using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	OVRGrabbable grabbable;

	private void Start()
	{
		grabbable = GetComponent<OVRGrabbable>();
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Holster")
		{
			WeaponHolster holster = other.GetComponent<WeaponHolster>();

			if(!holster)
			{
				Debug.LogError("No WeaponHolster component found for: " + gameObject.name);
			}

			if (!grabbable.isGrabbed && !holster.hasWeaponHolstered)
				holster.HolsterWeapon(transform);
			else if (grabbable.isGrabbed && holster.hasWeaponHolstered)
				holster.UnholsterWeapon();
		}

	}
}
