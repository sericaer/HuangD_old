using UnityEngine;
using System.Collections;


public class Slice  {

	public string name;
	public float percentage;
	public Color color;

	public Slice(){
	}
	public Slice(string n, float p, Color c){
		name = n;
		percentage = p;
		color = c;
	}

	public string getName(){return name;}
	public void setName(string a){name=a;}
	public float getPercentage(){return percentage;}
	public void setPercentage(float a){percentage+=a;}
	public Color getColor(){return color;}
	public void setColor(Color a){color=a;}
}
