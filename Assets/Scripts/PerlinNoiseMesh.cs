using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
public class PerlinNoiseMesh : MonoBehaviour
{    
    #region Properties
	public float NoiseScale = 0.3f;
	public float NoiseIntensity = 1f;
	#endregion

	#region Fields
	#endregion

	#region Unity Callbacks
	// Start is called before the first frame update
	void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;

		for (int i = 0; i < vertices.Length; i++)
		{
			float noiseValue = Mathf.PerlinNoise(vertices[i].x * NoiseScale, vertices[i].y * NoiseScale) * NoiseIntensity;
			vertices[i] += mesh.normals[i] * noiseValue;
		}

		mesh.vertices = vertices;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();

	}

    // Update is called once per frame
    void Update()
    {
        
    }
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
   
}
