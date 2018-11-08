using System.Collections;
using System.Collections.Generic;
using System.IO;
using MoonSharp.Interpreter;
using UnityEngine;

public class Dummy : MonoBehaviour {
	private void Start() {
		StartCoroutine(LuaFromStreamingAssets());
	}

	private void SimpleExample() {
		// redefine print to print in lowercase, for all new scripts
		Script.DefaultOptions.DebugPrint = s => Debug.Log(s.ToLower());
		Script script = new Script();
		DynValue fn = script.LoadString("print 'Hello, World!'");
		fn.Function.Call(); // this prints "hello, world!"
	}

	private double LuaFromResources() {
		var textAsset = Resources.Load<TextAsset>("TestScript0");
		string codeScript = textAsset.text;
		Script script = new Script();
		script.DoString(codeScript);
		DynValue luaFactFunction = script.Globals.Get("fact");
		DynValue res = script.Call(luaFactFunction, 5);
		Debug.Log(res.Number);
		return res.Number;
	}

	private IEnumerator LuaFromStreamingAssets() {
		string codeScript = string.Empty;
		var filePath = Path.Combine(Application.streamingAssetsPath, "TestScript0.txt");
		if (filePath.Contains("://") || filePath.Contains(":///")) {
			using (var www = new WWW(filePath)) {
				yield return www;
				codeScript = www.text;
			}
		}
		else {
			codeScript = File.ReadAllText(filePath);
		}
		Script script = new Script();
		script.DoString(codeScript);
		DynValue luaFactFunction = script.Globals.Get("fact");
		DynValue res = script.Call(luaFactFunction, 5);
		Debug.Log(res.Number);
	}
}
