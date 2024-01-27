using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerLimbMover : MonoBehaviour
{
    [SerializeField]
    private LimbHandler leftArm;
    [SerializeField]
    private LimbHandler rightArm;
    [SerializeField]
    private LimbHandler leftLeg;
    [SerializeField]
    private LimbHandler rightLeg;

    public void MoveLeftArm(Vector2 dir)
    {
        Vector3 moveOffset = new Vector3(dir.x, dir.y, 0);
        leftArm.MoveLimbByVector(moveOffset);
    }

    public void MoveRightArm(Vector2 dir)
    {
        Vector3 moveOffset = new Vector3(dir.x, dir.y, 0);
        rightArm.MoveLimbByVector(moveOffset);
    }

    public void MoveLeftLeg(Vector2 dir)
    {
        Vector3 moveOffset = new Vector3(dir.x, dir.y, 0);
        leftLeg.MoveLimbByVector(moveOffset);
    }

    public void MoveRightLeg(Vector2 dir)
    {
        Vector3 moveOffset = new Vector3(dir.x, dir.y, 0);
        rightLeg.MoveLimbByVector(moveOffset);
    }

    public void InvertLeftLeg()
    {
        leftLeg.UseSecondSolution = !leftLeg.UseSecondSolution;
    }

    public void InvertRightLeg()
    {
        rightLeg.UseSecondSolution = !rightLeg.UseSecondSolution;
    }

    public void InvertLeftArm()
    {
        leftArm.UseSecondSolution = !leftArm.UseSecondSolution;
    }

    public void InvertRightArm()
    {
        rightArm.UseSecondSolution = !rightArm.UseSecondSolution;
    }
}
