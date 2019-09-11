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
    [SerializeField]
    bool showArrow = true;

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
        if (!showArrow && line.gameObject.activeInHierarchy)
        {
            line.gameObject.SetActive(false);
            coneTransform.gameObject.SetActive(false);
            // Disabling the script means that if you change <c>showArrow</c> back to true, you also must enable the script
            enabled = false;
            return;
        }
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


