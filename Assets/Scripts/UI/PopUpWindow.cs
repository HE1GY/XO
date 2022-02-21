using GameLogic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Events;


[RequireComponent(typeof(Animator))]
public class PopUpWindow : MonoBehaviour
{
    [SerializeField] private Text _winnerText;

    private Animator _animator;
    private static readonly int ShownWindowHashCode = Animator.StringToHash("IsShown");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventsControllerXo.AddListener<Mark>(EventsTypeXo.Win, TurnOn);
        EventsControllerXo.AddListener<Mark>(EventsTypeXo.Draw, TurnOn);
        EventsControllerXo.AddListener(EventsTypeXo.ReStart, TurnOff);
    }

    private void OnDisable()
    {
        EventsControllerXo.RemoveListener<Mark>(EventsTypeXo.Win, TurnOn);
        EventsControllerXo.RemoveListener<Mark>(EventsTypeXo.Draw, TurnOn);
        EventsControllerXo.RemoveListener(EventsTypeXo.ReStart, TurnOff);
    }


    private void TurnOff()
    {
        _animator.SetBool(ShownWindowHashCode, false);
    }

    private void TurnOn(Mark winner)
    {
        _animator.SetBool(ShownWindowHashCode, true);
        SetWinner(winner);
    }

    private void SetWinner(Mark winner)
    {
        _winnerText.text = winner.ToString();
    }
}