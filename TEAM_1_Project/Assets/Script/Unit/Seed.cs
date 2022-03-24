using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Unit
{
    public int myresource;
    public int _growth;
    public int maxLevel = 3;

    float _effectTime = 1f;

    public Define.SeedState currState = Define.SeedState.nothing;
    public override float Ability()
    {
        GameManager.effectManager.UseSkill(Define.Effect.seed, this);
        if(level == maxLevel) {
            myresource += _growth;
            GameManager.sceneManager.getPlayer(_currPlace)._currResource += myresource;
            StartCoroutine(CoAttackedOrUsed(this, _effectTime));
            return _effectTime;
        }
        level += 1;
        return _effectTime;
    }
    private void Start()
    {
        base.Init();
    }

    private void Update()
    {
        switch (currState)
        {
            case Define.SeedState.nothing:
                {
                    break;
                }
            case Define.SeedState.skill:
                {
                    if (_currTime > _effectTime)
                    {
                        currState = Define.SeedState.nothing;
                        _currTime = 0;
                        break;
                    }
                    _currTime += Time.deltaTime;
                    if (_currTime <= _effectTime/2)
                    {
                        gameObject.transform.localScale += Vector3.one * _effectSize * Time.deltaTime;
                    }
                    else
                    {
                        gameObject.transform.localScale -= Vector3.one * _effectSize * Time.deltaTime;
                    }
                }
                break;
        }
    }


}
