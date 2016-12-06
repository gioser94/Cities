using UnityEngine;
using System.Collections;

public class StartParticle : MonoBehaviour {

	
	public MoveToCenterUp panel;

	private ParticleSystem particle;
	private bool canUseParticle = true;


	// Use this for initialization
	void Start () {
		
		particle = this.gameObject.GetComponent<ParticleSystem>();
		particle.loop = true;

	}
	
	// Update is called once per frame
	void Update () {
		if(panel.arrived && !panel.isChanging)
		{
			if(Input.GetMouseButtonDown(0) && canUseParticle)
			{
				particle.Play();
				canUseParticle = false;
			}
		}
		else
		{
			particle.Stop();
			canUseParticle = true;
		}


	}
}
