using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelectUI : MonoBehaviour
{
	public Text selectedWorldTitle;

	public void UpdateSelectedWorldName(string worldName)
	{
		selectedWorldTitle.text = worldName;
	}
}
