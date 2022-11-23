using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDecomposer : MonoBehaviour {

    [SerializeField] private Camera mainCamera;
	[SerializeField] private int currentLevel;

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
				worldData [row, col] = new Node(row, col, y, x, false);
			}
		}

		foreach(Vector2 coord in generateWalls()) {
			
		}

	}


	public List<Vector2> generateWalls() {
		List<Vector2> blockedCoords;
		if (currentLevel == 1) {
			blockedCoords = new List<Vector2> {
				new Vector2(2, 4),
				new Vector2(2, 5),
				new Vector2(2, 6),
				new Vector2(2, 4),
				new Vector2(2, 5),
				new Vector2(5, 12),
				new Vector2(5, 13),
				new Vector2(5, 14),
				new Vector2(5, 15),
			};
			
		} else {
			blockedCoords = new List<Vector2> {};
		}

		return blockedCoords;
	} 
	

}