namespace Portfolio.Models
{

    // Class used to pass data from json to controller
    public class ArtAddInfo
    {
        public Artwork ObjArtwork { get; set; }
        public int Qty { get; set; }

        public int Action { get; set; }
    }
}
