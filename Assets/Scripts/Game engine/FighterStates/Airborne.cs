// Airborne.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Airborne : A state describing a fighter in the air
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Airborne : AFighterState {
	
	// Properties
	//
	
	
	public int JumpLeft = 1;
	
	public bool FastFalling = false;
	
	private float LastX;
	private float LastY;
	
	// Method
	//
	
	public new void Start(){
		
		base.Start();
		
		// Assign the number of double jump left to the fighter
		this.JumpLeft = this.fighter.DoubleJump;
		
		// Play the airborne animation
		this.gameObject.animation.Play("airborne");		
		
		// Be sure that you are no longer edge grabbing
		if(this.gameObject.GetComponent<LedgeGrabbing>() != null){
			
			GameObject.Destroy(this.gameObject.GetComponent<LedgeGrabbing>());
			
		}
		
		// Be sure that you are no longer on the ground
		if(this.gameObject.GetComponent<OnGround>() != null){
			
			GameObject.Destroy(this.gameObject.GetComponent<OnGround>());
			
		}
		
		this.LastX = this.gameObject.transform.position.x;
		this.LastY = this.gameObject.transform.position.y;
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Airborne";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO air dodge
		
		//Debug.Log("Airborne readCommand fighter : "+this.fighter.gameObject.name);
		
		// Double jumping
		if(input.CommandJump && this.JumpLeft > 0 && this.gameObject.GetComponent<Jumping>() == null ){
			this.JumpLeft --;
			
			Momentum m;
			
			// If there is already a jump momentum, overwrite it, else create one
			if(this.gameObject.GetComponent<JumpMomentum>() == null){
				
				m = this.gameObject.AddComponent<JumpMomentum>();
				
			}
			else {
				m = this.gameObject.GetComponent<JumpMomentum>();
			}
			
			
			m.strength = this.fighter.DoubleJumpStrength;
			m.reduction = this.fighter.DoubleJumpReduction;
			m.angle = 90;	
		
			
			XMomentum xMom = this.gameObject.GetComponent<XMomentum>();
			xMom.strength = this.fighter.DoubleJumpHorizontalFactor * input.LeftStickX;
					
		}
		
		// Air Control
		this.AirControl(input);
		
		
		// Aerial attack
		// Special Move
		if(input.CommandSpecial){
			
			// Define the orientation of this special move
			Move.Orientation orientation;
			
			if(input.LeftStickY > 0.8){
				orientation = Move.Orientation.Up;
			}
			else if(input.LeftStickY < -0.8){
				orientation = Move.Orientation.Down;
			}
			else if(input.LeftStickX > 0.8 || input.LeftStickX < -0.8 ){
				orientation = Move.Orientation.Forward;
			}
			else{
				orientation = Move.Orientation.Neutral;
			}
			
			
			// Find the right move in the moveset
			foreach(AerialAttack ga in this.fighter.AerialMoveSet){
				
				if(ga.isSpecial && ga.orientation == orientation){
					
					// Start the special
					Attacking attacking = this.gameObject.AddComponent<Attacking>();
					this.fighter.State = attacking;
					attacking.Attack = ga;
					Object.Destroy(this);
					
				}
				
			}			
			
		}
		// Attack move
		else if(input.CommandAttack){
			
			// Define the orientation of this attack
			Move.Orientation orientation;
			
			if(input.LeftStickY > 0.8){
				orientation = Move.Orientation.Up;
			}
			else if(input.LeftStickY < -0.8){
				orientation = Move.Orientation.Down;
			}
			else if((this.fighter.isFacingRight && input.LeftStickX > 0.8) || (this.fighter.isFacingLeft && input.LeftStickX < -0.8) ){
				orientation = Move.Orientation.Forward;
			}
			else if((this.fighter.isFacingLeft && input.LeftStickX > 0.8) || (this.fighter.isFacingRight && input.LeftStickX < -0.8)){
				orientation = Move.Orientation.Back;
			}
			else{
				orientation = Move.Orientation.Neutral;
			}
			
			// Find the right move in the moveset
  			foreach(Move aa in this.fighter.AerialMoveSet){
				
				if(aa is AerialAttack && !aa.isSpecial && aa.orientation == orientation){
					
					// Start the attack
					Attacking attacking = this.gameObject.AddComponent<Attacking>();
					this.fighter.State = attacking;
					attacking.Attack = aa;
					
					break;
					
				}
				
			}
			
		}
		
		
	}	
	
	void FixedUpdate() {
		
		// Check if the fighter as land on a platform		
		if(this.gameObject.GetComponent<OnGround>() == null){
			this.CheckForPlatforms();
		}
		
				
		// Latest
		this.LastX = this.gameObject.transform.position.x;
		this.LastY = this.gameObject.transform.position.y;
		
	}// End FixedUpdate
	
	
	
	// Check if the fighter land on a platform
	private void CheckForPlatforms(){
		
		// For every platform in this stage
		foreach(Platform p in Platform.StagePlatforms){
			
			// Check if this fighter has landed
			if (p.CheckIfLanded(this.fighter, this.LastX, this.LastY)){
				Debug.Log("Airborne - fighter landing to a platform");
				// Set the fighter height to the platform's
				this.fighter.gameObject.transform.position = new Vector3( this.fighter.gameObject.transform.position.x, p.gameObject.transform.position.y , 0);
				
				// If you are not knocked while you hit the ground
				if(this.gameObject.GetComponent<Knocked>() == null){
					
					// Stop the fighter from falling
					//this.fighter.SpeedY = 0;
					
					Landing landing = this.gameObject.AddComponent<Landing>();
					this.fighter.State = landing;
					this.OnGround(p);
											
				}
				
				// If you are knocked when you hit the ground and do a tech
				else if(this.gameObject.GetComponent<Knocked>().isInTechWindow){
					
					// Stop the fighter from falling
					//this.fighter.SpeedY = 0;
					
					// You are no longer in the air, and not knocked anymore
					Teching teching = this.gameObject.AddComponent<Teching>();
					this.fighter.State = teching;
					
					Object.Destroy(this.gameObject.GetComponent<Knocked>());
					this.OnGround(p);
					
					
				}
				// If you are knocked when you hit the ground and do not tech
				else {
					
					float downStrength = 0;
					// For all the momentum of this fighter
					foreach (Momentum m in this.gameObject.GetComponents<Momentum>()){
						
						downStrength += m.vector.y;
						
					}
					
					// If the fighter is splashed to the ground
					if( downStrength < -(this.fighter.Weight * this.fighter.SplashFactor) ){
						
						// TODO deal damage and make the fighter rebounce
						Debug.Log("Splash");
						
					}
					// The fighter is lying on the floor
					else {
						
						// Stop the fighter from falling
						
						Lying lying = this.gameObject.AddComponent<Lying>();
						this.fighter.State = lying;
						this.OnGround(p);
						Debug.Log("Airborne - Lying on the floor");
												
					}						
					
				}
				
				// You can only land on one platform
				break;
				
			}
			
			
			// Ledge grab
			// If the fighte can ledge grab
			if(this.fighter.gameObject.GetComponent<JumpMomentum>() == null){
			
				float fighterEdgeY;
				float fighterEdgeX;
				
				if(p.LeftEdge != null){
					fighterEdgeY = (this.gameObject.transform.position.y + this.fighter.MinEdgeGrabHeight ) - p.LeftEdge.gameObject.transform.position.y;
					fighterEdgeX = this.gameObject.transform.position.x - p.LeftEdge.gameObject.transform.position.x;
					
					//If you can reach the left edge
					if( ((fighterEdgeY < 0) && (fighterEdgeY > -(this.fighter.MaxEdgeGrabHeight - this.fighter.MinEdgeGrabHeight))) && ((fighterEdgeX < 0) && (fighterEdgeX > -Edge.grabDistanceX)) ){
						
												
						Attacking attacking = this.gameObject.GetComponent<Attacking>();
						// If the fighter is not attacking
						if( attacking == null){
							
							if( this.fighter.isFacingRight ){
								
								this.GragEdge(p.LeftEdge);
							} 
							
						}
						// If he can grab the ledge
						else if (attacking.Attack.BlindAutoEdgeGrab || (attacking.Attack.AutoEdgeGrab && this.fighter.isFacingRight)){
							
							this.GragEdge(p.LeftEdge);
							
						}
						
						// You can only grab one ledge at once
						break;
						
					}
									
				}
				
				if (p.RightEdge != null){
					fighterEdgeY = (this.gameObject.transform.position.y + this.fighter.MinEdgeGrabHeight ) - p.LeftEdge.gameObject.transform.position.y;
					fighterEdgeX = this.gameObject.transform.position.x - p.RightEdge.gameObject.transform.position.x;
					
					if( ((fighterEdgeY < 0) && (fighterEdgeY > -(this.fighter.MaxEdgeGrabHeight - this.fighter.MinEdgeGrabHeight) )) && ((fighterEdgeX > 0) && (fighterEdgeX < Edge.grabDistanceX))){
						
						Attacking attacking = this.gameObject.GetComponent<Attacking>();
						// If the fighter is not attacking
						if( attacking == null){
							
							if( this.fighter.isFacingLeft ){
								
								this.GragEdge(p.RightEdge);
							} 
							
						}
						// If he can grab the ledge
						else if (attacking.Attack.BlindAutoEdgeGrab || (attacking.Attack.AutoEdgeGrab && this.fighter.isFacingLeft)){
							
							this.GragEdge(p.RightEdge);
							
						}
						
						// You can only grab one ledge at once
						break;
						
					}
					
				}
			}
			
		}
		
	}
	
	// Grab an edge
	private void GragEdge(Edge e){
		
		// If no one is already grabbing the ledge
		if(e.grabber == null){
		
			Debug.Log("Airborne - GrabEdge");
			
			// In case of blind auto edge grab
			if(this.fighter.isFacingLeft != e.isRight){
				
				this.fighter.TurnAround();
				
			}
			
			// Grab the edge, it will give the fighter invicibility
			e.EdgeGrabbed(this.fighter);
			
			// Stop the fighter movement
			Momentum.Clean(this.fighter);
			
			this.gameObject.GetComponent<XMomentum>().strength = 0;
			
			this.fighter.SpeedX = 0;
			this.fighter.SpeedY = 0;
			
			
			// State goes to ledge grabbing
			LedgeGrabbing ledgeGrabbing = this.gameObject.AddComponent<LedgeGrabbing>();
			ledgeGrabbing.edge = e;
			this.fighter.State = ledgeGrabbing;
			GameObject.Destroy(this);
			
		}		
		
	}
	
	private void OnGround(Platform p){
		
		Debug.Log("Airborne : On the ground");
		Debug.Log("SpeedY "+this.fighter.SpeedY);
		
		// You are no longer in the air
		OnGround onGround = this.gameObject.AddComponent<OnGround>();
		onGround.platform = p;
		GameObject.Destroy(this.gameObject.GetComponent<Airborne>());
		
		// Reset Gravity
		GameObject.Destroy(this.gameObject.GetComponent<GravityMomentum>());
		
	}
	
	
	public void AirControl(InputCommand input){
		
		
		// Air control
		
		if(this.fighter.isFacingLeft){
			
			XMomentum xMom = this.gameObject.GetComponent<XMomentum>();
			
			// Acceleration toward the X speed
			xMom.strength += (this.fighter.AirControlHorizontalSpeed/60) * input.LeftStickX;
			
			// Cap the X speed
			if(xMom.strength > this.fighter.JumpBackHorizontalFactor){
				xMom.strength = this.fighter.JumpBackHorizontalFactor;
			}
			
			if(xMom.strength < -this.fighter.JumpForwardHorizontalFactor){
				xMom.strength = -this.fighter.JumpForwardHorizontalFactor;
			}
			
		} else {
			
			XMomentum xMom = this.gameObject.GetComponent<XMomentum>();
			
			// Acceleration toward the X speed
			xMom.strength += (this.fighter.AirControlHorizontalSpeed/60) * input.LeftStickX;
			
			// Cap the X speed
			if(xMom.strength < -this.fighter.JumpBackHorizontalFactor){
				xMom.strength = -this.fighter.JumpBackHorizontalFactor;
			}
			
			if(xMom.strength > this.fighter.JumpForwardHorizontalFactor){
				xMom.strength = this.fighter.JumpForwardHorizontalFactor;
			}
		}
		
		// Fast Fall
		// If you are not jumping, not fast falling and your stick is down, Start Fast Fall
		if(this.gameObject.GetComponent<JumpMomentum>() == null && !this.FastFalling && input.LeftStickY < - 0.8){						
			this.FastFalling = true;
			
			// Double the gravity effect
			GravityMomentum gm = this.gameObject.GetComponent<GravityMomentum>();
			gm.maxStrength = gm.maxStrength * 2;
			
		}
		// If you are fast falling and your stick is not down, End Fast Fall
		else if(this.gameObject.GetComponent<JumpMomentum>() == null && (this.FastFalling && input.LeftStickY >= -0.8)){			
			this.FastFalling = false;
			
			// Gravity effect divised by 2
			GravityMomentum gm = this.gameObject.GetComponent<GravityMomentum>();
			gm.maxStrength = gm.maxStrength / 2;
			
			
		}
		
	}
	
	
}
