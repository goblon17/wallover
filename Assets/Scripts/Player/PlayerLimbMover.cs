using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimbMover : MonoBehaviour
{
    [SerializeField]
    private Transform leftArm;
    [SerializeField]
    private Transform rightArm;
    [SerializeField]
    private Transform leftLeg;
    [SerializeField]
    private Transform rightLeg;

    [SerializeField]
    private Vector3 leftArmLocalOffset;
    [SerializeField]
    private Vector3 rightArmLocalOffset;
    [SerializeField]
    private Vector3 leftLegLocalOffset;
    [SerializeField]
    private Vector3 rightLegLocalOffset;

    private void Start()
    {
        leftArm.localPosition = leftArmLocalOffset;
        rightArm.localPosition = rightArmLocalOffset;
        leftLeg.localPosition = leftLegLocalOffset;
        rightLeg.localPosition = rightLegLocalOffset;
    }

    public void MoveLeftArm(Vector2 dir)
    {
        leftArm.localPosition = leftArmLocalOffset + new Vector3(dir.x, 0, dir.y);
    }

    public void MoveRightArm(Vector2 dir)
    {
        rightArm.localPosition = rightArmLocalOffset + new Vector3(dir.x, 0, dir.y);
    }

    public void MoveLeftLeg(Vector2 dir)
    {
        leftLeg.localPosition = leftLegLocalOffset + new Vector3(dir.x, 0, dir.y);
    }

    public void MoveRightLeg(Vector2 dir)
    {
        rightLeg.localPosition = rightLegLocalOffset + new Vector3(dir.x, 0, dir.y);
    }
}
