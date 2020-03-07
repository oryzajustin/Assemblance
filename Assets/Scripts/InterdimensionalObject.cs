using UnityEngine;

public enum Dimension
{
    two,
    three
}
public class InterdimensionalObject : MonoBehaviour
{
    private Dimension currentDimension;

    public GameObject pixelModel;
    public GameObject meshModel;

    public void UpdateCurrentDimension(Dimension newDimension)
    {
        switch (newDimension)
        {
            case Dimension.two:
                pixelModel.SetActive(true);
                meshModel.SetActive(false);
                break;
            case Dimension.three:
                pixelModel.SetActive(false);
                meshModel.SetActive(true);
                break;
            default:
                break;
        }
        currentDimension = newDimension;
    }
}
