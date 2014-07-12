using UnityEngine;
using UnityEditor;
using System.Collections;

public class BrushPolygon2DWindow : EditorWindow
{
    private Vector2 _scrollPos;

    [MenuItem("Window/PolyTie/2D Brushes")]
    private static void init()
    {
        var test = EditorWindow.GetWindow<BrushPolygon2DWindow>("2D Brushes");
        Debug.Log(test);
    }


    void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUIStyle.none);
        // Polygons
        GUILayout.Label("Create 2D polygon objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Collider") == true)
            BrushPolygon2D.CreatePolygon();
        if (GUILayout.Button("Static") == true)
            BrushPolygon2D.CreateStaticPolygon();
        if (GUILayout.Button("Dynamic") == true)
            BrushPolygon2D.CreateDynamicPolygon();
        if (GUILayout.Button("Trigger") == true)
            BrushPolygon2D.CreateTriggerPolygon();
        if (GUILayout.Button("Moving") == true)
            BrushPolygon2D.CreateMovingPolygon();
        if (GUILayout.Button("Killing Zone") == true)
            BrushPolygon2D.CreateKillingZone();

        EditorGUILayout.Separator();
        GUILayout.Label("Create 2D circle objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Collider") == true)
            BrushCircle2D.CreateCircle();
        if (GUILayout.Button("Static") == true)
            BrushCircle2D.CreateStaticCircle();
        if (GUILayout.Button("Dynamic") == true)
            BrushCircle2D.CreateDynamicCircle();
        if (GUILayout.Button("Trigger") == true)
            BrushCircle2D.CreateTriggerCircle();
        if (GUILayout.Button("Moving") == true)
            BrushCircle2D.CreateMovingCircle();
        if (GUILayout.Button("Killing Zone") == true)
            BrushCircle2D.CreateKillingZone();

        EditorGUILayout.Separator();
        GUILayout.Label("Create 2D rectangle objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Collider") == true)
            BrushRectangle2D.CreateRectangle();
        if (GUILayout.Button("Static") == true)
            BrushRectangle2D.CreateStaticRectangle();
        if (GUILayout.Button("Dynamic") == true)
            BrushRectangle2D.CreateDynamicRectangle();
        if (GUILayout.Button("Trigger") == true)
            BrushRectangle2D.CreateTriggerRectangle();
        if (GUILayout.Button("Moving") == true)
            BrushRectangle2D.CreateMovingRectangle();
        if (GUILayout.Button("Killing Zone") == true)
            BrushRectangle2D.CreateKillingZone();

        EditorGUILayout.Separator();
        GUILayout.Label("Create 2D edge objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Collider") == true)
            BrushEdge2D.CreateEdge();
        if (GUILayout.Button("Static") == true)
            BrushEdge2D.CreateStaticEdge();
        if (GUILayout.Button("Trigger") == true)
            BrushEdge2D.CreateTriggerEdge();
        if (GUILayout.Button("Moving") == true)
            BrushEdge2D.CreateMovingEdge();
        if (GUILayout.Button("Killing Zone") == true)
            BrushEdge2D.CreateKillingZone();

        EditorGUILayout.Separator();
        GUILayout.Label("Create 2D path object", EditorStyles.boldLabel);

        if (GUILayout.Button("Path") == true)
            BrushVectorPath2D.CreatePath();
        EditorGUILayout.EndScrollView();
    }
}
