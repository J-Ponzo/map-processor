using NodeEditorFramework;
using NodeEditorFramework.IO;
using NodeEditorFramework.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_NodeEditorInterface
{
	public NodeEditorUserCache canvasCache;
	public Action<GUIContent> ShowNotificationAction;

	// GUI
	public string sceneCanvasName = "";
	public float toolbarHeight = 20;

	// Modal Panel
	public bool showModalPanel;
	public Rect modalPanelRect = new Rect(20, 50, 250, 70);
	public Action modalPanelContent;

	// IO Format modal panel
	private ImportExportFormat IOFormat;
	private object[] IOLocationArgs;
	private delegate bool? DefExportLocationGUI(string canvasName, ref object[] locationArgs);
	private delegate bool? DefImportLocationGUI(ref object[] locationArgs);
	private DefImportLocationGUI ImportLocationGUI;
	private DefExportLocationGUI ExportLocationGUI;

	public void ShowNotification(GUIContent message)
	{
		if (ShowNotificationAction != null)
			ShowNotificationAction(message);
	}

	#region GUI

	public void DrawToolbarGUI()
	{
		GUILayout.BeginHorizontal(GUI.skin.GetStyle("toolbar"));

		if (GUILayout.Button("File", GUI.skin.GetStyle("toolbarDropdown"), GUILayout.Width(50)))
		{
			GenericMenu menu = new GenericMenu(NodeEditorGUI.useUnityEditorToolbar && !Application.isPlaying);

			// New Canvas filled with canvas types
			NodeCanvasManager.FillCanvasTypeMenu(ref menu, NewNodeCanvas, "New Canvas/");
			menu.AddSeparator("");

			// Import / Export filled with import/export types
			ImportExportManager.FillImportFormatMenu(ref menu, ImportCanvasCallback, "Import/");
			if (canvasCache.nodeCanvas.allowSceneSaveOnly)
			{
				menu.AddDisabledItem(new GUIContent("Export"));
			}
			else
			{
				ImportExportManager.FillExportFormatMenu(ref menu, ExportCanvasCallback, "Export/");
			}
			menu.AddSeparator("");

			// Show dropdown
			menu.Show(new Vector2(3, toolbarHeight + 3));
		}

		GUILayout.Space(10);
		GUILayout.FlexibleSpace();

		GUILayout.Label(new GUIContent(canvasCache.nodeCanvas.saveName,
										"Save Type: " + (canvasCache.nodeCanvas.livesInScene ? "Scene" : "Asset") + "\n" +
										"Save Path: " + canvasCache.nodeCanvas.savePath), GUI.skin.GetStyle("toolbarLabel"));
		GUILayout.Label(new GUIContent(canvasCache.typeData.DisplayString, "Canvas Type: " + canvasCache.typeData.DisplayString), GUI.skin.GetStyle("toolbarLabel"));


		GUI.backgroundColor = new Color(1, 0.3f, 0.3f, 1);
		/*if (GUILayout.Button("Reinit", GUI.skin.GetStyle("toolbarButton"), GUILayout.Width(100)))
		{
			NodeEditor.ReInit(true);
			NodeEditorGUI.CreateDefaultSkin();
			canvasCache.nodeCanvas.Validate();
		}*/
		if (Application.isPlaying)
		{
			GUILayout.Space(5);
			if (GUILayout.Button("Quit", GUI.skin.GetStyle("toolbarButton"), GUILayout.Width(100)))
				Application.Quit();
		}
		GUI.backgroundColor = Color.white;

		GUILayout.EndHorizontal();
		if (Event.current.type == EventType.Repaint)
			toolbarHeight = GUILayoutUtility.GetLastRect().yMax;
	}

	private void SaveSceneCanvasPanel()
	{
		GUILayout.Label("Save Canvas To Scene");

		GUILayout.BeginHorizontal();
		sceneCanvasName = GUILayout.TextField(sceneCanvasName, GUILayout.ExpandWidth(true));
		bool overwrite = NodeEditorSaveManager.HasSceneSave(sceneCanvasName);
		if (overwrite)
			GUILayout.Label(new GUIContent("!!!", "A canvas with the specified name already exists. It will be overwritten!"), GUILayout.ExpandWidth(false));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Cancel"))
			showModalPanel = false;
		if (GUILayout.Button(new GUIContent(overwrite ? "Overwrite" : "Save", "Save the canvas to the Scene")))
		{
			showModalPanel = false;
			if (!string.IsNullOrEmpty(sceneCanvasName))
				canvasCache.SaveSceneNodeCanvas(sceneCanvasName);
		}
		GUILayout.EndHorizontal();
	}

	public void DrawModalPanel()
	{
		if (showModalPanel)
		{
			if (modalPanelContent == null)
				return;
			GUILayout.BeginArea(modalPanelRect, GUI.skin.box);
			modalPanelContent.Invoke();
			GUILayout.EndArea();
		}
	}

	#endregion

	#region Menu Callbacks

	private void NewNodeCanvas(Type canvasType)
	{
		canvasCache.NewNodeCanvas(canvasType);
	}

	private void LoadSceneCanvasCallback(object canvas)
	{
		canvasCache.LoadSceneNodeCanvas((string)canvas);
		sceneCanvasName = canvasCache.nodeCanvas.name;
	}

	private void SaveSceneCanvasCallback()
	{
		modalPanelContent = SaveSceneCanvasPanel;
		showModalPanel = true;
	}

	private void ImportCanvasCallback(string formatID)
	{
		IOFormat = ImportExportManager.ParseFormat(formatID);
		if (IOFormat.RequiresLocationGUI)
		{
			ImportLocationGUI = IOFormat.ImportLocationArgsGUI;
			modalPanelContent = ImportCanvasGUI;
			showModalPanel = true;
		}
		else if (IOFormat.ImportLocationArgsSelection(out IOLocationArgs))
			canvasCache.SetCanvas(ImportExportManager.ImportCanvas(IOFormat, IOLocationArgs));
	}

	private void ImportCanvasGUI()
	{
		if (ImportLocationGUI != null)
		{
			bool? state = ImportLocationGUI(ref IOLocationArgs);
			if (state == null)
				return;

			if (state == true)
				canvasCache.SetCanvas(ImportExportManager.ImportCanvas(IOFormat, IOLocationArgs));

			ImportLocationGUI = null;
			modalPanelContent = null;
			showModalPanel = false;
		}
		else
			showModalPanel = false;
	}

	private void ExportCanvasCallback(string formatID)
	{
		IOFormat = ImportExportManager.ParseFormat(formatID);
		if (IOFormat.RequiresLocationGUI)
		{
			ExportLocationGUI = IOFormat.ExportLocationArgsGUI;
			modalPanelContent = ExportCanvasGUI;
			showModalPanel = true;
		}
		else if (IOFormat.ExportLocationArgsSelection(canvasCache.nodeCanvas.saveName, out IOLocationArgs))
			ImportExportManager.ExportCanvas(canvasCache.nodeCanvas, IOFormat, IOLocationArgs);
	}

	private void ExportCanvasGUI()
	{
		if (ExportLocationGUI != null)
		{
			bool? state = ExportLocationGUI(canvasCache.nodeCanvas.saveName, ref IOLocationArgs);
			if (state == null)
				return;

			if (state == true)
				ImportExportManager.ExportCanvas(canvasCache.nodeCanvas, IOFormat, IOLocationArgs);

			ImportLocationGUI = null;
			modalPanelContent = null;
			showModalPanel = false;
		}
		else
			showModalPanel = false;
	}

	#endregion
}
