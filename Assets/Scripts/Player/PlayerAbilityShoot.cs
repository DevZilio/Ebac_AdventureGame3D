using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    // public List<UIFillUpdater> uiGunUpdaters;

    public GunBase gunBase;

    public Transform gunPosition;

    private GunBase _currentGun;

    public GunBase gun1Prefab; // First gun

    public GunBase gun2Prefab; // Second gun
    public GunBase gun3Prefab; // Third gun

    public FlashColor flashColorGun;

 

    private int selectedWeapon = 1; // 1 to first gun, 2 to second gun, 3 to third gun

    protected override void Init()
    {
        base.Init();

        CreateGun();
        SwitchWeapon(1);

        inputs.GamePlay.Shoot.performed += cts => StartShoot();
        inputs.GamePlay.Shoot.canceled += cts => CancelShoot();

        inputs.GamePlay.ChangeGun1.performed += cts => SwitchWeapon(1);
        inputs.GamePlay.ChangeGun2.performed += cts => SwitchWeapon(2);
        inputs.GamePlay.ChangeGun3.performed += cts => SwitchWeapon(3);
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);
        _currentGun.transform.localPosition =
            _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        flashColorGun?.Flash();
        
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Cancel Shoot");
    }

    private void SwitchWeapon(int weaponNumber)
    {
        GunBase selectedGun = null;

        switch (weaponNumber)
        {
            case 1:
                selectedGun = Instantiate(gun1Prefab, gunPosition);
                break;
            case 2:
                selectedGun = Instantiate(gun2Prefab, gunPosition);
                break;
            case 3:
                selectedGun = Instantiate(gun3Prefab, gunPosition);
                break;
            default:
                Debug.LogError("Weapon number out of range");
                break;
        }

        if (selectedGun != null)
        {
            Destroy(_currentGun.gameObject); // Destroy current gun
            _currentGun = selectedGun; // Create new gun
            _currentGun.transform.localPosition =
                _currentGun.transform.localEulerAngles = Vector3.zero;
        }
    }
}
