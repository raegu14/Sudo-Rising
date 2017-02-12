using System.Collections;
using System.Collections.Generic;
using System.IO;  
using System.Text;
using UnityEngine;

public class IO : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ReadLevel("test");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// DEPRECATED -- please refer to Main.cs
	void ReadLevel(string fileName) 
	{
		TextAsset levelData = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
		StringReader reader = new StringReader(levelData.text);
		if(reader == null)
		{
			Debug.Log("Can't find or read level data.");
		}
		else
		{
			string line = reader.ReadLine();
			while(line != null)
			{
				string[] coord = line.Split(':');
				if (coord.Length > 0)
				{
					string firstRead = coord[0];
					if(firstRead == "ID") //ID 
					{
						// DO SOMETHING WITH THE ID VALUE
					}
					else if(firstRead == "Difficulty") // Difficulty
					{
						// DO SOMETHING WITH THE DIFFICULTY VALUE
					}
					else // Assumed to be actual coordinates
					{
						SetTile(firstRead[0], int.Parse(firstRead[1].ToString()), int.Parse(coord[1]), "permanent");
					}
				}
				line = reader.ReadLine();
			}
		}
	}
	
	// Set the tile at Column, Row with a specific property
	// i.e. setTile('D', 4, "permanent"); or something like that
	void SetTile(char column, int row, int val, string property) 
	{
		Debug.Log(column);
		Debug.Log(row);
		Debug.Log(val);
	}
}
