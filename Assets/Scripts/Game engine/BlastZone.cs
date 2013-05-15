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
			
			Momentum m = f.gameObject.AddComponent<Momentum>();
			
			m.angle = this.Blast.angle;
			m.reduction = this.Blast.reduction;
			m.strength = this.Blast.strength;
			
			// 
			f.CurrentHp -= (2/3)*f.MaxHp;
			
			Debug.Log("BlastZone - ");
			
		}
		// Else destroy what touch the blastZone
		else if(other.gameObject.GetComponent<BlastZone>() == null){
			
			
			//GameObject.Destroy(other.gameObject);
			
		}
		
	}
	
}
