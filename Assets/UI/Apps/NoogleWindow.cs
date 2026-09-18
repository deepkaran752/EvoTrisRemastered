namespace babbarversestudios
{
    public class NoogleWindow : GeneralWindow
    {
        public override bool IsCurrentActiveWindow { get; set; } = false;
        public override bool IsMinimized { get; set; } = false;

        private void Awake()
        {
            UnityEngine.Debug.Log("[DK LOG] here");
        }
        public override void Open()
        {
            base.Open();
            IsCurrentActiveWindow = true;
            //this.gameObject.SetActive(true);
        }

        public override void Close()
        {
            //TODO: animator for open close
            base.Close();
            IsCurrentActiveWindow = false;
            this.gameObject.SetActive(false);
        }

        public void Minimize()
        {
            IsMinimized = true;
            Close();
        }
    }
}
