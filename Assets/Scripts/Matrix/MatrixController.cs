using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatrixController : MonoBehaviour
{
	public static MatrixController singleton;

	[SerializeField] GameObject cabinetSetPrefab;
	[SerializeField] GameObject tableSetPrefab;
	[SerializeField] int cabinetPoolSize;
	[SerializeField] Vector3 cabinetStartPos;
	[SerializeField] float forceDelay;
	[SerializeField] float forceStrength;

	public bool StopScroll { get; set; }

	List<GameObject> cabinetPool;

	private void Awake()
	{
		if(singleton != this)
			singleton = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("Matrix Inventory"));

		GenerateCabinets();
	}

	private void GenerateCabinets()
	{
		if(cabinetPool == null)
		{
			cabinetPool = new List<GameObject>();

			for (int i = 0; i < cabinetPoolSize; i++)
			{
				if (i == cabinetPoolSize - 1)
					cabinetPool.Add(Instantiate(tableSetPrefab, cabinetStartPos, Quaternion.identity));
				else
					cabinetPool.Add(Instantiate(cabinetSetPrefab, cabinetStartPos, Quaternion.identity));
			}
		}
		else
		{
			for (int i = 0; i < cabinetPoolSize; i++)
			{
				cabinetPool[i].transform.position = cabinetStartPos;
			}
		}

			StartCoroutine(BeginScenario());
	}

	private IEnumerator BeginScenario()
	{
		for (int i = 0; i < cabinetPoolSize; i++)
		{
			yield return new WaitForSeconds(forceDelay);
			cabinetPool[i].GetComponent<Rigidbody>().AddForce(Vector3.back * forceStrength, ForceMode.Impulse);
		}

		yield return new WaitWhile(() => StopScroll == false);

		for (int i = 0; i < cabinetPoolSize; i++)
		{
			cabinetPool[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
		}

		yield return null;
	}
}
