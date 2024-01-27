using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wall List")]
public class WallList : ScriptableObject
{
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private List<Mesh> wallMeshes;
    [SerializeField]
    private List<Material> wallMaterials;

    public GameObject GetRandomWall()
    {
        GameObject wall = Instantiate(wallPrefab);
        Mesh mesh = wallMeshes.GetRandomElement();
        Material material = wallMaterials.GetRandomElement();
        wall.GetComponent<MeshFilter>().sharedMesh = mesh;
        wall.GetComponent<MeshCollider>().sharedMesh = mesh;
        wall.GetComponent<MeshRenderer>().sharedMaterial = material;
        return wall;
    }
}
