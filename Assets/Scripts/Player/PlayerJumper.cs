using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBonesDataManager))]
public class PlayerJumper : MonoBehaviour
{

	[SerializeField]
	private Renderer renderer;

	PlayerBonesDataManager bonesDataManager;
	private bool isJumping;
	private PlayerBonesData targetPositions;
	private PlayerBonesData startPositions;
	private PlayerBone[] bones;
	private float currentJumpTime;
	private float jumpDuration = 1;

	public Material PlayerMaterial { set => renderer.material = value; }

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
		Debug.Log(targetPositions.Root.Item1);
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
			transform.position = targetPositions.Root.Item1;
			isJumping = false;
		}
		else
		{
			foreach (PlayerBone bone in bones)
			{
				bone.transform.rotation = Quaternion.Slerp(startPositions.BonesData[bone.boneName], targetPositions.BonesData[bone.boneName], currentJumpTime / jumpDuration);
			}
			transform.position = Vector3.Lerp(startPositions.Root.Item1, targetPositions.Root.Item1, -(currentJumpTime * currentJumpTime) + 2 * currentJumpTime * jumpDuration);
		}
	}
}
