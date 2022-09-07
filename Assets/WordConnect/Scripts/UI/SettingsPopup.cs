using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BBG;

namespace WordConnect
{
    public class SettingsPopup : Popup
    {
        #region Inspector Variables

        [Space]

        [SerializeField] private ToggleSlider musicToggle;
        [SerializeField] private ToggleSlider soundToggle;
        [SerializeField] private ToggleSlider notificationToggle;

        #endregion

        #region Unity Methods

        private void Start()
        {
            musicToggle.SetToggle(SoundManager.Instance.IsMusicOn, false);
            soundToggle.SetToggle(SoundManager.Instance.IsSoundEffectsOn, false);
            notificationToggle.SetToggle(NotificationsController.Instance.isNotificationsOn, false);

            musicToggle.OnValueChanged += OnMusicValueChanged;
            soundToggle.OnValueChanged += OnSoundEffectsValueChanged;
            notificationToggle.OnValueChanged += OnNotificationsValueChanged;
        }

        #endregion

        #region Private Methods

        private void OnMusicValueChanged(bool isOn)
        {
            SoundManager.Instance.SetSoundTypeOnOff(SoundManager.SoundType.Music, isOn);
        }

        private void OnSoundEffectsValueChanged(bool isOn)
        {
            SoundManager.Instance.SetSoundTypeOnOff(SoundManager.SoundType.SoundEffect, isOn);
        }

        private void OnNotificationsValueChanged(bool isOn)
        {
            NotificationsController.Instance.SetNotificationsOnOff(isOn);
        }

        #endregion
    }
}