/*
 * Deck.cs
 * 
 * A Deck that holds and stacks Cards.
 * Includes basic functionality such as dealing, shuffling and recalling cards.
 * 
 * Deck uses the following TouchScript Gestures: TransformGesture, TapGesture and
 * LongPressGesture.
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using TouchScript.Behaviors;

public class Deck : MonoBehaviour {

	public GameObject cardPrefab;

	public List<GameObject> cards = new List<GameObject>();
	public List<GameObject> cardLibrary = new List<GameObject>();

	void OnEnable() {
		GetComponent<TransformGesture>().TransformStarted += TransformHandler;
		GetComponent<TapGesture> ().Tapped += TapHandler;
		GetComponent<LongPressGesture> ().LongPressed += LongPressHandler;
	}

	void OnDisable() {
		GetComponent<TransformGesture>().TransformStarted -= TransformHandler;
		GetComponent<TapGesture> ().Tapped -= TapHandler;
		GetComponent<LongPressGesture> ().LongPressed -= LongPressHandler;
	}

	public void TransformHandler (object sender, System.EventArgs e) {
		if (GetComponent<Transformer>() != null) {
			Destroy (GetComponent<Transformer> ());
		}
		if (GetComponent<TransformGesture> ().NumTouches == 2) {
			gameObject.AddComponent<Transformer> ();
		} else {
			TransformGesture gesture = GetComponent<TransformGesture> ();
			deal (Camera.main.ScreenToWorldPoint(gesture.ActiveTouches[0].Position));
			gesture.Cancel(true, true);
			if (gesture.State != Gesture.GestureState.Changed && gesture.State != Gesture.GestureState.Began) return;
		}
	}

	public void TapHandler (object sender, System.EventArgs e) {
		recall ();
	}

	public void LongPressHandler (object sender, System.EventArgs e) {
		shuffle ();
	}

	public GameObject createCard() {
		float dX = gameObject.transform.rotation.x;
		float dY = gameObject.transform.rotation.y - 180;
		float dZ = gameObject.transform.rotation.z;
		GameObject newCard = (GameObject) Instantiate (cardPrefab, transform.position, Quaternion.Euler(new Vector3(dX, dY, dZ)));
		add (newCard);
		register (newCard);
		return newCard;
	}

	public void add(GameObject card) {
		card.transform.parent = this.gameObject.transform;
		iTween.RotateTo(card, transform.eulerAngles, 0.5f);
		iTween.MoveTo (card, transform.position, 0.5f);
		cards.Add (card);
	}

	public void register(GameObject card) {
		cardLibrary.Add (card);
	}

	public GameObject deal(GameObject card, Vector3 pos) {
		cards.Remove (card);
		card.transform.SetParent (null);
		card.transform.position = pos;
		return card;
	}

	public GameObject deal(Vector3 pos) {
		pos.z = 0;
		if (!isEmpty ()) {
			return deal (cards [cards.Count - 1], pos);
		}
		return null;
	}

	public void recall() {
		foreach (GameObject card in cardLibrary) {
			if (!cards.Contains (card)) {
				add (card);
			}
		}
	}

	public void shuffle() {
		if (cards.Count <= 1) {
			return;
		}
		for (int i = 0; i < cards.Count; i++) {
			System.Random r = new System.Random ();
			int first = r.Next (cards.Count);
			int second = r.Next (cards.Count);
			iTween.ShakePosition (cards [i], new Vector3 (1f, 0f, 0f), 0.5f);
			swap (first, second);
			
		}
	}

	public void flip() {
		gameObject.transform.Rotate (new Vector3 (0, 180, 0));
		foreach (GameObject card in cards) {
			iTween.RotateTo(card, transform.eulerAngles, 0.5f);
			iTween.MoveTo (card, transform.position, 0.5f);
		}
	}

	public void swap(int a, int b) {
		GameObject tmp = cards [a];
		cards [a] = cards [b];
		cards [b] = tmp;
	}

	public bool isEmpty() {
		return cards.Count == 0;
	}
}
