using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAdapter : MonoBehaviour
{
    [SerializeField]
    private PlayerLimbMover limbMover;
    [SerializeField]
    private PlayerBodyMover mover;

    [SerializeField]
    private GameManager gameManager;

    public event System.Action NoLimbSelectedEvent;

    private bool useLeftArm = false;
    private bool useRightArm = false;
    private bool useLeftLeg = false;
    private bool useRightLeg = false;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void MoveLeftLimb(InputAction.CallbackContext context)
    {
        if (gameManager.CurrentState != GameManager.State.Game)
        {
            return;
        }

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
        if (gameManager.CurrentState != GameManager.State.Game)
        {
            return;
        }

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
        if (gameManager.CurrentState != GameManager.State.Game)
        {
            return;
        }

        float value = context.ReadValue<float>();
        useLeftLeg = value > 0.5f;
        CheckIfNoLimbIsSelected();
    }

    public void RightLeg(InputAction.CallbackContext context)
    {
        if (gameManager.CurrentState != GameManager.State.Game)
        {
            return;
        }

        float value = context.ReadValue<float>();
        useRightLeg = value > 0.5f;
        CheckIfNoLimbIsSelected();
    }

    public void LeftArm(InputAction.CallbackContext context)
    {
        if (gameManager.CurrentState != GameManager.State.Game)
        {
            return;
        }

        useLeftArm = !context.canceled;
        CheckIfNoLimbIsSelected();
    }

    public void RightArm(InputAction.CallbackContext context)
    {
        if (gameManager.CurrentState != GameManager.State.Game)
        {
            return;
        }

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

    public void InvertLeftLimb(InputAction.CallbackContext context)
    {
        if (!context.performed || gameManager.CurrentState != GameManager.State.Game)
        {
            return;
        }

        if (useLeftArm)
        {
            limbMover.InvertLeftArm();
        }
        if (useLeftLeg)
        {
            limbMover.InvertLeftLeg();
        }
    }

    public void InvertRightLimb(InputAction.CallbackContext context)
    {
        if (!context.performed || gameManager.CurrentState != GameManager.State.Game)
        {
            return;
        }

        if (useRightArm)
        {
            limbMover.InvertRightArm();
        }
        if (useRightLeg)
        {
            limbMover.InvertRightLeg();
        }
    }

    public void MoveBody(InputAction.CallbackContext context)
    {
        if (!context.performed || gameManager.CurrentState != GameManager.State.Game)
        {
            return;
        }

        mover.MoveBody();
    }
}
