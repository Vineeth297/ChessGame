using UnityEngine;

public class Knight : MonoBehaviour, IPieceMover    
{
    private ChessBoardClass _chessBoard;
    private void Start() => _chessBoard = ChessBoardClass.ChessBoard;

    public void MoveThePiece(GameObject finalCheckBox)
    {
        print("KnightMoved");
        KnightMove(finalCheckBox);
    }

    private void KnightMove(GameObject finalCheckBox)
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

        if (finalRow == currentRow + 1 && finalColumn == currentColumn - 2 ||
            finalRow == currentRow + 2 && finalColumn == currentColumn - 1 ||
            finalRow == currentRow + 2 && finalColumn == currentColumn + 1 ||
            finalRow == currentRow + 1 && finalColumn == currentColumn + 2 ||
            finalRow == currentRow - 1 && finalColumn == currentColumn - 2 ||
            finalRow == currentRow - 2 && finalColumn == currentColumn - 1 ||
            finalRow == currentRow - 1 && finalColumn == currentColumn + 2 ||
            finalRow == currentRow - 2 && finalColumn == currentColumn + 1)
        {
            //check occupancy and Move
            _chessBoard.ValidateOccupancyAndMove(finalCheckBox.transform);
        }
		else
			_chessBoard.InvalidMove();
    }
}
