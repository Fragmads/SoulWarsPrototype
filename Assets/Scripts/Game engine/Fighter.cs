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
	
	public List<GroundAttack> GroundMoveSet;
	public List<AerialAttack> AerialMoveSet;
	
	// Fighter's hitboxes and hurtboxes
	public List<HitBox> Hitboxes;
	public List<HurtBox> HurtBoxes;
	
	
	public int MaxHp = 100;
	
	private int currentHp;
	
	public int CurrentHp{
		get { return this.currentHp; }
		set {
			// If the fighter is still alive
			if(value >0){
				this.currentHp = value;
			}
			else {
				
				Debug.Log("Fighter - Mort");
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
	
	// How much you can influence your X speed while in the air in one second
	public float AirControlHorizontalSpeed = 3;
	
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
	
	
	// Guard
	public float GuardInitLife = 100f;
	public float GuardLife;

	
	
	// Method
	//
	
	public new void Start(){
		
		// The momentum used tu move the fighter when he is on the ground or in the air on the x axis
		XMomentum gm = this.gameObject.AddComponent<XMomentum>();
		gm.reduction = 0;
		gm.strength = 0;
		
		// Init guard
		this.GuardLife = this.GuardInitLife;
		
		// Init HP
		this.CurrentHp = this.MaxHp;
		
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
		
		if(this.Player != null){
			// Read the commands from the player		
			this.State.readCommand(this.Player.InputCommand);		
		}
		
	}
	
	// Make the Fighter turn around his position
	public void TurnAround() {
		
		this.isFacingLeft = !this.isFacingLeft;
		this.isFacingRight = !this.isFacingRight;
		
		this.gameObject.transform.Rotate(Vector3.up, 180);
		
	}
	
	
	// Check What attack you are doing
	// return true if you perform an attack, else return false
	public bool CheckInputForStandingAttack(InputCommand i){
		
		// If it's a special attack
		if(i.CommandSpecial){
			
			// Up
			if(i.LeftStickY > 0.8){
				
				
				return true;
			}
			// Down
			else if(i.LeftStickY < -0.8){
				
				
				return true;
			}
			// Forward
			else if(i.LeftStickX > 0.8 || i.LeftStickX < -0.8){
				
				
				return true;
			}
			// Neutral
			else {
				
				
				return true;
			}
			
		}
		// If the button has been released and is now pressed
		else if(i.CommandAttack){
			
			
			Attacking attacking = this.gameObject.GetComponent<Attacking>();
			
			// Add a level of attack
			if(attacking != null && attacking.AttackLevel < 3){
				attacking.AttackLevel ++;
				
				// Cap at level 3 attack
				if(attacking.AttackLevel >= 3){
					attacking.AttackLevel = 3;
				}
				
			}
		}
			
		// If the attack button is pressed, you attack
		if(i.Attack){
		
			// Up
			if(i.LeftStickY > 0.8){
				
				foreach(GroundAttack ga in this.GroundMoveSet){
					
					if(ga.orientation == Move.Orientation.Up){
						
					}
					
				}
				
				
			}
			// Down
			else if(i.LeftStickY < -0.8){
				
			}
			// Forward
			else if(i.LeftStickX > 0.8 || i.LeftStickX < -0.8){
				
			}
			// Neutral
			else {
				
			}

			
			
		}
		
		return false;
		
	}
	
}
