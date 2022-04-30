using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
	public bool isOccupied;

	public bool isPieceWhite;
	private void Start()
	{
		if (transform.childCount != 2) return;
		isOccupied = true;
		if (transform.GetChild(1).CompareTag("WhitePiece"))
			isPieceWhite = true;
		else
			isPieceWhite = false;
	}
}
