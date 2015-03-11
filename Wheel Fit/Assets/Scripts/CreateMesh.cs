using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class CreateMesh : MonoBehaviour
{
	public Vector3 MeshPosition;
	public int MeshXSize;
	public int MeshZSize;
	public float SquareSize;
	
	void Start ()
	{
		MeshBuilder();
		PositionMesh();
	}	

	void MeshBuilder()
	{
		int NumberOfTiles = MeshZSize * MeshXSize;
		int NumberOfTris = NumberOfTiles * 2;
		int NumberOfVerticies = MeshZSize * MeshXSize;
		
		// Generate the mesh data
		Vector3[] VerticesV3 = new Vector3[ NumberOfVerticies ];
		Vector3[] NormalsV3 = new Vector3[NumberOfVerticies];
		Vector2[] UvV2 = new Vector2[NumberOfVerticies];
		
		int[] Triangles = new int[ NumberOfTris * 3 ];
		
		//Creating the vertices 
		for (int z = 0; z < MeshZSize; z++) {
			for (int x = 0; x < MeshXSize; x++) {
				VerticesV3 [z * MeshXSize + x] = new Vector3 (x * SquareSize, 0, -z * SquareSize); 
				NormalsV3 [z * MeshXSize + x] = Vector3.up;
				UvV2 [z * MeshXSize + x] = new Vector2 ((float)x / MeshZSize, 1f - (float)z / MeshXSize);
			}
		}			
		//Creating the triangles for each tile
		for (int z = 0; z < MeshZSize -1; z++) {
			for (int x = 0; x < MeshXSize -1; x++) {
				int SquareIndex = z * MeshXSize + x;
				int TriOffSet = SquareIndex * 6;
				Triangles [TriOffSet + 0] = z * MeshXSize + x + 0;
				Triangles [TriOffSet + 2] = z * MeshXSize + x + MeshXSize + 0;
				Triangles [TriOffSet + 1] = z * MeshXSize + x + MeshXSize + 1;
				
				Triangles [TriOffSet + 3] = z * MeshXSize + x + 0;
				Triangles [TriOffSet + 5] = z * MeshXSize + x + MeshXSize + 1;
				Triangles [TriOffSet + 4] = z * MeshXSize + x + 1;
			}
		}
		//Create a new Mesh and populate with the data
		Mesh NewMesh = new Mesh ();
		NewMesh.vertices = VerticesV3;
		NewMesh.triangles = Triangles;
		NewMesh.normals = NormalsV3;
		NewMesh.uv = UvV2;
		
		//Assign the filter/renderer/collider to the mesh
		MeshFilter meshFilter = GetComponent<MeshFilter> ();
		MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
		MeshCollider meshCollider = GetComponent<MeshCollider> ();
		
		meshFilter.mesh = NewMesh;
		meshCollider.sharedMesh = NewMesh;
		Debug.Log ("Mesh Created, Size X :" + MeshXSize + " | Y : " + MeshZSize + ", Squaresize : " + SquareSize+ ", At Position : " + MeshPosition);
	}

	void PositionMesh()
	{
		Vector3 EndPosition;
		//places the mesh at 0 on the Z axis, and centers it on the Y axis
		EndPosition.x = MeshPosition.x + -MeshXSize /2 + 0.5f; // -0.5f if using a even size Z axis
		EndPosition.y = MeshPosition.y;
		EndPosition.z = MeshPosition.z + +MeshZSize - 1.0f; // + 1.0f, because.. i donno its just + 1 on the x axis without this
		this.transform.position = EndPosition;
	}
}
