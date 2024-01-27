using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBonesDataManager : MonoBehaviour
{
	private PlayerBone[] bones;

	public PlayerBonesData GetBonesData()
	{
		PlayerBonesData data = new PlayerBonesData();
		if (bones == null)
		{
			bones = GetComponentsInChildren<PlayerBone>();
		}
		foreach (PlayerBone bone in bones)
		{
			data.BonesData.Add(bone.boneName, bone.transform.rotation);
		}
		data.Root = (transform.position, transform.rotation);
		return data;
	}

	public void Apply(PlayerBonesData data)
	{
		if (bones == null)
		{
			bones = GetComponentsInChildren<PlayerBone>();
		}
		foreach (PlayerBone bone in bones)
		{
			bone.transform.rotation = data.BonesData[bone.boneName];
		}
		transform.position = data.Root.Item1;
	}
}
