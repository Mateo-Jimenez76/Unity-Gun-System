using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float bulletSpeed = 10;
    [Header("Shooting Settings")]
    [SerializeField] int bulletsPerShot = 1;
    [SerializeField] float spread;
    [SerializeField] float shootingCoolDown = 0.25f;
    [SerializeField] bool allowButtonHolding;
    private InputAction shootAction;
    private bool onCooldown = false;
    private const string SHOOTACTIONSTRING = "Attack";
    private void OnValidate()
    {
        if(bulletsPerShot < 0)
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
        for(int i = 0; i < bulletsPerShot; i++)
        {
            spawnPoint.localRotation = Quaternion.Euler(new Vector3(spawnPoint.localRotation.x, spawnPoint.localRotation.y,Random.Range(-spread,spread)));
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            if (bullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.linearVelocity = spawnPoint.up * bulletSpeed;
            }
        }
        yield return new WaitForSeconds(shootingCoolDown);
        onCooldown = false;
    }

}
