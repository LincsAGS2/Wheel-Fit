using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour
{
	public float Scale = 0.25f;
	public float WaveSpeed = 1.0f;
	public float NoiseStrength = 1f;
	public float NoiseDistance = 1f;
	
	private Vector3[] BaseHeight;
	
	void Update () {

		Mesh WaterMesh = GetComponent<MeshFilter>().mesh; // watermech contains the mesh data of the gameobject this script is attached to
		
		if (BaseHeight == null)// checks if BaseHeight is null
		{
			BaseHeight = WaterMesh.vertices; // Adds the meshes current vertices to the BaseHeight array
		}

		Vector3[] NewVertices = new Vector3[BaseHeight.Length]; // creates a new vertices array that is the same size as the BaseHeight array

		for (int Count = 0; Count < NewVertices.Length; Count++)// loops througth the entire array
		{
			Vector3 vertex = BaseHeight[Count]; // current vertex that will be changed
			vertex.y += Mathf.Sin(Time.time * WaveSpeed + BaseHeight[Count].x + BaseHeight[Count].y + BaseHeight[Count].z) * Scale; // adds a sin wave
			vertex.y += Mathf.PerlinNoise(BaseHeight[Count].x + NoiseDistance, BaseHeight[Count].y + Mathf.Sin(Time.time * 0.1f)) * NoiseStrength; // adds PerlinNoise
			NewVertices[Count] = vertex; // adds the vertex data to the newVertices array
		}

		WaterMesh.vertices = NewVertices; // updates the meshes vertices
		WaterMesh.RecalculateNormals(); // recalculate the mesh's normals
	}
}