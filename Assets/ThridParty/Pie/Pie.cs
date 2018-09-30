using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Pie : MonoBehaviour {
	
	List<Slice> tab;
	int resolution=90;
	
	
	void Start () {
		List<Slice> tab=new List<Slice>();
		Slice[]temp=GetComponents<Slice> ();
		foreach(Slice s in temp){
			tab.Add (s);
		}
		PieGraph graphe = new PieGraph (new Vector3(0, 0, 0), 2, tab, gameObject, resolution);
	}
}
