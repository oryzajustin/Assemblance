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

    private void Start()
    {
        Dimension newDimension = transform.position.x > 0 ? Dimension.two : Dimension.three;
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

    private void Update()
    {
        Dimension newDimension = transform.position.x > 0 ? Dimension.two : Dimension.three;
        if(newDimension != currentDimension)
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
}
