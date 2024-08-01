using Core;

public class UserDataLoader : BaseDisposable
{
    [System.Serializable]
    public class ProgressData
    {
        public int money = 0;
    }

    [System.Serializable]
    public class SettingsData
    {
        public bool soundOn = true;
        public bool musicOn = true;
        public bool vibrationOn = true;
    }

    private ProgressData _progressData = new ProgressData();
    private SettingsData _settingsData = new SettingsData();

    private const string ProgressKey = "ProgressKey";
    private const string SettingsKey = "SettingsKey";

    private IStorageService _storageService;

    #region Data managment
    public UserDataLoader(IStorageService storageService)
    {
        _storageService = storageService;

        InitFirstData();

        _storageService.LoadAndPopulate(ProgressKey, _progressData);
        _storageService.LoadAndPopulate(SettingsKey, _settingsData);
    }

    private void InitFirstData()
    {
        _settingsData.musicOn = true;
        _settingsData.soundOn = true;
        _settingsData.vibrationOn = true;
    }

    public void SaveProgressData()
    {
        _storageService.SaveAsync(ProgressKey, _progressData);
    }

    public void SaveSettings()
    {
        _storageService.SaveAsync(SettingsKey, _settingsData);
    }

    public void ResetAllData()
    {
        _progressData = new ProgressData();
        _settingsData = new SettingsData();

        InitFirstData();
        SaveProgressData();
        SaveSettings();
    }
    #endregion

    #region Sound and vibro       
    public bool SoundOn
    {
        get { return _settingsData.soundOn; }
        set
        {
            if (_settingsData.soundOn == value)
                return;

            _settingsData.soundOn = value;
            SaveSettings();

            //SoundManager.Instance.TurnOnOffAudio(value);
        }
    }

    public bool VibrationOn
    {
        get { return _settingsData.vibrationOn; }
        set
        {
            if (_settingsData.vibrationOn == value)
                return;

            _settingsData.vibrationOn = value;
            SaveSettings();
        }
    }
    #endregion

    public int Money
    {
        get { return _progressData.money; }
        set
        {
            if (_progressData.money == value)
                return;

            _progressData.money = value;
            SaveProgressData();
        }
    }
}