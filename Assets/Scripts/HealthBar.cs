using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
	public FightingSkeleton skeleton;
	public Transform healthBarScaleTransform;

	void Update()
	{
		var healthBarScale = (1/Game.MaxHP)*skeleton.hp;
        healthBarScaleTransform.localScale = new Vector3(healthBarScale, 1f, 1f);
    }
}
