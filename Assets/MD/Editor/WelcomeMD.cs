using UnityEngine;
using UnityEditor;
using System.IO;

namespace MD.Window
{
	public class WelcomeMD : EditorWindow
	{
		private bool showHelpBox = true;
		private Vector2 scrollPosition;
		private Vector2 fixedMinSize = new Vector2(380, 580);
		private Vector2 fixedMaxSize = new Vector2(380, 580);

		[MenuItem("MD/Welcome")]
		public static void ShowWindow()
		{
			GetWindow<WelcomeMD>("Welcome To MD");
		}

		private void OnEnable()
		{
			minSize = fixedMinSize;
			maxSize = fixedMaxSize;
		}

		private void OnGUI()
		{
			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

			GUILayout.BeginArea(new Rect(10, 50, 100, 30));
			GUILayout.EndArea();

			GUILayout.Space(10);
			EditorGUILayout.EndScrollView();

			GUILayout.BeginVertical();

			GUILayout.Label("Thanks for using our tool!");
			GUILayout.Label("The Tools You Have:");

			ShowToolStatus("Nodecanvas Tasks+", "Assets/MD/Task+/Editor/MDLoginNPP.cs");
			ShowToolStatus("Blackboard Free version", "Assets/MD/Blackboard+ Demo Version/Editor/MDLoginBFV.cs");
			ShowToolStatus("Blackboard+", "Assets/MD/Blackboard+/Editor/MD/Blackboard+/Editor/MDLoginB.cs");
			ShowToolStatus("Nodecanvas Quest+", "Assets/MD/Systems/Systems/QuestSystem/Editor/QuestLogin.cs");
			ShowToolStatus("Nodecanvas Status+", "Assets/MD/Systems/Systems/Status+/Editor/StatusLogin.cs");
			ShowToolStatus("Achievement+", "", Color.grey);

			GUILayout.FlexibleSpace();
			{
				ShowHelpBoxes();

				if (GUILayout.Button("MD Asset Site"))
				{
					Application.OpenURL("https://assetstore.unity.com/publishers/56743");
				}

				if (GUILayout.Button("Our Website"))
				{
					Application.OpenURL("https://devvalues.com/");
				}

				if (GUILayout.Button("Discord Join We Will Help You :D"))
				{
					Application.OpenURL("https://discord.gg/8z3HWzangb");
				}
				if (GUILayout.Button("documentation"))
				{
					Application.OpenURL("https://md-assets.gitbook.io/md-assets/all-assets");
				}

				if (GUILayout.Button("Close"))
				{
					Close();
				}
			}
			GUILayout.EndVertical();
		}

		private void ShowToolStatus(string toolName, string toolFilePath, Color? color = null)
		{
			Rect rect = GUILayoutUtility.GetRect(new GUIContent(toolName), EditorStyles.label);
			Color backgroundColor = color ?? (File.Exists(toolFilePath) ? Color.green : Color.red);
			EditorGUI.DrawRect(rect, backgroundColor);
			EditorGUI.LabelField(rect, toolName);
		}

		private void ShowHelpBoxes()
		{
			EditorGUILayout.HelpBox("Blackboard Free Version On The Way :D", MessageType.Info);
			EditorGUILayout.HelpBox("Blackboard organizes outfit to go on the way :D", MessageType.Info);
			EditorGUILayout.HelpBox("IF Asset Color is Green: Happy Use", MessageType.Info);
			EditorGUILayout.HelpBox("IF Asset Color is Red: You Don't Have This Assets", MessageType.Info);
			EditorGUILayout.HelpBox("IF Asset Color is Gray: Soon Will be Published", MessageType.Info);
		}
	}
}