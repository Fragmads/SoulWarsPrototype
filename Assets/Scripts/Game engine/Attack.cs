// Attack.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 24/04/2013
//
// Move : An Attack inside a fighter move, there can be several attack per move
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack : MonoBehaviour {
	
	// Properties
	//
	
	public int Damage = 10;

	// Beginning and end of this attack in frame
	public int StartFrame;
	public int EndFrame;
	
	public KnockBack BaseKnockBack;
	
	public Fighter Owner;
	
	// List of the hiboxes this attack will use
	public List<string> HitboxName;
	private List<HitBox> HitBox = new List<HitBox>();
	
	
	
	// List of target that this attack has hit
	public List<AEntity> TargetHit;
	
	// Knock Time
	public float BaseKnockTime = 3f;
	public float PureKnockTime = 0;
	
	// Method
	//
	
	// Used at the frame the attack began
	public void StartAttack () {
		
		this.HitBox.Clear();
		
		// Enable and init the hitbox to the right bones
		foreach(string s in this.HitboxName){
			
			// In every hitbox of the fighter
			foreach(HitBox hb in this.Owner.GetComponentsInChildren<HitBox>()){
				
				// Find the right one
				if(hb.BoneName == s){
					
					// Add the hitbox to the list
					this.HitBox.Add(hb);
					
					//Debug.Log("Attack - StartAttack : add hitbox to "+hb.BoneName);
					hb.enabled = true;
					hb.attack = this;
					hb.Owner = this.Owner;
					
				}			
				
			}
			
			
		}
		
	}
	
	public void EndAttack(){
					
		// For every HitBox 
		foreach(HitBox hb in this.HitBox){
						
			// Disable the hitbox
			hb.enabled = false;
		}
		
	}
	
	public void ApplyAttack(AEntity e){
		
		
		
		
	}
	
	public void ApplyAttack(Fighter f){
		
		// Hit each target once per attack max
		if(!this.TargetHit.Contains(f) && f.InvincibilityTime <=0 ){
			
			this.TargetHit.Add(f);
			
			// If the opponent is guarding
			if(f.gameObject.GetComponent<Guarding>() != null){
				
				Debug.Log("Attack - Hit Guard");
				
				Guarding guard = f.gameObject.GetComponent<Guarding>();
				
				guard.GuardHit(this);
				
			}	
			// else, the opponent take the hit
			else {
				
				
					
				// Reduce HP
				f.CurrentHp -= this.Damage;
				
				// TODO Give momentum based on damage % and stuff
				
				// Add a momentum to the fighter
				KnockBack kb = f.gameObject.AddComponent<KnockBack>();
				
				// Set the angle of the momentum
				if(this.Owner.isFacingLeft){
					kb.angle = 180 - this.BaseKnockBack.angle;
					
				} 
				else{
					kb.angle = this.BaseKnockBack.angle;
				}
				
				// 
				//kb.reduction = this.BaseKnockBack.reduction;
									
				
				// KnockBack Strength formula : BaseStrength * (MissingHPPercentage)					
				float missingHp = f.MaxHp - f.CurrentHp;
									
				kb.strength = (missingHp/ f.MaxHp) * this.BaseKnockBack.strength;
				
				
				// Opponent is knocked
				
				// If the opponent is not airborne already
				if(f.gameObject.GetComponent<Airborne>() == null){
					
					// If he is on the ground
					if(f.gameObject.GetComponent<OnGround>() != null){
											
						OnGround og = f.gameObject.GetComponent<OnGround>();
						// He goes airborne
						og.GoAirborne();
						
					}
					// If he is ledge grabbing
					else if(f.gameObject.GetComponent<LedgeGrabbing>() != null){
						
						LedgeGrabbing lg = f.gameObject.GetComponent<LedgeGrabbing>();
						// He goes airborne
						lg.GoAirborne();
						
					}			
				}
				
				// Stop the other fighters state
				this.StopStates(f);
				
				// The other fighter goes airborne
				f.gameObject.AddComponent<Airborne>();
				
				// The other fighter is knocked
				Knocked k = f.gameObject.AddComponent<Knocked>();
				f.State = k;
				
				k.PureKnockTime = this.PureKnockTime;
				
				// Knock time formula
				k.KnockTime = (missingHp/ f.MaxHp) * this.BaseKnockTime;
				kb.length = k.KnockTime;
				
				
				
				// TODO Crouch Cancelling
				
				
				Debug.Log("Attack - ApplyAttack - Momentum Strength "+kb.strength+" knock time"+ k.KnockTime);
			}
		}
		
	}
	
	
	private void StopStates(Fighter f){
		
		// If the fighter is lying
		if(f.gameObject.GetComponent<Lying>() != null){
			// Stop it
			GameObject.Destroy(f.gameObject.GetComponent<Lying>());
		}
		
		if(f.gameObject.GetComponent<Attacking>() != null){
			
			// Stop the attack
			f.gameObject.GetComponent<Attacking>().StopAttacking();
				
			// Stop the fighter from attacking
			GameObject.Destroy(f.gameObject.GetComponent<Attacking>());
			
		}
		
		// Clean all other state
		foreach(AFighterState state in f.gameObject.GetComponents<AFighterState>()){
			
			GameObject.Destroy(state);
			
		}
		
		// TODO other state
	}
	
}
