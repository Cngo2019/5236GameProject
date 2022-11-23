using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDecomposer : MonoBehaviour {

    [SerializeField] private Camera mainCamera; // The camera (used for getting the target to go to)

	private Node [,] worldData;
	private int nodeSize;

	private int terrainWidth;
	private int terrainLength;

	private int rows;
	private int cols;

	public Node[,] getWorldData() {
		return worldData;
	}
	private void Start () {

		terrainWidth = 12;
		terrainLength = 22;

		nodeSize = 1;

		rows = terrainWidth / nodeSize;
		cols = terrainLength / nodeSize;

		worldData = new Node [rows, cols];

		DecomposeWorld();
	}

    void Update() {
       
    }

	public void DecomposeWorld () {

		float startX = -11f;
		float startY = -6f;

		float nodeCenterOffset = nodeSize / 2f; //is 1

		for (int row = 0; row < rows; row++) {

			for (int col = 0; col < cols; col++) {
				
				float x = startX + nodeCenterOffset + (nodeSize * col);
				float y = startY + nodeCenterOffset + (nodeSize * row);
				Debug.Log("row, col " + row + " " + col + " has been mapped to" + y +" " + x);


				Vector3 startPos = new Vector3 (x, y, 0f);

				

				// Does our raycast hit anything at this point in the map

				RaycastHit hit;

				// Does the ray intersect any objects excluding the player layer
				if (Physics.Raycast (startPos, Vector3.up, out hit, Mathf.Infinity)) {
					print ("Hit something at row: " + row + " col: " + col + " \n with position " + x + " " + y);
					//Debug.DrawRay (startPos, Vector3.down * 20, Color.red, 50000);
					worldData [row, col] = new Node(row, col, y, x, false);
					
				} else {
					//Debug.DrawRay (startPos, Vector3.down * 20, Color.green, 50000);
					worldData [row, col] = new Node(row, col, y, x, true);
				}

				//Debug.Log("Grid Position" + (row, col));
				//Debug.Log("World Position" + (x, z));
			}
		}

	}
	

}