using UnityEngine;
using UnityEditor;

public class ForestFixer : MonoBehaviour
{
    [MenuItem("Tools/THE ABSOLUTE FINAL FIX")]
    public static void AddColliders()
    {
        // 1. Grab everything in the scene
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        int count = 0;

        foreach (GameObject go in allObjects)
        {
            string name = go.name.ToLower();

            // 2. This catches 'tree', 'tree.001', 'tree_spring', etc.
            if (name.Contains("tree") && !name.Contains("cone"))
            {
                // Find the trunk (usually the first child mesh that isn't a leaf)
                MeshRenderer trunkMesh = go.GetComponentInChildren<MeshRenderer>();
                
                if (trunkMesh != null && !trunkMesh.name.ToLower().Contains("cone"))
                {
                    CapsuleCollider col = trunkMesh.gameObject.GetComponent<CapsuleCollider>();
                    if (col == null) col = trunkMesh.gameObject.AddComponent<CapsuleCollider>();

                    // --- THE BLENDER-TO-UNITY MAGIC SETTINGS ---
                    col.direction = 2; // Upright on the Z-axis (local to your model)
                    col.height = 6.0f; // Made it taller to be safe
                    col.radius = 0.4f; 
                    
                    // This -2.0f offset should force the bottom of the capsule 
                    // through the pivot and into the ground.
                    col.center = new Vector3(0, 0, -2.0f); 

                    count++;
                }
            }
            
            // 3. Clean up the cones so your friends' computers don't explode
            if (name.Contains("cone"))
            {
                CapsuleCollider extraCol = go.GetComponent<CapsuleCollider>();
                if (extraCol != null) DestroyImmediate(extraCol);
            }
        }
        Debug.Log("SUCCESS: " + count + " trees fixed. Check your scene, the green pills should be on the ground now!");
    }
}