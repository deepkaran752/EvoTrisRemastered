namespace babbarversestudios 
{ 
    public class BabaMessWindow : GeneralWindow
    {
        public override bool IsCurrentActiveWindow { get; set; } = false;
        public override bool IsMinimized { get; set; } = false;

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
