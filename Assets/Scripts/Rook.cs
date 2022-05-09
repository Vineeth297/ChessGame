using UnityEngine;

public class Rook : MonoBehaviour,IPieceMover
{
	private ChessBoardClass _chessBoard;
	private void Start() => _chessBoard = ChessBoardClass.ChessBoard;
	
	public void MoveThePiece(GameObject finalCheckBox) => RookMove(finalCheckBox);

	private void RookMove(GameObject finalCheckBox)
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
		
		//Vertical Move
		//row changes, column stays the same
		if (finalColumn == currentColumn && finalRow >= currentRow + 1)
		{
			if (finalRow == currentRow + 1)
			{
				_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
			}
			else
			{
				var maxIterationNumber = Mathf.Abs(finalRow - currentRow);
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow + iteration][currentColumn];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentRow + iteration == finalRow - 1 || currentRow + iteration == finalRow)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
						else continue;
					}
					else
					{
						_chessBoard.InvalidMove();
						return;
					}
				}
			}
		}
		else if (finalColumn == currentColumn && finalRow <= currentRow - 1)
		{
			if (finalRow == currentRow - 1)
			{
				_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
			}
			else
			{
				var maxIterationNumber = Mathf.Abs(finalRow - currentRow);
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow - iteration][currentColumn];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentRow - iteration == finalRow + 1 || currentRow - iteration == finalRow)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
						else continue;
					}
					else
					{
						_chessBoard.InvalidMove();
						return;
					}
				}
			}
		}
		//Horizontal Move
		//Columns changes, Rows stays the same
		else if (finalRow == currentRow && finalColumn >= currentColumn + 1)
		{
			if (finalColumn == currentColumn + 1)
			{
				_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
			}
			else
			{
				var maxIterationNumber = Mathf.Abs(finalColumn - currentColumn);
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow][currentColumn + iteration];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentColumn + iteration == finalColumn - 1 || currentColumn + iteration == finalColumn)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
						else continue;
					}
					else
					{
						_chessBoard.InvalidMove();
						return;
					}
				}	
			}
		}
		else if (finalRow == currentRow && finalColumn <= currentColumn - 1)
		{
			if (finalColumn == currentColumn - 1)
			{
				_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
			}
			else
			{
				var maxIterationNumber = Mathf.Abs(finalColumn - currentColumn);
				for (var iteration = 1; iteration <= maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow][currentColumn - iteration];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentColumn - iteration == finalColumn + 1 || currentColumn - iteration == finalColumn)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
						else continue;
					}
					else
					{
						_chessBoard.InvalidMove();
						return;
					}
				}	
			}
		}
		else
			_chessBoard.InvalidMove();
	}
}
