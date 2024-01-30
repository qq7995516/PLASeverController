using Newtonsoft.Json;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace 幻兽帕鲁配置一键生成
{
    public partial class Form1 : Form
    {
        string ConfigPath = "DefaultPalWorldSettings.ini";
        string AppConfig = "DefaultPalWorldSettings.json";
        List<string> UITexts = new List<string>();
        List<Label> Labels = new List<Label>();
        StringBuilder ConfigText = new StringBuilder();
        PLWorldConfig? ConfigObj = null;
        Dictionary<string, string> Translate = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Translate.Add("难度:", "Difficulty");
            Translate.Add("白天时间速度倍率:", "DayTimeSpeedRate");
            Translate.Add("夜晚时间倍率:", "NightTimeSpeedRate");
            Translate.Add("经验值获取倍率:", "ExpRate");
            Translate.Add("捕捉宠物倍率:", "PalCaptureRate");
            Translate.Add("野外宠物生成数量倍率:", "PalSpawnNumRate");
            Translate.Add("宠物攻击力倍率:", "PalDamageRateAttack");
            Translate.Add("宠物防御力倍率:", "PalDamageRateDefense");
            Translate.Add("玩家攻击力倍率:", "PlayerDamageRateAttack");
            Translate.Add("玩家防御力倍率:", "PlayerDamageRateDefense");
            Translate.Add("玩家饥饿度下降倍率:", "PlayerStomachDecreaceRate");
            Translate.Add("玩家体力下降倍率:", "PlayerStaminaDecreaceRate");
            Translate.Add("玩家生命值自然回复倍率:", "PlayerAutoHPRegeneRate");
            Translate.Add("玩家睡眠期生命值回复倍率:", "PlayerAutoHpRegeneRateInSleep");
            Translate.Add("宠物饥饿度下降倍率:", "PalStomachDecreaceRate");
            Translate.Add("宠物体力下降倍率:", "PalStaminaDecreaceRate");
            Translate.Add("宠物生命值自然回复倍率:", "PalAutoHPRegeneRate");
            Translate.Add("宠物睡眠期生命值回复倍率:", "PalAutoHpRegeneRateInSleep");
            Translate.Add("建筑受损倍率:", "BuildObjectDamageRate");
            Translate.Add("建筑折旧倍率:", "BuildObjectDeteriorationDamageRate");
            Translate.Add("收集成功率倍率:", "CollectionDropRate");
            Translate.Add("收集物体生命值倍率:", "CollectionObjectHpRate");
            Translate.Add("收集点刷新时间倍率:", "CollectionObjectRespawnSpeedRate");
            Translate.Add("NPC掉落物品率倍率:", "EnemyDropItemRate");
            Translate.Add("死亡惩罚:", "DeathPenalty");
            Translate.Add("是否允许玩家互殴:", "bEnablePlayerToPlayerDamage");
            Translate.Add("是否允许友军伤害:", "bEnableFriendlyFire");
            Translate.Add("是否开启入侵型NPC:", "bEnableInvaderEnemy");
            Translate.Add("是否启动未知功能:", "bActiveUNKO");
            Translate.Add("是否开启手柄瞄准辅助:", "bEnableAimAssistPad");
            Translate.Add("是否开启键盘瞄准辅助:", "bEnableAimAssistKeyboard");
            Translate.Add("掉落物品最大数量:", "DropItemMaxNum");
            Translate.Add("UNKO下掉落物品最大数:", "DropItemMaxNum_UNKO");
            Translate.Add("基地最大个数:", "BaseCampMaxNum");
            Translate.Add("每个基地最大工人数:", "BaseCampWorkerMaxNum");
            Translate.Add("掉落物品持续时间倍率:", "DropItemAliveMaxHours");
            Translate.Add("无成员是否自动解散公会:", "bAutoResetGuildNoOnlinePlayers");
            Translate.Add("无成员时长后自动解散公会:", "AutoResetGuildTimeNoOnlinePlayers");
            Translate.Add("公会最大成员数:", "GuildPlayerMaxNum");
            Translate.Add("蛋孵化小时:", "PalEggDefaultHatchingTime");
            Translate.Add("工作速度倍率:", "WorkSpeedRate");
            Translate.Add("是否为多人模式:", "bIsMultiplay");
            Translate.Add("是否开启玩家对战模式:", "bIsPvP");
            Translate.Add("是否允许捡别公会成员怪物掉落:", "bCanPickupOtherGuildDeathPenaltyDrop");
            Translate.Add("是否开启不登录产生惩罚:", "bEnableNonLoginPenalty");
            Translate.Add("是否开放快速移动:", "bEnableFastTravel");
            Translate.Add("是否允许选择开始位置:", "bIsStartLocationSelectByMap");
            Translate.Add("退出后玩家身体是否继续存在:", "bExistPlayerAfterLogout");
            Translate.Add("是否允许攻击别公会成员:", "bEnableDefenseOtherGuildPlayer");
            Translate.Add("合作模式最大人数:", "CoopPlayerMaxNum");
            Translate.Add("服务器最大人数:", "ServerPlayerMaxNum");
            Translate.Add("服务器名称:", "ServerName");
            Translate.Add("服务器描述:", "ServerDescription");
            Translate.Add("管理员密码:", "AdminPassword");
            Translate.Add("服务器密码:", "ServerPassword");
            Translate.Add("公开端口:", "PublicPort");
            Translate.Add("公开IP:", "PublicIP");
            Translate.Add("是否开启RCON远程:", "RCONEnabled");
            Translate.Add("端口:", "RCONPort");
            Translate.Add("区域设置:", "Region");
            Translate.Add("是否启用验证登录:", "bUseAuth");
            Translate.Add("封禁列表url:", "BanListURL");

            //检查配置文件是否存在,不存在就创建并写入默认数据
            if (!File.Exists(AppConfig))
            {
                using var file = File.Create(AppConfig);
                file.Close();
                File.WriteAllText(AppConfig, @"{
  ""Difficulty"": ""None"",
  ""DayTimeSpeedRate"": 0.50000,
  ""NightTimeSpeedRate"": 2.000000,
  ""ExpRate"": 1.000000,
  ""PalCaptureRate"": 1.000000,
  ""PalSpawnNumRate"": 1.000000,
  ""PalDamageRateAttack"": 1.000000,
  ""PalDamageRateDefense"": 1.000000,
  ""PlayerDamageRateAttack"": 1.000000,
  ""PlayerDamageRateDefense"": 1.000000,
  ""PlayerStomachDecreaceRate"": 1.000000,
  ""PlayerStaminaDecreaceRate"": 1.000000,
  ""PlayerAutoHPRegeneRate"": 1.000000,
  ""PlayerAutoHpRegeneRateInSleep"": 1.000000,
  ""PalStomachDecreaceRate"": 1.000000,
  ""PalStaminaDecreaceRate"": 1.000000,
  ""PalAutoHPRegeneRate"": 1.000000,
  ""PalAutoHpRegeneRateInSleep"": 1.000000,
  ""BuildObjectDamageRate"": 1.000000,
  ""BuildObjectDeteriorationDamageRate"": 1.000000,
  ""CollectionDropRate"": 1.000000,
  ""CollectionObjectHpRate"": 1.000000,
  ""CollectionObjectRespawnSpeedRate"": 1.000000,
  ""EnemyDropItemRate"": 1.000000,
  ""DeathPenalty"": ""All"",
  ""bEnablePlayerToPlayerDamage"": false,
  ""bEnableFriendlyFire"": false,
  ""bEnableInvaderEnemy"": true,
  ""bActiveUNKO"": false,
  ""bEnableAimAssistPad"": true,
  ""bEnableAimAssistKeyboard"": false,
  ""DropItemMaxNum"": 3000,
  ""DropItemMaxNum_UNKO"": 100,
  ""BaseCampMaxNum"": 128,
  ""BaseCampWorkerMaxNum"": 15,
  ""DropItemAliveMaxHours"": 1.000000,
  ""bAutoResetGuildNoOnlinePlayers"": false,
  ""AutoResetGuildTimeNoOnlinePlayers"": 72.000000,
  ""GuildPlayerMaxNum"": 20,
  ""PalEggDefaultHatchingTime"": 72.000000,
  ""WorkSpeedRate"": 1.000000,
  ""bIsMultiplay"": false,
  ""bIsPvP"": false,
  ""bCanPickupOtherGuildDeathPenaltyDrop"": false,
  ""bEnableNonLoginPenalty"": true,
  ""bEnableFastTravel"": true,
  ""bIsStartLocationSelectByMap"": true,
  ""bExistPlayerAfterLogout"": false,
  ""bEnableDefenseOtherGuildPlayer"": false,
  ""CoopPlayerMaxNum"": 4,
  ""ServerPlayerMaxNum"": 32,
  ""ServerName"": ""Default Palworld Server"",
  ""ServerDescription"": """",
  ""AdminPassword"": """",
  ""ServerPassword"": """",
  ""PublicPort"": 8211,
  ""PublicIP"": """",
  ""RCONEnabled"": false,
  ""RCONPort"": 25575,
  ""Region"": """",
  ""bUseAuth"": true,
  ""BanListURL"": ""https://api.palworldgame.com/api/banlist.txt"",
""IsOpenRestartIntervalTask"":false,
""RestartInterval"":24,
""PalSeverPath"":""""
}", Encoding.UTF8);
            }
            //检查游戏服务端配置文件是否存在
            //if (!File.Exists(ConfigPath))
            //    File.Create(ConfigPath);
            UITexts.Add("难度:");
            UITexts.Add("白天时间速度倍率:");
            UITexts.Add("夜晚时间倍率:");
            UITexts.Add("经验值获取倍率:");
            UITexts.Add("捕捉宠物倍率:");
            UITexts.Add("野外宠物生成数量倍率:");
            UITexts.Add("宠物攻击力倍率:");
            UITexts.Add("宠物防御力倍率:");
            UITexts.Add("玩家攻击力倍率:");
            UITexts.Add("玩家防御力倍率:");
            UITexts.Add("玩家饥饿度下降倍率:");
            UITexts.Add("玩家体力下降倍率:");
            UITexts.Add("玩家生命值自然回复倍率:");
            UITexts.Add("玩家睡眠期生命值回复倍率:");
            UITexts.Add("宠物饥饿度下降倍率:");
            UITexts.Add("宠物体力下降倍率:");
            UITexts.Add("宠物生命值自然回复倍率:");
            UITexts.Add("宠物睡眠期生命值回复倍率:");
            UITexts.Add("建筑受损倍率:");
            UITexts.Add("建筑折旧倍率:");
            UITexts.Add("收集成功率倍率:");
            UITexts.Add("收集物体生命值倍率:");
            UITexts.Add("收集点刷新时间倍率:");
            UITexts.Add("NPC掉落物品率倍率:");
            UITexts.Add("死亡惩罚:");
            UITexts.Add("是否允许玩家互殴:");
            UITexts.Add("是否允许友军伤害:");
            UITexts.Add("是否开启入侵型NPC:");
            UITexts.Add("是否启动未知功能:");
            UITexts.Add("是否开启手柄瞄准辅助:");
            UITexts.Add("是否开启键盘瞄准辅助:");
            UITexts.Add("掉落物品最大数量:");
            UITexts.Add("UNKO下掉落物品最大数:");
            UITexts.Add("基地最大个数:");
            UITexts.Add("每个基地最大工人数:");
            UITexts.Add("掉落物品持续时间倍率:");
            UITexts.Add("无成员是否自动解散公会:");
            UITexts.Add("无成员时长后自动解散公会:");
            UITexts.Add("公会最大成员数:");
            UITexts.Add("蛋孵化小时:");
            UITexts.Add("工作速度倍率:");
            UITexts.Add("是否为多人模式:");
            UITexts.Add("是否开启玩家对战模式:");
            UITexts.Add("是否允许捡别公会成员怪物掉落:");
            UITexts.Add("是否开启不登录产生惩罚:");
            UITexts.Add("是否开放快速移动:");
            UITexts.Add("是否允许选择开始位置:");
            UITexts.Add("退出后玩家身体是否继续存在:");
            UITexts.Add("是否允许攻击别公会成员:");
            UITexts.Add("合作模式最大人数:");
            UITexts.Add("服务器最大人数:");
            UITexts.Add("服务器名称:");
            UITexts.Add("服务器描述:");
            UITexts.Add("管理员密码:");
            UITexts.Add("服务器密码:");
            UITexts.Add("公开端口:");
            UITexts.Add("公开IP:");
            UITexts.Add("是否开启RCON远程:");
            UITexts.Add("端口:");
            UITexts.Add("区域设置:");
            UITexts.Add("是否启用验证登录:");
            UITexts.Add("封禁列表url:");

            //Label初始坐标
            var LableLocationX = 12;
            var LableLocationY = 9;
            //坐标增长距离
            var LableLocationGrowthX = 350;
            var LableLocationGrowthY = 30;
            //设置label的文本和位置属性
            for (int i = 1; i <= UITexts.Count; i++)
            {
                var d = UITexts[i - 1];

                var TempX = LableLocationX + (LableLocationGrowthX * (i / 20));

                LableLocationY = i == 1 ? 9 : LableLocationY + LableLocationGrowthY;
                if (i % 20 == 0)
                {
                    LableLocationY = 9;
                }

                var label = new Label();
                label.Text = d;
                label.Location = new Point(TempX, LableLocationY);
                label.AutoSize = true;
                label.Visible = true;
                label.TextAlign = ContentAlignment.TopLeft;
                this.Controls.Add(label);
                Labels.Add(label);

                //添加TextBox控件,每个label对应一个TextBox
                var textBox = new TextBox();
                textBox.Name = d;
                textBox.Location = new Point(LableLocationX + label.Width + TempX - 5, LableLocationY - 3);
                textBox.Size = new Size(150, 20);
                textBox.Visible = true;
                this.Controls.Add(textBox);
            }

            //从应用配置文件中读取保存的数据
            ConfigText.Append(File.ReadAllText(AppConfig, Encoding.UTF8));
            ConfigObj = JsonConvert.DeserializeObject<PLWorldConfig>(ConfigText.ToString());
            //将数据填充到TextBox中
            foreach (var item in Labels)
            {
                var textBox = this.Controls.Find(item.Text, true)[0] as TextBox;
                var type = ConfigObj.GetType();
                var property = type.GetProperty(Translate[item.Text]);
                var value = property.GetValue(ConfigObj, null);
                textBox.Text = value.ToString();
            }
            //读取程序操作配置
            IsOpenRestartIntervalTask.Checked = ConfigObj.IsOpenRestartIntervalTask;
            RestartInterval.Text = ConfigObj.RestartInterval;
            PalSeverPath.Text = ConfigObj.PalSeverPath;
        }
        public void TimedRestartTask(int IntervalHour = 24)
        {
            Task.Run(async () =>
            {
                var StartTime = DateTime.Now;//起始时间
                var TargetTime = StartTime.AddHours(IntervalHour);
                while (true)
                {
                    //检查是否开启
                    if (IsOpenRestartIntervalTask.Checked)
                    {
                        //检查是否到达指定时间,到达了就执行,然后再重新赋值起始时间
                        if (DateTime.Now.Day == TargetTime.Day && DateTime.Now.Hour == TargetTime.Hour)
                        {
                            await RestartPalSeverProcessesAsync();
                            //重置起点时间和目标时间,防止重复执行
                            StartTime = DateTime.Now;
                            TargetTime = StartTime.AddHours(IntervalHour);
                        }
                    }
                    await Task.Delay(30 * 1000);
                }
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //C:\Program Files\PalServer\steam\steamapps\common\PalServer\Pal\Saved\Config\WindowsServer\PalWorldSettings.ini  帕鲁存档位置
            //MessageBox.Show("选中名为[PalServer]的文件夹");
            var tmp = Tool.SelectFolder(PalSeverPath.Text);
            PalSeverPath.Text = tmp == null ? PalSeverPath.Text : tmp;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            TimedRestartTask();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //检查是否选择了有效路径
            if (PalSeverPath.Text.IsExist())
            {
                //检查对象是否为空
                if (ConfigObj != null)
                {
                    //修改配置对象
                    UITexts.ForEach(d =>
                    {
                        //获取TextBox的文本内容
                        var text = Controls.Find(d, true)[0].Text;
                        //获取到对象的类型
                        var type = ConfigObj.GetType();
                        //获取对象的属性
                        var property = type.GetProperty(d);
                        //获取属性的类型
                        var propertyType = property.PropertyType;
                        try
                        {
                            //转换类型
                            var value = Convert.ChangeType(text, propertyType);
                            property.SetValue(ConfigObj, value);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show($"请检查{d}是否填错或漏填");
                        }
                    });

                    var configText = $"\"; This configuration file is a sample of the default server settings.\r\n; Changes to this file will NOT be reflected on the server.\r\n; To change the server settings,\r\n modify Pal/Saved/Config/WindowsServer/PalWorldSettings.ini.\r\n[/Script/Pal.PalGameWorldSettings]\r\nOptionSettings=(Difficulty={ConfigObj.Difficulty},DayTimeSpeedRate={ConfigObj.DayTimeSpeedRate},NightTimeSpeedRate={ConfigObj.NightTimeSpeedRate},ExpRate={ConfigObj.ExpRate},PalCaptureRate={ConfigObj.PalCaptureRate},PalSpawnNumRate={ConfigObj.PalSpawnNumRate},PalDamageRateAttack={ConfigObj.PalDamageRateAttack},PalDamageRateDefense={ConfigObj.PalDamageRateDefense},PlayerDamageRateAttack={ConfigObj.PlayerDamageRateAttack},PlayerDamageRateDefense={ConfigObj.PlayerDamageRateDefense},PlayerStomachDecreaceRate={ConfigObj.PlayerStomachDecreaceRate},PlayerStaminaDecreaceRate={ConfigObj.PlayerStaminaDecreaceRate},PlayerAutoHPRegeneRate={ConfigObj.PlayerAutoHPRegeneRate},PlayerAutoHpRegeneRateInSleep={ConfigObj.PlayerAutoHpRegeneRateInSleep},PalStomachDecreaceRate={ConfigObj.PalStomachDecreaceRate},PalStaminaDecreaceRate={ConfigObj.PalStaminaDecreaceRate},PalAutoHPRegeneRate={ConfigObj.PalAutoHPRegeneRate},PalAutoHpRegeneRateInSleep={ConfigObj.PalAutoHpRegeneRateInSleep},BuildObjectDamageRate={ConfigObj.BuildObjectDamageRate},BuildObjectDeteriorationDamageRate={ConfigObj.BuildObjectDeteriorationDamageRate},CollectionDropRate={ConfigObj.CollectionDropRate},CollectionObjectHpRate={ConfigObj.CollectionObjectHpRate},CollectionObjectRespawnSpeedRate={ConfigObj.CollectionObjectRespawnSpeedRate},EnemyDropItemRate={ConfigObj.EnemyDropItemRate},DeathPenalty={ConfigObj.DeathPenalty},bEnablePlayerToPlayerDamage={ConfigObj.bEnablePlayerToPlayerDamage},bEnableFriendlyFire={ConfigObj.bEnableFriendlyFire},bEnableInvaderEnemy={ConfigObj.bEnableInvaderEnemy},bActiveUNKO={ConfigObj.bActiveUNKO},bEnableAimAssistPad={ConfigObj.bEnableAimAssistPad},bEnableAimAssistKeyboard={ConfigObj.bEnableAimAssistKeyboard},DropItemMaxNum={ConfigObj.DropItemMaxNum},DropItemMaxNum_UNKO={ConfigObj.DropItemMaxNum_UNKO},BaseCampMaxNum={ConfigObj.BaseCampMaxNum},BaseCampWorkerMaxNum={ConfigObj.BaseCampWorkerMaxNum},DropItemAliveMaxHours={ConfigObj.DropItemAliveMaxHours},bAutoResetGuildNoOnlinePlayers={ConfigObj.bAutoResetGuildNoOnlinePlayers},AutoResetGuildTimeNoOnlinePlayers={ConfigObj.AutoResetGuildTimeNoOnlinePlayers},GuildPlayerMaxNum={ConfigObj.GuildPlayerMaxNum},PalEggDefaultHatchingTime={ConfigObj.PalEggDefaultHatchingTime},WorkSpeedRate={ConfigObj.WorkSpeedRate},bIsMultiplay={ConfigObj.bIsMultiplay},bIsPvP={ConfigObj.bIsPvP},bCanPickupOtherGuildDeathPenaltyDrop={ConfigObj.bCanPickupOtherGuildDeathPenaltyDrop},bEnableNonLoginPenalty={ConfigObj.bEnableNonLoginPenalty},bEnableFastTravel={ConfigObj.bEnableFastTravel},bIsStartLocationSelectByMap={ConfigObj.bIsStartLocationSelectByMap},bExistPlayerAfterLogout={ConfigObj.bExistPlayerAfterLogout},bEnableDefenseOtherGuildPlayer={ConfigObj.bEnableDefenseOtherGuildPlayer},CoopPlayerMaxNum={ConfigObj.CoopPlayerMaxNum},ServerPlayerMaxNum={ConfigObj.ServerPlayerMaxNum},ServerName={ConfigObj.ServerName},ServerDescription={ConfigObj.ServerDescription},AdminPassword={ConfigObj.AdminPassword},ServerPassword={ConfigObj.ServerPassword},PublicPort={ConfigObj.PublicPort},PublicIP={ConfigObj.PublicIP},RCONEnabled={ConfigObj.RCONEnabled},RCONPort={ConfigObj.RCONPort},Region={ConfigObj.Region},bUseAuth={ConfigObj.bUseAuth},BanListURL=\"{ConfigObj.BanListURL}\")\r\n\r\n\r\n";
                    //写入文件  C:\Program Files\PalServer\steam\steamapps\common\PalServer\Pal\Saved\Config\WindowsServer\PalWorldSettings.ini
                    var path = $"{PalSeverPath.Text}\\steam\\steamapps\\common\\PalServer\\Pal\\Saved\\Config\\WindowsServer\\PalWorldSettings.ini";
                    if (path.IsExist())
                    {
                        File.WriteAllText(path, configText, Encoding.UTF8);
                        //获取帕鲁进程

                    }
                    else
                    {
                        MessageBox.Show("新存档似乎不存在,请启动服务端再尝试应用");
                        return;
                    }
                    //反序列化
                    var ConfigJson = JsonConvert.SerializeObject(ConfigObj);
                    File.WriteAllText(AppConfig, ConfigJson);
                }
            }
            else
            {
                MessageBox.Show("路径错误,请选择正确路径,比如[C:\\Program Files\\PalServer]");
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await KillPalSeverProcessesAsync();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await StartPalSeverProcessesAsync();
        }

        public Task StartPalSeverProcessesAsync() =>
            Task.Run(() =>
            {
                if (PalSeverPath.Text.IsExist())
                {
                    //C:\Program Files\PalServer\steam\steamapps\common\PalServer\PalServer.exe
                    var path = @$"{PalSeverPath.Text}\steam\steamapps\common\PalServer\PalServer.exe";
                    if (path.IsExist())
                    {
                        path.Run();
                        MessageBox.Show("服务端进程已启动,请检查");
                    }
                    else
                    {
                        MessageBox.Show("找不到帕鲁服务端应用程序,请检查文件夹是否正常");
                    }
                }
                else
                {
                    MessageBox.Show("帕鲁文件夹路径不正常,请检查文件夹是否存在");
                }
            });


        /// <summary>
        /// 关闭帕鲁服务端进程
        /// </summary>
        /// <returns></returns>
        public async Task KillPalSeverProcessesAsync()
        {
            await Task.Run(() =>
            {
                var processes = Process.GetProcesses().Where(d => d.ProcessName == "PalServer").ToList();
                if (processes.Count > 0)
                {
                    processes[0].Kill();
                }
                else
                {
                    MessageBox.Show("帕鲁服务端进程似乎没启动,请检查");
                }
            });
        }

        /// <summary>
        /// 重启帕鲁服务端进程
        /// </summary>
        /// <returns></returns>
        public async Task RestartPalSeverProcessesAsync()
        {
            await KillPalSeverProcessesAsync();
            await Task.Delay(500);
            await StartPalSeverProcessesAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await RestartPalSeverProcessesAsync();
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            //删档
            await Task.Run(async () =>
             {
                 //关闭进程
                 await KillPalSeverProcessesAsync();
                 //删除文件夹
                 Directory.Delete(PalSeverPath.Text, true);
                 //启动
                 await StartPalSeverProcessesAsync();
             });
        }

        private void PalSeverPath_TextChanged(object sender, EventArgs e)
        {
            ConfigObj.PalSeverPath = PalSeverPath.Text;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            var ConfigJson = JsonConvert.SerializeObject(ConfigObj);
            File.WriteAllText(AppConfig, ConfigJson);
        }
    }


    public class PLWorldConfig
    {
        public string Difficulty { get; set; }
        public float DayTimeSpeedRate { get; set; }
        public float NightTimeSpeedRate { get; set; }
        public float ExpRate { get; set; }
        public float PalCaptureRate { get; set; }
        public float PalSpawnNumRate { get; set; }
        public float PalDamageRateAttack { get; set; }
        public float PalDamageRateDefense { get; set; }
        public float PlayerDamageRateAttack { get; set; }
        public float PlayerDamageRateDefense { get; set; }
        public float PlayerStomachDecreaceRate { get; set; }
        public float PlayerStaminaDecreaceRate { get; set; }
        public float PlayerAutoHPRegeneRate { get; set; }
        public float PlayerAutoHpRegeneRateInSleep { get; set; }
        public float PalStomachDecreaceRate { get; set; }
        public float PalStaminaDecreaceRate { get; set; }
        public float PalAutoHPRegeneRate { get; set; }
        public float PalAutoHpRegeneRateInSleep { get; set; }
        public float BuildObjectDamageRate { get; set; }
        public float BuildObjectDeteriorationDamageRate { get; set; }
        public float CollectionDropRate { get; set; }
        public float CollectionObjectHpRate { get; set; }
        public float CollectionObjectRespawnSpeedRate { get; set; }
        public float EnemyDropItemRate { get; set; }
        public string DeathPenalty { get; set; }
        public bool bEnablePlayerToPlayerDamage { get; set; }
        public bool bEnableFriendlyFire { get; set; }
        public bool bEnableInvaderEnemy { get; set; }
        public bool bActiveUNKO { get; set; }
        public bool bEnableAimAssistPad { get; set; }
        public bool bEnableAimAssistKeyboard { get; set; }
        public int DropItemMaxNum { get; set; }
        public int DropItemMaxNum_UNKO { get; set; }
        public int BaseCampMaxNum { get; set; }
        public int BaseCampWorkerMaxNum { get; set; }
        public float DropItemAliveMaxHours { get; set; }
        public bool bAutoResetGuildNoOnlinePlayers { get; set; }
        public float AutoResetGuildTimeNoOnlinePlayers { get; set; }
        public int GuildPlayerMaxNum { get; set; }
        public float PalEggDefaultHatchingTime { get; set; }
        public float WorkSpeedRate { get; set; }
        public bool bIsMultiplay { get; set; }
        public bool bIsPvP { get; set; }
        public bool bCanPickupOtherGuildDeathPenaltyDrop { get; set; }
        public bool bEnableNonLoginPenalty { get; set; }
        public bool bEnableFastTravel { get; set; }
        public bool bIsStartLocationSelectByMap { get; set; }
        public bool bExistPlayerAfterLogout { get; set; }
        public bool bEnableDefenseOtherGuildPlayer { get; set; }
        public int CoopPlayerMaxNum { get; set; }
        public int ServerPlayerMaxNum { get; set; }
        public string ServerName { get; set; }
        public string ServerDescription { get; set; }
        public string AdminPassword { get; set; }
        public string ServerPassword { get; set; }
        public int PublicPort { get; set; }
        public string PublicIP { get; set; }
        public bool RCONEnabled { get; set; }
        public int RCONPort { get; set; }
        public string Region { get; set; }
        public bool bUseAuth { get; set; }
        public string BanListURL { get; set; }
        public bool IsOpenRestartIntervalTask { get; set; }
        public string RestartInterval { get; set; }
        public string PalSeverPath { get; set; }
    }

}