using UnityEngine;
using System.Collections;

public class BlastZone : MonoBehaviour {
	
	public Momentum Blast;
	
	// Use this for initialization
	void Start () {
	
	}
	
	
	public void OnTriggerEnter(Collider other){
		
		// If a fighter touch a blastZone
		if(other.gameObject.GetComponent<HurtBox>() != null){
			
			// 
			Fighter f = other.gameObject.GetComponent<HurtBox>().Owner;
			
			// Override all existing Momentum
			Momentum.Clean(f);
			
			// Reset all the double jump
			// TODO remove useless Stance
			if(f.gameObject.GetComponent<Airborne>() != null){
				Debug.Log("BlastZone - Reset Double Jump");
				f.gameObject.GetComponent<Airborne>().JumpLeft = f.DoubleJump;	
			}
			
			Momentum m = f.gameObject.AddComponent<Momentum>();
			
			m.angle = this.Blast.angle;
			m.reduction = this.Blast.reduction;
			m.strength = this.Blast.strength;
			
			// Deal 2/3 of life in damage, yeap that hurts good
			f.CurrentHp -= (2/3)*f.MaxHp;
			
			
		}
		// Else destroy what touch the blastZone
		else if(other.gameObject.GetComponent<BlastZone>() == null){
			
			
			//GameObject.Destroy(other.gameObject);
			
		}
		
	}
	
}
