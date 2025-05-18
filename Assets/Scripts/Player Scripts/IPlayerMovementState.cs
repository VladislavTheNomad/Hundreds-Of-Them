using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovementState //STATE PATTERN
{
    void EnterState(PlayerController player);
    void UpdateState(PlayerController player);
    void ExitState(PlayerController player);
}

class IdleState : IPlayerMovementState
{
    public void EnterState(PlayerController player)
    {
        player.animatorOfPlayer.SetFloat("Speed_f", 0.1f);
        player.currentSpeed = player.normalSpeed; // Reset speed when not running
    }

    public void ExitState(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(PlayerController player)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0 || verticalInput != 0)
        {
            player.playerMovementState = new RunState();
            player.playerMovementState.EnterState(player);
        }
    }
}

class RunState : IPlayerMovementState
{
    public void EnterState(PlayerController player)
    {
        player.animatorOfPlayer.SetFloat("Speed_f", 0.6f);
        UpdateState(player);
    }

    public void ExitState(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(PlayerController player)
    {
        player.currentSpeed += player.speedModificator * Time.deltaTime;  // Increase movement speed if the player is running
        if (player.currentSpeed > player.normalSpeed * 2f)
        {
            player.currentSpeed = player.normalSpeed * 2f; // Limit the maximum speed
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        player.transform.Translate(Vector3.right * horizontalInput * player.currentSpeed * Time.deltaTime, Space.World);
        player.transform.Translate(Vector3.forward * verticalInput * player.currentSpeed * Time.deltaTime, Space.World);

        if (horizontalInput == 0 && verticalInput == 0)
        {
            player.playerMovementState = new IdleState(); // Переход в состояние покоя
            player.playerMovementState.EnterState(player);
        }
    }
}

