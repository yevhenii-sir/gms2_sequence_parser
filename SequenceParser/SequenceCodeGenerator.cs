using System.Text;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SequenceParser
{
    public class SequenceCodeGenerator
    {
        private List<SpriteData> _spriteDataList;
        private int _indexSprite = 0;
        private double _lengthSequence = -1;
        private string _jsonText;

        public SequenceCodeGenerator(string jsonText)
        {
            _spriteDataList = new List<SpriteData>();
            _jsonText = jsonText;
            DataListInitialization();
        }

        public void RegenerateSpriteDataList(string jsonText)
        {
            _jsonText = jsonText;
            _spriteDataList = new List<SpriteData>();
            DataListInitialization();
        }
            
        private static string RemoveExcessiveNewlines(string input)
        {
            string pattern = @"(\n{3,})";
            string replacement = "\n\n";
            string result = Regex.Replace(input, pattern, replacement);
        
            return result;
        }

        public string GenerateFinalCode()
        {
            StringBuilder tempStringCreationVariables = 
                new StringBuilder($"seq = new seq_struct({_lengthSequence}, 1.0, function() {{seq.start_play();}});\n\n");
            StringBuilder tempStringDraw = new StringBuilder();
            StringBuilder addedSeqString = new StringBuilder();

            foreach (var spriteData in _spriteDataList)
            {
                addedSeqString.Append(String.Join("", 
                    spriteData.GetListNames().Select(nameVariable => "seq.add_element(" + nameVariable + ");\n"))
                );
                
                tempStringCreationVariables.Append(spriteData.CreationVariableCodeGenerate()).Append("\n");
                tempStringDraw.Append(spriteData.DrawSpriteCodeGenerate()).Append("\n");
            }
            
            StringBuilder finalText = new StringBuilder();
            finalText.Append(RemoveExcessiveNewlines(tempStringCreationVariables.ToString()));
            finalText.Append(addedSeqString).Append("seq.start_play();\n");
            finalText.Append(string.Join("\n", tempStringDraw.ToString().Split("\n").Reverse()));

            return finalText.ToString();
        }

        private void DataListInitialization()
        {
            try
            {
                Root? myDeserializedClass = JsonConvert.DeserializeObject<Root>(_jsonText);
                if (myDeserializedClass == null) throw new Exception("Не смогло конвертировать Json!");
                
                _lengthSequence = myDeserializedClass.length;
                
                foreach (var track in myDeserializedClass.tracks)
                {
                    var spriteName = track.keyframes == null ? "Undefined" : track.keyframes.Keyframes[0].Channels._0.Id.name;
                    
                    SpriteData spriteData = new SpriteData(spriteName, _indexSprite++);
                    foreach (var trackTracks in track.tracks)
                    {
                        switch (trackTracks.name)
                        {
                            case "image_speed":
                            case "blend_multiply":
                                continue;
                        }

                        ModificatorData modificatorData = new ModificatorData(trackTracks.name);
                        DataFrame frameXData = new DataFrame();
                        DataFrame frameYData = new DataFrame();

                        foreach (var keyframe in trackTracks.keyframes.Keyframes)
                        {
                            frameXData.AddedFrameData(keyframe.Key, keyframe.Channels._0.RealValue);
                            
                            if (keyframe.Channels._1 != null)
                                frameYData.AddedFrameData(keyframe.Key, keyframe.Channels._1.RealValue);
                        }

                        modificatorData.SetDataX(frameXData);
                        modificatorData.SetDataY(frameYData);
                        spriteData.ModificatorsSpriteData.Add(modificatorData);
                    }

                    _spriteDataList.Add(spriteData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("You invalid or JSON invalid!\n\n");
                Console.WriteLine("Возможно елементы находяться в групах (папках)!");
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}