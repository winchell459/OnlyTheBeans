using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour
{
    public Camera cam;
    public BoardPiece Cat;
    public List<BoardPiece> BoardPieces;
    //public float CatSpeed = 2;
    //public BoardSpace moveToBoard;
    //public BoardSpace currentBoardLoc;
    private bool isMoving = false;

    //public enum BoardStats
    //{
    //    canSelect,
    //    isMoving,
    //    isFalling,
    //    isRising
    //}


    public int moveCounter = 1;

    public BoardSpace[] BoardSpaces;

    public enum CatMoves
    {
        MoveOne,
        MoveTwo,
        MoveFour,
        JumpOneMoveOne,
        JumpOne,
        JumpTwo,
        JumpFour
    }
    public CatMoves CatMove;
    private bool canSelectMove = false;
    public GameObject SelectedButtonCatMove;
    private float selectedButtonScale;
    public float ScaleSelectedButton = 1.5f;

    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        Cat.moveToBoard = Cat.currentBoardLoc;
        isMoving = true;
        setupBoard();

        Debug.Log(Cat.moveToBoard.GetEastPoint() + " west: " + Cat.moveToBoard.GetWestPoint() + " north: " + Cat.moveToBoard.GetNorthPoint() + " south: " + Cat.moveToBoard.GetSouthPoint());

    }

    // Update is called once per frame
    void Update()
    {

        handleBoardSpaceSelection();
        handleMovement();
        
    }

    private void setupBoard()
    {
        for(int i = 0; i < BoardSpaces.Length; i++)
        {
            BoardSpace space = BoardSpaces[i];
            if (i % 5 < 4)
            {
                space.PosX = BoardSpaces[i + 1];
                setSpaceElevation(space, space.PosX);
            }
            if (i % 5 > 0)
            {
                space.NegX = BoardSpaces[i - 1];
                setSpaceElevation(space, space.NegX);
            }
            if ( (int)(i / 5) > 0)
            {
                space.NegZ = BoardSpaces[i - 5];
                setSpaceElevation(space, space.NegZ);
            }
            if ((int)(i / 5) < 4)
            {
                space.PosZ = BoardSpaces[i + 5];
                setSpaceElevation(space, space.PosZ);
            }

        }
    }
    private void setSpaceElevation(BoardSpace space, BoardSpace addedSpace)
    {
        BoardSpace.BoardSpaceElevations elevation = BoardSpace.BoardSpaceElevations.Level;
        if(addedSpace.y > space.y)
        {
            elevation = BoardSpace.BoardSpaceElevations.Up;
        }else if(addedSpace.y < space.y)
        {
            elevation = BoardSpace.BoardSpaceElevations.Down;
        }

        if (space.NegX == addedSpace) space.NegXElev = elevation;
        if (space.NegZ == addedSpace) space.NegZElev = elevation;
        if (space.PosX == addedSpace) space.PosXElev = elevation;
        if (space.PosZ == addedSpace) space.PosZElev = elevation;
    }
    private void handleBoardSpaceSelection()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                BoardSpace selected = hit.transform.GetComponent<BoardSpace>();
                if (Cat.currentBoardLoc.x == selected.x || Cat.currentBoardLoc.z == selected.z)
                {
                    Cat.moveToBoard = selected;
                    Cat.moveToBoard = getNextSpace(Cat);
                    isMoving = true;
                    if (CatMove == CatMoves.MoveOne) moveCounter = 1;
                    else if (CatMove == CatMoves.MoveFour) moveCounter = 4;
                    else if (CatMove == CatMoves.MoveTwo) moveCounter = 2;
                }

            }
        }
    }

    private void handleMovement()
    {
        if (isMoving)
        {
            float x = Cat.transform.position.x;
            float z = Cat.transform.position.z;
            
            Vector3 moveToTarget;
            if (moveCounter > 1)
            {
                
                if (Cat.currentBoardLoc.PosX == Cat.moveToBoard)
                {
                    moveToTarget = Cat.moveToBoard.GetEastPoint();
                }
                else if (Cat.currentBoardLoc.PosZ == Cat.moveToBoard)
                {
                    moveToTarget = Cat.moveToBoard.GetNorthPoint();
                }
                else if (Cat.currentBoardLoc.NegX == Cat.moveToBoard)
                {
                    moveToTarget = Cat.moveToBoard.GetWestPoint();
                }
                else
                {
                    moveToTarget = Cat.moveToBoard.GetSouthPoint();
                }
            }
            else
            {
                moveToTarget = new Vector3(Cat.moveToBoard.transform.position.x, Cat.moveToBoard.GetCenterPoint().y, Cat.moveToBoard.transform.position.z);
            }

            Vector3 catToSpacePos = new Vector3(x, moveToTarget.y, z); //cat position in x and z ignoring y

            if (Vector3.Distance(catToSpacePos, moveToTarget) < Cat.Speed * Time.deltaTime )
            {
                if(moveCounter <= 1 && !Cat.GetComponent<CatController>().inAir)
                {
                    Cat.transform.position = moveToTarget;
                    Cat.currentBoardLoc = Cat.moveToBoard;
                    isMoving = false;
                    canSelectMove = true;
                }
                else
                {
                    BoardSpace tempCurrent = Cat.moveToBoard;
                    if(Cat.currentBoardLoc.PosX == Cat.moveToBoard)
                    {
                        Cat.moveToBoard = Cat.moveToBoard.PosX;
                    }else if(Cat.currentBoardLoc.PosZ == Cat.moveToBoard)
                    {
                        Cat.moveToBoard = Cat.moveToBoard.PosZ;
                    }
                    else if(Cat.currentBoardLoc.NegX == Cat.moveToBoard)
                    {
                        Cat.moveToBoard = Cat.moveToBoard.NegX;
                    }
                    else
                    {
                        Cat.moveToBoard = Cat.moveToBoard.NegZ;
                    }
                    Cat.currentBoardLoc = tempCurrent;
                }

                moveCounter -= 1;

            }
            else
            {
                catToSpacePos = Vector3.MoveTowards(catToSpacePos, moveToTarget, Cat.Speed * Time.deltaTime);
                Cat.transform.position = new Vector3(catToSpacePos.x, Cat.transform.position.y, catToSpacePos.z);
            }


        }
    }

    private BoardSpace getNextSpace(BoardPiece piece)
    {
        if (piece.currentBoardLoc.PosX && piece.currentBoardLoc.PosX.GetNextPosX(piece.moveToBoard)) return piece.currentBoardLoc.PosX;
        else if (piece.currentBoardLoc.PosZ && piece.currentBoardLoc.PosZ.GetNextPosZ(piece.moveToBoard)) return piece.currentBoardLoc.PosZ;
        else if (piece.currentBoardLoc.NegX && piece.currentBoardLoc.NegX.GetNextNegX(piece.moveToBoard)) return piece.currentBoardLoc.NegX;
        else if (piece.currentBoardLoc.NegZ && piece.currentBoardLoc.NegZ.GetNextNegZ(piece.moveToBoard)) return piece.currentBoardLoc.NegZ;
        else return null;
    }

    public void BoardSpaceSelected(BoardSpace board)
    {
        if (!isMoving)
        {
            Cat.moveToBoard = board;
            canSelectMove = false;
        }
    }

    public void SetCatMove(string catMove)
    {

        if (canSelectMove)
        {
            SelectedButtonCatMove = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            CatMove = (CatMoves)System.Enum.Parse(typeof(CatMoves), catMove);
        }

    }
    public void SetCatMove(CatMove catMove)
    {
        if (canSelectMove)
        {

        }
    }

}
