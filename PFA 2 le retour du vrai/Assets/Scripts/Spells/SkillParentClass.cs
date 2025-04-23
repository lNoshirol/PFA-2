using UnityEngine;
using UnityEngine.VFX;

public abstract class SkillParentClass
{
    public string Name;

    public SkillParentClass() 
    {
            
    }

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

    /// <summary>
    /// Dash in a direction
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="force"></param>
    protected void Dash(Vector2 direction, float force)
    {
        PlayerMain.Instance.Rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }

    protected void /*Vector3*/ GetNearestEnemyPosition()
    {
        // Récupérer la positon de l'ennemi le plus proche
    }
    #endregion
}
