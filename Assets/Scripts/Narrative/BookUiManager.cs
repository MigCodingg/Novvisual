using DG.Tweening;
using UnityEngine;

public class BookUiManager : MonoBehaviour
{
    [SerializeField] GameObject Animationpage;
    void Start()
    {
        
    }
    public void GetInsidePicture()
    {
        Debug.Log("Enter Picture");
    }


    public void PassBookPage()
    {
        Animationpage.SetActive(true);
        Animationpage.transform.DORotate(new Vector3 (0,0,180), 1);
    }
}
