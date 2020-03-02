using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour
{
    public Camera cam;
    public BoardPiece Cat;
    public List<BoardPiece> BoardPieces;
    public float CatSpeed = 2;
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

        Debug.Log(Cat.moveToBoard.GetEastPoint());
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

            }
            if (i % 5 > 0)
            {
                space.NegX = BoardSpaces[i - 1];
            }
            if ( (int)(i / 5) > 0)
            {
                space.NegZ = BoardSpaces[i - 5];
            }
            if ((int)(i / 5) < 4)
            {
                space.PosZ = BoardSpaces[i + 5];
            }

        }
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
                    Cat.moveToBoard = getNextSpace();
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
            Vector3 catToSpacePos = new Vector3(x, Cat.moveToBoard.GetHeight(Cat.moveToBoard.x,Cat.moveToBoard.z), z); //cat position in x and z ignoring y
            if (Vector3.Distance(catToSpacePos, Cat.moveToBoard.top) < CatSpeed * Time.deltaTime )
            {
                if(moveCounter <= 1 && !Cat.GetComponent<CatController>().inAir)
                {
                    Cat.transform.position = Cat.moveToBoard.top;
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
                catToSpacePos = Vector3.MoveTowards(catToSpacePos, Cat.moveToBoard.top, Cat.Speed * Time.deltaTime);
                Cat.transform.position = new Vector3(catToSpacePos.x, Cat.transform.position.y, catToSpacePos.z);
            }


        }
    }

    private BoardSpace getNextSpace()
    {
        if (Cat.currentBoardLoc.PosX.GetNextPosX(Cat.moveToBoard)) return Cat.currentBoardLoc.PosX;
        else if (Cat.currentBoardLoc.PosZ.GetNextPosZ(Cat.moveToBoard)) return Cat.currentBoardLoc.PosZ;
        else if (Cat.currentBoardLoc.NegX.GetNextNegX(Cat.moveToBoard)) return Cat.currentBoardLoc.NegX;
        else if (Cat.currentBoardLoc.NegZ.GetNextNegZ(Cat.moveToBoard)) return Cat.currentBoardLoc.NegZ;
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
