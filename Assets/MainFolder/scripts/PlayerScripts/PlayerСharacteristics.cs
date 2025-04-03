using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PlayerСharacteristics : СreatureСharacteristics
{
    private PlayerAnimatorController playerAnimatorController;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Image playerHealthBar;
    public override Image HealthBar => playerHealthBar; 
    private void Start()
    {
        playerAnimatorController = GetComponent<PlayerAnimatorController>();

    }
    public override void Update()
    {
        base.Update();
    }

    public override void CheckisAlive()
    {
        if (currentHealth > 0f)
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
            AfterDeath();
        }
    }
    private void AfterDeath()
    {
        playerAnimatorController.UpdateDeathBool(!isAlive);
        deathPanel.SetActive(true);
        Destroy(gameObject, 10f);
        if (gameObject == null)
        {
            Time.timeScale = 0;
        }
    }
}
