public class Node {
    private float worldX;
    private float worldZ;


    private int row;
    private int col;

    private bool isPathable;

    private float stepCost;
    private float heuristicCost;
    private float fCost;

    private Node parent;

    public Node(int row, int col, float worldZ, float worldX, bool isPathable) {
        this.worldX = worldX;
        this.worldZ = worldZ;
        this.isPathable = isPathable;
        this.row = row;
        this.col = col;
    }

    public float getWorldX() {
        return worldX;
    }

    public float getWorldZ() {
        return worldZ;
    }

    public void setRow(int x) {
        row = x;
    }

    public void setCol(int x) {
        col = x;
    }

    public int getRow() {
        return row;
    }
    
    public int getCol() {
        return col;
    }

    public Node getParent() {
        return parent;
    }

    public void setParent(Node x) {
        parent = x;
    }
    public void setStepCost(float x) {
        stepCost = x;
    }

    public void setHeuristicCost(float x) {
        heuristicCost = x;
    }

    public void setFCost(float x) {
        fCost = x;
    }

    public void setIsPathable(bool x) {
        isPathable = x;
    }

    public float getStepCost() {
        return stepCost;
    }

    public float getHeuristicCost() {
        return heuristicCost;
    }

    public float getFCost() {
        return fCost;
    }

    public bool getIsPathable() {
        return isPathable;
    }

}