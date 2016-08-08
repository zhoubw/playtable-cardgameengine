using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;


public class ImageLoader : MonoBehaviour {

	/*
	void Awake() {
		Debug.LogError (getPathMap ("card1", parseJSON ("spriteIndex")).name);
	}
	*/

	[Serializable]
	public class PathMap {
		public string name;
		public string imagePathFront;
		public string imagePathBack;
		public int count;
	}

	public JSONObject parseJSON(string jsonPath) {
		TextAsset asset = Resources.Load (jsonPath) as TextAsset;
		JSONObject textToJson = new JSONObject (asset.text);
		return textToJson;
	}

	public PathMap getPathMap(string key, JSONObject json) {
		JSONObject block = json [key];
		PathMap map = JsonUtility.FromJson<PathMap> (block.ToString());
		return map;
	}
}
