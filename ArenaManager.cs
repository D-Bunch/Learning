using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public float matchTime = 180f;
    private float timeLeft;
    public bool matchActive = false;

    void Start()
    {
        // Optionally auto-start
    }

    void Update()
    {
        if (!matchActive) return;
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f) EndMatch();
    }

    public void StartMatch(float duration = 180f)
    {
        matchTime = duration;
        timeLeft = matchTime;
        matchActive = true;
        Debug.Log("Match started.");
        // Reset players
        foreach (var p in players)
        {
            var health = p.GetComponent<HealthSystem>();
            if (health != null) health.health = health.maxHealth;
        }
    }

    public void EndMatch()
    {
        matchActive = false;
        Debug.Log("Match ended.");
        // Determine winner by health or last standing
    }

    public void RegisterPlayer(GameObject player)
    {
        if (!players.Contains(player)) players.Add(player);
    }

    public void UnregisterPlayer(GameObject player)
    {
        if (players.Contains(player)) players.Remove(player);
    }
}
