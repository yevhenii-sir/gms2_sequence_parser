namespace SequenceParser
{
    public class DataFrame
    {
        public List<double> FrameList;
        public List<double> DataList;

        public DataFrame()
        {
            FrameList = new List<double>();
            DataList = new List<double>();
        }
        
        public void AddedFrameData(double frameIndex, double dataFrame)
        {
            FrameList.Add(frameIndex);
            DataList.Add(dataFrame);
        }
        
        public override string ToString()
        {
            return $"[{String.Join(", ", FrameList)}], [{String.Join(", ", DataList)}]";
        }
    }
}