using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonesData
{
    public enum PlayerBones
    {
        LeftLeg,
        LeftForleg,
        RightLeg,
        RightForleg,
		LeftArm,
		LeftForArm,
		RightArm,
		RightForArm,
        Pelvis,
        Spine
	}

    public Dictionary<PlayerBones, (Vector3, Quaternion)> BonesData = new Dictionary<PlayerBones, (Vector3, Quaternion)>();
    public (Vector3, Quaternion) Root;
}
