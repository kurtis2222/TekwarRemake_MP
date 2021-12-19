using UnityEngine;
using System.Collections;

public class NetAnim : MonoBehaviour {

	GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	public void PlayAnimation(NetworkViewID netid, string anim)
	{
		networkView.RPC("NetPlayAnim",RPCMode.Others,netid,anim);
		gameObject.animation.Play(anim);
	}
	
	[RPC]
	void NetPlayAnim(NetworkViewID netid, string anim)
	{
		NetworkView enemyID = NetworkView.Find(netid);
   		GameObject enemy = enemyID.observed.gameObject;
		enemy.animation.Play(anim);
	}
}
