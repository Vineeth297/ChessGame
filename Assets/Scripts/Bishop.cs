using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : MonoBehaviour, IPieceMover
{
    private ChessBoardClass _chessBoard;
    private void Start() => _chessBoard = ChessBoardClass.ChessBoard;

    public void MoveThePiece(GameObject finalCheckBox)
    {
        print("BishopMoved");
        BishopMove(finalCheckBox);
    }
    
    private void BishopMove(GameObject finalCheckBox)
	{
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

		if (currentRow < finalRow && currentColumn < finalColumn)
		{
			if (currentRow + 1 == finalRow)
			{
				_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
				return;
			}
			else
			{
				var maxIterationNumber = finalColumn - currentColumn;
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow + iteration][currentColumn + iteration];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentRow + iteration == finalRow - 1)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
						else continue;
					}
				}	
			}
		}
		else if(currentRow > finalRow && currentColumn > finalColumn)
		{
			if (currentRow - 1 == finalRow)
			{
				_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
				return;
			}
			else
			{
				var maxIterationNumber = currentColumn - finalColumn;
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow - iteration][currentColumn - iteration];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentRow - iteration == finalRow + 1)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
						else continue;
					}
				}	
			}
		}
		else if (currentRow < finalRow && currentColumn > finalColumn)
		{
			if (currentRow + 1 == finalRow && currentColumn - 1 == finalColumn)
			{
				_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
				return;
			}
			else
			{
				var maxIterationNumber = currentColumn - finalColumn;
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow + iteration][currentColumn - iteration];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentRow + iteration == finalRow - 1 && currentColumn - iteration == finalColumn + 1)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
						else continue;
					}
				}
			}
		}
		else if(currentRow > finalRow && currentColumn < finalColumn)
		{
			if (currentRow - 1 == finalRow && currentColumn + 1 == finalColumn)
			{
				_chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
				return;
			}
			else
			{
				var maxIterationNumber = finalColumn - currentColumn;
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow - iteration][currentColumn + iteration];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentRow - iteration == finalRow + 1 && currentColumn + iteration == finalColumn - 1)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
						else continue;
					}
				}
			}
		}
		else
		{
			ChessBoardClass.InvalidMove();
			return;
		}
	}
}
