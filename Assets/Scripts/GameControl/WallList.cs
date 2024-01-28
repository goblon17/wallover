using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wall List")]
public class WallList : ScriptableObject
{
    public enum MeshMeta { Normal, Tricross };
    public enum MaterialMeta { Normal, Frosch, MechPosnan, Bullech, Jez, Koziolki, Draakus };

    [System.Serializable]
    private class WallPrefab
    {
        public GameObject prefab;
        public MeshMeta meta;
    }

    [System.Serializable]
    private class WallMaterial
    {
        public Material material;
        public MaterialMeta meta;
    }

    [SerializeField]
    private List<WallPrefab> wallMeshes;
    [SerializeField]
    private List<WallMaterial> wallMaterials;

    public (GameObject wall, MeshMeta meshMeta, MaterialMeta materialMeta) GetRandomWall()
    {
        WallPrefab prefabData = wallMeshes.GetRandomElement();
        GameObject wall = Instantiate(prefabData.prefab);
        WallMaterial material = wallMaterials.GetRandomElement();
        wall.GetComponent<MeshRenderer>().sharedMaterial = material.material;
        return (wall, prefabData.meta, material.meta);
    }
}
