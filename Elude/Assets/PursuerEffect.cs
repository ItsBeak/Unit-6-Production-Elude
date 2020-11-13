using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PursuerEffect : MonoBehaviour
{

    public Transform player;
    public Transform pursuer;

    float effectStrengthInverse;
    float effectStrength;

    public PostProcessVolume volume;

    ChromaticAberration chromAb;
    Vignette vig;
    LensDistortion lensDis;

    private void Start()
    {
        volume.profile.TryGetSettings(out chromAb);
        volume.profile.TryGetSettings(out vig);
        volume.profile.TryGetSettings(out lensDis);

    }


    // Update is called once per frame
    void Update()
    {
        effectStrengthInverse = Mathf.InverseLerp(0, 15, Vector3.Distance(player.position, pursuer.position));
        effectStrength = Mathf.Abs(1 - effectStrengthInverse);

        chromAb.intensity.value = effectStrength;
        vig.intensity.value = effectStrength / 2;
        lensDis.intensity.value = effectStrength * -50;

    }




}
