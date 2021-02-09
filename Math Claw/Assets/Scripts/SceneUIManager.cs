using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class SceneUIManager : MonoBehaviour {
    private GameObject currencyPanel;
    private GameObject mainSettingsPanel;
    private GameObject MainBottomPanel;
    private GameObject CollectionPanel;
    private GameObject TaskPanel;
    private GameObject GashaponDamagedPanel;
    private GameObject GashaponBrokenPanel;

    private void Start() {
        currencyPanel = GameObject.Find("CurrencyPanel");
        mainSettingsPanel = GameObject.Find("MainSettingsPanel");
        MainBottomPanel = GameObject.Find("MainBottomPanel");
        CollectionPanel = GameObject.Find("CollectionPanel");
        TaskPanel = GameObject.Find("TaskPanel");
        GashaponDamagedPanel = GameObject.Find("GashaponDamagedPanel");
        GashaponBrokenPanel = GameObject.Find("GashaponBrokenPanel");
    }

    public void ChangeVisiblePanels(IEnumerable<GameObject> panelsToEnable, IEnumerable<GameObject> panelsToDisable) {
        foreach (var panel in panelsToEnable) 
            panel.SetActive(true);

        foreach (var panel in panelsToDisable) 
            panel.SetActive(false);
    }

}
