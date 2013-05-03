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
	
}
