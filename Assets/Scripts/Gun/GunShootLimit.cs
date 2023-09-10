using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIFillUpdater> uiGunUpdaters;

    public float maxShoot = 5;

    public float timeToRecharge = 1f;

    private float _currentShoots;

    private bool _recharging = false;

    public string nameUi;


    private void Awake()
    {
        GetAllUis();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_recharging) yield break;

        while (true)
        {
            if (_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
            else
            {
                {
                    yield break;
                }
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge)
        {
            time += Time.deltaTime;
            uiGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _recharging = false;
    }

    private void UpdateUI()
    {
        uiGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUis()
    {
       
    // Substitua "NomeDoObjeto" pelo nome real do objeto que você deseja encontrar.
    GameObject uiObject = GameObject.Find(nameUi);

    if (uiObject != null)
    {
        UIFillUpdater uiFillUpdater = uiObject.GetComponent<UIFillUpdater>();
        if (uiFillUpdater != null)
        {
            uiGunUpdaters.Add(uiFillUpdater);
        }
        else
        {
            Debug.LogError("UIFillUpdater component not found on object with name: " + uiObject.name);
        }
    }
    else
    {
        Debug.LogError("Object with name: " + "NomeDoObjeto" + " not found.");
    }


    }
}
