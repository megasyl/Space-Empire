using UnityEngine;
using System.Collections;

public class GameGUIFlotte : MonoBehaviour {

	public GameObject PlanetSliderFlotte;
	public GameObject[] Pages;
	public int tabId;
	//float val;
	//float val2;
	//public GameObject test2;
	public GameObject button_up;
	public GameObject button_down;
	public int i;

	private bool up_nowst;
	private bool up_lastst;
	private bool down_nowst;
	private bool down_lastst;

	// Use this for initialization
	void Start () {
		//Pages = new GameObject[1];
		i = 0;
		down_lastst = button_down.GetComponent<InstantGuiButton> ().pressed;
		down_nowst = false;
		up_lastst = button_up.GetComponent<InstantGuiButton> ().pressed;
		up_nowst = false;
		//scroll = GameObject.Find("VS_flotte_1");
		//scroll2 = GameObject.Find("VS_flotte_2");
	}

	bool isInRange(int i, int min, int max) {
		if (i >= min && i <= max)
			return true;
		return false;
	}

	void switchField(int tabId, int i) {
		foreach (var item in Pages) {
			item.SetActive(false);
		}
		Pages[i].SetActive(true);
		GameObject.Find("TabGroup").GetComponent<InstantGuiTabs>().fields[tabId] = Pages[i].GetComponent<InstantGuiElement>();
	}

	void Update () {
		down_nowst = button_down.GetComponent<InstantGuiButton> ().pressed;
		up_nowst = button_up.GetComponent<InstantGuiButton> ().pressed;
		if (this.GetComponentInParent<InstantGuiElement> ().check) {
			if (down_nowst == true && down_lastst == false) {
				if (isInRange (i - 1, 0, Pages.Length - 1))
				{
					i--;
					switchField(tabId, i);
				}
				Debug.Log (i + " Bas : " + down_lastst + "-" + down_nowst);
				down_lastst = true;
			}
			if (up_nowst == true && up_lastst == false) {
				if (isInRange (i + 1, 0, Pages.Length - 1))
				{
					i++;
					switchField(tabId, i);
				}
				Debug.Log (i + " Haut : " + up_lastst + "-" + up_nowst);
				up_lastst = true;
			}
		}
		down_lastst = down_nowst;
		up_lastst = up_nowst;
	}
}
