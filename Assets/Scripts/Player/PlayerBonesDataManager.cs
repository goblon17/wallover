using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonesDataManager : MonoBehaviour
{
    public PlayerBonesData GetBonesData()
    {
        PlayerBonesData data = new PlayerBonesData();
        PlayerBone[] bones = GetComponentsInChildren<PlayerBone>();
        foreach (PlayerBone bone in bones)
        {
            data.BonesData.Add(bone.boneName, bone.transform.rotation);
        }
        data.Root = (transform.position, transform.rotation);
        return data;
    }
}
