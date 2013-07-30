// ControllerDebugInput.cs
// Author : Fragmads
// Package : Network engine
// Last modification date : 30/07/2013
//
// Debug Input for pc with a controller
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using XInputDotNetPure;

public class ControllerDebugInput : InputCommand {
	
	private float OldLStickX;
	private float OldLStickY;
	
	public float DashSensitivityX = 0.4f;
	public float DashSensitivityY = 0.3f;
	
	// XInput
	bool playerIndexSet = false;
    PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	
	
	
	public void Update(){
		
		// Init the XInput player
		
		// Find a PlayerIndex, for a single player game
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }
		
		// Read debug input
		
		this.state = GamePad.GetState(this.playerIndex);
		
		// RStick 
		
		this.LeftStickX = this.state.ThumbSticks.Left.X;
		this.LeftStickY = this.state.ThumbSticks.Left.Y;
		
		// Check If the player Dash
		if( (this.LeftStickX < this.OldLStickX - this.DashSensitivityX) || (this.LeftStickX > this.OldLStickX + this.DashSensitivityX) || (this.LeftStickY < this.OldLStickY - this.DashSensitivityY) || (this.LeftStickY > this.OldLStickY + this.DashSensitivityY)){
			
			this.LeftStickDash = true;
			
		}
		else {
			
			this.LeftStickDash = false;
			
		}
			
			
			
		// Store the old Stick Value
		this.OldLStickX = this.LeftStickX;
		this.OldLStickY = this.LeftStickY;
		
		
		// Right Stick
		
		//Debug.Log( this.state.Buttons.RightStick.ToString() );
		this.R3 = this.state.Buttons.RightStick.ToString() == "Pressed";
		this.RightStickX = this.state.ThumbSticks.Right.X;
		this.RightStickY = this.state.ThumbSticks.Right.Y;
		
		
		// Button
		
		this.Attack = this.state.Buttons.A.ToString() == "Pressed";;
		this.Special = this.state.Buttons.X.ToString() == "Pressed";
		this.Guard = (this.state.Buttons.Y.ToString() == "Pressed" || this.state.Buttons.RightShoulder.ToString() == "Pressed" || this.state.Buttons.LeftShoulder.ToString() == "Pressed");
		this.Jump = this.state.Buttons.B.ToString() == "Pressed";
		
				
		// Command Input
		
		// Command are only true the first frame you hit the button
		if(this.CommandAttackReleased && this.Attack){
			
			this.CommandAttack = true;
			this.CommandAttackReleased = false;
			
		}
		
		if(this.CommandSpecialReleased && this.Special){
			
			this.CommandSpecial = true;
			this.CommandSpecialReleased = false;
			
		}
		
		if(this.CommandGuardReleased && this.Guard){
			
			this.CommandGuard = true;
			this.CommandGuardReleased = false;
			
		}
		
		if(this.CommandJumpReleased && this.Jump){
			
			this.CommandJump = true;
			this.CommandJumpReleased = false;
			
		}
		
		
		prevState = state;
		
		// TODO register input for replay
		
		
	}
}
