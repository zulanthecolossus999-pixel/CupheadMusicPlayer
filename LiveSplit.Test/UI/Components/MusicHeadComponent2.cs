using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

using LiveSplit.Model;
using LiveSplit.CupheadMusic.Music;

namespace LiveSplit.UI.Components
{
    public class MusicHeadComponent2 : IComponent
    {
        private readonly LiveSplitState state;
        private readonly CupheadSceneDetection sceneDetection;
        private readonly MusicPlayer musicPlayer;

        private MusicHeadSettings settings;

        private readonly Dictionary<string, string> musicMappings =
            new Dictionary<string, string>(
                StringComparer.OrdinalIgnoreCase)
            {
                {
                    "scene_level_veggies",
                    "root.mp3"
                },

                {
                    "scene_level_slime",
                    "goopy.mp3"
                },

                {
                    "scene_level_frogs",
                    "ribby.mp3"
                },

                {
                    "scene_level_flower",
                    "cagney.mp3"
                },
                {
                    "scene_level_baroness",
                    "baroness.mp3"
                },
                {
                    "scene_level_flying_bird",
                    "wally.mp3"
                },
                {
                    "scene_level_flying_genie",
                    "djimmi.mp3"
                },
                {
                    "scene_level_clown",
                    "beppi.mp3"
                },
                {
                    "scene_level_dragon",
                    "grim.mp3"
                },
                {
                    "scene_level_bee",
                    "rumor.mp3"
                },
                {
                    "scene_level_robot",
                    "kahl.mp3"
                },
                {
                    "scene_level_sally_stage_play",
                    "sally.mp3"
                },
                {
                    "scene_level_mouse",
                    "werner.mp3"
                },
                {
                    "scene_level_pirate",
                    "captain.mp3"
                },
                {
                    "scene_level_flying_,mermaid",
                    "cala.mp3"
                },
                {
                    "scene_level_train",
                    "phantom.mp3"
                },
                {
                    "scene_level_devil",
                    "devil.mp3"
                },
                {
                    "scene_map_world_1",
                    "isle1.mp3"
                },
                {
                    "scene_map_world_2",
                    "isle2.mp3"
                },
                {
                    "scene_map_world_3",
                    "isle3.mp3"
                },
                {
                    "scene_map_world_4",
                    "islehell.mp3"
                },
                {
                    "scene_map_world_DLC",
                    "isleDLC.mp3"
                },
                {
                    "scene_level_dice_palace_main",
                    "king.mp3"
                },
                {
                    "scene_level_dice_palace_domino",
                    "king.mp3"
                },
                {
                    "scene_level_dice_palace_chips",
                    "king.mp3"
                },
                {
                    "scene_level_dice_palace_cigar",
                    "king.mp3"
                },
                {
                    "scene_level_dice_palace_booze",
                    "king.mp3"
                },
                {
                    "scene_level_dice_palace_roulette",
                    "king.mp3"
                },
                {
                    "scene_level_dice_palace_rabbit",
                    "king.mp3"
                },
                {
                    "scene_level_dice_palace_flying_horse",
                    "king.mp3"
                },
                {
                    "scene_level_dice_palace_memory",
                    "king.mp3"
                },
                {
                    "scene_level_dice_palace_eight_ball",
                    "king.mp3"
                },
                {
                    "scene_level_old_man",
                    "glumstone.mp3"
                },
                {
                    "scene_level_snow_cult",
                    "mortimer.mp3"
                },
                {
                    "scene_level_airplane",
                    "howling.mp3"
                },
                {
                    "scene_level_flying_cowboy",
                    "esther.mp3"
                },
                {
                    "scene_level_rum_runners",
                    "moonshine.mp3"
                },
                {
                    "scene_level_saltbaker",
                    "chef.mp3"
                },
                {
                    "scene_win",
                    "scoreboard.mp3"
                },
                {
                    "scene_shop",
                    "shop.mp3"
                },
                {
                    "scene_slot_select",
                    "title.mp3"
                },
                {
                    "scene_shop_dlc",
                    "shopdlc.mp3"
                },
                {
                    "scene_level_house_elder_kettle",
                    "elder.mp3"
                },
                {
                    "scene_level_platforming_1_1F",
                    "forest.mp3"
                },
                {
                    "scene_level_platforming_1_2F",
                    "treetop.mp3"
                },
                {
                    "scene_level_platforming_2_1F",
                    "funfair.mp3"
                },
                {
                    "scene_level_platforming_2_2F",
                    "funhouse.mp3"
                },
                {
                    "scene_level_platforming_3_1F",
                    "perilous.mp3"
                },
                {
                    "scene_level_platforming_3_2F",
                    "rugged.mp3"
                },
                {
                    "scene_level_mausoleum",
                    "mausoleum.mp3"
                }
            };

