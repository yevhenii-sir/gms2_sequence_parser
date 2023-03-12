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

                    tempString.Append(
                        $"{nameVariable} = new seq_element({dataModificator[dataModificatorKey].Item1});\n");
                }
            }

            return tempString.ToString();
        }

        public List<string> GetListNames()
        {
            List<string> names = new List<string>();
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

                    names.Add(nameVariable);
                }
            }

            return names;
        }

        public string DrawSpriteCodeGenerate()
        {
            StringBuilder tempString = new StringBuilder($"draw_sprite_ext({SpriteName}, ");
            Dictionary<string, string> dataDrawSprite = GetStandardValuesDrawSprite();

            foreach (var modificatorData in ModificatorsSpriteData)
            {
                var dataModificator = modificatorData.GetData();
                foreach (var dataModificatorKey in dataModificator.Keys)
                {
                    var variableNames = GetVariableNames(modificatorData, dataModificatorKey);
                    dataDrawSprite[variableNames.drawDictionaryName] = $"{variableNames.variableName}.current_value";

                    if (dataModificator[dataModificatorKey].Item2 <= 1)
                    {
                        dataDrawSprite[variableNames.drawDictionaryName] = dataModificator[dataModificatorKey].Item3.ToString();
                    }
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

        private (string variableName, string drawDictionaryName) GetVariableNames(ModificatorData modificatorData, string dataModificatorKey)
        {
            return (modificatorData.ModificatorName +
                    "_" + SpriteName +
                    "_" + dataModificatorKey +
                    "_" + IndexVariable,
                    modificatorData.ModificatorName + "_" + dataModificatorKey);
        }

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