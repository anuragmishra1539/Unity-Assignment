using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class healthbar : MonoBehaviour
{
    [SerializeField] private Health Phealth;
    [SerializeField] private UnityEngine.UI.Image totalHealthBar;
    [SerializeField] private UnityEngine.UI.Image crrhealthBar;
    private void Start()
    {
        totalHealthBar.fillAmount = Phealth.crrHealth / 10;
    }
    private void Update()
    {
        crrhealthBar.fillAmount = Phealth.crrHealth/10;
    }

}
