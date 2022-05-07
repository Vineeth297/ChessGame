using System.Collections.Generic;
using UnityEngine;

public class ChessBoardClass : MonoBehaviour
{
	public static ChessBoardClass ChessBoard;
	public PieceMoverScript playerInput;
	
	public List<GameObject> checkBoxes;
	public List<List<GameObject>> checkBoxPositions;

	[SerializeField] private GameObject whitePawn;
	[SerializeField] private GameObject whiteRook;
	[SerializeField] private GameObject whiteKnight;
	[SerializeField] private GameObject whiteBishop;
	[SerializeField] private GameObject whiteQueen;
	[SerializeField] private GameObject whiteKing;

	private void OnEnable()
	{
		checkBoxPositions = new List<List<GameObject>>();
		SetUpTheCheckBoxes();
		ArrangeThePieces();
	}

	private void Awake()
	{
		#region Singleton
		
		if(ChessBoard)
			Destroy(gameObject);
		else
		{
			ChessBoard = this;
		}
		
		#endregion
	}
	
	private void SetUpTheCheckBoxes()
	{
		for (var i = 0; i < 8; i++)
		{
			var newList = new List<GameObject>();
			for (var j = 0; j < 8; j++)
			{
				newList.Add(checkBoxes[(i * 8) + j]);
				Debug.DrawRay(newList[j].transform.position, Vector3.up, Color.red, 2f);
				//print(newList[j]);
			}
			checkBoxPositions.Add(newList);
		}
	}

	public void SearchTheBoard(out int rowNum, out int columnNum,GameObject checkBox)
	{
		var rowNumber = -1;
		var columnNumber = -1;
		foreach (var column in checkBoxPositions)
		{
			rowNumber++;
			
			var idx = column.IndexOf(checkBox);
			
			if(idx == -1) continue;

			columnNumber = idx;
			break;
		}

		columnNum = columnNumber;
		rowNum = rowNumber;
	}

	public void ValidateOccupancyAndMove(Transform finalMove)
	{
		if(!finalMove.GetComponent<CheckBox>().isOccupied)
		{
			MoveThePiece(finalMove);
		}
		else
			MoveOrKillTheEnemy(finalMove);
	}

	private void MoveThePiece(Transform finalMove)
	{
		playerInput.selectedTransform.position = finalMove.GetChild(0).position;
		playerInput.selectedTransform.parent.GetComponent<CheckBox>().isOccupied = false;
		playerInput.selectedTransform.parent.GetComponent<CheckBox>().isPieceWhite = false;
		playerInput.pieceSelected = !playerInput.pieceSelected;
		playerInput.selectedTransform.parent = finalMove;
		finalMove.GetComponent<CheckBox>().isOccupied = true;
		
		playerInput.finalTransform.GetComponent<CheckBox>().isPieceWhite = 
			playerInput.selectedTransform.CompareTag("WhitePiece");
		
		playerInput.selectedTransform = null;
		playerInput.finalTransform = null;
	}

	public void MoveOrKillTheEnemy(Transform finalMove)
	{
		if (finalMove.childCount == 2)
		{
			//if the target piece is not of same color => kill
			if (playerInput.selectedTransform.parent.GetComponent<CheckBox>().isPieceWhite !=
			    playerInput.finalTransform.GetComponent<CheckBox>().isPieceWhite)
			{
				playerInput.selectedTransform.position = finalMove.GetChild(0).position;
				playerInput.selectedTransform.parent.GetComponent<CheckBox>().isOccupied = false;
				playerInput.selectedTransform.parent.GetComponent<CheckBox>().isPieceWhite = false;
				playerInput.finalTransform.GetComponent<CheckBox>().isPieceWhite = 
					playerInput.selectedTransform.CompareTag("WhitePiece");
				playerInput.pieceSelected = !playerInput.pieceSelected;
				playerInput.finalTransform.GetChild(1).gameObject.SetActive(false);
				playerInput.finalTransform.GetChild(1).parent = null;
				playerInput.selectedTransform.parent = finalMove;
			}
			else
				InvalidMove();
		}
		//else invalid move
		else
			InvalidMove();
	}

	public void InvalidMove()
	{
		print("Invalid Move");
		playerInput.selectedTransform = null;
		playerInput.finalTransform = null;
	}

	private void ArrangeThePieces()
	{
		for (var i = 0; i < 8; i++)
		{
			var pawnWhite = Instantiate(whitePawn);
			pawnWhite.transform.position = checkBoxPositions[1][i].transform.GetChild(0).position;
			pawnWhite.transform.parent = checkBoxPositions[1][i].transform;
		}

		var rookWhite1 = Instantiate(whiteRook);
		rookWhite1.transform.position = checkBoxPositions[0][0].transform.GetChild(0).position;
		rookWhite1.transform.parent = checkBoxPositions[0][0].transform;
		
		var rookWhite2 = Instantiate(whiteRook);
		rookWhite2.transform.position = checkBoxPositions[0][7].transform.GetChild(0).position;
		rookWhite2.transform.parent = checkBoxPositions[0][7].transform;

		var knightWhite1 = Instantiate(whiteKnight);
		knightWhite1.transform.position = checkBoxPositions[0][1].transform.GetChild(0).position;
		knightWhite1.transform.parent = checkBoxPositions[0][1].transform;
		
		var knightWhite2 = Instantiate(whiteKnight);
		knightWhite2.transform.position = checkBoxPositions[0][6].transform.GetChild(0).position;
		knightWhite2.transform.parent = checkBoxPositions[0][6].transform;
		
		var bishopWhite1 = Instantiate(whiteBishop);
		bishopWhite1.transform.position = checkBoxPositions[0][2].transform.GetChild(0).position;
		bishopWhite1.transform.parent = checkBoxPositions[0][2].transform;

		var bishopWhite2 = Instantiate(whiteBishop);
		bishopWhite2.transform.position = checkBoxPositions[0][5].transform.GetChild(0).position;
		bishopWhite2.transform.parent = checkBoxPositions[0][5].transform;
		
		var queenWhite = Instantiate(whiteQueen);
		queenWhite.transform.position = checkBoxPositions[0][3].transform.GetChild(0).position;
		queenWhite.transform.parent = checkBoxPositions[0][3].transform;
		
		var kingWhite = Instantiate(whiteKing);
		kingWhite.transform.position = checkBoxPositions[0][4].transform.GetChild(0).position;
		kingWhite.transform.parent = checkBoxPositions[0][4].transform;
	}
}
