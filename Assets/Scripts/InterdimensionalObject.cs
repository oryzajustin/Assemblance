using UnityEngine;

public class InterdimensionalObject : MonoBehaviour
{
    private int currentDimension;

    public GameObject model2;
    public GameObject model3;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCurrentDimension(2);
    }

    public void UpdateCurrentDimension(int newDimension)
    {
        switch (newDimension)
        {
            case 2:
                model2.SetActive(true);
                model3.SetActive(false);
                break;
            case 3:
                model2.SetActive(false);
                model3.SetActive(true);
                break;
            default:
                break;
        }
        currentDimension = newDimension;
    }
}
