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
	public List<GameObject> Hitbox;
	
	// List of target that this attack has hit
	public List<AEntity> TargetHit;
	
	// Method
	//
	
	// Used at the frame the attack began
	public void StartAttack () {
		
		// Add an hitbox to the right bones
		foreach(GameObject go in this.Hitbox){
			
			HitBox hb = go.AddComponent<HitBox>();
			hb.attack = this;
			hb.Damage = this.Damage;
			hb.KnockBack = this.BaseKnockBack;
			hb.Owner = this.Owner;
			
		}
		
	}
	
	public void EndAttack(){
		
		// Remove the hitboxes from the bones
		foreach(GameObject go in this.Hitbox){
			
			// For every HitBox found in the game object
			foreach(HitBox hb in go.GetComponents<HitBox>()){
				
				// If the hitboxes is linked to this attack
				if(hb.attack.Equals(this)){
					// Remove the hitbox component of the bones
					Object.Destroy(hb);
					break;
				}
				
			}
			
		}
		
	}
	
}
