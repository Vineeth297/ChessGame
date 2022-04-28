using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PieceMoverScript : MonoBehaviour
{
	private Camera _camera;
	public LayerMask layerMask;

	public Transform selectedTransform;
	public Transform finalTransform;

	public bool pieceSelected;

	public List<GameObject> checkBoxes;

	public Vector3[] checkBoxPositions = new Vector3[64];
	
	private List<List<GameObject>> _checkBoxPositions;
	private Vector3[,] _boxes = new Vector3[8,8];

	private void Start()
	{
		_camera = Camera.main;
		_checkBoxPositions = new List<List<GameObject>>();
		FillTheBoxes();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
		if (!Input.GetMouseButtonDown(0)) return;
		
		var ray =  _camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out var hit, 50f,layerMask))
		{
			if (pieceSelected)
			{
				if(hit.collider.CompareTag("Piece")) return;
				finalTransform = hit.collider.transform;
				//SoldierFunction(finalTransform);
				Rook(finalTransform.gameObject);
			}

			if (!pieceSelected)
			{
				if (hit.collider.CompareTag("Piece"))
				{
					selectedTransform = hit.collider.gameObject.transform;
					pieceSelected = !pieceSelected;
				}
			}
		}
	}

	private void Soldier(GameObject finalCheckBox)
	{
		//Get the CurrentIndex
		SearchTheBoard(out var currentRow,out var currentColumn,selectedTransform.parent.gameObject);
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
		SearchTheBoard(out var finalRow, out var finalColumn, finalCheckBox.gameObject);
		if (finalRow == -1 || finalColumn == -1)
		{
			print("Index Not Found!!");
			return;
		}
		
		if (finalRow == validMoveRow && finalColumn == validMoveColumn)
		{
			//if target checkBox is not occupied => then move
			ValidateOccupancyAndMove(finalCheckBox.transform);
		}
		else if ((finalRow == killMove1Row && finalColumn == killMove1Column) || 
				 (finalRow == killMove2Row && finalColumn == killMove2Column))
		{
			//if the positions are occupied by enemy => kill
			KillTheEnemy(finalCheckBox.transform);
		}
		else
			InvalidMove();
	}

	private void Rook(GameObject finalMove)
	{
		//Vertical Move
		//row changes, column stays the same
		SearchTheBoard(out var currentRow,out var currentColumn,selectedTransform.parent.gameObject);
		if (currentRow == -1 || currentColumn == -1)
		{
			print("Index Not Found!!");
			return;
		}
		
		SearchTheBoard(out var finalRow, out var finalColumn, finalMove);
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
					ValidateOccupancyAndMove(finalMove.transform);
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
					ValidateOccupancyAndMove(finalMove.transform);
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
					ValidateOccupancyAndMove(finalMove.transform);
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
					ValidateOccupancyAndMove(finalMove.transform);
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
			InvalidMove();
		}
	}

	private bool IfAllCheckBoxesInThePathAreEmpty(int moveDirectionIndex, int finalColumn, int finalRow, int currentRow, int currentColumn)
	{
		GameObject checkBox;
		int finalIndex;
		if (currentColumn == finalColumn)
		{
			checkBox = _checkBoxPositions[moveDirectionIndex][finalColumn];
			finalIndex = finalRow;
		}
		else
		{
			checkBox = _checkBoxPositions[finalRow][moveDirectionIndex];
			finalIndex = finalColumn;
		}
		
		//print(checkBox);
		if (!checkBox.GetComponent<CheckBox>().isOccupied)
		{
			//print(moveDirectionIndex == finalRow - 1);
			if (moveDirectionIndex == finalIndex - 1)
			{
				//check for final indexes occupancy
				var finalCheckBox = _checkBoxPositions[finalRow][finalColumn];
				ValidateOccupancyAndMove(finalCheckBox.transform);
			}
			else if (moveDirectionIndex == finalIndex + 1)
			{
				var finalCheckBox = _checkBoxPositions[finalRow][finalColumn];
				ValidateOccupancyAndMove(finalCheckBox.transform);
			}
			else return false;
		}
		return true;
	}

	private void RookFunction(Transform finalMove)
	{
		//final position is divisible by 8
		var currentIndex = Array.IndexOf(checkBoxPositions, selectedTransform.parent.position);
		//var index = FindTheIndex();
		var finalPositionIndex = Array.IndexOf(checkBoxPositions, finalMove.position);
		var remainder = (Mathf.Abs(currentIndex - finalPositionIndex)) % 8;
		//print(index);
		//Vertical Switch
		if (remainder == 0)
		{
			var maxIterationNumber = Mathf.Abs(currentIndex - finalPositionIndex) / 8;
			var numberOfEmptyBoxes = 0;
			
			for (var i = 1; i <= maxIterationNumber; i++)
			{
				
				if (currentIndex < finalPositionIndex)
				{
					if (!checkBoxes[currentIndex + (8 * i)].GetComponent<CheckBox>().isOccupied)
					{
						//	print(checkBoxes[currentIndex + (8 * i)].GetComponent<CheckBox>().isOccupied);
						numberOfEmptyBoxes += 1;
					}
					else
					{
						InvalidMove();
						return;
					}
				}
				if (currentIndex > finalPositionIndex)
				{
					if (!checkBoxes[currentIndex - (8 * i)].GetComponent<CheckBox>().isOccupied)
					{
						//print(checkBoxes[currentIndex - (8 * i)].GetComponent<CheckBox>().isOccupied);
						numberOfEmptyBoxes += 1;
					}
					else
					{
						InvalidMove();
						return;
					}
				}
			}

			if(numberOfEmptyBoxes == maxIterationNumber)
			{
				//if there is enemy on final Check box => kill
				ValidateOccupancyAndMove(finalMove);
				//print("CanMove");
			}
		}
		//Horizontal Switch
		else if(MathF.Abs(currentIndex - finalPositionIndex) < 8)
		{
			var maxIterationNumber = Mathf.Abs(currentIndex - finalPositionIndex);
			var numberOfEmptyBoxes = 0;
			print("Max Iter Number " + maxIterationNumber);
			for (var i = 1; i <= maxIterationNumber; i++)
			{
				print(currentIndex);
				if (currentIndex < finalPositionIndex)
				{
					if (!checkBoxes[currentIndex + i].GetComponent<CheckBox>().isOccupied)
					{
						//print(checkBoxes[currentIndex + i].GetComponent<CheckBox>().isOccupied);
						//move the piece
						numberOfEmptyBoxes += 1;
					}
					else
					{
						InvalidMove();
						return;
					}
				}
				if (currentIndex > finalPositionIndex)
				{
					if (!checkBoxes[currentIndex - i].GetComponent<CheckBox>().isOccupied)
					{
						//print(checkBoxes[currentIndex - i].GetComponent<CheckBox>().isOccupied);
						//move the piece
						numberOfEmptyBoxes += 1;
					}
					else
					{
						InvalidMove();
						return;
					}
				}
			}
			//print("Number of Empty Boxes " + numberOfEmptyBoxes);
			
			if(numberOfEmptyBoxes == maxIterationNumber)
			{
				print("CanMove");
				//ValidateOccupancyAndMove(finalMove);
				MoveTheRook(currentIndex,finalPositionIndex);
				print("CanMove");

			}
			else InvalidMove();
			
		}
	}

	private void MoveTheRook(int currentIndex, int finalPositionIndex)
	{
		/*var currentIndex = Array.IndexOf(checkBoxPositions, selectedTransform.parent.position);
		var finalPositionIndex = Array.IndexOf(checkBoxPositions, finalTransform.position);*/

		var minIndex = 0;
		var maxIndex = 0;
		
		if (finalPositionIndex == (currentIndex + 7) ||
		    finalPositionIndex == (currentIndex + 9) ||
		    finalPositionIndex == (Mathf.Abs(currentIndex - 7)) ||
		    finalPositionIndex == (Mathf.Abs(currentIndex - 9)))
		{
			InvalidMove();
			return;
		}

		if (currentIndex >= 0 && currentIndex < 8)
		{
			minIndex = 0;
			maxIndex = 7;
		}
		if (currentIndex >= 8 && currentIndex < 16)
		{
			minIndex = 8;
			maxIndex = 15;
		}
		if (currentIndex >= 16 && currentIndex < 24)
		{
			minIndex = 16;
			maxIndex = 23;
		}
		if (currentIndex >= 24 && currentIndex < 32)
		{
			minIndex = 24;
			maxIndex = 31;
		}
		if (currentIndex >= 32 && currentIndex < 40)
		{
			minIndex = 32;
			maxIndex = 39;
		}
		if (currentIndex >= 40 && currentIndex < 48)
		{
			minIndex = 40;
			maxIndex = 47;
		}
		if (currentIndex >= 48 && currentIndex < 56)
		{
			minIndex = 48;
			maxIndex = 56;
		}
		if (currentIndex >= 56 && currentIndex < 64)
		{
			minIndex = 56;
			maxIndex = 63;
		}
		
		//if the final position falls between min and max => move the piece
		print(minIndex);
		print(maxIndex);
		print(finalPositionIndex);
		if (finalPositionIndex >= minIndex && finalPositionIndex <= maxIndex)
		{
			print("MOveMethod");
			MoveThePiece(finalTransform);
		}
		else return;
	}

	private void FillTheBoxes()
	{
		for (var i = 0; i < 8; i++)
		{
			var newList = new List<GameObject>();
			for (var j = 0; j < 8; j++)
			{
				newList.Add(checkBoxes[(i * 8) + j]);
				Debug.DrawRay(newList[j].transform.position, Vector3.up, Color.red, 2f);
				//print(newList[j]);
			}
			_checkBoxPositions.Add(newList);
		}
	}

	private void SearchTheBoard(out int rowNum, out int columnNum,GameObject checkBox)
	{
		var rowNumber = -1;
		var columnNumber = -1;
		foreach (var column in _checkBoxPositions)
		{
			rowNumber++;
			
			var idx = column.IndexOf(checkBox);
			
			if(idx == -1) continue;

			columnNumber = idx;
			break;
		}

		columnNum = columnNumber;
		rowNum = rowNumber;
	}

	private void ValidateOccupancyAndMove(Transform finalMove)
	{
		if(!finalMove.GetComponent<CheckBox>().isOccupied)
		{
			MoveThePiece(finalMove);
		}
		else
			KillTheEnemy(finalMove);
	}

	private void MoveThePiece(Transform finalMove)
	{
		selectedTransform.position = finalMove.GetChild(0).position;
		selectedTransform.parent.GetComponent<CheckBox>().isOccupied = false;
		pieceSelected = !pieceSelected;
		selectedTransform.parent = finalMove;
		finalMove.GetComponent<CheckBox>().isOccupied = true;
		selectedTransform = null;
		finalTransform = null;
	}

	private void KillTheEnemy(Transform finalMove)
	{
		if (finalMove.childCount == 2)
		{
			//if the target piece is not of same color => kill
			if (selectedTransform.GetComponent<Soldier>().isWhite !=
				finalTransform.GetChild(1).GetComponent<Soldier>().isWhite)
			{
				selectedTransform.position = finalMove.GetChild(0).position;
				selectedTransform.parent.GetComponent<CheckBox>().isOccupied = false;
				pieceSelected = !pieceSelected;
				finalTransform.GetChild(1).gameObject.SetActive(false);
				finalTransform.GetChild(1).parent = null;
				selectedTransform.parent = finalMove;
				
			}
			else
				InvalidMove();
		}
		//else invalid move
		else
			InvalidMove();
	}

	private static void InvalidMove()
	{
		print("Invalid Move");
	}
}
