using UnityEngine;

public class Queen : MonoBehaviour
{
	private ChessBoardClass _chessBoard;
	private void Start() => _chessBoard = ChessBoardClass.ChessBoard;

	public void MoveThePiece(GameObject finalCheckBox)
	{
		print("KnightMoved");
		QueenMove(finalCheckBox);
	}

	private void QueenMove(GameObject finalCheckBox)
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
		
		//can Move like Bishop
		//can Move like Rook
		//can Move like King
	}
}
