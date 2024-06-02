using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using NaughtyAttributes;
public class DialogController : MonoBehaviour
{
    public static DialogController Instance;

    [Header("Main Window Elements")]
    public GameObject dialogWindow;
    public Button nextBtn;

    [Header("Speaker Elements")]
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI speakerDialogLine;
    public Image speakerPortrait;

    [Header("Dialog Events")]
    public GameEvent OnDialogEnd;
    public GameEvent OnDialogStart;
    public GameEvent OnEndDialog;
    public GameEvent OnEndDialogFinish;

    [Space]
    [Header("Config")]
    // this should be given by level Manager if dialog is in the chapter. 
    [ReadOnly, SerializeField]
    internal DialogData startingConversation;
    [ReadOnly, SerializeField]
    // conversation data for ending dialogs. 
    public DialogData endingConversation;

    public bool hasDialogMusic;

    //[ShowIf("hasDialogMusic")]
    //public SoundEffectSO dialogMusic;

    [ShowIf("hasDialogMusic")]
    public AudioSource mainSource;

    private DialogData runningDialog;
    private DialogData.Conversation runningConversation;

    public Action OnNextDialog = delegate { };

    private bool dialogEnd = false;

    internal int dialogChapter;

    public void EndMusic()
    {
        if (!hasDialogMusic) return;

        mainSource.Stop();
        mainSource.clip = levelClip;
        mainSource.Play();
    }
    private AudioClip levelClip;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //OnEndDialog.OnRaise.AddListener((v) => ConcludeDialog());

        OnNextDialog += CheckConversationLimit;

        //InitiateDialog(startingConversation);

        //if (dialogMusic != null && mainSource != null && hasDialogMusic)
        //{
        //    SetUpDialogMusic();
        //}
    }

    void SetUpDialogMusic()
    {
        levelClip = mainSource.clip;
        //mainSource.clip = dialogMusic.clip;

        mainSource.Play();
    }

    void ConcludeDialog()
    {
        OnEndDialog.OnRaise.RemoveListener((v) => ConcludeDialog());
        //InitiateDialog(endingConversation);
    }

    int dialogIdx = 0;

    /// <summary>
    /// Initiate Dialog values upon completion of tasks, then call Start initiated dialog when 
    /// </summary>
    /// <param name="_dialogToUse"> Dialog Segment to use </param>
    /// <param name="conversation"> Dialog Conversation List to use </param>
    /// <param name="startImmediate"> Should it begin immediately after choosing to do tasks or after an event. </param>
    public void InitiateDialog(DialogData _dialogToUse, DialogData.Conversation conversation, bool startImmediate = false)
    {
        runningDialog = _dialogToUse;
        runningConversation = conversation;
        // get chapter, for now we assume it's first. 
        dialogEnd = false;
        nextBtn.onClick.AddListener(() => NextDialog());

        if (startImmediate)
        {
            PlayDialog(dialogIdx); // starts index 0. 
            OnDialogStart.Raise();
        }
    }
    public void InitiateDialog(DialogData _dialogToUse, int conversationID, bool startImmediate = false)
    {
        Debug.Log("Dialog to use is: " + _dialogToUse);
        runningDialog = _dialogToUse;
        runningConversation = _dialogToUse.conversationList[conversationID];
        // get chapter, for now we assume it's first. 
        dialogEnd = false;
        nextBtn.onClick.AddListener(() => NextDialog());

        if (startImmediate)
        {
            PlayDialog(dialogIdx); // starts index 0. 
            OnDialogStart.Raise();
        }
    }

    /// <summary>
    /// Called, when the task event has occurred. 
    /// </summary>
    public void StartInitiatedDialog()
    {
        dialogIdx = 0;
        PlayDialog(dialogIdx);
        OnDialogStart.Raise();
    }

    void PlayDialog(int dialogIdx)
    {
        speakerName.text = runningConversation.dialogues[dialogIdx].speaker.Name;
        speakerDialogLine.text = runningConversation.dialogues[dialogIdx].line;

        #region PORTRAIT AREA
        switch (runningConversation
            .dialogues[dialogIdx].dialogExpression)
        {
            case PortraitEmotions.NORMAL:
                speakerPortrait.sprite = runningConversation
            .dialogues[dialogIdx].speaker.portraitEmotions.Default;
                break;
            case PortraitEmotions.ANGRY:
                speakerPortrait.sprite = runningConversation
            .dialogues[dialogIdx].speaker.portraitEmotions.Angry;
                break;
            case PortraitEmotions.SAD:
                speakerPortrait.sprite = runningConversation
            .dialogues[dialogIdx].speaker.portraitEmotions.Sad;
                break;
            case PortraitEmotions.HAPPY:
                speakerPortrait.sprite = runningConversation
            .dialogues[dialogIdx].speaker.portraitEmotions.Happy;
                break;
            case PortraitEmotions.DETERMINED:
                speakerPortrait.sprite = runningConversation
            .dialogues[dialogIdx].speaker.portraitEmotions.Determined;
                break;
            case PortraitEmotions.TERRIFIED:
                speakerPortrait.sprite = runningConversation
            .dialogues[dialogIdx].speaker.portraitEmotions.Terrified;
                break;
            default:
                break;
        }
        #endregion

        // play voice 
        if (runningConversation
            .dialogues[dialogIdx].isVoiceOver)
        {
            Debug.Log("Play one shot audio clip for voice: " + runningConversation
                .dialogues[dialogIdx].voice);
        }
    }
    public void NextDialog()
    {
        dialogIdx++;
        OnNextDialog?.Invoke();
        if (!dialogEnd)
        {
            PlayDialog(dialogIdx);
        }
    }

    public void CheckConversationLimit()
    {
        int limit = runningConversation.dialogues.Count;
        if (!(dialogIdx < limit))
        {
            //if (runningDialog == startingConversation)
            //    OnDialogEnd.Raise();

            // for ending conversation it's done externally for now. 

            dialogWindow.SetActive(false);
            // begin game and so on. 
            dialogEnd = true;
            nextBtn.onClick.RemoveAllListeners();
            if (runningDialog == endingConversation)
            {
                OnEndDialogFinish.Raise();
            }
            dialogIdx = 0;
        }
    }
}
