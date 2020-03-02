using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour
{
    public Camera cam;
    public Transform Cat;
    public float CatSpeed = 2;
    public BoardSpace moveToBoard;
    public BoardSpace currentBoardLoc;
    private bool isMoving = false;


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
        moveToBoard = currentBoardLoc;
        isMoving = true;
        setupBoard();
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
                if (currentBoardLoc.x == selected.x || currentBoardLoc.z == selected.z)
                {
                    moveToBoard = selected;
                    moveToBoard = getNextSpace();
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
            Vector3 catToSpacePos = new Vector3(Cat.position.x, moveToBoard.top.y, Cat.position.z); //cat position in x and z ignoring y
            if (Vector3.Distance(catToSpacePos, moveToBoard.top) < CatSpeed * Time.deltaTime )
            {
                if(moveCounter <= 1 && !Cat.GetComponent<CatController>().inAir)
                {
                    Cat.position = moveToBoard.top;
                    currentBoardLoc = moveToBoard;
                    isMoving = false;

                }
                else
                {
                    BoardSpace tempCurrent = moveToBoard;
                    if(currentBoardLoc.PosX == moveToBoard)
                    {
                        moveToBoard = moveToBoard.PosX;
                    }else if(currentBoardLoc.PosZ == moveToBoard)
                    {
                        moveToBoard = moveToBoard.PosZ;
                    }
                    else if(currentBoardLoc.NegX == moveToBoard)
                    {
                        moveToBoard = moveToBoard.NegX;
                    }
                    else
                    {
                        moveToBoard = moveToBoard.NegZ;
                    }
                    currentBoardLoc = tempCurrent;
                }

                moveCounter -= 1;

            }
            else
            {
                catToSpacePos = Vector3.MoveTowards(catToSpacePos, moveToBoard.top, CatSpeed * Time.deltaTime);
                Cat.position = new Vector3(catToSpacePos.x, Cat.position.y, catToSpacePos.z);
            }


        }
    }

    private BoardSpace getNextSpace()
    {
        if (currentBoardLoc.PosX.GetNextPosX(moveToBoard)) return currentBoardLoc.PosX;
        else if (currentBoardLoc.PosZ.GetNextPosZ(moveToBoard)) return currentBoardLoc.PosZ;
        else if (currentBoardLoc.NegX.GetNextNegX(moveToBoard)) return currentBoardLoc.NegX;
        else if (currentBoardLoc.NegZ.GetNextNegZ(moveToBoard)) return currentBoardLoc.NegZ;
        else return null;
    }

    public void BoardSpaceSelected(BoardSpace board)
    {
        if (!isMoving)
        {
            moveToBoard = board;
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

}
