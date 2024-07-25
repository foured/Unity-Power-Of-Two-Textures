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
    private bool _copyData = true;

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
        _copyData = EditorGUILayout.Toggle("Copy texture settings", _copyData);
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

        bool isReadable = _texture == null ? false : _texture.isReadable;

        if (GUILayout.Button($"Set read/write to {!isReadable}"))
        {
            if(_texture == null)
            {
                _outputMSG = "Set texture befor setting parameters";
            }
            else
            {
                TexturePOTter.SetReadWriteEnable(_texture, !isReadable);
                AssetDatabase.Refresh();
                _outputMSG = $"Read/write was correctly set to {!isReadable}";
            }
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
                if (potter.SetTextureData())
                {
                    string tn = _textureName == DefaultNewTextureName ? null : _textureName;
                    _outputMSG = potter.Process(_path, tn);
                    if (_copyData)
                        potter.CopyData();
                    AssetDatabase.Refresh();
                }
                else
                {
                    _outputMSG = "Can`t read texture data. Try enable read write texture parameter";
                }
            }
        }

        GUILayout.Label(_outputMSG, EditorStyles.boldLabel);
    }
}
