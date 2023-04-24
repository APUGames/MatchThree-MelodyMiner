
using System.Collections.Generic;
using UnityEngine;

public enum PieceTypes
{
    crumbDance = 0,
    popCat = 1,
    catJAM = 2,
    catKiss = 3,
    cursedCat = 4,
    sadCat = 5,
    longCat = 6,
}

public class Piece
{
    private Vector3 position;
    private Vector2 gridPosition;
    private PieceTypes pieceType;
    private bool setForDestruction;

    public Piece()
    {
        position = Vector3.zero;
        gridPosition = Vector2.zero;
        pieceType = PieceTypes.popCat;
        setForDestruction = false;
    }
    public Piece(Vector3 position, Vector2 gridPosition)
    {
        this.position = position;
        this.gridPosition = gridPosition;
        this.pieceType = PieceTypes.catJAM;
        this.setForDestruction = false;

    }
    public Piece(Vector3 position, Vector2 gridPosition, PieceTypes pieceType)
    {
        this.position = position;
        this.gridPosition = gridPosition;
        this.pieceType = pieceType;
        this.setForDestruction = false;

    }
    public void SetForDestruction()
    {
        this.setForDestruction = true;
    }
    public void SetForDestruction(bool value)
    {
        this.setForDestruction = value;
    }
    public void SetPieceType(PieceTypes pieceType)
    {
        this.pieceType = pieceType;
    }

    public void SetGridPosition(Vector2 position)
    {
        this.gridPosition = position;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public Vector3 GetGridPosition()
    {
        return gridPosition;
    }

    public PieceTypes GetPieceType()
    {
        return pieceType;
    }

    public bool GetDestruction()
    {
        return setForDestruction;
    }
}