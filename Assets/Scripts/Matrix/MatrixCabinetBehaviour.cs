using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MatrixCabinetBehaviour : MonoBehaviour
{
	Vector3 startPos;
	public float endZ = -50.0f;

    void Awake()
    {
		startPos = transform.position;
    }

	private void Update()
	{
		if (transform.position.z <= endZ)
			transform.position = startPos + new Vector3(0,0, startPos.z + Mathf.Abs(endZ));
	}
}
