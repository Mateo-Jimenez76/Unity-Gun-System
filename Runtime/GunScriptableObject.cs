using UnityEngine;

[CreateAssetMenu(fileName = "NewGunScriptableObject", menuName = "SimpleGunSystem/ScriptableObjects/GunScriptableObject")]
public class GunScriptableObject : ScriptableObject
{
    [Header("Projectile Settings")]
    [Tooltip("The GameObject to instantiate upon shooting.")]
    [SerializeField] GameObject projectilePrefab;
    [Tooltip("The transform to instantiate the projectiles at. Note that the transform will rotate in order provide projectile spread, the object shouldnt be visible because of this.")]
    [SerializeField] Transform spawnPoint;
    [Tooltip("The force to apply to the projectiles upon instatiation.")]
    [SerializeField] float projectileSpeed = 10;
    [Header("Shooting Settings")]
    [Tooltip("The amount of projectiles to spawn when the DoShoot function is called.")]
    [SerializeField] int projectilesPerShot = 1;
    [Tooltip("Applies random deviation of a fired projectile from the exact aim point. Example: spread = 1, projectile will can go between -1 - 1 degree off target.")]
    [SerializeField] float spread;
    [Tooltip("The minimum amount of seconds that must pass before another projectile can be fired.")]
    [SerializeField] float shootingCooldown = 0.25f;
    [Tooltip("If enabled, holding down the shoot button will keep automatically firing projectiles.")]
    [SerializeField] bool allowButtonHolding;
}
