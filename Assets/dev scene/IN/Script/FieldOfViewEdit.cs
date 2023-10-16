using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyField))]
public class FieldOfViewEdit : Editor
{
    private void OnSceneGUI()
    {
        EnemyField EF = (EnemyField)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(EF.transform.position, Vector3.up, Vector3.forward, 360, EF.radius);
    }
}
