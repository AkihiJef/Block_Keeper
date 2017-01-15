using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(Ball))]
public class RadiusEditor : Editor {
    void OnSceneGUI()
    {
        Ball ball = (Ball)target;
        Handles.color = Color.black;
        Handles.DrawWireDisc(ball.transform.position, new Vector3(0, 0, -1), ball.strongHitRadius);
    }
}
