using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_NodeEditor : MonoBehaviour
{
	// Startup-canvas, cache and interface
	//public NodeCanvas assetSave;
	//public string sceneSave;
	private NodeEditorUserCache canvasCache;
	private MP_NodeEditorInterface editorInterface;

	// GUI rects
	public bool fullscreen = false;
	public Rect canvasRect = new Rect(50, 50, 900, 400);
	public Rect rect { get { return fullscreen ? new Rect(0, 0, Screen.width, Screen.height) : canvasRect; } }

    private void Awake()
    {
		System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
		customCulture.NumberFormat.NumberDecimalSeparator = ".";
		System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
	}
    private void Start()
	{
		NormalReInit();
	}

	private void Update()
	{
		NodeEditor.Update();
	}

	private void NormalReInit()
	{
		NodeEditor.ReInit(false);
		AssureSetup();
		canvasCache.NewNodeCanvas(typeof(MP_NodeCanvas));
		if (canvasCache.nodeCanvas)
			canvasCache.nodeCanvas.Validate();
	}

	private void AssureSetup()
	{
		if (canvasCache == null)
		{ // Create cache and load startup-canvas
			canvasCache = new NodeEditorUserCache();
			//if (assetSave != null)
			//	canvasCache.SetCanvas(NodeEditorSaveManager.CreateWorkingCopy(assetSave));
			//else if (!string.IsNullOrEmpty(sceneSave))
			//	canvasCache.LoadSceneNodeCanvas(sceneSave);
		}
		canvasCache.AssureCanvas();
		if (editorInterface == null)
		{ // Setup editor interface
			editorInterface = new MP_NodeEditorInterface();
			editorInterface.canvasCache = canvasCache;
		}
	}

	private void OnGUI()
	{
		// Initiation
		NodeEditor.checkInit(true);
		if (NodeEditor.InitiationError)
		{
			GUILayout.Label("Node Editor Initiation failed! Check console for more information!");
			return;
		}
		AssureSetup();

		// Start Overlay GUI for popups (before any other GUI)
		OverlayGUI.StartOverlayGUI("RTNodeEditor");

		// Set root rect (can be any number of arbitrary groups, e.g. a nested UI, but at least one)
		GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

		// Begin Node Editor GUI and set canvas rect
		NodeEditorGUI.StartNodeGUI(false);
		canvasCache.editorState.canvasRect = new Rect(rect.x, rect.y + editorInterface.toolbarHeight, rect.width, rect.height - editorInterface.toolbarHeight);

		try
		{ // Perform drawing with error-handling
			NodeEditor.DrawCanvas(canvasCache.nodeCanvas, canvasCache.editorState);
		}
		catch (UnityException e)
		{ // On exceptions in drawing flush the canvas to avoid locking the UI
			canvasCache.NewNodeCanvas();
			NodeEditor.ReInit(true);
			Debug.LogError("Unloaded Canvas due to exception in Draw!");
			Debug.LogException(e);
		}

		// Draw Interface
		GUILayout.BeginArea(rect);
		editorInterface.DrawToolbarGUI();
		GUILayout.EndArea();
		editorInterface.DrawModalPanel();

		// End Node Editor GUI
		NodeEditorGUI.EndNodeGUI();

		// End root rect
		GUI.EndGroup();

		// End Overlay GUI and draw popups
		OverlayGUI.EndOverlayGUI();
	}
}
