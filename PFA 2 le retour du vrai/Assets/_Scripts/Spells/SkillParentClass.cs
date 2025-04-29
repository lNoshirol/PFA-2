using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public abstract class SkillParentClass
{
    protected string Name;
    protected delegate void Delegate();

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

    #region Utilites
    protected async void DelayedFunction(Delegate function, float time)
    {
        await Task.Delay((int)(1000 * time));
        function();
    }
    protected async void DelayedFunction(Delegate[] functions, float time)
    {
        await Task.Delay((int)(1000 * time));
        foreach (Delegate function in functions)
        {
            function();
        }
    }
    #endregion

    #region Subskills
    protected void Dash(Vector3 direction, float force)
    {
        PlayerMain.Instance.Rigidbody.AddForce(direction * force, ForceMode.Impulse);
        //Delegate[] functions = { PrintRandomTest, PrintRandomTest, PrintRandomTest };
        //DelayedFunction(functions, 1f);
    }

    protected void PrintRandomTest()
    {
        string[] textDatas = { "zarma", "tout roule", "bigre", "zebi", "corneguidouille", "fichtre" };
        string text = textDatas[Random.Range(0, textDatas.Length)];
        Debug.Log(text);
    }

    protected GameObject GetProjectile(string projectileName)
    {
        return ProjectileManager.Instance.ProjectilePools[projectileName].GetObject();
        //return ProjectileManager.Instance.V2[projectileName].GetObject().gameObject;
    }

    protected void AlignToPlayerTransform(Transform transform)
    {
        Transform targetTransform = PlayerMain.Instance.ProjectileSocket.transform;
        transform.localPosition = targetTransform.position;
        transform.rotation = targetTransform.parent.rotation;
    }

    protected void AlignToCameraTransform(Transform transform)
    {
        AlignToTransform(transform, Camera.main.transform);
    }

    protected void AlignToTransform(Transform transform, Transform target)
    {
        transform.localPosition = target.position;
        transform.rotation = target.rotation;
    }

    protected void /*Vector3*/ GetNearestEnemyPosition()
    {
        // Récupérer la positon de l'ennemi le plus proche
    }
    #endregion
}
