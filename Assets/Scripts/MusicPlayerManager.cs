using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RhythmTool
{
    public class MusicPlayerManager : MonoBehaviour
    {
        #region Instance
        public static MusicPlayerManager Instance => instance;
        private static MusicPlayerManager instance;
        #endregion


        public RhythmAnalyzer analyzer;
        public RhythmPlayer player;
        public RhythmEventProvider eventProvider;

        public event Action<Beat> onBeat;
        public event Action<Onset, Note> onOnset;

        private List<Chroma> chromaFeatures;

        private Note lastNote = Note.FSHARP;

        void Awake()
        {
            if (instance == null)
                instance = this;

            analyzer.Initialized += OnInitialized;
            player.Reset += OnReset;

            eventProvider.Register<Beat>(OnBeat);
            eventProvider.Register<Onset>(OnOnset);
            eventProvider.Register<Value>(OnSegment, "Segments");

            chromaFeatures = new List<Chroma>();
        }

        void Update()
        {
            if (!player.isPlaying)
                return;
        }

        private void OnInitialized(RhythmData rhythmData)
        {
            //Start playing the song.
            player.Play();

        }

        private void OnReset()
        {

        }

        private void OnBeat(Beat beat)
        {
            onBeat?.Invoke(beat);
            //Debug.Log("Beat TimeStamp : " + beatTimeStamp);

            //Instantiate a line to represent the Beat.
            //CreateLine(beat.timestamp, 0, 0.3f, Color.black, 1);

            //Update BPM text.
            float bpm = Mathf.Round(beat.bpm * 10) / 10;
        }

        private void OnOnset(Onset onset)
        {
            //Clear any previous Chroma features.
            chromaFeatures.Clear();

            //Find Chroma features that intersect the Onset's timestamp.
            player.rhythmData.GetIntersectingFeatures(chromaFeatures, onset.timestamp, onset.timestamp);

            if (chromaFeatures.Count > 0)
                lastNote = chromaFeatures[chromaFeatures.Count - 1].note;

            onOnset?.Invoke(onset, lastNote);
        }

        private void OnSegment(Value segment)
        {
            
        }
    }
}