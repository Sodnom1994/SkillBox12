using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PlayerСharacteristics : СreatureСharacteristics
{
    public static PlayerСharacteristics Instance { get; private set; }
    private PlayerAnimatorController playerAnimatorController;
    private Camera playerCamera;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Image playerHealthBar;
    public override Image HealthBar => playerHealthBar;

    public override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    private void Start()
    {
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        playerCamera = GetComponentInChildren<Camera>();
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
        playerCamera.transform.parent = null;

        playerAnimatorController.UpdateDeathBool(isAlive);
        deathPanel.SetActive(true);
        Destroy(gameObject, 5f);
        if (gameObject == null)
        {
            Time.timeScale = 0;
        }
    }
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        HealthBar.fillAmount = currentHealth / maxHealth;
    }
}
