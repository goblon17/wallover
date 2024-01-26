using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerLimbMover))]
public class PlayerInputAdapter : MonoBehaviour
{
    [SerializeField]
    private PlayerLimbMover limbMover;

    public event System.Action NoLimbSelectedEvent;

    private bool useLeftArm = false;
    private bool useRightArm = false;
    private bool useLeftLeg = false;
    private bool useRightLeg = false;

    public void MoveLeftLimb(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (useLeftArm)
        {
            limbMover.MoveLeftArm(input);
        }
        if (useLeftLeg)
        {
            limbMover.MoveLeftLeg(input);
        }
    }

    public void MoveRightLimb(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (useRightArm)
        {
            limbMover.MoveRightArm(input);
        }
        if (useRightLeg)
        {
            limbMover.MoveRightLeg(input);
        }
    }

    public void LeftLeg(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        useLeftLeg = value > 0.5f;
        CheckIfNoLimbIsSelected();
    }

    public void RightLeg(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        useRightLeg = value > 0.5f;
        CheckIfNoLimbIsSelected();
    }

    public void LeftArm(InputAction.CallbackContext context)
    {
        useLeftArm = !context.canceled;
        CheckIfNoLimbIsSelected();
    }

    public void RightArm(InputAction.CallbackContext context)
    {
        useRightArm = !context.canceled;
        CheckIfNoLimbIsSelected();
    }

    private void CheckIfNoLimbIsSelected()
    {
        if (!useRightArm &&
            !useLeftArm &&
            !useRightLeg &&
            !useLeftLeg)
        {
            NoLimbSelectedEvent?.Invoke();
        }
    }
}
