using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBonesDataManager))]
public class PlayerJumper : MonoBehaviour
{
	[SerializeField]
	private float jumpDuration;

	PlayerBonesDataManager bonesDataManager;
	private bool isJumping;
	private PlayerBonesData targetPositions;
	private PlayerBonesData startPositions;
	private PlayerBone[] bones;
	private float currentJumpTime;

	private void Start()
	{
		bones = GetComponentsInChildren<PlayerBone>();
		bonesDataManager = GetComponent<PlayerBonesDataManager>();
		startPositions = bonesDataManager.GetBonesData();
	}

	public void Jump(PlayerBonesData targetPosition)
	{
		isJumping = true;
		targetPositions = targetPosition;
		currentJumpTime = 0;
		//Turn off animation
	}

	private void Update()
	{
		if (!isJumping)
		{
			return;
		}
		currentJumpTime += Time.deltaTime;
		if (currentJumpTime >= jumpDuration)
		{
			foreach (PlayerBone bone in bones)
			{
				bone.transform.rotation = targetPositions.BonesData[bone.boneName];
			}
			isJumping = false;
		}
		else
		{
			foreach (PlayerBone bone in bones)
			{
				bone.transform.rotation = Quaternion.Slerp(startPositions.BonesData[bone.boneName], targetPositions.BonesData[bone.boneName], currentJumpTime / jumpDuration);
			}
		}
	}
}
