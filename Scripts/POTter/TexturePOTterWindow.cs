using UnityEngine;
using UnityEditor;
using System.IO;

public class TexturePOTterWindow : EditorWindow
{
    private const string DefaultNewTextureName = "'texture name'_POT";
    private string _textureName = DefaultNewTextureName;
    private string _path = Application.dataPath;
    private string _outputMSG = "";
    private Texture2D _texture;

    [MenuItem("Window/TexturePOTter")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<TexturePOTterWindow>("TexturePOTter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Texture save folder:", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(_path, EditorStyles.label);
            if (GUILayout.Button("Browse", GUILayout.Width(100f)))
            {
                _path = EditorUtility.OpenFolderPanel("TexturePOTter", Application.dataPath, "");
            }
        }
        GUILayout.EndHorizontal();

        _textureName = EditorGUILayout.TextField("New texture name", _textureName);
        _texture = (Texture2D)EditorGUILayout.ObjectField(_texture, typeof(Texture2D), false);

        GUILayout.Space(10);

        if (GUILayout.Button("Rest folder"))
        {
            _path = Application.dataPath;
            _outputMSG = "Folder rested";
        }

        if (GUILayout.Button("Rest name"))
        {
            _textureName = DefaultNewTextureName;
            _outputMSG = "Name rested";
        }

        if (GUILayout.Button("Process"))
        {
            if (_texture == null) 
            {
                _outputMSG = "The texture is null. Please set it.";
            }
            else
            {
                TexturePOTter potter = new TexturePOTter(_texture);
                string tn = _textureName == DefaultNewTextureName ? null : _textureName;
                _outputMSG = potter.Process(_path, tn);
                AssetDatabase.Refresh();
            }
        }

        GUILayout.Label(_outputMSG, EditorStyles.boldLabel);
    }
}
