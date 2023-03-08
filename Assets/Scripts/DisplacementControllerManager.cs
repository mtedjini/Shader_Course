using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RhythmTool.Examples;
using RhythmTool;

public class DisplacementControllerManager : MonoBehaviour
{
    #region Instance
    public static DisplacementControllerManager Instance => instance;
    private static DisplacementControllerManager instance;
    #endregion

    #region Attributes
    [SerializeField] private DisplacementControl sphere;
    [SerializeField] private Color A, ASharp, B, C, CSharp, D, DSharp, E, F, FSharp, G, GSharp;

    private List<Chroma> chromaFeatures;
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        MusicPlayerManager.Instance.onOnset += OnOnset;

        chromaFeatures = new List<Chroma>();
    }

    private void OnDestroy()
    {
        MusicPlayerManager.Instance.onOnset -= OnOnset;
    }

    private Color GetColor(Note note)
    {
        switch (note)
        {
            case Note.A:
                return A;
            case Note.ASharp:
                return ASharp;
            case Note.B:
                return B;
            case Note.C:
                return C;
            case Note.CSHARP:
                return CSharp;
            case Note.D:
                return D;
            case Note.DSHARP:
                return DSharp;
            case Note.E:
                return E;
            case Note.F:
                return F;
            case Note.FSHARP:
                return FSharp;
            case Note.G:
                return G;
            case Note.GSHARP:
                return GSharp;
            default:
                return Color.white;
        }
    }

    private void OnOnset(Onset onset, Note note)
    {
        sphere.OnOnset(onset, GetColor(note));
    }
}
