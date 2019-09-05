using UnityEngine;
using UnityEngine.InputSystem;

public class AddForce : MonoBehaviour
{
    [SerializeField]
    Rigidbody body;
    [SerializeField]
    float strength = 10;
    [SerializeField]
    float coolDown = 0.25f;
    [SerializeField]
    LineRenderer line;
    [SerializeField]
    Transform coneTransform;

    Transform target;
    float nextTime;

    // Start is called before the first frame update
    private void Start()
    {
        nextTime = 0;
        target = GameObject.FindGameObjectWithTag("target").transform;
    }

    public void OnForce(InputAction.CallbackContext context)
    {
        ApplyForce();
    }

    private void Update()
    {
        // editor arrow handle cap reproduced at runtime
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + body.velocity);
        // Move the cone at the top
        coneTransform.position = transform.position + body.velocity;
        coneTransform.rotation = Quaternion.LookRotation(body.velocity);
    }

    void ApplyForce()
    {
        if (Time.time > nextTime)
        {
            nextTime = Time.time + coolDown;
            Vector3 direction = (target.position - transform.position).normalized;
            body.AddForce(direction * strength, ForceMode.Impulse);
        }
    }
}


