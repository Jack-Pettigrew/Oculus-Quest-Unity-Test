using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandgunShoot : MonoBehaviour
{
	private SimpleShoot simpleShoot;
	private OVRGrabbable grabbable;
	public OVRInput.Button vrShootButton;

    // Start is called before the first frame update
    void Start()
    {
		simpleShoot = GetComponent<SimpleShoot>();
		grabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbable.isGrabbed && OVRInput.GetDown(vrShootButton, grabbable.grabbedBy.GetController()))
		{
			simpleShoot.TriggerShoot();
		}
    }
}
