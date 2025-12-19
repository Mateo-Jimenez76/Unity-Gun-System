using UnityEngine;

[CreateAssetMenu(fileName = "NewGunScriptableObject", menuName = "SimpleGunSystem/ScriptableObjects/GunScriptableObject")]
public class GunScriptableObject : ScriptableObject
{
    [Tooltip("The GameObject to instantiate upon shooting.")]
    public GameObject projectilePrefab;
    [Tooltip("The force to apply to the projectiles upon instatiation.")]
    public float projectileSpeed = 10;

    [Tooltip("The amount of projectiles to spawn when the DoShoot function is called.")]
    public int projectilesPerShot = 1;
    [Tooltip("Applies random deviation of a fired projectile from the exact aim point. Example: spread = 1, projectile will can go between -1 - 1 degree off target.")]
    public float spread;
    [Tooltip("The minimum amount of seconds that must pass before another projectile can be fired.")]
    public float shootingCooldown = 0.25f;
    [Tooltip("If enabled, holding down the shoot button will keep automatically firing projectiles.")]
    public bool allowButtonHolding;
}
