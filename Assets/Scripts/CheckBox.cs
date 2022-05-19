using UnityEngine;

public class CheckBox : MonoBehaviour
{
	public bool isOccupied;

	public bool isPieceWhite;

	public MeshRenderer mesh;
	public Color defaultCheckBoxColor;
	private void Start()
	{
		mesh = GetComponent<MeshRenderer>();
		defaultCheckBoxColor = mesh.material.color;
		
		if (transform.childCount != 2) return;
		isOccupied = true;
		isPieceWhite = transform.GetChild(1).CompareTag("WhitePiece");
	}

	public void CheckBoxSelected() => mesh.material.color = Color.yellow;

	public void CheckBoxDeSelected() => mesh.material.color = defaultCheckBoxColor;

	public void ResetOccupancy() => isOccupied = !isOccupied;
}
