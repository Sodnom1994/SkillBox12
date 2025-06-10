using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NUnit.Framework;
using DG.Tweening.Plugins.Options;
public class Bat : EnemyCharacteristics
{
    [SerializeField] private Transform[] patrolPath;
    [SerializeField] private float moveDuration = 3.0f;
    [SerializeField] private LoopType loopType;
    public override void PatrolAndChase()
    {

    }
    private void OnDestroy()
    {
        
    }
    public override void Start()
    {
       
            enemyAnimatorController = GetComponent<EnemyAnimatorController>();
            visionCollider = GetComponentInChildren<PolygonCollider2D>();
            Image[] images = GetComponentsInChildren<Image>();

            foreach (Image image in images)
            {
                if (image.name == "EnemyHealthBar")
                    enemyHealthBar = image;
                break;
            }

            {
                transform.DOPath(new Vector3[]
                        {
            patrolPath[0].position,
            patrolPath[1].position,
            patrolPath[2].position,
            patrolPath[3].position,
                            }, moveDuration,
                            PathType.CatmullRom,
                            PathMode.Sidescroller2D, 10)
                            .SetOptions(AxisConstraint.None)
                            .SetLoops(-1, loopType)
                            .SetEase(Ease.Linear);
            }

        


    }
    public override void Update()
    {
        CheckIsAlive();
    }
    public override void CheckIsAlive()
    {
        if(gameObject.activeSelf)
        {
            if (currentHealth > 0f)
            {
                isAlive = true;
            }
            else
            {
                //Debug.Log($"Using deathAnimation");
                isRunning = false;
                isAlive = false;
                if (!isDroped)
                {
                    Debug.Log("Çïóñêàþ âûïàäåíèå ëóòà");
                    EventBus.EnemyDied(this.gameObject);
                    isDroped = true;
                }
                enemyAnimatorController.UpdateDeathBool(isAlive);
                transform.DOKill();
                Destroy(gameObject);
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (collision.gameObject.TryGetComponent(out ÑreatureÑharacteristics characteristics))
        {
            if (Time.time >= nextAttackTime)
            {
                characteristics.TakeDamage(enemyDamage);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}


