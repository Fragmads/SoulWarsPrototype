// InputCommand.cs
// Author : Fragmads
// Package : Network engine
// Last modification date : 03/09/2012
//
// InputCommand : A class that describe a set of input command sent by a player
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class InputCommand : MonoBehaviour  {
	
	// Properties
	//
	
	//public Player Player; 
	
	// Input status
	public float RightStickX = 0;
	public float RightStickY = 0;
	
	public bool RightStickDash = false;
	
	public float LeftStickX = 0;
	public float LeftStickY = 0;
	
	public bool L3 = false;
	
	// State of the button
	public bool Attack = false;
	public bool Special = false;
	public bool Guard = false;
	public bool Jump = false;
	
	// Command are true on the first Frame of the button down
	public bool CommandAttack = false;
	private bool CommandAttackReleased = true;
	public bool CommandSpecial = false;
	private bool CommandSpecialReleased = true;
	public bool CommandGuard = false;
	private bool CommandGuardReleased = true;
	public bool CommandJump = false;
	private bool CommandJumpReleased = false;
	
	
	// When true, you will perform a LCancel when you land
	public bool LCancelWindow = false;
	// When true, you will perform a wall tech/ tech if you hit a Wall / ground
	public bool TechWindow = false;
	
	
	public void FixedUpdate(){
		
		// Command are only true the first frame you hit the button
		if(this.CommandAttackReleased && this.Attack){
			
			this.CommandAttack = true;
			this.CommandAttackReleased = false;
			
		}
		else {
			this.CommandAttack = false;
		}
		
		if(this.CommandSpecialReleased && this.Special){
			
			this.CommandSpecial = true;
			this.CommandSpecialReleased = false;
			
		}
		else {
			this.CommandSpecial = false;
		}
		
		if(this.CommandGuardReleased && this.Guard){
			
			this.CommandGuard = true;
			this.CommandGuardReleased = false;
			
		}
		else {
			this.CommandGuard = false;
		}
		
		if(this.CommandJumpReleased && this.Jump){
			
			this.CommandJump = true;
			this.CommandJumpReleased = false;
			
		}
		else {
			this.CommandJump = false;
		}
		
		
		// Check if a button was released
		if(!this.CommandAttackReleased && !this.Attack){
			this.CommandAttackReleased = true;
		}
		
		if(!this.CommandGuardReleased && !this.Guard){
			this.CommandGuardReleased = true;
		}
		
		if(!this.CommandSpecialReleased && !this.Special){
			this.CommandSpecialReleased = true;
		}
		
		if(!this.CommandJumpReleased && !this.Jump){
			this.CommandJumpReleased = true;
		}
		
		// TODO calculate if
		
		// TODO read commands from the controller
		// TODO do it in subclasses for each platforms !
		
	}
	
}
