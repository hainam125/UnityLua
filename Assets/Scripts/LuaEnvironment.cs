using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using System.IO;
using System;

public class LuaEnvironment : MonoBehaviour {
	[SerializeField]
	private string loadFile;

	private Script script;
	private Stack<MoonSharp.Interpreter.Coroutine> corStack;
	private GameState luaGameState;
	public GameState LuaGameState { get { return luaGameState; } }

	private void Awake() {
		luaGameState = new GameState();
	}

	private IEnumerator Start() {
		corStack = new Stack<MoonSharp.Interpreter.Coroutine>();

		Script.DefaultOptions.DebugPrint = s => Debug.Log(s.ToLower());
		UserData.RegisterAssembly();

		script = new Script(CoreModules.Preset_SoftSandbox);
		script.Globals["SetText"] = (Action<string>)LuaCommand.SetText;
		script.Globals["ShowButtons"] = (Action<string, string>)LuaCommand.ShowButtons;
		script.Globals["State"] = UserData.Create(luaGameState);

		yield return 1;

		LoadFile2(loadFile);
		AdvanceScript();
	}

	private void LoadFile1(string filename) {
		var filePath = Path.Combine(Application.streamingAssetsPath, loadFile);
		using (BufferedStream stream = new BufferedStream(new FileStream(filePath, FileMode.Open, FileAccess.Read))) {
			script.DoStream(stream);
		}
	}

	private void LoadFile2(string filename) {
		var filePath = Path.Combine(Application.streamingAssetsPath, loadFile);
		DynValue ret = DynValue.Nil;
		using (BufferedStream stream = new BufferedStream(new FileStream(filePath, FileMode.Open, FileAccess.Read))) {
			ret = script.DoStream(stream);
		}

		if (ret.Type == DataType.Function) {
			corStack.Push(script.CreateCoroutine(ret).Coroutine);
		}
	}

	public void AdvanceScript() {
		if (corStack.Count > 0) {
			var cor = corStack.Peek();
			var ret = cor.Resume();
			if (cor.State == CoroutineState.Dead) {
				corStack.Pop();
			}
			if (ret.Type == DataType.Function) {
				corStack.Push(script.CreateCoroutine(ret).Coroutine);
			}
		}
	}
}