        public MusicHeadComponent2(LiveSplitState state)
        {
            this.state = state;

            sceneDetection =
                new CupheadSceneDetection();

            musicPlayer =
                new MusicPlayer();

            settings =
                new MusicHeadSettings();
            // Sync initial volume and subscribe to changes from the settings control.
            try
            {
                musicPlayer.Volume = settings.Volume;
                settings.VolumeChanged += (v) => musicPlayer.Volume = v;
            }
            catch
            {
                // Ignore subscription errors; volume control is optional.
            }
        }

        public string ComponentName
        {
            get { return "Cuphead Music"; }
        }

        public float HorizontalWidth
        {
            get { return 0; }
        }

        public float MinimumWidth
        {
            get { return 0; }
        }

        public float VerticalHeight
        {
            get { return 0; }
        }

        public float MinimumHeight
        {
            get { return 0; }
        }

        public float PaddingLeft
        {
            get { return 0; }
        }

        public float PaddingRight
        {
            get { return 0; }
        }

        public float PaddingTop
        {
            get { return 0; }
        }

        public float PaddingBottom
        {
            get { return 0; }
        }

        public void Update(
            IInvalidator invalidator,
            LiveSplitState state,
            float width,
            float height,
            LayoutMode mode)
        {
            sceneDetection.Update();

            string currentScene =
                sceneDetection.CurrentScene;

            /*
             * No active Cuphead scene.
             */
            if (string.IsNullOrWhiteSpace(currentScene))
            {
                musicPlayer.Stop();
                return;
            }

            /*
             * Does this scene have music assigned?
             */
            string musicFileName;

            if (!musicMappings.TryGetValue(
                currentScene,
                out musicFileName))
            {
                musicPlayer.Stop();
                return;
            }

            /*
             * Get the folder selected in the settings UI.
             */
            string musicDirectory =
                settings.MusicDirectory;

            if (string.IsNullOrWhiteSpace(musicDirectory))
            {
                musicPlayer.Stop();
                return;
            }

            /*
             * Build the complete path:
             *
             * Selected folder
             * +
             * Music filename
             */
            string musicFile =
                Path.Combine(
                    musicDirectory,
                    musicFileName);

            /*
             * Make sure the file actually exists.
             */
            if (!File.Exists(musicFile))
            {
                musicPlayer.Stop();
                return;
            }

            /*
             * Start the music.
             *
             * MusicPlayer will not restart it if the
             * same file is already playing.
             */
            try
            {
                musicPlayer.PlayLooping(musicFile);
            }
            catch
            {
                musicPlayer.Stop();
            }
        }

        public void DrawHorizontal(
            Graphics g,
            LiveSplitState state,
            float height,
            Region clipRegion)
        {
            // UI drawing can remain here.
        }

        public void DrawVertical(
            Graphics g,
            LiveSplitState state,
            float width,
            Region clipRegion)
        {
            // UI drawing can remain here.
        }

        public Control GetSettingsControl(
            LayoutMode mode)
        {
            settings.Mode = mode;
            return settings;
        }

        public XmlNode GetSettings(
            XmlDocument document)
        {
            return settings.GetSettings(document);
        }

        public void SetSettings(
            XmlNode settingsNode)
        {
            settings.SetSettings(settingsNode);
        }

        public IDictionary<string, Action> ContextMenuControls
        {
            get { return null; }
        }

        public void Dispose()
        {
            musicPlayer.Stop();
            musicPlayer.Dispose();

            sceneDetection.Dispose();

            if (settings != null)
            {
                settings.Dispose();
                settings = null;
            }
        }
    }
}