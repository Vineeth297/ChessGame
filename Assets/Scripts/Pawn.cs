using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour,IPieceMover
{
	private ChessBoardClass _chessBoard;
    private void Start() => _chessBoard = ChessBoardClass.ChessBoard;

	public void MoveThePiece(GameObject finalCheckBox)
	{
		print("PawnMoved");
		PawnMove(finalCheckBox);
	}

	private void PawnMove(GameObject finalCheckBox)
	{
		//Get the CurrentIndex
		_chessBoard.SearchTheBoard(out var currentRow,out var currentColumn,_chessBoard.playerInput.selectedTransform.parent.gameObject);
		if (currentRow == -1 || currentColumn == -1)
		{
			print("Index Not Found!!");
			return;
		}

		var validMoveRow = currentRow + 1;
		var validMoveColumn = currentColumn;
		var killMove1Row = currentRow + 1;
		var killMove1Column = currentColumn - 1;
		var killMove2Row = currentRow + 1;
		var killMove2Column = currentColumn + 1;
		
		//Check whether the final move checkbox index is valid
		//if valid => move
		_chessBoard.SearchTheBoard(out var finalRow, out var finalColumn, finalCheckBox.gameObject);
		if (finalRow == -1 || finalColumn == -1)
		{
			print("Index Not Found!!");
			return;
		}
		
		if (finalRow == validMoveRow && finalColumn == validMoveColumn)
		{
			//if target checkBox is not occupied => then move
			_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
		}
		else if ((finalRow == killMove1Row && finalColumn == killMove1Column) || 
				 (finalRow == killMove2Row && finalColumn == killMove2Column))
		{
			//if the positions are occupied by enemy => kill
			_chessBoard.MoveOrKillTheEnemy(finalCheckBox.transform);
		}
		else
			ChessBoardClass.InvalidMove();
	}
}
