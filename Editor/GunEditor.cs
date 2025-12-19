using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Gun))]
public class GunEditor : Editor
{
    SerializedProperty gunScriptableObject;
    SerializedProperty projectilePrefab;
    SerializedProperty spawnPoint;
    SerializedProperty projectileSpeed;
    SerializedProperty projectilesPerShot;
    SerializedProperty spread;
    SerializedProperty shootingCooldown;
    SerializedProperty allowButtonHolding;

    private bool usingScriptableObject = false;
    private void OnEnable()
    {
        // Link the properties to the variables
        gunScriptableObject = serializedObject.FindProperty("gunScriptableObject");
        projectilePrefab = serializedObject.FindProperty("projectilePrefab");
        spawnPoint = serializedObject.FindProperty("spawnPoint");
        projectileSpeed = serializedObject.FindProperty("projectileSpeed");
        projectilesPerShot = serializedObject.FindProperty("projectilesPerShot");
        spread = serializedObject.FindProperty("spread");
        shootingCooldown = serializedObject.FindProperty("shootingCooldown");
        allowButtonHolding = serializedObject.FindProperty("allowButtonHolding");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(gunScriptableObject);

        EditorGUILayout.Space();

        if (gunScriptableObject.objectReferenceValue != null)
        {
            usingScriptableObject = true;
        } else
        {
            usingScriptableObject = false;
        }

        EditorGUI.BeginDisabledGroup(usingScriptableObject);

        // --- Header: Projectile Settings ---
        EditorGUILayout.LabelField("Projectile Settings", EditorStyles.boldLabel);

        // Projectile Prefab
        EditorGUILayout.PropertyField(projectilePrefab);
        
        // Check if the assigned prefab has a Rigidbody2D...
        if (projectilePrefab.objectReferenceValue != null)
        {
            GameObject prefab = (GameObject)projectilePrefab.objectReferenceValue;
            //...If no rigidbody2D is found
            if (prefab.GetComponent<Rigidbody2D>() == null)
            {
                //Show warning
                EditorGUILayout.HelpBox("The projectile needs to have a Rigidbody2D in order for force to be applied to it. No force means no moving.", MessageType.Warning);
            }
        }

        // Spawn Point
        EditorGUILayout.PropertyField(spawnPoint);

        // Projectile Speed
        EditorGUILayout.PropertyField(projectileSpeed);
        
        if (projectileSpeed.floatValue < 0)
        {
            EditorGUILayout.HelpBox("Setting Projectile speed below 0 will cause the bullets to fly backwards.", MessageType.Warning);
        }

        EditorGUILayout.Space();

        // --- Header: Shooting Settings ---
        EditorGUILayout.LabelField("Shooting Settings", EditorStyles.boldLabel);

        // Projectiles Per Shot
        EditorGUILayout.PropertyField(projectilesPerShot);
        
        if (projectilesPerShot.intValue < 0)
        {
            EditorGUILayout.HelpBox("You cant shoot negative amount of bullets.", MessageType.Warning);
        }

        // Spread
        EditorGUILayout.PropertyField(spread);

        // Shooting Cooldown
        EditorGUILayout.PropertyField(shootingCooldown);
        if (shootingCooldown.floatValue < 0)
        {
            EditorGUILayout.HelpBox("The cooldown can't be negative seconds.", MessageType.Warning);
        }

        // Allow Button Holding
        EditorGUILayout.PropertyField(allowButtonHolding);

        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
