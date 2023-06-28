using UnityEngine;
using UnityEngine.UI;

public class ViewerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public VariableJoystick leftJoystick;
    public VariableJoystick rightJoystick;
    public Rigidbody rb;
    public Scrollbar scrollbar;
    public float scrollSpeed = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    private Vector3 originPosition;
    private Quaternion originRotation;
    private float originScrollbarValue;

    private void Start()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        originScrollbarValue = scrollbar.value;
    }

    public void ClickReset()
    {
        transform.position = originPosition;
        transform.rotation = originRotation;
        scrollbar.value = originScrollbarValue;
    }

    private void Update()
    {
        float scrollValue = scrollbar.value; // ��ũ�� UI�� ���� ������

        // ������Ʈ�� ���ο� ��ġ�� ���
        float targetY = Mathf.Lerp(minY, maxY, scrollValue);

        // ���ο� ��ġ�� ������Ʈ�� �̵�
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.left * leftJoystick.Vertical + Vector3.forward * leftJoystick.Horizontal;
        Vector3 targetVelocity = direction * speed;

        // ���� �ӵ��� ���� ���� �ʰ����� �ʴ� ��쿡�� ��ǥ �ӵ��� ����
        if (rb.velocity.magnitude <= maxSpeed)
        {
            rb.velocity = targetVelocity;
        }

        // ������Ʈ�� ȸ����Ŵ
        float rotationSpeedLR = rightJoystick.Horizontal * scrollSpeed;
        float rotationSpeedUD = rightJoystick.Vertical * scrollSpeed;
        transform.Rotate(Vector3.up, rotationSpeedLR * Time.fixedDeltaTime);
        transform.Rotate(Vector3.right, rotationSpeedUD * Time.fixedDeltaTime);
    }
}
