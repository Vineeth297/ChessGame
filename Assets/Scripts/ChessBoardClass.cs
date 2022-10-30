using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChessBoardClass : MonoBehaviour
{
	public static ChessBoardClass ChessBoard;
	public PieceMoverScript playerInput;
	
	public List<GameObject> checkBoxes;
	public List<List<GameObject>> checkBoxPositions;

	[Header("WHITE PIECES")]
	[SerializeField] private GameObject whitePawn;
	[SerializeField] private GameObject whiteRook;
	[SerializeField] private GameObject whiteKnight;
	[SerializeField] private GameObject whiteBishop;
	[SerializeField] private GameObject whiteQueen;
	[SerializeField] private GameObject whiteKing;

	[Header("BLACK PIECES")]
	[SerializeField] private GameObject blackPawn;
	[SerializeField] private GameObject blackRook;
	[SerializeField] private GameObject blackKnight;
	[SerializeField] private GameObject blackBishop;
	[SerializeField] private GameObject blackQueen;
	[SerializeField] private GameObject blackKing;
	
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

	public void MoveThePiece(Transform finalMove)
	{
		//playerInput.selectedTransform.position = finalMove.GetChild(0).position;
		var selectedTransform = playerInput.selectedTransform;
		var selectedTransformCheckBox = selectedTransform.parent.GetComponent<CheckBox>();
		var finalTransform = playerInput.finalTransform;
		var finalTransformCheckBox = finalTransform.GetComponent<CheckBox>();
		
		selectedTransform.DOMove(finalMove.GetChild(0).position,1.5f);
		selectedTransformCheckBox.isOccupied = false;
		selectedTransformCheckBox.CheckBoxDeSelected();
		selectedTransformCheckBox.isPieceWhite = false;
		playerInput.selectedTransform.parent = finalMove;
		finalMove.GetComponent<CheckBox>().isOccupied = true;

		finalTransformCheckBox.isPieceWhite = playerInput.selectedTransform.CompareTag("WhitePiece");

		playerInput.ClearPlayerInput();
	}

	public void MoveOrKillTheEnemy(Transform finalMove)
	{
		if (finalMove.childCount == 2)
		{
			//if the target piece is not of same color => kill
			var selectedTransform = playerInput.selectedTransform;
			var selectedTransformCheckBox = selectedTransform.parent.GetComponent<CheckBox>();
			var finalTransform = playerInput.finalTransform;
			var finalTransformCheckBox = finalTransform.GetComponent<CheckBox>();
			
			if (selectedTransformCheckBox.isPieceWhite != finalTransformCheckBox.isPieceWhite)
			{
				// playerInput.selectedTransform.position = finalMove.GetChild(0).position;
				selectedTransform.DOMove(finalMove.GetChild(0).position,1.5f);
				selectedTransformCheckBox.isOccupied = false;
				selectedTransformCheckBox.CheckBoxDeSelected();
				selectedTransformCheckBox.isPieceWhite = false;
				selectedTransform.parent = finalMove;
				
				finalTransformCheckBox.isPieceWhite = selectedTransform.CompareTag("WhitePiece");
				playerInput.pieceSelected = !playerInput.pieceSelected;
				finalTransform.GetChild(1).gameObject.SetActive(false);
				finalTransform.GetChild(1).parent = null;
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
		playerInput.selectedTransform.parent.GetComponent<CheckBox>().CheckBoxDeSelected();
		playerInput.ClearPlayerInput();
	}

	private void ArrangeThePieces()
	{
		/* Spawn All the Pawns */
		SpawnAllThePawns();

		/* White Pieces */
		SpawnWhitePieces();
		
		/* Black Pieces */
		SpawnBlackPieces();
	}

	private void SpawnAllThePawns()
	{
		for (var i = 0; i < 8; i++)
		{
			var pawnWhite = Instantiate(whitePawn);
			pawnWhite.transform.position = checkBoxPositions[1][i].transform.GetChild(0).position;
			pawnWhite.transform.parent = checkBoxPositions[1][i].transform;
			
			var pawnBlack = Instantiate(blackPawn);
			pawnBlack.transform.position = checkBoxPositions[6][i].transform.GetChild(0).position;
			pawnBlack.transform.parent = checkBoxPositions[6][i].transform;
		}
	}
	
	private void SpawnWhitePieces()
	{
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

	private void SpawnBlackPieces()
	{
		var rookBlack1 = Instantiate(blackRook);
		rookBlack1.transform.position = checkBoxPositions[7][0].transform.GetChild(0).position;
		rookBlack1.transform.parent = checkBoxPositions[7][0].transform;
		
		var rookBlack2 = Instantiate(blackRook);
		rookBlack2.transform.position = checkBoxPositions[7][7].transform.GetChild(0).position;
		rookBlack2.transform.parent = checkBoxPositions[7][7].transform;

		var knightBlack1 = Instantiate(blackKnight);
		knightBlack1.transform.position = checkBoxPositions[7][1].transform.GetChild(0).position;
		knightBlack1.transform.parent = checkBoxPositions[7][1].transform;
		
		var knightBlack2 = Instantiate(blackKnight);
		knightBlack2.transform.position = checkBoxPositions[7][6].transform.GetChild(0).position;
		knightBlack2.transform.parent = checkBoxPositions[7][6].transform;
		
		var bishopBlack1 = Instantiate(blackBishop);
		bishopBlack1.transform.position = checkBoxPositions[7][2].transform.GetChild(0).position;
		bishopBlack1.transform.parent = checkBoxPositions[7][2].transform;

		var bishopBlack2 = Instantiate(blackBishop);
		bishopBlack2.transform.position = checkBoxPositions[7][5].transform.GetChild(0).position;
		bishopBlack2.transform.parent = checkBoxPositions[7][5].transform;
		
		var queenBlack = Instantiate(blackQueen);
		queenBlack.transform.position = checkBoxPositions[7][3].transform.GetChild(0).position;
		queenBlack.transform.parent = checkBoxPositions[7][3].transform;
		
		var kingBlack = Instantiate(blackKing);
		kingBlack.transform.position = checkBoxPositions[7][4].transform.GetChild(0).position;
		kingBlack.transform.parent = checkBoxPositions[7][4].transform;
	}
}
