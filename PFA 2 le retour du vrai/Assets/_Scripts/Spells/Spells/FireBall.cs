using UnityEngine;

public class FireBall : SkillParentClass
{
    public override void Activate()
    {
        // PROTO
        GameObject fireBall = GetProjectile("FireBall");
        AlignToCameraTransform(fireBall.transform);
        Projectile proj = fireBall.TryGetComponent(out Projectile projectile) ? projectile : null;
        proj.Launch();
    }
}
