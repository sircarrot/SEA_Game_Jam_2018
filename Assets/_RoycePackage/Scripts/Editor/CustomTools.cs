#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Reflection;

public static class CustomTools
{
    [MenuItem("Custom Tools/Achexi/Delete Empty Folders")]
    public static void DeleteEmptyFolders()
    {
        string currentDirectory = Application.dataPath;
        string path = currentDirectory;

        while (TraverseDeleteDirectories(path) > 0) ;

        AssetDatabase.Refresh();
    }

    private static int TraverseDeleteDirectories(string path)
    {
        //DebugTools.DefaultLog("Checking " + path);
        if (path.Contains(".git")) return 0;
        string[] dirs = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);


        if (files != null && files.Length == 1 && files[0].EndsWith(".DS_Store"))
        {
            Debug.LogWarning("Deleting: " + files[0]);
            File.Delete(files[0]);
            files = Directory.GetFiles(path);
        }


        if ((dirs == null || dirs.Length == 0) && (files == null || files.Length == 0))
        {
            Debug.LogWarning("Deleting " + path);
            Directory.Delete(path);
            File.Delete(path + ".meta");
            return 1;
        }



        int n = 0;
        for (int i = 0; i < dirs.Length; i++)
        {
            n += TraverseDeleteDirectories(dirs[i]);
        }
        return n;
    }

    [MenuItem("Custom Tools/Achexi/Create Scriptable Object")]
    public static void CreateScriptableObject()
    {
        foreach (object o in Selection.objects)
        {
            MonoScript mono = o as MonoScript;
            if (mono == null)
                continue;

            System.Type type = mono.GetClass();
            if (!type.IsSubclassOf(typeof(ScriptableObject)))
                continue;

            var asset = ScriptableObject.CreateInstance(type);
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + type.ToString() + ".asset");
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }

    [MenuItem("Custom Tools/Royce/Create Enum")]
    public static void CreateEnum()
    {
        List<string> enumList = new List<string>();
        string enumName = null;

        foreach (object o in Selection.objects)
        {
            DebugUtils.DefaultLog(o.GetType().ToString());

            MonoScript mono = o as MonoScript;
            if (mono == null)
            {
                enumName = o.ToString();
                continue;
            }

            System.Type type = mono.GetClass();
            string[] typeNameArray = type.ToString().Split('.');
            if (typeNameArray.Length <= 0)
                continue;

            string typeName = typeNameArray[typeNameArray.Length - 1];

            enumList.Add(typeName);
            DebugUtils.DefaultLog(typeName);
        }
    }
    
}
#endif