using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wall List")]
public class WallList : ScriptableObject
{
    public enum MeshMeta { Normal, Tricross };
    public enum MaterialMeta { Normal, Frosch, MechPosnan, Bullech, Jez, Koziolki };

    [System.Serializable]
    private class WallMesh
    {
        public Mesh mesh;
        public MeshMeta meta;
    }

    [System.Serializable]
    private class WallMaterial
    {
        public Material material;
        public MaterialMeta meta;
    }

    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private List<WallMesh> wallMeshes;
    [SerializeField]
    private List<WallMaterial> wallMaterials;

    public (GameObject wall, MeshMeta meshMeta, MaterialMeta materialMeta) GetRandomWall()
    {
        GameObject wall = Instantiate(wallPrefab);
        WallMesh mesh = wallMeshes.GetRandomElement();
        WallMaterial material = wallMaterials.GetRandomElement();
        wall.GetComponent<MeshFilter>().sharedMesh = mesh.mesh;
        wall.GetComponent<MeshCollider>().sharedMesh = mesh.mesh;
        wall.GetComponent<MeshRenderer>().sharedMaterial = material.material;
        return (wall, mesh.meta, material.meta);
    }
}
