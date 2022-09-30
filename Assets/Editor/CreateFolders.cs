using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class CreateFolders : EditorWindow
{
    private static string projectName = "PROJECT_NAME";
    [MenuItem("Assets/Create Default Folders")]
    private static void SetUpFolders()
    {
       // CreateFolders window = ScriptableObject.CreateInstance<CreateFolders>();
        CreateFolders window = (CreateFolders)EditorWindow.GetWindow(typeof(CreateFolders));
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 150);
        window.ShowPopup();
    }
    private static void CreateAllFolders()
    {
        List<string> folders = new List<string>
            {
            "Art",
            "Audio",
            "Editor",
            "Code",
            "Level",
            "UI",
            "Docs",
            "Project",
            "ThirdPartyAssets"
            };  
        foreach (string folder in folders)
        {
            if (!Directory.Exists("Assets/" + folder))
            {
                Directory.CreateDirectory("Assets/" + folder);
               // Directory.CreateDirectory("Assets/" + projectName + "/" + folder);
            }
        }
        MakeSubfolders("Art", new string[] {"Materials", "Meshes", "Textures", "Animations" });
        MakeSubfolders("Audio", new string[] { "Sound", "Music" });
        MakeSubfolders("Code", new string[] { "Scripts", "Shaders", "Test" });
        MakeSubfolders("Level", new string[] { "Prefabs", "Scenes", "Settings", "ScriptableObjects"});
        MakeSubfolders("UI", new string[] {"Assets", "Fonts", "Icon" });
        MakeSubfolders("Project", new string[] { "Settings", "Localization", "Actions" });
       
        /*
        List<string> uiFolders = new List<string>
        {
        "Assets",
        "Fonts",
        "Icon"
        };
        foreach (string subfolder in uiFolders)
        {
            if (!Directory.Exists("Assets/" + projectName + "/UI/" + subfolder))
            {
                Directory.CreateDirectory("Assets/" + projectName + "/UI/" + subfolder);
            }
        }
        */
        AssetDatabase.Refresh();
    }

    private static void MakeSubfolders(string mainFolder, string[] subfolders)
    {
        foreach (string subfolder in subfolders)
        {
            if (!Directory.Exists("Assets/"+mainFolder+"/" + subfolder))
            {
                Directory.CreateDirectory("Assets/" +mainFolder + "/" + subfolder);
            }
        }
    }
    void OnGUI()
    {
        EditorGUILayout.LabelField("Insert the Project name used as the root folder");
        projectName = EditorGUILayout.TextField("Project Name: ", projectName);
        this.Repaint();
        GUILayout.Space(70);
        if (GUILayout.Button("Generate!"))
        {
            CreateAllFolders();
            this.Close();
        }
    }
}