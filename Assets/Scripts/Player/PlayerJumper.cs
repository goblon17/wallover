using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerJumper : PlayerMaterialSetter
{
	[SerializeField]
	private GameObject ragdollPrefab;

	[SerializeField]
	private PlayerBonesDataManager bonesDataManager;

	private bool isJumping;
	private PlayerBonesData targetPositions;
	private PlayerBonesData startPositions;
	private PlayerBonesDataManager ragdoll;
	private PlayerBone[] bones;
	private float currentJumpTime;
	private float jumpDuration = 1;

	private void Start()
	{
		bones = GetComponentsInChildren<PlayerBone>();
		startPositions = bonesDataManager.GetBonesData();
		ragdoll = Instantiate(ragdollPrefab).GetComponentInChildren<PlayerBonesDataManager>();
		ragdoll.gameObject.SetActive(false);
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
			transform.position = targetPositions.Root.Item1;
			isJumping = false;
			ShowRagdoll();
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

	private void ShowRagdoll()
	{
		ragdoll.gameObject.SetActive(true);
		ragdoll.Apply(targetPositions);
		Rigidbody[] ragdollRb = ragdoll.GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody rb in ragdollRb)
		{
			rb.isKinematic = false;
		}
		ragdoll.GetComponent<PlayerMaterialSetter>().PlayerMaterial = renderer.material;
	}
}
