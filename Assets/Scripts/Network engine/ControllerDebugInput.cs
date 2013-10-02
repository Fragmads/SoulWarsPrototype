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
	public bool playerIndexSet = false;
    public PlayerIndex playerIndex;
	public GamePadState state;
	public GamePadState prevState;
	
	// Are the GamePad AlreadyUsed
	public static bool PadOneUsed;
	public static bool PadTwoUsed;
	public static bool PadThreeUsed;
	public static bool PadFourUsed;
	
	
	
	public void Update(){
		
		// Init the XInput player
		
		
		// TODO ensure that the controller is not already used by another fighter
		// Find a PlayerIndex, for a single player game
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
				//Debug.Log("Player Controller Index"+testPlayerIndex.ToString());
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                //if (this.CheckPadAvailable(testState, testPlayerIndex))
                if(this.CheckPadAvailable(testState, testPlayerIndex))
				{
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    this.playerIndexSet = true;
					break;
                }
            }
        }
		
		// Read debug input
		
		if(this.playerIndexSet){
		
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
			
			
			
		}
		
		this.prevState = this.state;
			
		// TODO register input for replay
		
		
	}
	
	public bool CheckPadAvailable(GamePadState GPState, PlayerIndex PIndex){
		
		//If the controller tested is connected
		if(GPState.IsConnected){
			
			// If the player index is available
			if(PIndex == PlayerIndex.One && !ControllerDebugInput.PadOneUsed){
				
				Debug.Log("PadOneUsed Before"+ControllerDebugInput.PadOneUsed.ToString());
				ControllerDebugInput.PadOneUsed = true;
				Debug.Log("PadOneUsed After"+ControllerDebugInput.PadOneUsed.ToString());
				return true;
				
			} else if(PIndex == PlayerIndex.Two && !ControllerDebugInput.PadTwoUsed){
				
				ControllerDebugInput.PadTwoUsed = true;
				return true;
				
			} else if(PIndex == PlayerIndex.Three && !ControllerDebugInput.PadThreeUsed){
				
				ControllerDebugInput.PadThreeUsed = true;
				return true;
				
			} else if(PIndex == PlayerIndex.Four && !ControllerDebugInput.PadFourUsed){
				
				ControllerDebugInput.PadFourUsed = true;
				return true;
				
			} 
			// If not, the return false in the end
			
		}
		else {
				
			// If the controller is not connected, but the port is marked as used
			if(PIndex == PlayerIndex.One && ControllerDebugInput.PadOneUsed){
				ControllerDebugInput.PadOneUsed = false;
			} 
			else if(PIndex == PlayerIndex.Two && ControllerDebugInput.PadTwoUsed){
				ControllerDebugInput.PadTwoUsed = false;
			} 
			else if(PIndex == PlayerIndex.Three && ControllerDebugInput.PadThreeUsed){
				ControllerDebugInput.PadThreeUsed = false;
			} 
			else if(PIndex == PlayerIndex.Four && ControllerDebugInput.PadFourUsed){
				ControllerDebugInput.PadFourUsed = false;
			} 
				
				
		}
		
		return false;
	}
	
}
