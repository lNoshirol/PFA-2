using UnityEngine;

public class FireBall : SkillParentClass
{
    public override void Activate()
    {
        // PROTO
        GameObject fireBall = GetProjectile("FireBall");
        UpdateTransform(fireBall.transform);
        fireBall.GetComponent<Projectile>().Launch(); // ATTENTION PAS OUBLIER
    }
}
