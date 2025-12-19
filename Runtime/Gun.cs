using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] GunScriptableObject gunScriptableObject;

    [Tooltip("The GameObject to instantiate upon shooting.")]
    [SerializeField] GameObject projectilePrefab;
    [Tooltip("The transform to instantiate the projectiles at. Note that the transform will rotate in order provide projectile spread, the object shouldnt be visible because of this.")]
    [SerializeField] Transform spawnPoint;
    [Tooltip("The force to apply to the projectiles upon instatiation.")]
    [SerializeField] float projectileSpeed = 10;

    [Tooltip("The amount of projectiles to spawn when the DoShoot function is called.")]
    [SerializeField] int projectilesPerShot = 1;
    [Tooltip("Applies random deviation of a fired projectile from the exact aim point. Example: spread = 1, projectile will can go between -1 - 1 degree off target.")]
    [SerializeField] float spread;
    [Tooltip("The minimum amount of seconds that must pass before another projectile can be fired.")]
    [SerializeField] float shootingCooldown = 0.25f;
    [Tooltip("If enabled, holding down the shoot button will keep automatically firing projectiles.")]
    [SerializeField] bool allowButtonHolding;
    
    private InputAction shootAction;
    private bool onCooldown = false;
    private const string SHOOTACTIONSTRING = "Attack";
    private void OnValidate()
    {
        if(projectilesPerShot < 0)
        {
            Debug.LogError("Shots Per Click cannot be below 0!!");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootAction = InputSystem.actions.FindAction(SHOOTACTIONSTRING);
        if (allowButtonHolding)
        {

            shootAction.started += Shoot;
        }
        else 
        {
            shootAction.performed += Shoot;
        }
    }

    private void Update()
    {
        if (allowButtonHolding == false) 
        {
            return;
        }

        if (InputSystem.actions.FindAction(SHOOTACTIONSTRING, true).IsPressed())
        {
            Debug.Log("Held Down");
            Shoot(new InputAction.CallbackContext());
        }
    }

    private void Shoot(InputAction.CallbackContext context) 
    {
        if (onCooldown == true) { return; }
        StartCoroutine(DoShoot());
    }

    private IEnumerator DoShoot()
    {
        onCooldown = true;
        for(int i = 0; i < projectilesPerShot; i++)
        {
            spawnPoint.localRotation = Quaternion.Euler(new Vector3(spawnPoint.localRotation.x, spawnPoint.localRotation.y,Random.Range(-spread,spread)));
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            if (projectile.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.linearVelocity = spawnPoint.up * projectileSpeed;
            }
        }
        yield return new WaitForSeconds(shootingCooldown);
        onCooldown = false;
    }

    public void SetBulletSpeed(float amount)
    {
        projectileSpeed = amount;
    }

    public void SetBulletsPerShot(int amount)
    {
        projectilesPerShot = amount;
    }

    public void SetSpread(float amount)
    {
        spread = amount;
    }

    public bool IsOnCooldown()
    {
        return onCooldown;
    }

    public void SetShootingCooldown(float amount)
    {
        shootingCooldown = amount;
    }

}
