namespace QhTemplate.AdminWeb.Models.Permissions
{
    public class JsTreeState
    {
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
        
        public JsTreeState()
        {
            opened = false;
            disabled = false;
            selected = false;
        }

        public JsTreeState(bool newOpened, bool newDisabled, bool newSelected)
        {
            opened = newOpened;
            disabled = newDisabled;
            selected = newSelected;
        }
    }
}