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

	[SerializeField]
	private float additionalForwardRagdollFroce;

	private PlayerBonesData targetPositions;
	private PlayerBonesData startPositions;
	private PlayerBonesDataManager ragdoll;
	private PlayerBone[] bones;
	private PlayerData data;
	private float currentJumpTime;
	private float jumpDuration = 1;
	private bool isJumping;
	private bool ragdollShown = false;
	private bool canJump = false;

	private void Start()
	{
		bones = GetComponentsInChildren<PlayerBone>();
		startPositions = bonesDataManager.GetBonesData();
		ragdoll = Instantiate(ragdollPrefab, transform.parent).GetComponentInChildren<PlayerBonesDataManager>();
		data = GetComponentInParent<PlayerData>();
		data.Ragdoll = ragdoll.GetComponentInChildren<PlayerRagdoll>();
		data.Ragdoll.Color = Color;
		data.Ragdoll.PlayerParent = transform.parent.gameObject;
		ragdoll.gameObject.SetActive(false);
		WallManager.Instance.EnableRagdollEvent += OnRagdollEnable;
		WallManager.Instance.WallEndedEvent += OnWallEnded;
		WallManager.Instance.WallStartedEvent += OnWallStarted;
		WallManager.Instance.WallSpawned += OnWallSpawned;
	}

	private void OnWallSpawned()
	{
		if (data.Setter != null)
		{
			data.Setter.gameObject.SetActive(true);
		}
	}

	private void OnDestroy()
	{
		WallManager.Instance.EnableRagdollEvent -= OnRagdollEnable;
		WallManager.Instance.WallEndedEvent -= OnWallEnded;
		WallManager.Instance.WallStartedEvent -= OnWallStarted;
		WallManager.Instance.WallSpawned -= OnWallSpawned;
	}

	public void Jump(PlayerBonesData targetPosition)
	{
		if (canJump)
		{
			isJumping = true;
			targetPositions = targetPosition;
			currentJumpTime = 0;
			canJump = false;
			data.Setter.gameObject.SetActive(false);
		}
	}

	private void OnRagdollEnable()
	{
		data.Setter.gameObject.SetActive(false);
		ShowRagdoll();
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
		if (ragdollShown)
		{
			return;
		}
		isJumping = false;
		ragdoll.gameObject.SetActive(true);
		Rigidbody[] ragdollRb = ragdoll.GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody rb in ragdollRb)
		{
			rb.isKinematic = true;
		}
		ragdoll.Apply(bonesDataManager.GetBonesData());
		foreach (Rigidbody rb in ragdollRb)
		{
			rb.isKinematic = false;
			rb.AddForce(Vector3.forward * additionalForwardRagdollFroce);
		}
		data.Ragdoll.PlayerMaterial = renderer.material;
		ragdollShown = true;
		renderer.enabled = false;
	}

	private void OnWallEnded()
	{
		HideRagdoll();
		renderer.enabled = true;
		foreach (PlayerBone bone in bones)
		{
			bone.transform.rotation = startPositions.BonesData[bone.boneName];
		}
		transform.position = startPositions.Root.Item1;
		isJumping = false;
		canJump = false;
	}

	private void OnWallStarted()
	{
		canJump = true;
		renderer.enabled = true;
	}

	private void HideRagdoll()
	{
		ragdoll.gameObject.SetActive(false);
		ragdollShown = false;
	}
}
