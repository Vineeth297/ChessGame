using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
			if (hit.collider.CompareTag("WhitePiece") || hit.collider.CompareTag("BlackPiece")) return;
			finalTransform = hit.collider.transform;
			//SoldierFunction(finalTransform);
			//Soldier(finalTransform.gameObject);
			var chessPiece = selectedTransform.GetComponent<IPieceMover>();
			chessPiece?.MoveThePiece(finalTransform.gameObject);
			print(chessPiece);
			//selectedTransform.GetComponent<Pawn>().MoveThePiece(finalTransform.gameObject);
		}

		if (pieceSelected) return;
		if (!hit.collider.CompareTag("WhitePiece") && !hit.collider.CompareTag("BlackPiece")) return;
		selectedTransform = hit.collider.gameObject.transform;
		pieceSelected = !pieceSelected;
	}
}
