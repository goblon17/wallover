using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonesData
{
    public enum PlayerBones
    {
        LeftLeg,
        LeftForeleg,
        RightLeg,
        RightForeleg,
		Leftarm,
		LeftForearm,
		Rightarm,
		RightForearm,
        Pelvis,
        Spine
	}

    public Dictionary<PlayerBones, Quaternion> BonesData = new Dictionary<PlayerBones, Quaternion>();
    public (Vector3, Quaternion) Root;
}
