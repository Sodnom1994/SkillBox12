using UnityEngine;

public class Level1HatchScript : MonoBehaviour
{
    [SerializeField] private Knight knight;    
    private void Update()
    {
        if (knight.CurrentHealth <= 0)
        {
            Destroy(gameObject, 5f);
        }
    }
}
