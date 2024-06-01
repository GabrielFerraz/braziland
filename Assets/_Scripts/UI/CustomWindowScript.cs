using NaughtyAttributes;
using System;
using System.Collections;
using UltEvents;
using UnityEngine;
using UnityEngine.UIElements;
public class CustomWindowScript : MonoBehaviour
{
    [Serializable]
    public class TransformTransition
    {
        public Vector3 startVector;
        public Vector3 endVector;

        public LeanTweenType startEase;
        public LeanTweenType endEase;
        public bool isUnscaled;

        public float easeTime;
        public float startDelay;
        public float endDelay;
    }

    public enum PlayCondition
    {
        OnEnable, OnStart, OnButton
    }
    public WindowType windowType;
    public TransitionType transitionType;

    public PlayCondition playCondition;

    [MinMaxSlider(0, 1), Tooltip("Min value is start fade, and max is end fade"), ShowIf("isFade")]
    public Vector2 fadeValues;
    [Tooltip("Min value is start fade, and max is end fade"), ShowIf("isFade")]
    public float fadeTime;

    [ShowIf("isTransform")]
    public Vector2 startPos;
    [ShowIf("isTransform")]
    public Vector2 endPos;

    [ShowIf("isRotate")]
    public Vector3 startRotation;
    [ShowIf("isRotate")]
    public Vector3 endRotation;

    [SerializeField, ShowIf(EConditionOperator.Or, "isScale", "isTransform", "isRotate"), HideIf("useCurves")]
    TransformTransition transition;

    //public bool useCurves = false;
    //[ShowIf("useCurves")]
    //public AnimationCurve _easeInCurve; 
    //[ShowIf("useCurves")]
    //public AnimationCurve _easeOutCurve;

    #region WINDOW ELEMENTS 
    [Space]
    public GameObject[] windowElements;

    public bool useElements;
    public float elementDelay;
    public float elementStepDelay;

    [ShowIf("useElements")]
    public TransformTransition elementsTransition;


    [Space]
    public UltEvent OnWindowOn;
    public UltEvent OnWindowOff;

    #endregion


