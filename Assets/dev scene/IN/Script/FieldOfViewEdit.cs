using UnityEditor;
using UnityEngine;

/*[CustomEditor(typeof(EnemyField))]
public class FieldOfViewEdit : Editor
{
    private void OnSceneGUI()
    {
        EnemyField EF = (EnemyField)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(EF.transform.position, Vector3.up, Vector3.forward, 360, EF.radius);

        Vector3 viewAngle01 = DirectionFromAngle(EF.transform.eulerAngles.y, -EF.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(EF.transform.eulerAngles.y, EF.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(EF.transform.position, EF.transform.position + viewAngle01 * EF.radius);
        Handles.DrawLine(EF.transform.position, EF.transform.position + viewAngle02 * EF.radius);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
*/
