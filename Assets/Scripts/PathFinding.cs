using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PathFinding
{


    public static List<Node> generatePath(Node[,] grid, int startRow, int startCol, int goalRow, int goalCol) {
    
        // start row is Z
        // start col is X
        Debug.Log(startRow + " "  + startCol);
        Node current = grid[startRow, startCol];
        if (!grid[goalRow, goalCol].getIsPathable()) {
            Debug.Log("The position is not pathable");
            return new List<Node>();
        }
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(current);
        current.setStepCost(0);
        current.setHeuristicCost(getHeuristicCost(startRow, startCol, goalRow, goalCol));
        current.setFCost(current.getHeuristicCost() + current.getStepCost());
        current.setParent(null);

        
        while (true) {

            if (openList.Count <= 0) {
                // Indicates that there is no path in this point
                Debug.Log("The position is not pathable");
                return new List<Node>();
            }
            current = openList[getLowestIndex(openList)];
            openList.RemoveAt(getLowestIndex(openList));
            closedList.Add(current);


            int row = current.getRow();
            int col = current.getCol();

            if (row == goalRow && col == goalCol) {
                return generatePath(current);
            }
            
            List<Node> neighbors = getNeighbors(row, col, grid, openList, closedList);
            
            for (int i = 0; i < neighbors.Count; i++) {
                Node neighbor = neighbors[i];
                if (openList.Contains(neighbor)) {
                    //compute current cost
                    float openListStepCost = neighbor.getStepCost();
                    float currentStepCost = current.getStepCost() + 10;
                    if (currentStepCost < openListStepCost) {
                        neighbor.setStepCost(currentStepCost);
                        neighbor.setParent(current);
                    }
                } else {
                    neighbor.setStepCost(current.getStepCost() + 10);
                    neighbor.setHeuristicCost(getHeuristicCost(neighbor.getRow(), neighbor.getCol(), goalRow, goalCol));
                    neighbor.setFCost(neighbor.getStepCost() + neighbor.getHeuristicCost());
                    neighbor.setParent(current);
                    openList.Add(neighbor);

                }
            }
        }
    }



    private static List<Node> getNeighbors(int row, int col, Node[,] grid, List<Node> openList, HashSet<Node> closedList) {

        List<Node> neighbors = new List<Node>();

        // Checking down
        if (row - 1 > -1) {
            Node neighbor = grid[row - 1, col];
            if (!closedList.Contains(neighbor) && neighbor.getIsPathable()) {
                neighbors.Add(neighbor);
            }
        }

        // Checking up
        if (row + 1 < 12) {
            Node neighbor = grid[row + 1, col];
            if (!closedList.Contains(neighbor) && neighbor.getIsPathable()) {
                neighbors.Add(neighbor);
            } 
        }
        
        // Checking left
        if (col - 1 > -1) {
            Node neighbor = grid[row, col - 1];
            if (!closedList.Contains(neighbor) && neighbor.getIsPathable()) {
                neighbors.Add(neighbor);
            }
        }

        // Checking right
        if (col + 1 < 22) {
            Node neighbor = grid[row, col + 1];
            if (!closedList.Contains(neighbor) && neighbor.getIsPathable()) {
                neighbors.Add(neighbor);
            }
        }
        
       
        return neighbors;
    }

    private static int getLowestIndex(List<Node> openList) {
        int min = 0;
        
        for (int i = 0; i < openList.Count; i++) {
            if (openList[min].getFCost() > openList[i].getFCost()) {
                min = i;
            }
        }
        return min;
    }

    private static float getHeuristicCost(int x, int z, int xg, int zg) {
        return Mathf.Abs(x - xg) + Mathf.Abs(z - zg);
    }

    private static List<Node> generatePath(Node goalNode) {
        Node current = goalNode;
        List<Node> path = new List<Node>();
        while(current != null) {
            if (!current.getIsPathable()) {
                Debug.Log("ERROR!");
            }
            path.Add(current);
            current = current.getParent();

        }
        path.Reverse();
        return path;
    }
}