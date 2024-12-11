using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{

    private StageManager stageManager;

    [SerializeField] private int leftOrRight;

    private CanvasGroup infoParent;
    private Image image;
    private TextMeshProUGUI hintText;

    private Animator anim;
    private bool isOpen = false;
    private bool isInteractable = false;

    private void Start()
    {
        stageManager = StageManager.instance;

        anim = GetComponent<Animator>();
        infoParent = GetComponentInChildren<CanvasGroup>();
        image = GetComponentInChildren<Image>();
        hintText = GetComponentInChildren<TextMeshProUGUI>();

        UpdateDoorInfo();
        OnStageStateChange(stageManager.CurrentState, 0);

        stageManager.OnStageStateChange += OnStageStateChange;
    }

    private void UpdateDoorInfo()
    {
        var node = stageManager.GetNextNode(leftOrRight);
        if (node == null) return;
        image.sprite = Resources.Load<Sprite>(node.spritePath);
        hintText.text = node.hint;
    }

    private void HideDoorInfo()
    {
        infoParent.alpha = 0;
    }

    private void ShowDoorInfo()
    {
        infoParent.alpha = 1;
    }

    private void OnDisable()
    {
        stageManager.OnStageStateChange -= OnStageStateChange;
    }

    private void OnStageStateChange(StageManager.StageState state, int stageLevel)
    {

        switch (state)
        {
            case StageManager.StageState.Start:
                isOpen = false;
                anim.SetBool("Open", false);
                UpdateDoorInfo();
                HideDoorInfo();
                break;
            case StageManager.StageState.Reward:
                isOpen = false;
                anim.SetBool("Open", false);
                break;
            case StageManager.StageState.Complete:
                isOpen = true;
                anim.SetBool("Open", true);
                ShowDoorInfo();
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Next Stage" + isInteractable + " " + isOpen);
        }

        if (isInteractable && isOpen && Input.GetKeyDown(KeyCode.W))
        {
            stageManager.NextStage(leftOrRight);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = false;
        }
    }
}
