using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PathologicalGames;
using Assets.Scripts.Managers;

public class DamageTextDespawn : MonoBehaviour {

    public float origDespawnTime = 1.0f;
    public float scrollingVelocity = 0.5f;
    private float timeAlive;
    private TextMesh mesh;

	void Update () {
        mesh.transform.Translate(new Vector3(0, scrollingVelocity * Time.deltaTime, 0));
        timeAlive += Time.deltaTime;
	}

    public void OnSpawned()
    {
        mesh = this.gameObject.GetComponent<TextMesh>();
        mesh.renderer.sortingLayerName = "HighestLayer";
        timeAlive = 0.0f;
        this.StartCoroutine(this.TimedDespawn());
    }

    private IEnumerator TimedDespawn()
    {
        yield return new WaitForSeconds(this.origDespawnTime);
        PrefabManager.Instance.DespawnPrefab(this.gameObject);
    }

    public void OnDespawned()
    {
    }
}
