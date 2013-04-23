// Fighter.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 03/09/2012
//
// Fighter : an entity able to fight
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fighter : GravityEntity {
	
	// Properties
	//
	
	public Player Player;
	
	public AFighterState State;
	
	public List<Move> MoveSet;
	
	public int MaxHp;
	
	private int currentHp;
	
	public int CurrentHp{
		get { return this.currentHp; }
		set {
			// If the fighter is still alive
			if(value >0){
				this.currentHp = value;
			}
			else {
				// TODO
				// Trigger death
			}		
		}		
	}
	
	
	
	// Invicibility time gained by the player
	public float InvincibilityTime = 0f;
	
	public bool isFacingLeft;
	public bool isFacingRight;
	
	public float WalkSpeed = 1.0f;
	
	// In unity meter per second
	public float DashInitialSpeed = 5.0f;
	public float DashMaxSpeed = 10.0f;
	// In unity meter per second gain per second (mps²)
	public float DashAcceleration = 2.0f;
	
	// The time it take for a fighter to jump
	public float JumpLag = 5/60;
	
	// Jump strength, used to make a rising momentum
	public float FullJumpStrength = 10;
	public float FullJumpReduction = 10;
	public float ShortJumpStrength = 5;
	public float ShortJumpReduction = 10;
	
	// Horizontal jump factor, how fast you go when you jump and put your RStick Forward/Back
	public float JumpForwardHorizontalFactor = 5;
	public float JumpBackHorizontalFactor = 3;
	
	// Number of double jump of the fighter
	public int DoubleJump = 1;
	
	// Double jump height, reduction, and horizontal factor
	public float DoubleJumpStrength = 5;
	public float DoubleJumpReduction = 10;
	public float DoubleJumpHorizontalFactor = 5;
	
	// A fighter will splash to the ground if he hits it with more momentum than SplashFactor * Weight
	public float SplashFactor = 4f; 
	
	// How much a fighter will slide when standing : Speed is divised by this factor every frame.
	public float GroundFriction = 2f;
	
	// Method
	//
	
	public new void Start(){
		
		// The momentum used tu move the fighter when he is on the ground or in the air on the x axis
		XMomentum gm = this.gameObject.AddComponent<XMomentum>();
		gm.reduction = 0;
		gm.strength = 0;
		
	}	
	
	
	// FixedUpdate is called once per 1/60sec
	public new void FixedUpdate () {
		
		base.FixedUpdate();
		
		// If this fighter has invicibility time left
		if (this.InvincibilityTime > 0){
			
			// Reduce it
			this.InvincibilityTime -= Time.deltaTime;
			
			// Minimal invincible time is 0
			if(this.InvincibilityTime < 0){
				this.InvincibilityTime = 0;
			}
			
		}
		
		// Read the commands from the player
		
		this.State.readCommand(this.Player.InputCommand);		
		
		
		
	}
	
	// Make the Fighter turn around his position
	public void TurnAround() {
		
		this.isFacingLeft = !this.isFacingLeft;
		this.isFacingRight = !this.isFacingRight;
		
		this.gameObject.transform.Rotate(Vector3.up, 180);
		
	}
	
	
	// TODO ?
	// Order the MoveSet so it follow an easy to use pattern
	private void MoveSetOrdering() {
		
	}
	
}
