using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimations : MonoBehaviour
{
    [SerializeField]
    private ActorMovement movementComponent;
    [SerializeField]
    private WeaponHandler weaponHandleComponent;
    [SerializeField]
    private Animator animationController;

    private const string IS_MOVING_PARAMETER = "IsMoving";
    private const string IS_SHOOTING_PARAMETER = "IsShooting";

    private void Awake()
    {
        movementComponent = GetComponent<ActorMovement>();
        weaponHandleComponent = GetComponent<WeaponHandler>();
    }

    private void Start()
    {
        movementComponent.OnMovement += OnActorMovement;
        movementComponent.OnStandingStill += OnActorStandingStill;

        weaponHandleComponent.OnShootWeapon += OnActorShootingWeapon;
    }

    private void OnDestroy()
    {
        movementComponent.OnMovement -= OnActorMovement;
        movementComponent.OnStandingStill -= OnActorStandingStill;

        weaponHandleComponent.OnShootWeapon -= OnActorShootingWeapon;
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
        animationController.SetTrigger(IS_SHOOTING_PARAMETER);
    }
}
