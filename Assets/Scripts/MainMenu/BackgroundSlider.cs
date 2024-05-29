using UnityEngine;
using UnityEngine.UI;

public class BackgroundSlider : MonoBehaviour
{

    [SerializeField] private RawImage slider;
    [SerializeField] private float xPos, yPos;


    private void Update()
    {
        slider.uvRect = new Rect(slider.uvRect.position + new Vector2(xPos, yPos) * Time.deltaTime, slider.uvRect.size);
    }



}
