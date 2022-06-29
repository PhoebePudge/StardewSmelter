using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlideableMask : MonoBehaviour {
    [SerializeField]
    private Slider slider;

    private RectTransform rectTransform;
    private Vector3 farLeft;
    private Vector3 farRight;

    private void Awake() {
        //add listener to slider
        rectTransform = GetComponent<RectTransform>();
        slider.onValueChanged.AddListener(HandleSliderChanged);

        //set positions for far left and right positions
        farLeft = rectTransform.position - new Vector3(rectTransform.rect.width, 0f);
        farRight = rectTransform.position;
    }

    private void Start() {
        HandleSliderChanged(slider.value); // This needs to be called AFTER RectTransformLockPositions's Awake().
    }

    private void HandleSliderChanged(float value) {
        //lerp slider area
        rectTransform.position = Vector2.Lerp(farLeft, farRight, value);
    }
}
