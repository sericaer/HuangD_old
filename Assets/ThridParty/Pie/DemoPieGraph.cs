using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemoPieGraph : MonoBehaviour
{
	void Start ()
    {
		List<Slice> tab=new List<Slice>();
		tab.Add (new Slice ("enemy killed by sword", 30f, new Color (0.3f, 1, 0.5f,1)));
		tab.Add (new Slice ("enemy killed with magic",30f,new Color(1,0.3f,0.5f,1)));
		tab.Add (new Slice ("enemy killed by arrows", 30f, new Color (0.3f, 0.5f, 1,0)));
		PieGraph graphe = new PieGraph (new Vector3 (0,0,0), 2, tab,gameObject,180);
	}
}
