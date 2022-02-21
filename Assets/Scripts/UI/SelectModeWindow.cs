using Utilities.Events;

namespace UI
{
    public class SelectModeWindow : SelectWindow
    {
        private void Start()
        {
            TurnOn();
        }
        protected override void OnFirstButtonClick()
        {
            EventsControllerXo.Broadcast<bool>(EventsTypeXo.SelectMode, true);
            TurnOff();
        }

        protected override void OnSecondButtonClick()
        {
            EventsControllerXo.Broadcast<bool>(EventsTypeXo.SelectMode, false);
            TurnOff();
        }
    }
}