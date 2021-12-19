using UnityEngine;
using System.Collections;

public class civil_nav : MonoBehaviour {
	
	const string folder_panic = "civ_panic";
	const string folder_die = "civ_die";
	Object[] tmp;
	AudioClip[] snd_panic;
	AudioClip[] snd_die;
	public Transform nav_obj;
	public Transform detector;
	
	bool ispanicked = false;
	Ray ray;
	RaycastHit hit = new RaycastHit();
	
	bool rnd(int percent)
        {
            if (Random.Range(0,101) <= percent) return true;
            else return false;
        }
	
	void Start () {
		tmp = Resources.LoadAll(folder_panic,typeof(AudioClip));
		snd_panic = new AudioClip[tmp.Length];
		for(int i = 0; i < tmp.Length; i++)
			snd_panic[i] = (AudioClip)tmp[i];
		tmp = null;
		tmp = Resources.LoadAll(folder_die,typeof(AudioClip));
		snd_die = new AudioClip[tmp.Length];
		for(int i = 0; i < tmp.Length; i++)
			snd_die[i] = (AudioClip)tmp[i];
		GetComponent<NavMeshAgent>().destination = nav_obj.position;
	}
	
	void FixedUpdate()
	{
		if(Network.isServer)
		{
			if(Vector3.Distance(transform.position,nav_obj.position) < 2.5f)
			{
				if(GetComponent<NavMeshAgent>().speed == 2)
				{
					if(rnd(35))
					{
						networkView.RPC("AI_Idle",RPCMode.All);
					}
					else if(rnd(20))
					{
						networkView.RPC("AI_Run",RPCMode.All);
					}
				}
				else
				{
					networkView.RPC("AI_Walk",RPCMode.All);
				}
				networkView.RPC("AI_UpdateTarget",RPCMode.All);
			}
		}
		if(!ispanicked)
		{
			ray = new Ray(detector.transform.position,detector.transform.forward);
			Physics.SphereCast(ray,1.0f,out hit,40.0f);
			if(hit.collider)
			{
				if(hit.collider.gameObject.name.Contains("Player") && hit.collider.networkView.isMine)
				{
					if(hit.collider.gameObject.GetComponentInChildren<WeaponScript>().currweap > 0)
					{
						networkView.RPC("AI_Panic",RPCMode.All);
					}
				}
			}
		}
	}
	
	[RPC]
	void AI_Idle()
	{
		GetComponent<NavMeshAgent>().speed = 0;
		GetComponentInChildren<Animation>().Play("civ_idle");
		if(Network.isServer)
			StartCoroutine(Move());
	}
	
	[RPC]
	void AI_Run()
	{
		GetComponent<NavMeshAgent>().speed = 8;
		GetComponentInChildren<Animation>().Play("civ_run");
	}
	
	[RPC]
	void AI_Walk()
	{
		GetComponent<NavMeshAgent>().speed = 2;
		GetComponentInChildren<Animation>().Play("civ_walk");
	}
	
	[RPC]
	void AI_IdleEnd()
	{
		GetComponent<NavMeshAgent>().speed = 2;
		GetComponentInChildren<Animation>().Play("civ_walk");	
	}
	
	[RPC]
	void AI_Die()
	{
		enabled = false;
		GetComponent<NavMeshAgent>().enabled = false;
		collider.enabled = false;
		GetComponentInChildren<Animation>().Play("civ_die");
		audio.PlayOneShot(snd_die[Random.Range(0,snd_die.Length)]);
		networkView.stateSynchronization = NetworkStateSynchronization.Off;
	}
	
	[RPC]
	void AI_UpdateTarget()
	{
		nav_obj = nav_obj.GetComponent<nav_path>().target;
		GetComponent<NavMeshAgent>().destination = nav_obj.position;
	}
	
	[RPC]
	void AI_Panic()
	{
		GetComponent<NavMeshAgent>().speed = 0;
		GetComponentInChildren<Animation>().Play("civ_hndsup");
		audio.PlayOneShot(snd_panic[Random.Range(0,snd_panic.Length)]);
		ispanicked = true;
		StartCoroutine(ResetPanic());
	}
	
	[RPC]
	void AI_LookAtPlayer(Vector3 nplayer)
	{
		transform.LookAt(nplayer);
	}
	
	public void LookTrigger(GameObject trg)
	{
		networkView.RPC("AI_LookAtPlayer",RPCMode.All,trg.transform.position);
	}
	
	public void DeathTrigger()
	{
		networkView.RPC("AI_Die",RPCMode.All);
	}
	
	public void DeathClient()
	{
		enabled = false;
		GetComponent<NavMeshAgent>().enabled = false;
		collider.enabled = false;
		GetComponentInChildren<Animation>().Play("civ_die");
		networkView.stateSynchronization = NetworkStateSynchronization.Off;
	}
	
	IEnumerator Move()
	{
		yield return new WaitForSeconds(Random.Range(2,5));
		if(collider.enabled && !ispanicked)
			networkView.RPC("AI_IdleEnd",RPCMode.All);
	}
	
	IEnumerator ResetPanic()
	{
		yield return new WaitForSeconds(10.0f);
		ispanicked = false;
		if(collider.enabled)
			networkView.RPC("AI_IdleEnd",RPCMode.All);
	}
}
