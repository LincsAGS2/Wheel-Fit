using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Voxel {
	Mesh mesh;
	Material mat;
	List<Vector3> Points;
	List<Vector3> Verts;
	List<int> Triangles;
	List<Vector2> UVs;
	GameObject gameObject;
	float size = 0.5f;
	Vector3 position;
	public Voxel(Vector3 pos)
	{
		position = pos;
		gameObject = new GameObject ();
		Points = new List<Vector3> ();
		Points.Add (new Vector3 (position.x - size, position.y + size, position.z - size));
		Points.Add (new Vector3 (position.x + size, position.y + size, position.z - size));
		Points.Add (new Vector3 (position.x + size, position.y - size, position.z - size));
		Points.Add (new Vector3 (position.x - size, position.y - size, position.z - size));

		Points.Add (new Vector3 (position.x + size, position.y + size, position.z + size));
		Points.Add (new Vector3 (position.x - size, position.y + size, position.z + size));
		Points.Add (new Vector3 (position.x - size, position.y - size, position.z + size));
		Points.Add (new Vector3 (position.x + size, position.y - size, position.z + size));

		Verts = new List<Vector3> ();
		Triangles = new List<int>();
		UVs = new List<Vector2> ();

		CreateMesh();
	}

	private void CreateMesh()
	{
		gameObject.AddComponent("MeshFilter");
		gameObject.AddComponent("MeshRenderer");
		gameObject.AddComponent("MeshCollider");

		mat = Resources.Load ("Materials/Default") as Material;

		if (mat == null) 
		{
			Debug.LogError("Material not found!");
			return;
		}

		MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
		if (meshFilter == null) 
		{
			Debug.LogError("MeshFilter not found!");
			return;
		}

		mesh = meshFilter.sharedMesh;
		if (mesh == null) 
		{
			meshFilter.mesh = new Mesh();
			mesh = meshFilter.sharedMesh;
		}

		MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
		if (meshCollider == null) 
		{
			Debug.LogError("MeshCollider not found!");
			return;
		}

		mesh.Clear();

		Update();

	
	}
	// Use this for initialization
	public void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () 
	{
		//Builds points from top left point going clockwise

		//Front
		Verts.Add(Points [0]);
		Verts.Add(Points [1]);
		Verts.Add(Points [2]);
		Verts.Add(Points [3]);

		//Back
		Verts.Add(Points [4]);
		Verts.Add(Points [5]);
		Verts.Add(Points [6]);
		Verts.Add(Points [7]);

		//Left
		Verts.Add(Points [5]);
		Verts.Add(Points [0]);
		Verts.Add(Points [3]);
		Verts.Add(Points [6]);

		//Right
		Verts.Add(Points [1]);
		Verts.Add(Points [4]);
		Verts.Add(Points [7]);
		Verts.Add(Points [2]);

		//Top
		Verts.Add(Points [5]);
		Verts.Add(Points [4]);
		Verts.Add(Points [1]);
		Verts.Add(Points [0]);

		//Bottom
		Verts.Add(Points [3]);
		Verts.Add(Points [2]);
		Verts.Add(Points [7]);
		Verts.Add(Points [6]);

		//Builds 2 triangles to fill gaps between points for each face

		//Front 
		Triangles.Add(0);
		Triangles.Add(1);
		Triangles.Add(2);

		Triangles.Add(2);
		Triangles.Add(3);
		Triangles.Add(0);

		//Back
		Triangles.Add(4);
		Triangles.Add(5);
		Triangles.Add(6);
		
		Triangles.Add(6);
		Triangles.Add(7);
		Triangles.Add(4);

		//Left
		Triangles.Add(8);
		Triangles.Add(9);
		Triangles.Add(10);
		
		Triangles.Add(10);
		Triangles.Add(11);
		Triangles.Add(8);

		//Right
		Triangles.Add(12);
		Triangles.Add(13);
		Triangles.Add(14);
		
		Triangles.Add(14);
		Triangles.Add(15);
		Triangles.Add(12);

		//Top
		Triangles.Add(16);
		Triangles.Add(17);
		Triangles.Add(18);
		
		Triangles.Add(18);
		Triangles.Add(19);
		Triangles.Add(16);

		//Bottom
		Triangles.Add(20);
		Triangles.Add(21);
		Triangles.Add(22);
		
		Triangles.Add(22);
		Triangles.Add(23);
		Triangles.Add(20);

		//add UV texture to face or plane

		//Front
		UVs.Add(new Vector2(0,1));
		UVs.Add(new Vector2(1,1));
		UVs.Add(new Vector2(1,0));
		UVs.Add(new Vector2(0,0));

		//Back
		UVs.Add(new Vector2(0,1));
		UVs.Add(new Vector2(1,1));
		UVs.Add(new Vector2(1,0));
		UVs.Add(new Vector2(0,0));

		//Left
		UVs.Add(new Vector2(0,1));
		UVs.Add(new Vector2(1,1));
		UVs.Add(new Vector2(1,0));
		UVs.Add(new Vector2(0,0));

		//Right
		UVs.Add(new Vector2(0,1));
		UVs.Add(new Vector2(1,1));
		UVs.Add(new Vector2(1,0));
		UVs.Add(new Vector2(0,0));

		//Top
		UVs.Add(new Vector2(0,1));
		UVs.Add(new Vector2(1,1));
		UVs.Add(new Vector2(1,0));
		UVs.Add(new Vector2(0,0));

		//Bottom
		UVs.Add(new Vector2(0,1));
		UVs.Add(new Vector2(1,1));
		UVs.Add(new Vector2(1,0));
		UVs.Add(new Vector2(0,0));

		mesh.vertices = Verts.ToArray();
		mesh.triangles = Triangles.ToArray();
		mesh.uv = UVs.ToArray();

		Verts.Clear();
		Triangles.Clear();
		UVs.Clear();

		MeshCollider meshCollider = gameObject.GetComponent<MeshCollider> ();
		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
		RecalculateTangents (mesh);
		meshCollider.sharedMesh = null;
		meshCollider.sharedMesh = mesh;
		gameObject.renderer.material = mat;
		mesh.Optimize ();
	
	}

	private static void RecalculateTangents(Mesh mesh)
	{
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uv = mesh.uv;
		Vector3[] normals = mesh.normals;

		int triCount = triangles.Length;
		int vertextCount = vertices.Length;

		Vector3[] tan1 = new Vector3[vertextCount];
		Vector3[] tan2 = new Vector3[vertextCount];

		Vector4[] tangents = new Vector4[vertextCount];

		for (int i = 0; i <triCount; i += 3) 
		{
			int p1 = triangles[i];
			int p2 = triangles[i+1];
			int p3 = triangles[i+2];

			Vector3 v1 = vertices[p1];
			Vector3 v2 = vertices[p2];
			Vector3 v3 = vertices[p3];

			Vector2 w1 = uv[p1];
			Vector2 w2 = uv[p2];
			Vector2 w3 = uv[p3];

			float x1 = v2.x - v1.x;
			float x2 = v3.x - v1.x;
			float y1 = v2.y - v1.y;
			float y2 = v3.y - v1.y;
			float z1 = v2.z - v1.z;
			float z2 = v3.z - v1.z;

			float s1 = w2.x - v1.x;
			float s2 = w3.x - v1.x;
			float t1 = w2.y - v1.y;
			float t2 = w3.y - v1.y;

			float div = s1 * t2 - s2 * t1;
			float r = div == 0.0f ? 0.0f : 1.0f / div;

			Vector3 sdir = new Vector3 ((t2* x1 - t1 * x2)* r,(t2* y1 - t1 * y2)* r, (t2* z1 - t1 * z2) *r);
			Vector3 tdir = new Vector3 ((s1* x2 - s2 * x1)* r,(s1* y2 - s2 * y1)* r, (s1* z2 - s2 * z1) *r);

			tan1[p1] += sdir;
			tan1[p2] += sdir;
			tan1[p3] += sdir;

			tan2[p1] += tdir;
			tan2[p2] += tdir;
			tan2[p3] += tdir;

		}

		for (int i = 0; i < vertextCount; ++i) 
		{
			Vector3 n = normals[i];
			Vector3 t = tan1[i];

			Vector3.OrthoNormalize(ref n, ref t);
			tangents[i].x = t.x;
			tangents[i].y = t.y;
			tangents[i].z = t.z;

			tangents[i].w = (Vector3.Dot(Vector3.Cross(n,t), tan2[i]) < 0.0f) ?-1.0f: 1.0f;
		}

		mesh.tangents = tangents;
	}
}
