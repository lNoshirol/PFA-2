using UnityEngine;
using UnityEngine.VFX;

public abstract class SkillParentClass
{
    public abstract void Activate();

    protected void PlayVFX(VisualEffect vfx)
    {
        // jouer le VFX
    }

    protected void PlaySFX(AudioClip sfx)
    {
        // jouer le son dans le sound manager
    }

    protected void PlayAnimation(AnimationClip animation) 
    {
        // jouer l'animation
    }

    #region Subskills

    protected void Dash(Vector3 direction, float force)
    {
        
    }

    protected void /*Vector3*/ GetNearestEnemyPosition()
    {
        // Récupérer la positon de l'ennemi le plus proche
    }
    #endregion
}
