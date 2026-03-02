using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 240f;
    public Transform headTransform; // camera or headset transform reference
    private CharacterController cc;

    // references
    public HapticGlove leftGlove;
    public HapticGlove rightGlove;
    public HealthSystem healthSystem;
    public BioManager bioManager;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (!headTransform) headTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
        HandleActions();
        UpdateVitalsAffects();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 forward = Vector3.ProjectOnPlane(headTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(headTransform.right, Vector3.up).normalized;
        Vector3 move = (forward * v + right * h).normalized;
        cc.SimpleMove(move * moveSpeed);
        // Smooth rotate with mouse (optional)
        float yaw = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, yaw * rotateSpeed * Time.deltaTime);
    }

    void HandleActions()
    {
        // Example: left click = shoot (trigger haptic)
        if (Input.GetMouseButtonDown(0))
        {
            // apply recoil feedback to right glove
            if (rightGlove != null)
                rightGlove.SendImpact(0.15f, 0.2f); // intensity, duration

            // simple hit test
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f))
            {
                var target = hit.collider.GetComponent<HealthSystem>();
                if (target != null)
                    target.TakeDamage(10f);
            }
        }
    }

    void UpdateVitalsAffects()
    {
        // Example: increase movement penalty when heart rate is high
        float hr = bioManager.CurrentHeartRate;
        float hrFactor = Mathf.Clamp01((hr - bioManager.restingHeartRate) / (bioManager.maxHeartRate - bioManager.restingHeartRate));
        float speedPenalty = Mathf.Lerp(0f, 2f, hrFactor); // 0-2 m/s penalty
        cc.Move(Vector3.zero); // ensure physics tick
        // apply speed penalty (simple)
        moveSpeed = Mathf.Max(2f, 5f - speedPenalty);
    }
}
