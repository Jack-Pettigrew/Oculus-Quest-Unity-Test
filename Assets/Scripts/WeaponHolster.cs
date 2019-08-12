using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WeaponHolster : MonoBehaviour
{
	public Transform holsteredWeapon;
	public Transform holsterParent;
	public Vector3 holsteredGunRotation;
	[HideInInspector] public bool hasWeaponHolstered = false;

	private void Awake()
	{
		GetComponent<BoxCollider>().isTrigger = true;
	}

	private void Start()
	{
		if(holsteredWeapon)
		{
			HolsterWeapon(holsteredWeapon);
		}
	}

	public void HolsterWeapon(Transform weaponTransform)
	{
		holsteredWeapon = weaponTransform;
		hasWeaponHolstered = true;

		holsteredWeapon.GetComponent<Rigidbody>().isKinematic = true;

		holsteredWeapon.transform.localPosition = transform.position;
		holsteredWeapon.transform.localRotation = Quaternion.Euler(holsteredGunRotation);
		holsteredWeapon.transform.SetParent(holsterParent, true);
	}

	public void UnholsterWeapon()
	{
			holsteredWeapon.transform.SetParent(null);
			//holsteredWeapon.GetComponent<Rigidbody>().isKinematic = false;

			holsteredWeapon = null;
			hasWeaponHolstered = false;
	}



}
