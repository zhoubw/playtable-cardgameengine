/*
 * Card.cs
 * 
 * The basic card unit.
 * Supports a front and back image, as well as basic card functionality.
 * 
 * Card uses the following TouchScript Gestures: ReleaseGesture, TapGesture, TransforGesture.
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TouchScript.Gestures;

public class Card : MonoBehaviour {
	public GameObject collidedObject;
	public GameObject frontImage;
	public GameObject backImage;

	public int DeckLayer = 8;

	void OnEnable() {
		GetComponent<ReleaseGesture> ().Released += ReleaseHandler;
		GetComponent<TapGesture> ().Tapped += TapHandler;
	}

	void OnDisable() {
		GetComponent<ReleaseGesture> ().Released -= ReleaseHandler;
		GetComponent<TapGesture> ().Tapped -= TapHandler;
	}


	void OnTriggerEnter(Collider col) {
		collidedObject = col.gameObject;
	}

	void OnTriggerExit(Collider col) {
		collidedObject = null;
	}

	public void ReleaseHandler (object sender, System.EventArgs e) {
		if (collidedObject != null && collidedObject.layer == DeckLayer) {
			collidedObject.GetComponent<Deck> ().add (gameObject);
		}
	}

	public void TapHandler (object sender, System.EventArgs e) {
		flip ();
	}

	public void flip() {
		iTween.RotateAdd (gameObject, iTween.Hash ("x", 0, "y", 180, "z", 0));
	}

	public void setSpriteFront(Sprite s) {
		frontImage.GetComponent<Image> ().sprite = s;
	}

	public void setSpriteBack(Sprite s) {
		backImage.GetComponent<Image> ().sprite = s;
	}
}
