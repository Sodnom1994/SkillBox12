using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class BossScript : EnemyCharacteristics
{
    [SerializeField] private GameObject winPanel;
    

    private int limbsLeft = 2;

    public void OnLimbDestroyed()
    {
        limbsLeft--;

        if (limbsLeft <= 0)
        {
            TakeDamage(currentHealth); // Наносим полный урон 
        }
        else
        {
            TakeDamage(currentHealth / 2); //урон игрока 
        }
    }
    public override void VisionColiTransform(float viewDirection)
    {

    }

    public override void PatrolAndChase()
    {

    }
    public override void Update()
    {
        base.Update();
        if (currentHealth <= 0)
        {
            winPanel.SetActive(true);
        }
    }

}
