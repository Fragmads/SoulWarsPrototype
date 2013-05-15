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
	
	public Momentum BaseKnockBack;
	
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
					hb.Damage = this.Damage;
					hb.KnockBack = this.BaseKnockBack;
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
		if(!this.TargetHit.Contains(f)){
			
						
			this.TargetHit.Add(f);
			
				
			// Reduce HP
			f.CurrentHp -= this.Damage;
			
			// TODO Give momentum based on damage % and stuff
			
			// Add a momentum to the fighter
			Momentum hit = f.gameObject.AddComponent<Momentum>();
			
			// Set the angle of the momentum
			if(this.Owner.isFacingLeft){
				hit.angle = 180 - this.BaseKnockBack.angle;
				
			} 
			else{
				hit.angle = this.BaseKnockBack.angle;
			}
			
			// 
			hit.reduction = this.BaseKnockBack.reduction;
								
			
			// KnockBack Strength formula : BaseStrength * (MissingHPPercentage)					
			float missingHp = f.MaxHp - f.CurrentHp;
								
			hit.strength = (missingHp/ f.MaxHp) * this.BaseKnockBack.strength;
			
						
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
			
			// The other fighter is knocked
			Knocked k = f.gameObject.AddComponent<Knocked>();
			f.State = k;
			
			k.PureKnockTime = this.PureKnockTime;
			
			// Knock time formula
			k.KnockTime = (missingHp/ f.MaxHp) * this.BaseKnockTime;
			
			// Stop the other fighters state
			this.StopStates(f);
			
			
			Debug.Log("Attack - ApplyAttack - Momentum Strength "+hit.strength+" knock time"+ k.KnockTime);
			
		}
		
	}
	
	
	private void StopStates(Fighter f){
		
		// If the fighter is lying
		if(f.gameObject.GetComponent<Lying>() != null){
			// Stop it
			GameObject.Destroy(f.gameObject.GetComponent<Lying>());
		}
		// TODO other state
		
		
	}
	
}
