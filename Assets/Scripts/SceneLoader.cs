using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public static SceneLoader singleton;

	public OVRInput.Button startButton;
	private bool hasLoaded = false;

	private void Awake()
	{
		if (singleton != this)
			singleton = this;
	}

	private void Update()
	{
		if((OVRInput.GetDown(startButton) || Input.anyKeyDown) && !hasLoaded)
		{
			hasLoaded = true;
			LoadMatrixScroller();
		}
	}

	public void LoadMatrixScroller()
	{
		SceneManager.LoadSceneAsync("Matrix Inventory", LoadSceneMode.Additive);
	}
}
