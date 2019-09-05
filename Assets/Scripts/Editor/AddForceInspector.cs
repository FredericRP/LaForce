using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AddForce))]
public class AddForceInspector : Editor
{
    const float sizeRatio = 0.1f;

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnScene;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnScene;
    }

    private void OnScene(SceneView sceneview)
    {
        Rigidbody body = (this.target as MonoBehaviour).GetComponent<Rigidbody>();
        Handles.color = Color.yellow;
        // Draw an arrow handler from the rigidbody velocity (representing the result from all combined forces)
        // we constraint the ball on the XY plane and the camera is backward from the ball
        Handles.ArrowHandleCap(0, body.transform.position, Quaternion.LookRotation(body.velocity, Vector3.back), 
                                    body.velocity.sqrMagnitude * sizeRatio, EventType.Repaint);
    }
}
