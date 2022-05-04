using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardClass : MonoBehaviour
{
	public static ChessBoardClass ChessBoard;
	public PieceMoverScript playerInput;
	
	public List<GameObject> checkBoxes;
	public List<List<GameObject>> checkBoxPositions;

	private void Awake()
	{
		if(ChessBoard)
			Destroy(gameObject);
		else
		{
			ChessBoard = this;
		}
	}
	private void Start()
	{
		checkBoxPositions = new List<List<GameObject>>();
		FillTheBoxes();
	}
	
	private void FillTheBoxes()
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
			KillTheEnemy(finalMove);
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

	public void KillTheEnemy(Transform finalMove)
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

	public static void InvalidMove() => print("Invalid Move");
}
