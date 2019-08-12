using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GuardianManager : MonoBehaviour
{
	MeshFilter meshFilter;
	OVRBoundary boundary;

	int xSize;
	int ySize = 1;
	Vector3[] boundaryVertices;
	List<Transform> prefabBoundaryPoints;
	[SerializeField] GameObject boundaryPointPrefab;
	[SerializeField] GameObject quadPrefab;
	[SerializeField] float maxBoundaryHeight = 2.4f;

	public OVRInput.Button outerBoundaryButton;
	public OVRInput.Button generateMeshButton;
	public OVRInput.Button gerenateTestMeshButton;
	public OVRInput.Button generateQuadTestButton;

	// Start is called before the first frame update
	void Awake()
	{
		meshFilter = GetComponent<MeshFilter>();
		boundary = OVRManager.boundary;

		prefabBoundaryPoints = new List<Transform>();
	}

	private void Update()
	{
		if (OVRInput.GetDown(outerBoundaryButton))
		{
			GetGuardianBoundary(OVRBoundary.BoundaryType.OuterBoundary);
			DrawBoundary();
		}
		else if (OVRInput.GetDown(generateMeshButton))
		{
			GetGuardianBoundary(OVRBoundary.BoundaryType.OuterBoundary);
			GenerateOuterMesh();
		}
		else if (OVRInput.GetDown(gerenateTestMeshButton))
		{
			GenerateTestMesh();
		}
		else if (OVRInput.GetDown(generateQuadTestButton))
		{
			GetGuardianBoundary(OVRBoundary.BoundaryType.OuterBoundary);
			PlaceQuad();
		}
	}

	void GetGuardianBoundary(OVRBoundary.BoundaryType boundaryType)
	{
		ClearBoundaryPrefabList();

		List<Vector3> boundaryTemp = new List<Vector3>();
		List<Vector3> heightVerts = new List<Vector3>();

		boundaryTemp.AddRange(boundary.GetGeometry(boundaryType));

		for (int i = 0; i < boundaryTemp.Count; i++)
		{
			Vector3 temp = boundaryTemp[i];
			temp.y += maxBoundaryHeight;
			heightVerts.Add(temp);
		}

		boundaryTemp.AddRange(heightVerts);

		boundaryVertices = boundaryTemp.ToArray();

		xSize = boundaryVertices.Length;
	}

	/// <summary>
	/// Draws Guardian Boundary using Sphere prefabss
	/// </summary>
	void DrawBoundary()
	{
		for (int i = 0; i < boundaryVertices.Length; i++)
		{
			prefabBoundaryPoints.Add(Instantiate(boundaryPointPrefab, boundaryVertices[i], Quaternion.identity).transform);
		}
	}

	/// <summary>
	///  Clears up pre-exsisting drawn boundary
	/// </summary>
	void ClearBoundaryPrefabList()
	{
		for (int i = 0; i < prefabBoundaryPoints.Count; i++)
		{
			Destroy(prefabBoundaryPoints[i].gameObject);
		}

		prefabBoundaryPoints.Clear();
	}

	/// <summary>
	/// Generates a Mesh representing the outer Boundary
	/// </summary>
	void GenerateOuterMesh()
	{
		Mesh mesh = new Mesh();

		// Vertices (e.g. 2 quads requires 3 verts (first, middle, end))
		Vector3[] verts = boundaryVertices;

		// Quads (e.g. 20 x 20 quads, each needing 6 verts to form that quad)
		int[] triangles = new int[xSize * ySize * 6];

		int vert = 0;
		int tris = 0;
		for (int i = 0; i < ySize; i++)
		{
			for (int j = 0; j < xSize; j++)
			{
				triangles[tris + 0] = vert + 0;
				triangles[tris + 1] = vert + xSize + 1;
				triangles[tris + 2] = vert + 1;
				triangles[tris + 3] = vert + 1;
				triangles[tris + 4] = vert + xSize + 1;
				triangles[tris + 5] = vert + xSize + 2;

				vert++;
				tris += 6;
			}
			vert++;
		}

		mesh.vertices = verts;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();

		meshFilter.mesh = mesh;
	}

	void GenerateTestMesh()
	{
		Mesh mesh = new Mesh();

		// Vertices (e.g. 2 quads requires 3 verts (first, middle, end))
		Vector3[] vertices = new Vector3[(3 + 1) * (3 + 1)];

		int i = 0;
		for (int j = 0; j <= 3; j++)
		{
			for (int k = 0; k <= 3; k++)
			{
				vertices[i] = new Vector3(k, 0, j);
				i++;
			}
		}

		// Quads (e.g. 20 x 20 quads, each needing 6 verts to form that quad)
		int[] triangles = new int[3 * 3 * 6];

		int vert = 0;
		int tris = 0;
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				triangles[tris + 0] = vert + 0;
				triangles[tris + 1] = vert + 3 + 1;
				triangles[tris + 2] = vert + 1;
				triangles[tris + 3] = vert + 1;
				triangles[tris + 4] = vert + 3 + 1;
				triangles[tris + 5] = vert + 3 + 2;

				vert++;
				tris += 6;
			}
			vert++;
		}


		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();

		meshFilter.mesh = mesh;
	}

	void PlaceQuad()
	{
		Vector3 sum = Vector3.zero;
		for (int i = 0; i < boundaryVertices.Length; i++)
		{
			sum += boundaryVertices[i];
		}

		for (int i = 0; i < boundaryVertices.Length; i += 8)
		{
			GameObject temp = Instantiate(quadPrefab, boundaryVertices[i], Quaternion.identity);
			temp.transform.LookAt(new Vector3(transform.position.x, temp.transform.position.y, transform.position.z));
		}
	}
}
