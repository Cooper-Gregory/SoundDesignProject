using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class attackSpawn : MonoBehaviour
{
    [Header("Attack Defaults")]
    public float defaultRange = 3.0f;
    public Vector2 defaultDirection = Vector2.right;
    public float defaultDamage = 10.0f;
    public float defaultKnockback = 0.01f;
    public float defaultDuration = 5.0f;
    public float defaultCooldown = 1.0f;
    public float defaultSpeed = 1.0f;

    [Header("Attack Spawner Settings")]
    public GameObject attackHurtbox;
    public GameObject projectileHurtboxToIntance;
    public GameObject player;
    public GameObject knockbackCenter;
    public Component playerStats;
    public GameObject gameplayManager;
    public Component cooldownManager;
    public GameObject playerSettings;
    public Component currentAbilityUIManager;

    [Header("Attack Spawner Logic")]
    public int playerID = 0;
    public int attackNumber = 0;
    public bool attacked;
    public bool attacking = false;
    public bool onCooldown = false;
    public bool attackFriendlyFire = false;

    protected float attackDurationTimer;
    protected float cooldownDurationTimer;

    void Start()
    {
        playerID = player.GetComponent<playerStats>().playerID;
        playerStats = player.GetComponent<playerStats>();
        gameplayManager = GameObject.Find("cooldownManager");
        playerSettings = GameObject.Find("playerSettings");

        if (playerID == 0)
            player.GetComponent<currentAbilityManager>().UpdateActiveAbility(playerSettings.GetComponent<playerSettings>().p0_attacks_simple[0]);
        else
            player.GetComponent<currentAbilityManager>().UpdateActiveAbility(playerSettings.GetComponent<playerSettings>().p1_attacks_simple[0]);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        attacked = true;
    }

    void Update()
    {
        if (player.GetComponent<playerStats>().attackSlots == null)
        {
            Debug.LogWarning("You don't seem to have attacks yet..");
        }
        else if (player.GetComponent<playerStats>().attackSlots[attackNumber, 7] == 1)
        {
            attackFriendlyFire = true;
        }
        else
        {
            attackFriendlyFire = false;
        }
        UpdateAttackDirection();
        CallAttack();
        ResolveCooldowns();
    }

    public void UpdateAttackDirection()
    {
        defaultDirection = (player.GetComponent<movementController>().playerLookDirection).normalized;
        if (defaultDirection == Vector2.zero && !(player.GetComponent<movementController>().playerMoveDirection.normalized == Vector2.zero))
        {
            defaultDirection = (player.GetComponent<movementController>().playerMoveDirection).normalized;
        }
        else if (defaultDirection == Vector2.zero)
        {
            defaultDirection = Vector2.down;
        }

        attackHurtbox.transform.position = (new Vector2(player.transform.position.x, player.transform.position.y) + defaultDirection * (defaultRange / 2 + 0.375f));
        knockbackCenter.transform.position = (new Vector2(player.transform.position.x, player.transform.position.y) + defaultDirection * 0.81f);
    }

    public void CallAttack()
    {
        if (player.GetComponent<playerStats>().attackSlots == null)
        {
            Debug.LogWarning("You don't seem to have attacks yet..");
        }
        else if (player.GetComponent<playerStats>().attackSlots[attackNumber, 6] == 1)
        {
            //Ranged attack
            if (attacked && !(onCooldown || attacking))
            {
                defaultRange = player.GetComponent<playerStats>().attackSlots[attackNumber, 0];
                defaultDamage = player.GetComponent<playerStats>().attackSlots[attackNumber, 2];
                defaultKnockback = player.GetComponent<playerStats>().attackSlots[attackNumber, 3];
                defaultDuration = player.GetComponent<playerStats>().attackSlots[attackNumber, 4];
                defaultCooldown = player.GetComponent<playerStats>().attackSlots[attackNumber, 5];
                SpawnProjectile(defaultRange, defaultDirection, defaultDamage, defaultKnockback, defaultDuration, projectileHurtboxToIntance, player, attackFriendlyFire);
                gameplayManager.GetComponent<gameplayUIcooldownmanager>().DisplayCooldown(playerID, defaultCooldown, defaultDuration, attackNumber);
            }
            else
            {
                attacked = false;
            }
            
        }
        else
        {
            //Melee Attack
            if (attacked && !(onCooldown || attacking))
            {
                defaultRange = player.GetComponent<playerStats>().attackSlots[attackNumber, 0];
                defaultDamage = player.GetComponent<playerStats>().attackSlots[attackNumber, 2];
                defaultKnockback = player.GetComponent<playerStats>().attackSlots[attackNumber, 3];
                defaultDuration = player.GetComponent<playerStats>().attackSlots[attackNumber, 4];
                defaultCooldown = player.GetComponent<playerStats>().attackSlots[attackNumber, 5];
                SpawnAttack(defaultRange, defaultDirection, defaultDamage, defaultKnockback, attackHurtbox, player, attackFriendlyFire);
                gameplayManager.GetComponent<gameplayUIcooldownmanager>().DisplayCooldown(playerID, defaultCooldown, defaultDuration, attackNumber);
            }
            else
            {
                attacked = false;
            }
            
        }
        
    }

    public void SpawnAttack(float attackRange, Vector2 attackDirection, float attackDamage, float attackKnockback, GameObject hurtbox, GameObject player, bool friendlyFire)
    {
        hurtbox.SetActive(true);

        hurtbox.transform.localScale = new Vector3(attackRange, attackRange, attackRange);
        hurtbox.GetComponent<damager>().attackRange = attackRange;
        hurtbox.GetComponent<damager>().attackDamage = attackDamage; 
        hurtbox.GetComponent<damager>().attackKnockback = attackKnockback;
        hurtbox.GetComponent<damager>().playerID = player.GetComponent<playerStats>().playerID;
        
        if (friendlyFire)
        {
            hurtbox.GetComponent<damager>().friendlyFire = true;
        }
        else
        {
            hurtbox.GetComponent<damager>().friendlyFire = false;
        }

        attacked = false;
        attacking = true;
        Debug.Log("attack duration started");
        attackNumber += 1;
        if (attackNumber >= 3)
        {
            attackNumber = 0;
        }
    }

    public void DisableAttack(GameObject hurtbox)
    {
        hurtbox.SetActive(false);
        hurtbox.GetComponent<damager>().damageDealt = false;
        Debug.Log("Disabled Hurtbox!");
    }

    public void SpawnProjectile(float attackRange, Vector2 attackSpeed, float attackDamage, float attackKnockback, float attackDuration, GameObject hurtbox, GameObject player, bool friendlyFire)
    {
        GameObject projectile = Instantiate(hurtbox, knockbackCenter.transform.position, knockbackCenter.transform.rotation);
        projectile.transform.localScale = new Vector3(attackRange, attackRange, attackRange);
        projectile.GetComponent<Rigidbody2D>().AddForce(attackSpeed * 0.5f);
        projectile.GetComponent<rangedDamager>().attackDamage = attackDamage;
        projectile.GetComponent<rangedDamager>().attackKnockback = attackKnockback;
        projectile.GetComponent<rangedDamager>().attackRange = attackRange;
        projectile.GetComponent<rangedDamager>().player = player;
        projectile.GetComponent<rangedDamager>().friendlyFire = friendlyFire;
        projectile.GetComponent<rangedDamager>().knockbackCenter = projectile;
        projectile.GetComponent<rangedDamager>().attackDuration = attackDuration;
        projectile.transform.GetChild(1).transform.localScale = new Vector3(attackRange, attackRange, attackRange);

        if (playerID == 0)
        {
            projectile.GetComponent<rangedDamager>().playerID = 0;
        }
        else
        {
            projectile.GetComponent<rangedDamager>().playerID = 1;
        }

        attacked = false;
        attacking = true;
        Debug.Log("projectile attack duration started");
        attackNumber += 1;
        if (attackNumber >= 3)
        {
            attackNumber = 0;
        }
    }


    public void DisableProjectile(GameObject hurtbox)
    {

    }

    public void ResolveCooldowns()
    {
        if (attacking)
        {
                attackDurationTimer += Time.deltaTime;
        }
        if (attackDurationTimer >= defaultDuration)
        {
            attackDurationTimer = 0.0f;
            DisableAttack(attackHurtbox);
            Debug.Log("projectile duration complete, beginning cooldown");
            attacking = false;
            onCooldown = true;
            player.GetComponent<currentAbilityManager>().UpdateActiveAbility(3);
        }
        if (onCooldown)
        {
            cooldownDurationTimer += Time.deltaTime;
        }
        if (cooldownDurationTimer >= defaultCooldown)
        {
            cooldownDurationTimer = 0.0f;
            Debug.Log("projectile cooldown finished.");
            onCooldown = false;
            //If you want different sounds for the attack effects, I'd totally compare them by pX_attacks_simple as these are just a value 0-2
            // 0 = sword, 1 = explosion, 2 = gun :)
            //I'd call the audio for *activating* attacks within SpawnAttack() or SpawnProjectile(), not here though.
            //If you wanted audio for when cooldowns are finished, I'd call it within currentAbilityManage.UpdateActiveAbility(), but it's up to you.
            if (playerID == 0)
                player.GetComponent<currentAbilityManager>().UpdateActiveAbility(playerSettings.GetComponent<playerSettings>().p0_attacks_simple[attackNumber]);
            else
                player.GetComponent<currentAbilityManager>().UpdateActiveAbility(playerSettings.GetComponent<playerSettings>().p1_attacks_simple[attackNumber]);
        }
    }
}
