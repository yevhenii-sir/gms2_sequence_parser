namespace SequenceParser
{
    public class ModificatorData
    {
        public string ModificatorName;
        private DataFrame XFrameData;
        private DataFrame YFrameData;
        private bool OnlyOneDataLine;

        public ModificatorData(string nameModificator)
        {
            ModificatorName = nameModificator;
            OnlyOneDataLine = IsOneDataLine();
        }

        public void SetDataX(DataFrame dataFrame)
        {
            XFrameData = dataFrame;
        }
        
        public void SetDataY(DataFrame dataFrame)
        {
            YFrameData = dataFrame;
        }
        
        public Dictionary<string, (string, int, double?)> GetData()
        {
            Dictionary<string, (string, int, double?)> data = new Dictionary<string, (string, int, double?)>();
            
            data["x"] = (XFrameData.ToString(), XFrameData.DataList.Count, XFrameData.DataList.ElementAtOrDefault(0));
            if (!OnlyOneDataLine) data["y"] = (YFrameData.ToString(), YFrameData.DataList.Count, YFrameData.DataList.ElementAtOrDefault(0));

            return data;
        }

        private bool IsOneDataLine()
        {
            switch (ModificatorName)
            {
                case "image_index":
                case "rotation":
                    return true;
                
                default: return false;
            }
        }
    }
}