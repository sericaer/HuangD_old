using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PieGraph
{

	float angleStep=0;
	float curAngle=0;
	int nbTriangleAssigned;
	List<GameObject>tri=new List<GameObject>();

	public PieGraph(Vector3 vecCenter, int radii, List<Slice>tab, GameObject go, int res)
    {
		for (int i=0; i<res; i++)
        {
			createTriangle(vecCenter, radii, go, res);
		}
		for (int i=0; i<tab.Count; i++)
        {
			assignTriangles(tab[i],go,res);
		}

		createText(tab,go);
	}
	void assignTriangles(Slice s,GameObject go,int res){
		int nbTriangle = (int)Mathf.Round((s.getPercentage()*res)/100);
		if (nbTriangleAssigned + nbTriangle > res)
			nbTriangle = res - nbTriangleAssigned;
		for (int i=0; i<nbTriangle; i++) {
			tri[nbTriangleAssigned+i].GetComponent<MeshRenderer>().GetComponent<Renderer>().material.color = s.getColor();

		}
		nbTriangleAssigned += nbTriangle;
	}
	void createTriangle(Vector3 vecCenter, int radii, GameObject go, int resolution)
    {
		GameObject piePart = new GameObject ();
		piePart.transform.parent = go.GetComponent<Transform> ();
		piePart.transform.position = go.GetComponent<Transform> ().position;

		piePart.AddComponent<MeshFilter>();
		piePart.AddComponent<MeshRenderer>();
		MeshFilter mf = piePart.GetComponent<MeshFilter> ();
		Renderer renderer = piePart.GetComponent<MeshRenderer>().GetComponent<Renderer>();
		
		Mesh mesh = new Mesh ();
		mf.mesh = mesh;
		
		//Vertices
		Vector3 [] vertices = new Vector3[3];
		
		vertices [0] = vecCenter;//centre
		angleStep = 360 / resolution;
		
		for(int i=1;i<=2;i++){//NB POINTS
			vertices[i]=new Vector3 (radii * Mathf.Cos(curAngle*(Mathf.PI * 2) / 360), radii * Mathf.Sin(curAngle*(Mathf.PI * 2) / 360), 0);
			curAngle+=angleStep;
		}
		curAngle -= angleStep;
		
		//Triangles
		int [] triangles = new int[3];
		triangles[0]=0;
		triangles[1]=2;
		triangles[2]=1;

		//Normals
		Vector3[] normals = new Vector3[3];
		
		for(int i=0;i<=2;i++){
			normals[i] = -Vector3.forward;
		}

		//Assign arrays
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;

		tri.Add (piePart);
	}
	void createText(List<Slice>tab,GameObject go){
		// Handle the texts:
		float i = 0;
		foreach (Slice s in tab) {
			GameObject g = new GameObject ();
			g.transform.parent = go.transform;
			Canvas canvas = g.AddComponent<Canvas> ();
			canvas.renderMode = RenderMode.WorldSpace;
			CanvasScaler cs = g.AddComponent<CanvasScaler> ();
			cs.scaleFactor = 10.0f;
			cs.dynamicPixelsPerUnit = 10f;
			g.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, 20.0f);
			g.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 5.0f);
			GameObject g2 = new GameObject ();
			g2.name = "Text";
			g2.transform.parent = g.transform;
			Text t = g2.AddComponent<Text> ();
			g2.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, 20f);
			g2.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 5.0f);
			t.alignment = TextAnchor.MiddleCenter;
			Font ArialFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
			t.font = ArialFont;
			t.fontSize = 2;
			t.text = s.getName () + " " + s.getPercentage () + "%";
			t.enabled = true;
			Color col = new Color ();
			col = s.getColor ();
			col.a = 1;
			t.color = col;
			
			g.name = "text of " + s.getName ();
			bool bWorldPosition = false;
			
			g.GetComponent<RectTransform> ().SetParent (go.transform, bWorldPosition);
			g.transform.localPosition = new Vector3 (2f, 1f - i, 0f);
			g.transform.localScale = new Vector3 (
			                                     1.0f / go.transform.localScale.x * 0.1f,
			                                     1.0f / go.transform.localScale.y * 0.1f, 
			                                     1.0f / go.transform.localScale.z * 0.1f);
			i += 0.5f;
		}
	}
}
