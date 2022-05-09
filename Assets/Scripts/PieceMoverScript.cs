using UnityEngine;
using UnityEngine.SceneManagement;

public class PieceMoverScript : MonoBehaviour
{
	private Camera _camera;
	public LayerMask layerMask;

	public Transform selectedTransform;
	public Transform finalTransform;

	public bool pieceSelected;

	
	private void Start() => _camera = Camera.main;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
		if (!Input.GetMouseButtonDown(0)) return;
		
		SelectThePiece();
	}

	private void SelectThePiece()
	{
		var ray = _camera.ScreenPointToRay(Input.mousePosition);

		if (!Physics.Raycast(ray, out var hit, 50f, layerMask)) return;
		if (pieceSelected)
		{
			//if (hit.collider.CompareTag("WhitePiece") || hit.collider.CompareTag("BlackPiece")) return;
			if (hit.collider.CompareTag("WhitePiece") || hit.collider.CompareTag("BlackPiece"))
			{
				if (hit.collider.transform == selectedTransform) return;
				finalTransform = hit.collider.transform.parent;
			}
			else
			{
				pieceSelected = !pieceSelected;
				finalTransform = hit.collider.transform;
			}
			
			var chessPiece = selectedTransform.GetComponent<IPieceMover>();
			chessPiece?.MoveThePiece(finalTransform.gameObject);
			print(chessPiece);
		}

		if (pieceSelected) return;
		
		if (!hit.collider.CompareTag("WhitePiece") && !hit.collider.CompareTag("BlackPiece")) return;
		selectedTransform = hit.collider.gameObject.transform;
		selectedTransform.parent.GetComponent<CheckBox>().CheckBoxSelected();
		pieceSelected = !pieceSelected;
	}
}
