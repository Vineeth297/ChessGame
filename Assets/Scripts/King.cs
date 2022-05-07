using UnityEngine;

public class King : MonoBehaviour, IPieceMover
{
	private ChessBoardClass _chessBoard;
	private void Start() => _chessBoard = ChessBoardClass.ChessBoard;

	public void MoveThePiece(GameObject finalCheckBox) => KingMove(finalCheckBox);

	private void KingMove(GameObject finalCheckBox)
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
}
