using UnityEngine;
using static UnityEditor.UIElements.ToolbarMenu;
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlatformScript : MonoBehaviour
{
    [SerializeField] private float initialMotorSpeed = -3f; // Исходная скорость мотора
    [Header("Присваиваются автоматически")]
    [Tooltip("SliderJoint2D для доступа к объекту ссылочного типа (motorSpeed)")]
    [SerializeField] private SliderJoint2D sliderJointforPlatform2D;
    
    
    private void Awake()
    {        
        sliderJointforPlatform2D = GetComponent<SliderJoint2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateMotorSpeed(3f); // Устанавливаем новую скорость
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateMotorSpeed(initialMotorSpeed); // Восстанавливаем исходную скорость
        }
    }
    private void UpdateMotorSpeed(float newSpeed)
    {
        JointMotor2D motor = sliderJointforPlatform2D.motor; // Получаем текущие настройки мотора
        motor.motorSpeed = newSpeed; // Изменяем скорость
        sliderJointforPlatform2D.motor = motor; // Применяем изменения
        sliderJointforPlatform2D.useMotor = true; // Убедитесь, что мотор включён
    }
   
}
