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

		if (finalRow == 0 && finalColumn == 7)
		{
			KingSlideMove(finalCheckBox, finalRow, finalColumn);
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

	private void KingSlideMove(GameObject finalCheckBox,int finalRow,int finalColumn)
	{
		//Rook's position must be [0][7] for white or [7][7] for black
		var maxIteration = Mathf.Abs(_kingsColumn - finalColumn);
		var stepCount = 0;
		for (var iteration = 1; iteration < maxIteration; iteration++)
		{
			var checkBox = _chessBoard.checkBoxPositions[finalRow][_kingsColumn + iteration];
			if (!checkBox.GetComponent<CheckBox>().isOccupied)
			{
				stepCount += 1;
			}
			else
			{
				_chessBoard.InvalidMove();
				return;
			}
		}
		if (stepCount == 2)
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
		}
	}
	private void QueenSlideMove(GameObject finalCheckBox)
	{
		//Rook's position must be [0][0] for white or [7][0] for black
	}
}
