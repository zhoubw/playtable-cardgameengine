/*
 * GameManager.cs
 * 
 * The main handler of cards and decks.
 * 
 */

using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour {

	public GameObject deckPrefab;
	public GameObject cardPrefab;

    /*
	void Awake() {
		// Example of creating a deck of 52 cards at (0, 0)
		GameObject newDeck = createDeck (0, 0);
		for (int i = 0; i < 52; i++) {
			newDeck.GetComponent<Deck> ().createCard ();
		}
		newDeck.GetComponent<Deck> ().flip ();
	}
    */

	public GameObject createDeck(Vector2 pos) {
		return (GameObject) Instantiate (deckPrefab, new Vector3 (pos.x, pos.y, deckPrefab.transform.position.z), deckPrefab.transform.rotation);
	}

	public GameObject createDeck(float x, float y) {
		Vector2 pos = new Vector2 (x, y);
		return createDeck (pos);
	}

	public GameObject createCard(Vector2 pos) {
		return (GameObject)Instantiate (cardPrefab, new Vector3 (pos.x, pos.y, cardPrefab.transform.position.z), cardPrefab.transform.rotation);
	}

	public GameObject createCard(float x, float y) {
		Vector2 pos = new Vector2 (x, y);
		return createCard (pos);
	}

	public GameObject createCardFromJSON(float x, float y, string jsonPath, string key) {
		ImageLoader il = new ImageLoader ();
		JSONObject fullJSON = il.parseJSON (jsonPath);
		ImageLoader.PathMap pMap = il.getPathMap (key, fullJSON);
		GameObject newCard = createCard (x, y);
		Sprite back = Resources.Load<Sprite> ("cardSprites/" + pMap.imagePathBack);
		Sprite front = Resources.Load<Sprite> ("cardSprites/" + pMap.imagePathFront);
		newCard.GetComponent<Card> ().setSpriteBack (back);
		newCard.GetComponent<Card> ().setSpriteFront (front);
		return newCard;
	}

	public GameObject createCardFromJSON(Vector2 pos, string jsonPath, string key) {
		return createCardFromJSON (pos.x, pos.y, jsonPath, key);
	}
}
