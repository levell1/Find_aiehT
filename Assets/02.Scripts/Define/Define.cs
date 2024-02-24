
public struct UIName
{
    public const string ControlKeyUI = "ControlKeyUI";
    public const string GoDungeonUI = "GoDungeonUI";
    public const string PlayerStatusUI = "PlayerStatusUI";
    public const string ReforgeUI = "ReforgeUI";
    public const string RestartUI = "RestartUI";
    public const string TimeToVillageUI = "TimeToVillageUI";
    public const string SceneMoveUI = "SceneMoveUI";
    public const string RestaurantUI = "RestaurantUI";
    public const string ResultUI = "ResultUI";
    public const string SettingUI = "SettingUI";
    public const string ShopUI = "ShopUI";
    public const string InventoryUI = "InventoryUI";
    public const string QuestUI = "QuestUI";
    public const string DungeontUI = "GoDungeonUI";
    public const string PlayerBaseUI = "PlayerBaseUI";
    public const string ChoiceTutorialUI = "ChoiceTutorialUI";
    public const string TutorialGuideBook1 = "TutorialGuideBook1";
    public const string TutorialGuideBook2 = "TutorialGuideBook2";
}

public struct SceneName
{
    public const string TitleScene = "TitleScene";
    public const string LoadingScene = "LoadingScene";
    public const string VillageScene = "VillageScene";
    public const string HuntingScene = "HuntingScene";
    public const string TycoonScene = "TycoonScene";
    public const string TutorialScene = "TutorialScene";
    public const string DungeonScene = "DungeonScene";
}

public struct TagName
{
    public const string Respawn = "Respawn";
    public const string Player = "Player";
    public const string CookedFood = "CookedFood";
    public const string Enemy = "Enemy";
    public const string Item = "Item";
    public const string Door = "Door";
    public const string AI = "AI";
    public const string PotionShop = "PotionShop";
    public const string Enhancement = "Enhancement";
    public const string RealDoor = "RealDoor";
    public const string QuestNPC = "QuestNPC";
    public const string DungeonDoor = "DungeonDoor";
    public const string DungeonNPC = "DungeonNPC";
    public const string Wall = "Wall";
}

public struct LayerName
{
    public const string UI = "UI";
    public const string Ground = "Ground";
    public const string Item = "Item";
    public const string NpcInteract = "NpcInteract";
    public const string Player = "Player";
    public const string Enemy = "Enemy";
    public const string Title = "Title";
}


public struct PoolingObjectName
{
    public const string Customer = "Customer";
}

public struct JsonDataName
{
    public const string PlayerData = "JSON/PlayerData";
    public const string PlayerSkillData = "JSON/PlayerSkillData";
    public const string PlayerLevelData = "JSON/LevelData";
    public const string SaveFile = "SaveFile";
}

public struct AnimationParameterName
{
    public const string TycoonGetFood = "GetFood";
    public const string TycoonAngry = "Angry";
    public const string TycoonIsWalk = "IsWalk";
    public const string TycoonIsEat = "IsEat";
    public const string TycoonIsIdle= "IsIdle";
    public const string BossIdle = "Idle";
    public const string BossWalk = "Walk";
    public const string BossAttack = "Attack";
    public const string BossSpin = "Spin";
    public const string BossFear = "Fear";
    public const string BossRun = "Run";
    public const string BossIdleC = "IdleC";
    public const string BossRoll = "Roll";
    public const string BossHit = "Hit";
    public const string BossFly = "Fly";
    public const string BossSit = "Sit";
    public const string BossDead = "Die";
}

public struct CoolTimeObjName
{
    public const string Dash = "Dash";
    public const string ThrowSkill = "ThrowSkill";
    public const string SpreadSkill = "SpreadSkill";
    public const string HealthPotion = "HealthPotion";
    public const string StaminaPotion = "StaminaPotion";
}

public struct BGMSoundName
{
    public const string VillageBGM1 = "VillageBGM1";
    public const string VillageBGM2 = "VillageBGM2";

    public const string TycoonBGM1 = "TycoonBGM1";
    public const string TycoonBGM2 = "TycoonBGM2";

   
    public const string DungeonBGM = "DungeonBGM";
    public const string DungeonBGM2 = "DungeonBGM2";
    public const string DungeonBoss = "DungeonBossBGM";

    public const string TitleBGM = "TitleBGM";
    public const string EndingBGM = "EndingBGM";

    public const string HuntingField = "HuntingField";
    public const string HuntingField2 = "HuntingField2";
    public const string HuntingField3 = "HuntingField3";
    public const string HuntingField4 = "HuntingField4";

}

public struct ErrorMessageTxt
{
    public const string DontSceneMoveErrorMessage = "입장가능시간이 아닙니다!";
    public const string TenMinutesLeft = "10분 남았습니다.";
    public const string TwentyMinutesLeft = "20분 남았습니다.";
    public const string ThirtyMinutesLeft = "30분 남았습니다.";
    public const string OneHourLeft = "1시간 뒤에 사냥터에서 퇴장됩니다.";
}
public struct SFXSoundPathName
{
    public const string ClickSound = "Click";
    public const string UISound = "UISound";

    public const string Eat = "Eat";
    public const string Money = "Money";
    public const string PutDownFood = "PutDownFood";

    public const string AttackSound = "AttackSound";

    public const string Dash = "Dash";
    public const string Hit = "Hit";
    public const string Landing = "Landing";
    public const string GetItem = "GetItem";

    public const string Heal = "Heal";
    public const string Stamina = "Stamina";
    public const string LevelUp = "LevelUp";
    public const string Quest = "Quest";

    public const string Skill1 = "Skill1";
    public const string Skill2 = "Skill2";
    
    public const string Dead = "Dead";
    public const string Sleep = "Sleep";

    public const string Door = "Door";
    public const string HouseDoor = "HouseDoor";
    public const string Portal = "Portal";
    public const string Upgrade = "Upgrade";

    public const string LightMagic = "LightMagic";
    public const string Levitate = "Levitate";
    public const string Pig1 = "Pig1";
    public const string Pig2 = "Pig2";

}
public enum Region
{
    초원,
    사막,
    설산,
    돌산
}

