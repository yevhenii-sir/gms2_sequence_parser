using System.Collections.Generic;
using Newtonsoft.Json;

namespace SequenceParser
{
    public class _0
    {
        public Id Id { get; set; }
        public string resourceVersion { get; set; }
        public string resourceType { get; set; }
        public object EmbeddedAnimCurve { get; set; }
        public double RealValue { get; set; }
        public object AnimCurveId { get; set; }
        public long? Colour { get; set; }
    }

    public class _1
    {
        public object EmbeddedAnimCurve { get; set; }
        public double RealValue { get; set; }
        public object AnimCurveId { get; set; }
        public string resourceVersion { get; set; }
        public string resourceType { get; set; }
    }

    public class Channels
    {
        [JsonProperty("0")] public _0 _0 { get; set; }

        [JsonProperty("1")] public _1 _1 { get; set; }
    }

    public class Events
    {
        public List<object> Keyframes { get; set; }
        public string resourceVersion { get; set; }
        public string resourceType { get; set; }
    }

    public class EventToFunction
    {
    }

    public class Id
    {
        public string name { get; set; }
        public string path { get; set; }
    }

    public class Keyframe
    {
        public string id { get; set; }
        public double Key { get; set; }
        public double Length { get; set; }
        public bool Stretch { get; set; }
        public bool Disabled { get; set; }
        public bool IsCreationKey { get; set; }
        public Channels Channels { get; set; }
        public string resourceVersion { get; set; }
        public string resourceType { get; set; }
    }

    public class kkkkjkframes
    {
        public List<Keyframe> Keyframes { get; set; }
        public string resourceVersion { get; set; }
        public string resourceType { get; set; }
    }

    public class Moments
    {
        public List<object> Keyframes { get; set; }
        public string resourceVersion { get; set; }
        public string resourceType { get; set; }
    }

    public class Parent
    {
        public string name { get; set; }
        public string path { get; set; }
    }

    public class Root
    {
        public object spriteId { get; set; }
        public int timeUnits { get; set; }
        public int playback { get; set; }
        public double playbackSpeed { get; set; }
        public int playbackSpeedType { get; set; }
        public bool autoRecord { get; set; }
        public double volume { get; set; }
        public double length { get; set; }
        public Events events { get; set; }
        public Moments moments { get; set; }
        public List<Track> tracks { get; set; }
        public VisibleRange visibleRange { get; set; }
        public bool lockOrigin { get; set; }
        public bool showBackdrop { get; set; }
        public bool showBackdropImage { get; set; }
        public string backdropImagePath { get; set; }
        public double backdropImageOpacity { get; set; }
        public int backdropWidth { get; set; }
        public int backdropHeight { get; set; }
        public double backdropXOffset { get; set; }
        public double backdropYOffset { get; set; }
        public int xorigin { get; set; }
        public int yorigin { get; set; }
        public EventToFunction eventToFunction { get; set; }
        public object eventStubScript { get; set; }
        public Parent parent { get; set; }
        public string resourceVersion { get; set; }
        public string name { get; set; }
        public List<object> tags { get; set; }
        public string resourceType { get; set; }
    }

    public class Track
    {
        public kkkkjkframes keyframes { get; set; }
        public object trackColour { get; set; }
        public bool inheritsTrackColour { get; set; }
        public int builtinName { get; set; }
        public int traits { get; set; }
        public int interpolation { get; set; }
        public List<Track> tracks { get; set; }
        public List<object> events { get; set; }
        public bool isCreationTrack { get; set; }
        public string resourceVersion { get; set; }
        public string name { get; set; }
        public List<object> tags { get; set; }
        public string resourceType { get; set; }
        public List<object> modifiers { get; set; }
    }

    public class VisibleRange
    {
        public double x { get; set; }
        public double y { get; set; }
    }
}