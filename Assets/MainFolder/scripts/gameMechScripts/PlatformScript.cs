using UnityEngine;
using static UnityEditor.UIElements.ToolbarMenu;
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlatformScript : MonoBehaviour
{
    [SerializeField] private float initialMotorSpeed = -3f; // �������� �������� ������
    [Header("������������� �������������")]
    [Tooltip("SliderJoint2D ��� ������� � ������� ���������� ���� (motorSpeed)")]
    [SerializeField] private SliderJoint2D sliderJointforPlatform2D;
    
    
    private void Awake()
    {        
        sliderJointforPlatform2D = GetComponent<SliderJoint2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateMotorSpeed(3f); // ������������� ����� ��������
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateMotorSpeed(initialMotorSpeed); // ��������������� �������� ��������
        }
    }
    private void UpdateMotorSpeed(float newSpeed)
    {
        JointMotor2D motor = sliderJointforPlatform2D.motor; // �������� ������� ��������� ������
        motor.motorSpeed = newSpeed; // �������� ��������
        sliderJointforPlatform2D.motor = motor; // ��������� ���������
        sliderJointforPlatform2D.useMotor = true; // ���������, ��� ����� �������
    }
   
}
