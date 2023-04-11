
using System;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    [SerializeField]
    private GameObject piecePrefab;
    [SerializeField]
    private Vector3 originPosition;

    [Header("Piece Colors")]
    [SerializeField]
    private Material pieceOneMaterial;
    [SerializeField]
    private Material pieceSecondMaterial;
    [SerializeField]
    private Material pieceThirdMaterial;
    [SerializeField]
    private Material pieceFourMaterial;
    [SerializeField]
    private Material pieceFiveMaterial;
    [SerializeField]
    private Material pieceSixMaterial;
    [SerializeField]
    private Material pieceSevenMaterial;

    public bool pressedDown;
    public Vector2 pressedDownPosition;
    public GameObject pressedDownGameObject;
    public Vector2 pressedUpPosition;
    public GameObject pressedUpGameObject;

    [Header("UI")]
    [SerializeField]
    private GameObject matchesFoundText;

    private Vector2 startMovementPiecePosition;
    private Vector2 endMovementPiecePosition;

    public bool validMoveInProcess = false;

    private int matchesFound;

    private Piece[,] grid = new Piece[8, 8];
    void Start()
    {
        System.Random rand = new System.Random();

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int column = 0; column < grid.GetLength(1); column++)
            {
                Vector3 newWorldPosition = new Vector3(originPosition.x + row, originPosition.y, originPosition.z - column);
                grid[row, column] = new Piece(newWorldPosition, new Vector2(row, column));

                GameObject gameObject = Instantiate(piecePrefab, grid[row, column].GetPosition(), Quaternion.identity);
                int theNumber = rand.Next(0, 60);
                if (theNumber > 0 && theNumber < 10)
                {
                    var gameObjectRenderer = gameObject.GetComponent<Renderer>();
                    gameObjectRenderer.material = pieceOneMaterial;
                }
                else if (theNumber >= 10 && theNumber < 20)
                {
                    var gameObjectRenderer = gameObject.GetComponent<Renderer>();
                    gameObjectRenderer.material = pieceFourMaterial;
                }
                else if (theNumber >= 20 && theNumber < 30)
                {
                    var gameObjectRenderer = gameObject.GetComponent<Renderer>();

                    gameObjectRenderer.material = pieceSecondMaterial;
                }
                else if (theNumber >= 30 && theNumber < 40)
                {
                    var gameObjectRenderer = gameObject.GetComponent<Renderer>();
                    gameObjectRenderer.material = pieceThirdMaterial;
                }
                else if (theNumber >= 40 && theNumber < 50)
                {
                    var gameObjectRenderer = gameObject.GetComponent<Renderer>();
                    gameObjectRenderer.material = pieceSixMaterial;
                }
                else if (theNumber >= 50 && theNumber < 60)
                {
                    var gameObjectRenderer = gameObject.GetComponent<Renderer>();
                    gameObjectRenderer.material = pieceFiveMaterial;
                }
                else
                {
                    var gameObjectRenderer = gameObject.GetComponent<Renderer>();
                    gameObjectRenderer.material = pieceSevenMaterial;
                }
            }
        }
    }
    private void Update()
    {
        if (validMoveInProcess)
        {

            Vector3 placeHolderPosition = pressedDownGameObject.transform.position;
            pressedDownGameObject.transform.position = pressedUpGameObject.transform.position;
            pressedUpGameObject.transform.position = placeHolderPosition;

            // Update the data layer to match the visual layer
            Piece placeHolderPiece = grid[(int)endMovementPiecePosition.x, (int)endMovementPiecePosition.y];
            grid[(int)endMovementPiecePosition.x, (int)endMovementPiecePosition.y] = grid[(int)startMovementPiecePosition.x, (int)startMovementPiecePosition.y];
            grid[(int)startMovementPiecePosition.x, (int)startMovementPiecePosition.y] = placeHolderPiece;

            validMoveInProcess = false;

            matchesFound += 1;
        }

        matchesFoundText.GetComponent<Text>().text = matchesFound.ToString();
    }

    private Piece GetGridPiece(int row, int column)
    {
        Piece foundPiece;
        try
        {
            foundPiece = grid[row, column];
            if (foundPiece == null || foundPiece.GetDestruction())
            {
                return null;
            }

            return foundPiece;
        }
        catch (IndexOutOfRangeException)
        {
        }

        return null;
    }

    private Piece GetGridPiece(int row, int column, bool isDestroyed)
    {
        Piece foundPiece;
        try
        {
            foundPiece = grid[row, column];
            if (foundPiece == null)
            {
                return null;
            }

            if (!isDestroyed)
            {
                return null;
            }

            return foundPiece;
        }
        catch (IndexOutOfRangeException)
        {
        }

        return null;
    }


    public void ValidMove(Vector2 start, Vector2 end)
    {
        startMovementPiecePosition = start;
        endMovementPiecePosition = end;

        bool matchFound = false;

        if (!matchFound)
        {

            try
            {
                Piece topPiece1 = grid[(int)end.x, (int)end.y - 1];
                Piece bottomPiece1 = grid[(int)end.x, (int)end.y + 1];
                Piece midPiece1 = grid[(int)start.x, (int)start.y];
                Piece toDestroy1 = grid[(int)end.x, (int)end.y];
                if (topPiece1.GetPieceType() == bottomPiece1.GetPieceType())
                {
                    if (topPiece1.GetPieceType() == midPiece1.GetPieceType())
                    {
                        matchFound = true;
                        validMoveInProcess = true;
                        topPiece1.SetForDestruction();
                        bottomPiece1.SetForDestruction();
                        toDestroy1.SetForDestruction();
                    }
                }
            }
            catch (IndexOutOfRangeException)  // CS0168
            {
                // Set IndexOutOfRangeException to the new exception's InnerException.
            }
        }

        if (!matchFound)
        {

            try
            {
                Piece leftPiece = grid[(int)end.x - 1, (int)end.y];
                Piece leftLeftPiece = grid[(int)end.x - 2, (int)end.y];
                Piece checkPiece1 = grid[(int)start.x, (int)start.y];
                if (leftPiece.GetPieceType() == leftLeftPiece.GetPieceType())
                {
                    if (leftPiece.GetPieceType() == checkPiece1.GetPieceType())
                    {
                        validMoveInProcess = true;
                        Piece toDestroy2 = grid[(int)end.x, (int)end.y];

                        leftPiece.SetForDestruction();
                        leftLeftPiece.SetForDestruction();
                        toDestroy2.SetForDestruction();
                    }
                }
            }
            catch (IndexOutOfRangeException) 
            {
                // Set IndexOutOfRangeException to the new exception's InnerException.
            }
        }

        if (!matchFound)
        {

            try
            {
                Piece rightPiece = grid[(int)end.x + 1, (int)end.y];
                Piece rightRightPiece = grid[(int)end.x + 2, (int)end.y];
                Piece checkPiece2 = grid[(int)start.x, (int)start.y];
                if (rightPiece.GetPieceType() == rightRightPiece.GetPieceType())
                {
                    if (rightPiece.GetPieceType() == checkPiece2.GetPieceType())
                    {
                        validMoveInProcess = true;
                        Piece toDestroy2 = grid[(int)end.x, (int)end.y];

                        rightPiece.SetForDestruction();
                        rightRightPiece.SetForDestruction();
                        toDestroy2.SetForDestruction();
                    }
                }
            }
            catch (IndexOutOfRangeException)  
            {
            }
        }

        if (!matchFound)
        {

            try
            {
                Piece rightPiece = grid[(int)end.x + 1, (int)end.y];
                Piece leftPiece = grid[(int)end.x - 1, (int)end.y];
                Piece checkPiece3 = grid[(int)start.x, (int)start.y];
                if (rightPiece.GetPieceType() == leftPiece.GetPieceType())
                {
                    if (rightPiece.GetPieceType() == checkPiece3.GetPieceType())
                    {
                        validMoveInProcess = true;
                        Piece toDestroy2 = grid[(int)end.x, (int)end.y];

                        rightPiece.SetForDestruction();
                        leftPiece.SetForDestruction();
                        toDestroy2.SetForDestruction();
                    }
                }
            }
            catch (IndexOutOfRangeException)  
            {
            }
        }

        if (!matchFound)
        {
            try
            {
                Piece abovePiece = GetGridPiece((int)end.x, (int)end.y + 1);
                Piece aboveAbovePiece = GetGridPiece((int)end.x, (int)end.y + 2);
                Piece checkPiece4 = GetGridPiece((int)start.x, (int)start.y);
                if (abovePiece.GetPieceType() == aboveAbovePiece.GetPieceType())
                {
                    if (abovePiece.GetPieceType() == checkPiece4.GetPieceType())
                    {
                        matchFound = true;
                        validMoveInProcess = true;
                        Piece toDestroy2 = GetGridPiece((int)end.x, (int)end.y);

                        abovePiece.SetForDestruction();
                        aboveAbovePiece.SetForDestruction();
                        toDestroy2.SetForDestruction();
                    }
                }
            }
            catch (NullReferenceException)
            {
            }
        }

    }

    public bool IsDestroyed(Vector2 gridPosition)
    {
        Piece piece = GetGridPiece((int)gridPosition.x, (int)gridPosition.y, true);
        if (piece != null)
        {
            return piece.GetDestruction();
        }
        return false;
    }
}