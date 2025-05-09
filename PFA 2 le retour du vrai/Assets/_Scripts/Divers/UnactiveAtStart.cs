using System.Threading.Tasks;
using UnityEngine;

public class UnactiveAtStart : MonoBehaviour
{
    //� mettre sur des objets qui set up des ref au start/awake mais dont le gameObject doit �tre d�sactiv� au d�but

    async void Start()
    {
        await Task.Delay(10);
        gameObject.SetActive(false);
    }
}
