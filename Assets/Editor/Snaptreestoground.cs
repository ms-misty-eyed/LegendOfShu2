using UnityEngine;
using UnityEditor;

public class SnapTreesToMeshGround : EditorWindow
{
    private string treeTag = "Tree";
    private string trunkName = "Cylinder";
    private float sinkOffset = 0.05f;

    private bool useSelection = false;

    [MenuItem("Tools/Snap Trees (Mesh Ground)")]
    public static void ShowWindow()
    {
        GetWindow<SnapTreesToMeshGround>("Snap Trees Mesh");
    }

    private void OnGUI()
    {
        GUILayout.Label("Snap Trees to Mesh Ground", EditorStyles.boldLabel);

        useSelection = EditorGUILayout.Toggle("Use Selection", useSelection);

        if (!useSelection)
            treeTag = EditorGUILayout.TextField("Tree Tag", treeTag);

        trunkName = EditorGUILayout.TextField("Trunk Name", trunkName);
        sinkOffset = EditorGUILayout.FloatField("Sink Offset", sinkOffset);

        EditorGUILayout.Space();

        if (GUILayout.Button("Snap Trees", GUILayout.Height(35)))
            Snap();
    }

    private void Snap()
    {
        GameObject[] trees = useSelection
            ? Selection.gameObjects
            : GameObject.FindGameObjectsWithTag(treeTag);

        // Find ground collider
        MeshCollider ground = FindObjectOfType<MeshCollider>();

        if (ground == null)
        {
            Debug.LogError("❌ No MeshCollider found on ground.");
            return;
        }

        int snapped = 0;

        Undo.SetCurrentGroupName("Snap Trees Mesh");
        int group = Undo.GetCurrentGroup();

        foreach (var tree in trees)
        {
            Transform trunk = FindChild(tree.transform, trunkName);

            if (trunk == null)
                continue;

            Vector3 origin = trunk.position;

            // 🔥 Key trick: project point onto mesh collider
            Vector3 closestPoint = ground.ClosestPoint(origin);

            // Ensure we are actually on top of mesh (not inside side walls)
            Vector3 upOffset = Vector3.up * 10f;
            Vector3 projected = ground.ClosestPoint(origin + upOffset);

            float terrainY = Mathf.Max(closestPoint.y, projected.y);

            Undo.RecordObject(tree.transform, "Snap Tree");

            float offset = trunk.position.y - tree.transform.position.y;

            Vector3 pos = tree.transform.position;
            pos.y = terrainY - offset - sinkOffset;

            tree.transform.position = pos;

            snapped++;
        }

        Undo.CollapseUndoOperations(group);

        Debug.Log($"✅ Snapped {snapped} trees to mesh ground.");
    }

    private Transform FindChild(Transform parent, string name)
    {
        foreach (Transform t in parent.GetComponentsInChildren<Transform>())
        {
            if (t.name == name)
                return t;
        }
        return null;
    }
}