using UnityEngine;
using System.Collections;

public class DoorScript3 : MonoBehaviour {
	
	public float trigdistance = 0.0f;
	public AudioClip doorsnd;
	bool isbusy = false;
	bool iskeydown = false;
	bool isopen = false;
	GameObject playerobj;
	
	void Start () {
	
	}
	
	void FixedUpdate () {
		if(playerobj == null)
			playerobj = GameObject.Find("Player(Clone)");
		else
		{
			if(Vector3.Distance(gameObject.transform.position,playerobj.transform.position) <= trigdistance && !isbusy)
			{
				if(Input.GetButtonDown("Use") && !iskeydown)
				{
					if(isopen)
					{
						gameObject.animation.Play("close");
						isopen = false;
						isbusy = true;
					}
					else
					{
						gameObject.animation.Play("open");
						isopen = true;
						isbusy = true;
					}
					audio.PlayOneShot(doorsnd);
					networkView.RPC("OperateDoor",RPCMode.Others);
					iskeydown = true;
				}
				else iskeydown = false;
			}
			else
			{
				if(!gameObject.animation.isPlaying) isbusy = false;
			}
		}
	}
	
	[RPC]
	void OperateDoor()
	{
		if(isopen)
		{
			gameObject.animation.Play("close");
			isopen = false;
			isbusy = true;
		}
		else
		{
			gameObject.animation.Play("open");
			isopen = true;
			isbusy = true;
		}
		audio.PlayOneShot(doorsnd);
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(gameObject.transform.position,trigdistance);
	}
}