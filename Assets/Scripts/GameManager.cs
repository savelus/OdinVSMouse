using Assets.Scripts.Effects;
using Core.Timer;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    [field: SerializeField]
    public SpriteDrawer CraterDrawer { get; private set; }

    [field: SerializeField]
    public SpriteDrawer CrossDrawer { get; private set; }

    [field: SerializeField]
    public Timer Timer { get; private set; }

    [field: SerializeField]
    public EffectManager EffectManager { get; private set; }

    public static bool IsGameStarted { get; set; }

    [field: SerializeField]
    public AudioSource MouseKilledSound { get; private set; }
    
    [field: SerializeField]
    public AudioSource MisClickSound { get; private set; }
    
    [field: SerializeField]
    public AudioSource ShootingSound { get; private set; }
        
    public static bool IsGameEnded { get; set; } //for spawn

    private void Awake()
    {
        Singleton = this;
    }
    
}