    #region Enum Meta Validators
    public bool isFade()
    {
        return transitionType == TransitionType.FADE;
    }
    public bool isScale()
    {
        return transitionType == TransitionType.SCALE;
    }
    public bool isTransform()
    {
        return transitionType == TransitionType.TRANSFORM;
    }
    public bool isRotate()
    {
        return transitionType == TransitionType.ROTATE;
    }
    #endregion
    private Timer startTimer = null;
    private Timer endTimer = null;
    private void Awake()
    {
        // set the start values so we don't always have to in editor. 
        switch (transitionType)
        {
            case TransitionType.FADE:
                if (GetComponent<CanvasGroup>() != null)
                    GetComponent<CanvasGroup>().alpha = fadeValues.x;
                else
                {
                    if (GetComponent<CanvasGroup>() != null)
                        transform.gameObject.AddComponent<CanvasGroup>().alpha = fadeValues.x;
                    else
                    {
                        transform.gameObject.GetComponent<CanvasGroup>().alpha = fadeValues.x;
                    }
                }
                break;
            case TransitionType.SCALE:
                if (windowElements.Length <= 0)
                {
                    transform.localScale = transition.startVector;
                }
                else
                {
                    if (!useElements)
                        for (int i = 0; i < windowElements.Length; i++)
                        {
                            windowElements[i].transform.localScale = transition.startVector;
                        }
                    else
                    {
                        // do that of the major window first. 
                        transform.localScale = transition.startVector;
                        for (int i = 0; i < windowElements.Length; i++)
                        {
                            windowElements[i].transform.localScale = elementsTransition.startVector;
                        }
                    }
                }

                break;
            case TransitionType.TRANSFORM:
                transform.localPosition = startPos;
                break;
            case TransitionType.ROTATE:
                transform.localRotation = Quaternion.Euler(startRotation);
                break;
        }
    }
    void DoScaleUp(Transform _t, TransformTransition _transitionClass, bool isMultiple = false)
    {
        _t.LeanScale(_transitionClass.endVector, _transitionClass.easeTime).
            setEase(_transitionClass.startEase).
            setOnComplete(() => { if (!isMultiple) OnWindowOn?.Invoke(); }).setIgnoreTimeScale(_transitionClass.isUnscaled);
    }
    void DoFadeOn(CanvasGroup grp)
    {
        // fading in. 
        LeanTween.value(grp.gameObject, (e) => { grp.alpha = e; }, fadeValues.x, fadeValues.y, fadeTime).
         setOnComplete(() => OnWindowOn?.Invoke()).setIgnoreTimeScale(transition.isUnscaled);
    }
    void DoScaleDown(Transform _t, TransformTransition _tClass, bool isMultiple = false)
    {
        _t.LeanScale(_tClass.startVector, _tClass.easeTime).
            setEase(_tClass.endEase).
            setOnComplete(() =>
            {
                if (!isMultiple)
                    OnWindowOff?.Invoke();
            }
            ).setIgnoreTimeScale(_tClass.isUnscaled);
    }
    void DoFadeOff(CanvasGroup grp)
    {
        // back fade
        LeanTween.value(grp.gameObject, (e) => { grp.alpha = e; }, fadeValues.y, fadeValues.x, fadeTime).
        setOnComplete(() => OnWindowOff?.Invoke()).setIgnoreTimeScale(transition.isUnscaled);
    }
    private void Start()
    {
        if (playCondition == PlayCondition.OnStart)
            WindowOn();
    }
    private void OnEnable()
    {
        if (playCondition == PlayCondition.OnEnable)
            WindowOn();
    }
    public void WindowOn()
    {
        //if (windowType == WindowType.OnStart)
        //{
        //}

        if (startTimer != null)
            startTimer.Cancel();

        startTimer = Timer.Register(transition.startDelay, () =>
        {
            if (windowType == WindowType.MajorWindow)
            {
                switch (transitionType)
                {
                    case TransitionType.FADE:
                        if (GetComponent<CanvasGroup>() == null)
                            return;
                        CanvasGroup group = GetComponent<CanvasGroup>();
                        // fading in. 
                        DoFadeOn(group);
                        break;
                    case TransitionType.SCALE:
                        if (windowElements.Length <= 0)
                        {
                            DoScaleUp(transform, transition);
                        }
                        else
                        {
                            if (!useElements)
                                for (int i = 0; i < windowElements.Length; i++)
                                {
                                    DoScaleUp(windowElements[i].transform, transition);
                                }
                            else
                            {
                                // do that of the major window first. 
                                DoScaleUp(transform, transition);
                                ScaleUpObjects();
                            }
                        }
                        break;
                    case TransitionType.TRANSFORM:
                        LeanTween.moveLocal(this.gameObject, endPos, transition.easeTime).setEase(transition.endEase).
                         setOnComplete(() => OnWindowOn?.Invoke()).setIgnoreTimeScale(transition.isUnscaled);
                        break;
                    case TransitionType.ROTATE:
                        LeanTween.rotateLocal(gameObject, startRotation, transition.easeTime).setEase(transition.startEase).
                         setOnComplete(() => OnWindowOn?.Invoke()).setIgnoreTimeScale(transition.isUnscaled);
                        break;
                    default:
                        break;
                }
            }
        });

        OnWindowOn?.Invoke();
    }
    public void WindowOff()
    {
        endTimer?.Cancel();

        endTimer = Timer.Register(transition.endDelay, () =>
        {
            if (windowType == WindowType.MajorWindow)
            {
                switch (transitionType)
                {
                    case TransitionType.FADE:
                        if (GetComponent<CanvasGroup>() == null)
                            return;
                        CanvasGroup group = GetComponent<CanvasGroup>();
                        // back fade
                        DoFadeOff(group);
                        break;

                    case TransitionType.SCALE:
                        if (windowElements.Length <= 0)
                        {
                            DoScaleDown(transform, transition);
                        }
                        else
                        {
                            if (!useElements)
                                for (int i = 0; i < windowElements.Length; i++)
                                {
                                    DoScaleDown(windowElements[i].transform, transition);
                                }
                            else
                            {
                                // do that of the major window first. 
                                DoScaleDown(transform, transition);
                                TriggerWindowOff();
                            }
                        }
                        break;

                    case TransitionType.TRANSFORM:
                        LeanTween.moveLocal(gameObject, startPos, transition.easeTime).setEase(transition.endEase).
                        setOnComplete(() => OnWindowOff?.Invoke()).setIgnoreTimeScale(transition.isUnscaled);
                        break;
                    case TransitionType.ROTATE:
                        LeanTween.rotateLocal(gameObject, startRotation, transition.easeTime).setEase(transition.endEase).
                        setOnComplete(() => OnWindowOff?.Invoke()).setIgnoreTimeScale(transition.isUnscaled);
                        break;
                    default:
                        break;
                }
            }
        });
    }

    #region Editor Controls

    [Button("WindowON")]
    public void ScaleUpObjects()
    {
        if (windowElements.Length <= 0) return;
        Timer.Register(elementDelay, () =>
        {
            StartCoroutine(ScaleUpObjectsRoutine());
        });
    }

    IEnumerator ScaleUpObjectsRoutine()
    {
        yield return null;
        for (int i = 0; i < windowElements.Length; i++)
        {
            DoScaleUp(windowElements[i].transform, elementsTransition, true);
            yield return new WaitForSeconds(elementStepDelay);
        }
        OnWindowOn?.Invoke();
    }
    [Button("WindowOFF")]
    public void TriggerWindowOff()
    {
        if (windowElements.Length <= 0) return;
        for (int i = 0; i < windowElements.Length; i++)
        {
            StartCoroutine(ScaleDownObjectsRoutine());
        }
    }
    IEnumerator ScaleDownObjectsRoutine()
    {
        yield return null;
        for (int i = 0; i < windowElements.Length; i++)
        {
            DoScaleDown(windowElements[i].transform, elementsTransition, true);
            yield return new WaitForSeconds(elementStepDelay);
        }
        OnWindowOff?.Invoke();
    }

    [Button("Test Transform")]
    public void SetPos()
    {
        startPos = new Vector2(transform.localPosition.x, transform.localPosition.y);
    }
    #endregion

}
public enum WindowType
{
    MajorWindow,
    MinorWindow,
}

public enum TransitionType
{
    FADE, SCALE, TRANSFORM, ROTATE
}