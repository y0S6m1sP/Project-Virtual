using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{

    private StageManager stageManager;

    [SerializeField] private int leftOrRight;

    private Image image;
    private TextMeshProUGUI hintText;

    private Animator anim;
    private bool isOpen = false;
    private bool isInteractable = false;

    private void Start()
    {
        stageManager = StageManager.instance;

        anim = GetComponent<Animator>();
        image = GetComponentInChildren<Image>();
        hintText = GetComponentInChildren<TextMeshProUGUI>();

        UpdateDoorInfo();
        OnStageStateChange(stageManager.CurrentState);

        stageManager.OnStageStateChange += OnStageStateChange;
    }

    private void UpdateDoorInfo()
    {
        var node = stageManager.GetNextNode(leftOrRight);
        if (node == null) return;
        image.sprite = Resources.Load<Sprite>(node.spritePath);
        hintText.text = node.hint;
    }

    private void OnDisable()
    {
        stageManager.OnStageStateChange -= OnStageStateChange;
    }

    private void OnStageStateChange(StageManager.StageState state)
    {
        Debug.Log("Door State: " + state);
        switch (state)
        {
            case StageManager.StageState.Start:
                isOpen = false;
                anim.SetBool("Open", false);
                UpdateDoorInfo();
                break;
            case StageManager.StageState.Reward:
                isOpen = false;
                anim.SetBool("Open", false);
                break;
            case StageManager.StageState.Complete:
                Debug.Log("hello");
                isOpen = true;
                anim.SetBool("Open", true);
                break;
        }
    }

    private void Update()
    {
        if (isInteractable && isOpen && Input.GetKeyDown(KeyCode.W))
        {
            stageManager.NextStage(leftOrRight);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInteractable = false;
    }
}
