using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobiusStrip : MonoBehaviour
{
	private List<GameObject> markers = new List<GameObject>();
	private List<Vector2> uvs = new List<Vector2>();
	public float uResolution = .1f;
	public float vResolution = .1f;
	public float radius;
	public float w;
	public float n;
	public int planeResolution = 100;

	private List<Vector3> vertices = new List<Vector3>();
	private List<int> triangles = new List<int>();
	// Use this for initialization
	public GameObject marker;
	void Start()
	{
		vertices = new List<Vector3>(planeResolution * planeResolution);
		float u = 0;
		float v = -1;
		float uStepSize = (Mathf.PI * 2) / planeResolution;
		float vStepSize = 2.0f / planeResolution;
		//u += uStepSize;
		v += vStepSize;
		float currX = 0;
		while (u <= Mathf.PI * 2.0f)
		{
			float currY = 0;
			
			while(v <= 1)
			{
				//vertices.Add(new Vector3(u, v, 0));
				uvs.Add(new Vector2(currX / (planeResolution-1), currY / (planeResolution-1)));
				//Instantiate(marker, new Vector3(u, v, 0), Quaternion.identity);
				float x = (1 + ((v / 2.0f) * Mathf.Cos(u / 2.0f))) * Mathf.Cos(u);
				float y = (1 + ((v / 2.0f) * Mathf.Cos(u / 2.0f))) * Mathf.Sin(u);
				float z = (v / 2.0f) * Mathf.Sin((u) / 2.0f);
				Vector3 position = new Vector3(x, y, z);
				vertices.Add(position);
				markers.Add(Instantiate(marker, position, Quaternion.identity));
				v += vStepSize;
				currY++;
			}
			currX++;
			v = -1+vStepSize;
			u += uStepSize;
		}
		//Vector3 first = markers[0].transform.position;
		//Vector3 last = markers[markers.Count - 2].transform.position;
		//print(first-last);

		for(int i = 0; i < vertices.Count; i++)
		{
			if (!((i+1) % (planeResolution) == 0))
			{
				int index1 = i + 1;
				int index2 = i + planeResolution;
				int index3 = i + planeResolution + 1;
				print(vertices.Count);
				if(index1%vertices.Count != index1)
				{
					index1 %= vertices.Count;
					index1 = planeResolution - index1-1;
				}
				if (index2 % vertices.Count != index2)
				{
					index2 %= vertices.Count;
					index2 = planeResolution - index2-1;
				}
				if (index3 % vertices.Count != index3)
				{
					index3 %= vertices.Count;
					index3 = planeResolution - index3-1;
				}
				/*if (index1 % vertices.Count != index1 || index2 % vertices.Count != index2 || index3 % vertices.Count != index3)
				{
					if(index1 % vertices.Count - 1 < 0)
					{
						print("Index1 = " + index1);
						print(index1 % vertices.Count - 1);
					}
					index1 %= vertices.Count-1;
					index2 %= vertices.Count-1;
					index3 %= vertices.Count-1;
					print(index1);
					print(index2);
					print(index3);
					index1 = planeResolution- index1+1;
					index2 = planeResolution- index2+1;
					index3 = planeResolution- index3+1;
					
				}
				else
				{
					index1 %= vertices.Count;
					index2 %= vertices.Count;
					index3 %= vertices.Count;
				}*/
				

				triangles.Add(i);
				triangles.Add(index1);
				triangles.Add(index2);

				triangles.Add(index2);
				triangles.Add(index1);
				triangles.Add(index3);

				/*triangles.Add(i + 1);
				triangles.Add(i);
				triangles.Add(i + planeResolution);
				
				triangles.Add(i + planeResolution + 1);
				triangles.Add(i + 1);
				triangles.Add(i + planeResolution);*/
			}
		}

		Mesh m = new Mesh();
		m.vertices = vertices.ToArray();
		m.triangles = triangles.ToArray();
		m.uv = uvs.ToArray();
		m.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = m;
		UnityEditor.AssetDatabase.CreateAsset(m, "Assets/Mobius.asset");
		UnityEditor.AssetDatabase.SaveAssets();
	}

	// Update is called once per frame
	void Update()
	{

	}

	/*private void OnDrawGizmos()
	{
		for(int i = 0; i < triangles.Count; i++)
		{
			Gizmos.DrawLine(vertices[triangles[i]], vertices[triangles[i + 1]]);
			Gizmos.DrawLine(vertices[triangles[i]], vertices[triangles[i + 2]]);
			Gizmos.DrawLine(vertices[triangles[i+1]], vertices[triangles[i + 2]]);
		}
		
	}*/
}

