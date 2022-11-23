using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDecomposer : MonoBehaviour {

	[SerializeField] private int currentLevel;

	[SerializeField] private GameObject obstacle;

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
				//Debug.Log("row, col " + row + " " + col + " has been mapped to" + y +" " + x);
				worldData [row, col] = new Node(row, col, y, x, true);
			}
		}

		foreach(Vector2 coord in generateWalls()) {
			worldData[(int) coord.y, (int) coord.x].setIsPathable(false);


			float worldPositionX = worldData[(int) coord.y, (int) coord.x].getWorldX();
			float worldPositionY = worldData[(int) coord.y, (int) coord.x].getWorldZ();
			Vector3 obstaclePosition = new Vector2(worldPositionX, worldPositionY);
			Object.Instantiate(obstacle, obstaclePosition, Quaternion.Euler(0, .5f, 0));

		}

	}


	public List<Vector2> generateWalls() {
		List<Vector2> blockedCoords;
		if (currentLevel == 2) {
			blockedCoords = new List<Vector2> {
				new Vector2(7, 4),
				new Vector2(7, 5),
				new Vector2(7, 6),
				new Vector2(7, 7),
				new Vector2(7, 8),

				new Vector2(7, 1),
				new Vector2(8, 1),
				new Vector2(9, 1),
				new Vector2(10, 1),
				new Vector2(11, 1),

				new Vector2(7, 9),
				new Vector2(8, 9),
				new Vector2(9, 9),
				new Vector2(10, 9),
				new Vector2(11, 9),

				new Vector2(13, 4),
				new Vector2(13, 5),
				new Vector2(13, 6),
				new Vector2(13, 7),
				new Vector2(13, 8),


				
			};
			
		} else {
			blockedCoords = new List<Vector2> {};
		}

		return blockedCoords;
	} 
	

}