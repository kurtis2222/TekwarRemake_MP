using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour {
	
	public AudioClip snd_explosion;
	
	void Start () {
		rigidbody.velocity = transform.TransformDirection(new Vector3 (0, 0, 30));
		rigidbody.angularVelocity = transform.TransformDirection(new Vector3(25, 0, 0));
		StartCoroutine(Explosion());
	}
	
	IEnumerator Explosion()
	{
		yield return new WaitForSeconds(3.0f);
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		audio.PlayOneShot(snd_explosion);
		light.enabled = true;
		StartCoroutine(LightOff());
		StartCoroutine(Clean());
		Collider[] other = Physics.OverlapSphere(transform.position,10.0f);
		for(int i = 0; i < other.Length; i++)
		{
			if(other[i].name.Contains("Player") && other[i].networkView.isMine && networkView.isMine)
				other[i].GetComponent<MainScript>().hp = 0;
			else if(other[i].name.Contains("Player") && !other[i].networkView.isMine)
				other[i].collider.GetComponent<NetHit>().SendHit(100, 20, 35,
					other[i].collider.networkView.viewID,gameObject.networkView.owner,false);
			else if(other[i].name == "cop")
				other[i].GetComponent<cop_nav>().DeathTrigger();
			else if(other[i].name == "civilian")
				other[i].GetComponent<civil_nav>().DeathTrigger();
			else if(other[i].name == "ebarrel")
				other[i].GetComponent<BarrelScript>().Explode();
			else if(other[i].name == "glass")
				other[i].GetComponent<GlassScript>().Break();
		}
	}
	
	IEnumerator LightOff()
	{
		yield return new WaitForSeconds(0.5f);
		light.enabled = false;
	}
	
	IEnumerator Clean()
	{
		if(networkView.isMine)
		{
		yield return new WaitForSeconds(2.5f);
		Network.RemoveRPCs(gameObject.networkView.viewID);
		Network.Destroy(gameObject);
		Destroy(gameObject);
		}
	}
}
