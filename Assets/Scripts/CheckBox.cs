using UnityEngine;

public class CheckBox : MonoBehaviour
{
	public bool isOccupied;

	public bool isPieceWhite;
	private void Start()
	{
		if (transform.childCount != 2) return;
		isOccupied = true;
		isPieceWhite = transform.GetChild(1).CompareTag("WhitePiece");
	}
}
