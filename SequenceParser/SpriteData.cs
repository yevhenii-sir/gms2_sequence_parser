using System.Text;

namespace SequenceParser
{
    public class SpriteData
    {
        public string SpriteName;
        public int IndexVariable;
        public List<ModificatorData> ModificatorsSpriteData;

        public SpriteData(string spriteName, int indexVariable)
        {
            ModificatorsSpriteData = new List<ModificatorData>();
            SpriteName = spriteName;
            IndexVariable = indexVariable;
        }

        public string CreationVariableCodeGenerate()
        {
            StringBuilder tempString = new StringBuilder();

            foreach (var (naming, modificatorData) in GetNames())
                tempString.AppendLine($"{naming.variableName} = new seq_element({modificatorData.DataValues});");

            return tempString.ToString();
        }

        public string DrawSpriteCodeGenerate()
        {
            StringBuilder tempString = new StringBuilder($"draw_sprite_ext({SpriteName}, ");
            Dictionary<string, string> dataDrawSprite = GetStandardValuesDrawSprite();

            foreach (var (naming, modificatorData) in GetNames())
            {
                dataDrawSprite[naming.drawDictionaryName] = $"{naming.variableName}.current_value";
                if (modificatorData.Item2 <= 1)
                {
                    dataDrawSprite[naming.drawDictionaryName] = modificatorData.Item3.ToString() ?? "Undefined";
                }
            }

            var xPosition = double.TryParse(dataDrawSprite["origin_x"], out double originX)
                ? $"- {originX} * {dataDrawSprite["scale_x"]}"
                : $"- {dataDrawSprite["origin_x"]} * {dataDrawSprite["scale_x"]}";

            var yPosition = double.TryParse(dataDrawSprite["origin_y"], out double originY)
                ? $"- {originY} * {dataDrawSprite["scale_y"]}"
                : $"- {dataDrawSprite["origin_y"]} * {dataDrawSprite["scale_y"]}";

            tempString.Append($"{dataDrawSprite["image_index_x"]}, ");
            tempString.Append(dataDrawSprite["origin_x"] == "0" && dataDrawSprite["origin_y"] == "0"
                ? $"x + {dataDrawSprite["position_x"]}, y + {dataDrawSprite["position_y"]}, "
                : $"x + {dataDrawSprite["position_x"]} + {xPosition}, y + {dataDrawSprite["position_y"]} + {yPosition}, ");
            
            tempString.Append($"{dataDrawSprite["scale_x"]}, {dataDrawSprite["scale_y"]}, {dataDrawSprite["rotation_x"]}, c_white, 1");
            tempString.Append(");");
            return tempString.ToString();
        }

        public IEnumerable<((string variableName, string drawDictionaryName) naming, (string DataValues, int DataListSize, double? firstElementValue) modificatorData)> GetNames()
        {
            foreach (var modificatorData in ModificatorsSpriteData)
            {
                var dataModificator = modificatorData.GetData();
                foreach (var dataModificatorKey in dataModificator.Keys)
                {
                    if (dataModificator[dataModificatorKey].Item2 <= 1) continue;
                    var nameVariable = modificatorData.ModificatorName +
                                       "_" + SpriteName +
                                       "_" + dataModificatorKey +
                                       "_" + IndexVariable;

                    yield return ((nameVariable, modificatorData.ModificatorName + "_" + dataModificatorKey), dataModificator[dataModificatorKey]);
                }
            }
        }
        
        public List<string> GetListNames() =>
            GetNames().Select(x => x.naming.variableName).ToList();

        private Dictionary<string, string> GetStandardValuesDrawSprite()
        {
            Dictionary<string, string> standardData = new Dictionary<string, string>
            {
                ["image_index_x"] = "0",
                ["rotation_x"] = "0",
                ["position_x"] = "0",
                ["position_y"] = "0",
                ["origin_x"] = "0",
                ["origin_y"] = "0",
                ["scale_x"] = "1",
                ["scale_y"] = "1"
            };

            return standardData;
        }
    }
}