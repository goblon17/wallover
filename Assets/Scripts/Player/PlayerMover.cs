using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	[SerializeField]
	private PlayerInputAdapter playerInputAdapter;

    [SerializeField]
    private Transform leftHandHandler;

	[SerializeField]
	private Transform rightHandHandler;

	[SerializeField]
	private Transform leftFootHandler;

	[SerializeField]
	private Transform rightFootHandler;

	[SerializeField]
	private Transform core;

	private void Start()
	{
		
	}

	public void MoveBody()
	{
		Vector3[] limbsPositions = { leftFootHandler.position, rightFootHandler.position, leftHandHandler.position, rightHandHandler.position };
		Vector3 corePosition = Vector3.zero;
		foreach (var position in limbsPositions)
		{
			corePosition += position;
		}
		core.position = new Vector3(corePosition.x/4, corePosition.y/4, corePosition.z/4);
		leftFootHandler.position = limbsPositions[0];
		rightFootHandler.position = limbsPositions[1];
		leftHandHandler.position = limbsPositions[2];
		rightHandHandler.position = limbsPositions[3];
	}
}
