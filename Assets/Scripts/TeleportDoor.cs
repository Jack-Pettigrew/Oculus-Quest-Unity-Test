using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TeleportDoor : MonoBehaviour
{
	[SerializeField] WorldSelectUI worldSelectUI;

	public string[] sceneNames;

	private int sceneNameIndex = 0;

	public UnityEvent onEnterTeleportDoor;

	private void Start()
	{
		worldSelectUI.UpdateSelectedWorldName(sceneNames[sceneNameIndex]);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			onEnterTeleportDoor.Invoke();
		}
	}

	public void LoadScene()
	{
		SceneManager.LoadSceneAsync(sceneNames[sceneNameIndex], LoadSceneMode.Single);
	}

	public void SelectNextWorld()
	{
		sceneNameIndex++;

		if (sceneNameIndex >= sceneNames.Length)
			sceneNameIndex = 0;

		worldSelectUI.UpdateSelectedWorldName(sceneNames[sceneNameIndex]);
	}
}
