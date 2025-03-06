namespace ComputerRepairService.Helpers
{
    public class SelectedObjectMessage<SelectedObjectType>
    {
        public object WhoRequestedToSelect { get; set; }
        public SelectedObjectType SelectedObject { get; set; }

        public SelectedObjectMessage(object whoRequestedToSelect, SelectedObjectType selectedObject)
        {
            WhoRequestedToSelect = whoRequestedToSelect;
            SelectedObject = selectedObject;
        }
    }
}
