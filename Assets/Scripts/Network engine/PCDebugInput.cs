// PCDebugInput.cs
// Author : Fragmads
// Package : Network engine
// Last modification date : 03/09/2012
//
// Debug INput for pc
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PCDebugInput : InputCommand {
	
	public float RStickCoef = 1.0f;
	
	public void Update(){
		
		//base.FixedUpdate();
		
		// Read debug input
		
		// RStick = zqsd (azerty keyboard)
		if(Input.GetKey(KeyCode.Q)){
			
			this.RightStickX = -this.RStickCoef;
			
		}
		else if(Input.GetKey(KeyCode.D)){
			
			this.RightStickX = this.RStickCoef;
		}
		else {			
			this.RightStickX = 0;			
		}
			
		if(Input.GetKey(KeyCode.Z)){
			this.RightStickY = this.RStickCoef;
		}
		else if(Input.GetKey(KeyCode.S)){
			this.RightStickY = -this.RStickCoef;
		}
		else {			
			this.RightStickY = 0;			
		}
		
		// Attack : H, Special : G, Guard : Y, Jump : J
		
		this.Attack = Input.GetKey(KeyCode.H);
		this.Special = Input.GetKey(KeyCode.G);
		this.Guard = Input.GetKey(KeyCode.Y);
		this.Jump = Input.GetKey(KeyCode.J);
		
		
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
		
		
		// TODO register input for replay
		
		
	}
	
	
}

