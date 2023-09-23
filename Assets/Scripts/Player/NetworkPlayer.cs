using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(NetworkCharacterControllerPrototype))]
public class NetworkPlayer : NetworkBehaviour
{
    PlayerInput input;
    Vector2 directionInput = Vector2.zero;
    NetworkCharacterControllerPrototype characterControllerPrototype;

    private void OnEnable()
    {
        input.Enable();
    }

    private void Awake()
    {
        input = new PlayerInput();
    }

    public override void Spawned()
    {
        characterControllerPrototype = GetComponent<NetworkCharacterControllerPrototype>();

        gameObject.name = $"Player {Object.InputAuthority.PlayerId}";
    }

    private void Update()
    {
        if (!HasStateAuthority) return;

        directionInput = input.Player.Movement.ReadValue<Vector2>();
    }

    public override void FixedUpdateNetwork()
    {
        characterControllerPrototype.Move(new Vector3(directionInput.x, 0, directionInput.y));
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
