using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCassidyComponents : MonoBehaviour
{
    [Header("Body Setup Objects")]
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject bodyMesh;
    [SerializeField] private GameObject combatAnchor;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private GameObject meleeWeapon;
    [Space(10)]
    [Header("Camera Setup Objects")]
    [SerializeField] private GameObject cameraPrefab;
    [SerializeField] private GameObject cameraTarget;
    [SerializeField] private GameObject cameraPoint;
    [Space(10)]
    [Header("Menu Setup Objects")]
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject menuPrefab;
    [SerializeField] private GameObject menuOverlay;
    [Space(10)]
    [Header("Menu Pages")]
    [SerializeField] private List<GameObject> menuPages;
    [Space(10)]
    [Header("Menu Controls")]
    [SerializeField] private GameObject menuControlsPanel;
    [SerializeField] private GameObject menuControlsButton;
    [SerializeField] private GameObject menuControlsLeft;
    [SerializeField] private GameObject menuControlsRight;

    void Awake () {
        SpawnPrimaryComponents();
    }

    private void SpawnPrimaryComponents()
    {
        var body = Instantiate(this.bodyPrefab);
        var canvas = Instantiate(this.menuCanvas);

        body.transform.SetParent(gameObject.transform, false);
        canvas.transform.SetParent(gameObject.transform, false);

        SpawnBodyComponents(body);
        SpawnMenuComponents(canvas, body);
    }

    private void SpawnMenuComponents(GameObject canvas, GameObject body)
    {
        var menu = Instantiate(this.menuPrefab);
        menu.transform.SetParent(canvas.transform, false);
        menu.GetComponent<InventoryAdapter>().inventory = body.GetComponent<CassidyInventorySystem>();
        menu.GetComponent<InventoryAdapter>().combat = body.GetComponent<CassidyCombat>();

        var overlay = Instantiate(this.menuOverlay);
        overlay.transform.SetParent(menu.transform, false);

        var pages = new GameObject("Pages");
        pages.transform.SetParent(menu.transform, false);
        AddMenuLayer(pages);

        var controls = new GameObject("Controls");
        controls.transform.SetParent(menu.transform, false);
        AddMenuLayer(controls);

        var controlsPanel = Instantiate(this.menuControlsPanel);
        controlsPanel.transform.SetParent(controls.transform, false);

        SpawnMenuPages(menu, pages, controlsPanel);

        body.GetComponent<CassidyInventorySystem>().inventoryMenu = menu;
    }

    private void SpawnMenuPages(GameObject menu, GameObject pages, GameObject controlsPanel)
    {
        CreatePages(this.menuPages, pages, menu, controlsPanel);

    }

    private void CreatePages(IEnumerable<GameObject> pagesToBuild, GameObject pages, GameObject menu, GameObject controlsPanel)
    {
        var controls = menu.GetComponent<ControlPanelControl>();

        var leftButton = Instantiate(this.menuControlsLeft);
        leftButton.transform.SetParent(controlsPanel.transform, false);
        foreach (var page in pagesToBuild)
        {
            var crafting = Instantiate(page);
            crafting.transform.SetParent(pages.transform, false);
            var index = controls.AddPanel(crafting);

            var craftingButton = Instantiate(this.menuControlsButton);
            craftingButton.transform.SetParent(controlsPanel.transform, false);
            craftingButton.GetComponent<Button>().onClick.AddListener(() => controls.SetPanelActive(index));
            craftingButton.GetComponentInChildren<Text>().text = crafting.name;
        }
        var rightButton = Instantiate(this.menuControlsRight);
        rightButton.transform.SetParent(controlsPanel.transform, false);
    }

    private void SpawnBodyComponents(GameObject body)
    {
        var mesh = Instantiate(this.bodyMesh);
        var anchor = Instantiate(this.combatAnchor);
        var target = Instantiate(this.cameraTarget);
        var ground = Instantiate(this.groundCheck);

        mesh.transform.SetParent(body.transform, false);
        anchor.transform.SetParent(body.transform, false);
        target.transform.SetParent(body.transform, false);
        ground.transform.SetParent(body.transform, false);

        body.GetComponent<CassidyMovement>().groundCheck = ground.transform;
        body.GetComponent<CassidyCombat>().target = target.transform;
        body.GetComponent<CassidyCombat>().source = anchor.transform;
        body.GetComponent<CassidyCombat>().meleeWeaponItem = this.meleeWeapon;

        SpawnCameraComponents(body, anchor, target);
    }

    private void SpawnCameraComponents(GameObject body, GameObject anchor, GameObject target)
    {
        var point = Instantiate(this.cameraPoint);
        point.transform.SetParent(body.transform, false);
        point.GetComponent<LerpTwoTransforms>().Setup(anchor.transform, target.transform);

        var camera = Instantiate(this.cameraPrefab);
        camera.transform.SetParent(gameObject.transform, false);
        camera.GetComponent<LerpTwoTransforms>().Setup(camera.transform, point.transform);
    }

    private void AddMenuLayer(GameObject layer)
    {
        var rect = layer.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(1, 1);
    }
}
