using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : MonoBehaviour,IPieceMover
{
	private ChessBoardClass _chessBoard;
	private void Start() => _chessBoard = ChessBoardClass.ChessBoard;
	
	public void MoveThePiece(GameObject finalCheckBox)
	{
		print("RookPieceMoved");
		RookMove(finalCheckBox);
	}
	
    private void RookMove(GameObject finalCheckBox)
	{
		//Vertical Move
		//row changes, column stays the same
		_chessBoard.SearchTheBoard(out var currentRow,out var currentColumn,_chessBoard.playerInput.selectedTransform.parent.gameObject);
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

		if (currentRow != finalRow && currentColumn == finalColumn)
		{
			//if all the checkboxes are empty between current and final check box => move
			if (currentRow < finalRow)
			{
				if (finalRow == currentRow + 1)
				{
					_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
					return;
				}
				for (var row = currentRow + 1; row < finalRow; row++)
				{
					if (IfAllCheckBoxesInThePathAreEmpty(row, finalColumn, finalRow, currentRow,currentColumn))
						return;
					else
						continue;
				}
			}
			else
			{
				if (finalRow == currentRow - 1)
				{
					_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
					return;
				}
				for (var row = currentRow - 1; row > finalRow; row--)
				{
					if (IfAllCheckBoxesInThePathAreEmpty(row, finalColumn, finalRow,currentRow,currentColumn))
						return;
					else
						continue;
				}
			}
		}
		//Horizontal Move
		//row stays the same, column changes
		else if (currentRow == finalRow && currentColumn != finalColumn)
		{
			//if all the checkboxes are empty between current and final check box => move
			if (currentColumn < finalColumn)
			{
				if (finalColumn == currentColumn + 1)
				{
					_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
					return;
				}
				for (var column = currentColumn + 1; column < finalColumn; column++)
				{
					if (IfAllCheckBoxesInThePathAreEmpty(column, finalColumn, finalRow, currentRow,currentColumn))
						return;
					else
						continue;
				}
			}
			else
			{
				if (finalColumn == currentColumn - 1)
				{
					_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
					return;
				}
				for (var column = currentRow - 1; column > finalColumn; column--)
				{
					if (IfAllCheckBoxesInThePathAreEmpty(column, finalColumn, finalRow,currentRow,currentColumn))
						return;
					else
						continue;
				}
			}
		}
		else
		{
			_chessBoard.InvalidMove();
		}
	}

	private bool IfAllCheckBoxesInThePathAreEmpty(int moveDirectionIndex, int finalColumn, int finalRow, int currentRow, int currentColumn)
	{
		GameObject checkBox;
		int finalIndex;
		if (currentColumn == finalColumn)
		{
			checkBox = _chessBoard.checkBoxPositions[moveDirectionIndex][finalColumn];
			finalIndex = finalRow;
		}
		else
		{
			checkBox = _chessBoard.checkBoxPositions[finalRow][moveDirectionIndex];
			finalIndex = finalColumn;
		}
		
		//print(checkBox);
		if (!checkBox.GetComponent<CheckBox>().isOccupied)
		{
			//print(moveDirectionIndex == finalRow - 1);
			if (moveDirectionIndex == finalIndex - 1)
			{
				//check for final indexes occupancy
				var finalCheckBox = _chessBoard.checkBoxPositions[finalRow][finalColumn];
				_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
			}
			else if (moveDirectionIndex == finalIndex + 1)
			{
				var finalCheckBox = _chessBoard.checkBoxPositions[finalRow][finalColumn];
				_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
			}
			else return false;
		}
		return true;
	}
}
