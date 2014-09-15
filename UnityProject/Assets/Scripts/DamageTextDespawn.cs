using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PathologicalGames;

public class DamageTextDespawn : MonoBehaviour {

    public float origDespawnTime = 1.0f;
    public float scrollingVelocity = 0.5f;

    public Color colorFrom;
    public Color colorTo;
    Color origColor;
    private float timeAlive;
    private TextMesh dmgText;

	// Update is called once per frame
	void Update () {
        dmgText.color = Color.Lerp(colorFrom, colorTo, timeAlive/origDespawnTime);
        dmgText.transform.Translate(new Vector3(0,scrollingVelocity * Time.deltaTime,0));
        timeAlive += Time.deltaTime;
	}

    public void OnSpawned()
    {
        dmgText = GetComponent<TextMesh>(); 
        PoolManager.Pools["DamageTextPool"].Despawn(dmgText.transform, origDespawnTime);
        timeAlive = 0.0f;
        origColor = colorFrom;
        dmgText.color = origColor;
        this.StartCoroutine(this.TimedDespawn());
    }

    private IEnumerator TimedDespawn()
    {
        yield return new WaitForSeconds(this.origDespawnTime);
        PoolManager.Pools["DamageTextPool"].Despawn(dmgText.transform);
    }

    public void OnDespawned()
    {
        dmgText.color = origColor;
    }
}
