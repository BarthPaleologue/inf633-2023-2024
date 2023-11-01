using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FovUI : MonoBehaviour
{
    [SerializeField] private float fov = 70f;
    private Image image;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = fov / 360f;
        // set Z rotation to fov / 2
        /*Vector3 planeRotation = rectTransform.eulerAngles;
        planeRotation.z = fov / 2f;
        rectTransform.eulerAngles = planeRotation;*/
    }
}
