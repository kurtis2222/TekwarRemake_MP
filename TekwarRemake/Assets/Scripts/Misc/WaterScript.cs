using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {
	
	public AudioClip sound;
	
	void Start () {
		
	}
	
	Ray ray;
	RaycastHit hit = new RaycastHit();
	bool isinwater = false;
	
	void FixedUpdate () {
		ray = new Ray(new Vector3(transform.position.x,transform.position.y-3,transform.position.z),
			-transform.up);
		Physics.SphereCast(ray,0.25f,out hit,2);
		if(hit.collider)
		{
			if(hit.collider.gameObject.tag == "Water")
			{
				if(!isinwater)
				{
				audio.PlayOneShot(sound);
				networkView.RPC("SendWaterSND",RPCMode.Others,networkView.viewID);
				isinwater = true;
				}
			}
			else isinwater = false;
		}
	}
	
	[RPC]
	void SendWaterSND(NetworkViewID netid)
	{
		NetworkView enemyID = NetworkView.Find(netid);
   		GameObject enemy = enemyID.observed.gameObject;
		enemy.audio.PlayOneShot(sound);
	}
}
