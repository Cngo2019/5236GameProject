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

    public int getRowNum() {
		return rows;
	}

	public int getColNum() {
		return cols;
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

	/**
	* For this method, we randomly generate walls based on the level. Level 1 does not contain
	any walls at all.
	For level 2 and 3, we generate walls randomly.
	**/
	public List<Vector2> generateWalls() {
		switch(currentLevel) {
			case 1:
			    return randomlyGenerateWalls(.1f);
			case 2:
				return randomlyGenerateWalls(.25f);
			default:
				return randomlyGenerateWalls(.3f);
		}
		
	}

	private List<Vector2> randomlyGenerateWalls(float threshold) {
		List<Vector2> obstacleCoords = new List<Vector2>();
		for(int i = 0; i < rows; i++) {
			for (int j = 0; j < cols; j++) {
				float randomRange = Random.value;
				Debug.Log("random int " + randomRange);

				// 7 and 16 is the player's starting position
				if (randomRange < threshold && i != 7 && j != 16) {
					obstacleCoords.Add(new Vector2(j, i));
				}

			}
		}

		return obstacleCoords;
	}

}