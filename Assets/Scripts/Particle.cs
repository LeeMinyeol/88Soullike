using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public bool playAura = true; //��ƼŬ ���� bool
    public ParticleSystem damagedEffect;
    public ParticleSystem guardEffect;
    public ParticleSystem parryEffect;
    public Transform player;
    
    

    void Start()
    {
        playAura = true;
    }

    public void DamagedEffect() //�¾��� �� ����Ʈ
    {
        if (playAura)
        {
            Vector2 particleSpawn = player.position + new Vector3 (0f,1.5f);
            Instantiate(damagedEffect, particleSpawn, Quaternion.identity);
            damagedEffect.Play();
        }
        else if (!playAura)
        {
         damagedEffect.Stop();
        
        }
    }

    public void GuardEffect() //������� �� ����Ʈ
    {
        if (playAura)
        {
            Vector2 particleSpawn = player.position + new Vector3(0f, 1.5f);
            Instantiate(guardEffect, particleSpawn, Quaternion.identity);
            guardEffect.Play();
        }
        else if (!playAura)
        {
            guardEffect.Stop();

        }
    }

    public void ParryEffect() //�и����� �� ����Ʈ
    {
        if (playAura)
        {
            Vector2 particleSpawn = player.position + new Vector3(0f, 1.5f);
            Instantiate(parryEffect, particleSpawn, Quaternion.identity);
            parryEffect.Play();
        }
        else if (!playAura)
        {
            parryEffect.Stop();
        }
    }
}
