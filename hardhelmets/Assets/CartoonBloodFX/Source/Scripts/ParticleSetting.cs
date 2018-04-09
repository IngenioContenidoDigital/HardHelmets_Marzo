using UnityEngine;
using System.Collections;

namespace HWREfx
{
	public class ParticleSetting : MonoBehaviour
	{

		public float LightIntensityMult = -0.5f;
		public float LifeTime = 1;
		public bool RandomRotation = false;
		public Vector3 PositionOffset;
		public GameObject SpawnEnd;
		public float Scale = 1;
		private float timetemp;

		void Start ()
		{
			timetemp = Time.time;
			if (RandomRotation) {
				this.gameObject.transform.rotation = Random.rotation;
			
			}
	
			SetScale (this.transform);	
			foreach (Transform child in this.transform) {
				SetScale (child);
			}
		}

		void Update ()
		{
			if (Time.time > timetemp + LifeTime) {
				if (SpawnEnd) {
					GameObject obj = (GameObject)GameObject.Instantiate (SpawnEnd, this.transform.position, this.transform.rotation);
				}
				GameObject.Destroy (this.gameObject);
			}
			if (this.gameObject.GetComponent<Light>()) {
				this.GetComponent<Light>().intensity += LightIntensityMult * Time.deltaTime;
			}
		}

		void SetScale (Transform obj)
		{
			obj.localScale *= Scale;
	
			if (obj.gameObject.GetComponent <Light> ()) {
				obj.gameObject.GetComponent <Light> ().range *= Scale;
			}
	
			if (obj.GetComponent <ParticleSystem> ()) {
				ParticleSystem particle = obj.GetComponent<ParticleSystem> ();
				particle.startSpeed *= Scale;
				particle.startSize *= Scale;
				particle.gravityModifier *= Scale;
		
			}
		}
	}
}
