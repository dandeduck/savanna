 using UnityEngine;
 using UnityEditor;
 
 public class NoSharedVertices : EditorWindow {
    private string error = "";
    
    [MenuItem("Window/No Shared Vertices")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(NoSharedVertices));
    }

    void OnGUI() {
        GUILayout.Label ("Creates a clone of the game object where the triangles\n" + 
            "do not share vertices");
        GUILayout.Space(20);

        if (GUILayout.Button ("Process")) {
            error = "";
            NoShared();
        }
        
        GUILayout.Space(20);
        GUILayout.Label(error);
    }
    
    void NoShared() {
        Transform selected = Selection.activeTransform;

        if (selected == null) {
            error = "No appropriate object selected.";
            Debug.Log (error);    
            return;
        }

        MeshFilter meshFilter = selected.GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null) {
            error = "No mesh on the selected object";
            Debug.Log (error);
            return;
        }
        
        // Create the duplicate game object
        GameObject gameObj = Instantiate (selected.gameObject) as GameObject;
        meshFilter = gameObj.GetComponent<MeshFilter>();
        Mesh mesh = Instantiate (meshFilter.sharedMesh) as Mesh;
        meshFilter.sharedMesh = mesh;
        Selection.activeObject = gameObj.transform;

        GraphicsUtil.RemoveSharedVertices(mesh);
        SaveNewAsset(gameObj, meshFilter);
    }

    private void SaveNewAsset(GameObject gameObj, MeshFilter meshFilter)
    {
        string path = "Assets/LowPolyMeshes/"+gameObj.name+"NoShared"+".asset";
        AssetDatabase.CreateAsset(meshFilter.sharedMesh, path);
        AssetDatabase.SaveAssets();
    }
 }