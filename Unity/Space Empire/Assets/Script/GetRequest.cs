using UnityEngine;
using System.Collections;

public class GetRequest : MonoBehaviour {
	
	void Start () {
		string url = "http://server-space-empire.azure-mobile.net/tables/Empire";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
		DontDestroyOnLoad (this);
	}
	
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
}
