using System;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class King : MonoBehaviour, IPieceMover
{
	private ChessBoardClass _chessBoard;
	private int _kingsRow;
	private int _kingsColumn;
	private void Start()
	{
		_chessBoard = ChessBoardClass.ChessBoard;
		if (CompareTag("WhitePiece"))
		{
			_kingsRow = 0;
			_kingsColumn = 4;
		}
		else
		{
			_kingsRow = 7;
			_kingsColumn = 4;
		}
	}

	public void MoveThePiece(GameObject finalCheckBox) => KingMove(finalCheckBox);

	private void KingMove(GameObject finalCheckBox)
	{
		_chessBoard.SearchTheBoard(out var currentRow,out var currentColumn,
			_chessBoard.playerInput.selectedTransform.parent.gameObject);
		
		if (currentRow == -1 || currentColumn == -1)
		{
			print("Index Not Found!!");
			return;
		}
		
		_chessBoard.SearchTheBoard(out var finalRow, out var finalColumn, finalCheckBox);
		if (finalRow == -1 || finalColumn == -1)
		{
			print("Index Not Found!!");
			return;
		}

		if (finalRow == 0 && finalColumn == 6)
		{
			KingSlideMove(finalCheckBox, currentRow, currentColumn, finalRow, finalColumn);
			return;
		}
		if (finalRow == currentRow - 1 && finalColumn == currentColumn - 1 ||
		    finalRow == currentRow + 1 && finalColumn == currentColumn + 1 ||
		    finalRow == currentRow + 1 && finalColumn == currentColumn - 1 ||
		    finalRow == currentRow - 1 && finalColumn == currentColumn + 1)
		{
			_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
		}
		else if (finalColumn == currentColumn && (finalRow == currentRow + 1 || finalRow == currentRow - 1))
		{
			_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
		}
		else if (finalRow == currentRow && (finalColumn == currentColumn + 1 || finalColumn == currentColumn - 1))
		{
			_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
		}
		else
		{
			_chessBoard.InvalidMove();
		}
	}

	private void KingSlideMove(GameObject finalCheckBox,int currentRow,int currentColumn,int finalRow,int finalColumn)
	{
		//if final position is [0][6]
		//Rook's position must be [0][7] for white or [7][7] for black
		//current kings position must be [0][4]

		/*var emptyColumn1 = currentColumn + 1;
		var emptyColumn2 = currentColumn + 2;
		var checkBox1 = _chessBoard.checkBoxPositions[finalRow][emptyColumn1];
		var checkBox2 = _chessBoard.checkBoxPositions[finalRow][ emptyColumn2];
		if (!checkBox1.GetComponent<CheckBox>().isOccupied && !checkBox2.GetComponent<CheckBox>().isOccupied)
		{
			var kingsFinalCheckBox = _chessBoard.checkBoxPositions[0][6];
			var rooksFinalCheckBox = _chessBoard.checkBoxPositions[0][5];
			_chessBoard.MoveThePiece(kingsFinalCheckBox.transform);
			finalCheckBox.transform.GetChild(1).transform.position = rooksFinalCheckBox.transform.GetChild(0).position;
			finalCheckBox.transform.GetComponent<CheckBox>().isOccupied = false;
			finalCheckBox.transform.GetComponent<CheckBox>().CheckBoxDeSelected();
			finalCheckBox.transform.GetComponent<CheckBox>().isPieceWhite = false;
			_chessBoard.playerInput.pieceSelected = !_chessBoard.playerInput.pieceSelected;
			finalCheckBox.transform.GetChild(1).transform.parent = rooksFinalCheckBox.transform;
			
			_chessBoard.playerInput.selectedTransform = null;
			_chessBoard.playerInput.finalTransform = null;
			
			
			
			/*if (stepCount == 2)
			{
				
			}#1#
		}
		else
		{
			_chessBoard.InvalidMove();
		}
		*/

		var maxIterations = Mathf.Abs(finalColumn - currentColumn);
		var stepCount = 0;
		for (var iteration = 1; iteration <= maxIterations; iteration++)
		{
			var checkBox = _chessBoard.checkBoxPositions[currentRow][currentColumn + iteration];
			if (!checkBox.GetComponent<CheckBox>().isOccupied) stepCount++;
			else
			{
				_chessBoard.InvalidMove();
				return;
			}
			print("In FOr Loop");
			print(stepCount);

		}
		
		if (stepCount == 2)
		{
			//check if piece at [0][7] is a rook and if of same color of the king
			var rookInitialCheckBox = _chessBoard.checkBoxPositions[currentRow][7];
			var rookFinalCheckBox = _chessBoard.checkBoxPositions[currentRow][finalColumn - 1];
			var rook = rookInitialCheckBox.transform.GetChild(1).gameObject;
			var rookComponent = rookInitialCheckBox.transform.GetChild(1).GetComponent<Rook>();
			if (rookComponent.isRook)
			{
				_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
			}

			//Move King To [0][6]
			//update occupancy and piece white bools

			//Move Rook To [0][5]
			//update occupancy and piece white bools

			//update occupancy and piece white bools on [0][7]

		}
	}
	private void QueenSlideMove(GameObject finalCheckBox)
	{
		//Rook's position must be [0][0] for white or [7][0] for black
	}
}
