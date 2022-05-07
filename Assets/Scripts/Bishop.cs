using UnityEngine;

public class Bishop : MonoBehaviour, IPieceMover
{
    private ChessBoardClass _chessBoard;
    private void Start() => _chessBoard = ChessBoardClass.ChessBoard;

    public void MoveThePiece(GameObject finalCheckBox) => BishopMove(finalCheckBox);

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

		if (finalRow >= currentRow + 1 && finalColumn >= currentColumn + 1)
		{
			if (currentRow + 1 == finalRow && currentColumn + 1 == finalColumn)
				_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
			else
			{
				var maxIterationNumber = Mathf.Abs(finalColumn - currentColumn);
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow + iteration][currentColumn + iteration];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentRow + iteration == finalRow - 1 && currentColumn + iteration == finalColumn - 1 ||
						    currentRow + iteration == finalRow && currentColumn + iteration == finalColumn)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
					}
					else
					{
						_chessBoard.InvalidMove();
						return;
					}	
				}
			}
		}
		else if (finalRow <= currentRow - 1 && finalColumn <= currentColumn - 1)
		{
			if (currentRow - 1 == finalRow && currentColumn - 1 == finalColumn)
				_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
			else
			{
				var maxIterationNumber = Mathf.Abs(finalColumn - currentColumn);
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow - iteration][currentColumn - iteration];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentRow - iteration == finalRow + 1 && currentColumn - iteration == finalColumn + 1 ||
						    currentRow - iteration == finalRow && currentColumn - iteration == finalColumn)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
					}
					else
					{
						_chessBoard.InvalidMove();
						return;
					}
				}
			}
			
		}
		else if (finalRow <= currentRow - 1 && finalColumn >= currentColumn + 1)
		{
			if (currentRow - 1 == finalRow && currentColumn + 1 == finalColumn)
				_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
			else
			{
				var maxIterationNumber = Mathf.Abs(finalColumn - currentColumn);
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow - iteration][currentColumn + iteration];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentRow - iteration == finalRow + 1 && currentColumn + iteration == finalColumn - 1 ||
						    currentRow - iteration == finalRow && currentColumn + iteration == finalColumn)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
					}
					else
					{
						_chessBoard.InvalidMove();
						return;
					}
				}
			}
		}
		else if (finalRow >= currentRow + 1 && finalColumn <= currentColumn - 1)
		{
			if (currentRow + 1 == finalRow && currentColumn - 1 == finalColumn)
				_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
			else
			{
				var maxIterationNumber = Mathf.Abs(finalColumn - currentColumn);
				for (var iteration = 1; iteration < maxIterationNumber; iteration++)
				{
					var checkBox = _chessBoard.checkBoxPositions[currentRow + iteration][currentColumn - iteration];
					if (!checkBox.GetComponent<CheckBox>().isOccupied)
					{
						if (currentRow + iteration == finalRow - 1 && currentColumn - iteration == finalColumn + 1 ||
						    currentRow + iteration == finalRow && currentColumn - iteration == finalColumn)
						{
							_chessBoard.ValidateOccupancyAndMove(_chessBoard.checkBoxPositions[finalRow][finalColumn].transform);
						}
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
		{
			_chessBoard.InvalidMove();
			return;
		}
	}
}
