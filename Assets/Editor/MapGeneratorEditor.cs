using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (TestMapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TestMapGenerator generator = (TestMapGenerator) target;

        if (DrawDefaultInspector())
        {
            if (generator.autoUpdate)
                generator.GenerateMap();    
        }

        if (GUILayout.Button("Generate"))
            generator.GenerateMap();
    }
}
