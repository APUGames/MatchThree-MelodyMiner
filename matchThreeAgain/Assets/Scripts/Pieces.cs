using UnityEngine;

public enum PieceTypes
{
    crumbDance = 0,
    popCat = 1,
    catJAM = 2,
    catKiss = 3,
    cursedCat = 4,
    sadCat = 5,
}

public class Piece
{
    private Vector3 position;
    private Vector2 gridPosition;
    private PieceTypes pieceType;

    public Piece()
    {
        position = Vector3.zero;
        gridPosition = Vector2.zero;
        pieceType = PieceTypes.popCat;
    }
    public Piece(Vector3 position, Vector2 gridPosition)
    {
        this.position = position;
        this.gridPosition = gridPosition;
        this.pieceType = PieceTypes.catJAM;
    }
    public Piece(Vector3 position, Vector2 gridPosition, PieceTypes pieceType)
    {
        this.position = position;
        this.gridPosition = gridPosition;
        this.pieceType = pieceType;
    }
    public Vector3 GetPosition()
    {
        return position;
    }
    public PieceTypes GetPieceType()
    {
        return pieceType;
    }
}