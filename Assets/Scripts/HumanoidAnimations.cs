using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimations : MonoBehaviour
{
    [SerializeField]
    private Movement movementComponent;
    [SerializeField]
    private WeaponHandler weaponHandleComponent;
    [SerializeField]
    private Animator animationController;

    private const string IS_MOVING_PARAMETER = "IsMoving";
    private const string IS_SHOOTING_PARAMETER = "IsShootingBool";

    private void Awake()
    {
        movementComponent = GetComponent<Movement>();
        weaponHandleComponent = GetComponent<WeaponHandler>();
    }

    private void Start()
    {
        movementComponent.OnMovement += OnActorMovement;
        movementComponent.OnStandingStill += OnActorStandingStill;

        weaponHandleComponent.OnPullTrigger += OnActorShootingWeapon;
        weaponHandleComponent.OnPullTrigger += OnActorStopShootingWeapon;
    }

    private void OnDestroy()
    {
        movementComponent.OnMovement -= OnActorMovement;
        movementComponent.OnStandingStill -= OnActorStandingStill;

        weaponHandleComponent.OnPullTrigger -= OnActorShootingWeapon;
        weaponHandleComponent.OnPullTrigger -= OnActorStopShootingWeapon;
    }

    private void OnActorMovement()
    {
        if (!animationController.GetBool(IS_MOVING_PARAMETER))
        {
            animationController.SetBool(IS_MOVING_PARAMETER, true);
        }
    }

    private void OnActorStandingStill()
    {
        if (animationController.GetBool(IS_MOVING_PARAMETER))
        {
            animationController.SetBool(IS_MOVING_PARAMETER, false);
        }
    }

    private void OnActorShootingWeapon()
    {
        animationController.SetBool(IS_SHOOTING_PARAMETER, true);
    }

    private void OnActorStopShootingWeapon()
    {
        animationController.SetBool(IS_SHOOTING_PARAMETER, false);
    }
}
