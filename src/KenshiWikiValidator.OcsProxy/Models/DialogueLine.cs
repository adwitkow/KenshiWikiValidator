// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using KenshiWikiValidator.OcsProxy.DialogueComponents;
using OpenConstructionSet.Data;
using OpenConstructionSet.Mods;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class DialogueLine : ItemBase
    {
        public DialogueLine(ModItem item)
            : base(item)
        {
            this.Text0 = string.Empty;
            this.Unlocks = Enumerable.Empty<ItemReference<Dialogue>>();
            this.Effects = Enumerable.Empty<ItemReference<DialogAction>>();
            this.Lines = Enumerable.Empty<ItemReference<DialogueLine>>();
            this.Conditions = Enumerable.Empty<ItemReference<DialogAction>>();
            this.ChangeRelations = Enumerable.Empty<ItemReference<Faction>>();
            this.MyRace = Enumerable.Empty<ItemReference<Race>>();
            this.Interrupt = Enumerable.Empty<ItemReference<Dialogue>>();
            this.UnlockButKeepMe = Enumerable.Empty<ItemReference<Dialogue>>();
            this.TargetFaction = Enumerable.Empty<ItemReference<Faction>>();
            this.InTownOf = Enumerable.Empty<ItemReference<Faction>>();
            this.TargetRace = Enumerable.Empty<ItemReference<Race>>();
            this.WorldState = Enumerable.Empty<ItemReference<WorldEventState>>();
            this.ChangeAi = Enumerable.Empty<ItemReference<AiPackage>>();
            this.AiContract = Enumerable.Empty<ItemReference<AiPackage>>();
            this.TargetHasItemType = Enumerable.Empty<ItemReference<Item>>();
            this.MyFaction = Enumerable.Empty<ItemReference<Faction>>();
            this.MySubrace = Enumerable.Empty<ItemReference<Race>>();
            this.CrowdTrigger = Enumerable.Empty<ItemReference<Dialogue>>();
            this.TriggerCampaign = Enumerable.Empty<ItemReference<FactionCampaign>>();
            this.HasPackage = Enumerable.Empty<ItemReference<DialoguePackage>>();
            this.IsCharacter = Enumerable.Empty<ItemReference<Character>>();
            this.GiveItem = Enumerable.Empty<ItemReference<Item>>();
            this.TargetCarryingCharacter = Enumerable.Empty<ItemReference<Character>>();
            this.TargetHasItem = Enumerable.Empty<ItemReference<Item>>();
            this.DeliveryAiPackage = Enumerable.Empty<ItemReference<AiPackage>>();
            this.SetAiPackage = Enumerable.Empty<ItemReference<AiPackage>>();
            this.Locks = Enumerable.Empty<ItemReference<Dialogue>>();
            this.LockCampaign = Enumerable.Empty<ItemReference<FactionCampaign>>();
        }

        public override ItemType Type => ItemType.DialogueLine;

        [Value("chance permanent")]
        public float? ChancePermanent { get; set; }

        [Value("chance temporary")]
        public float? ChanceTemporary { get; set; }

        [Value("repetition limit")]
        public int? RepetitionLimit { get; set; }

        [Value("score bonus")]
        public int? ScoreBonus { get; set; }

        [Value("speaker")]
        public DialogueSpeaker Speaker { get; set; }

        [Value("target is type")]
        public int? TargetIsType { get; set; }

        [Value("text0")]
        public string Text0 { get; set; }

        [Value("text1")]
        public string? Text1 { get; set; }

        [Value("text2")]
        public string? Text2 { get; set; }

        [Value("text3")]
        public string? Text3 { get; set; }

        [Value("text4")]
        public string? Text4 { get; set; }

        [Value("text5")]
        public string? Text5 { get; set; }

        [Value("text6")]
        public string? Text6 { get; set; }

        [Value("text7")]
        public string? Text7 { get; set; }

        [Value("text8")]
        public string? Text8 { get; set; }

        [Value("text9")]
        public string? Text9 { get; set; }

        [Value("interjection")]
        public bool? Interjection { get; set; }

        [Value("text10")]
        public string? Text10 { get; set; }

        [Value("text11")]
        public string? Text11 { get; set; }

        [Value("text12")]
        public string? Text12 { get; set; }

        [Value("text13")]
        public string? Text13 { get; set; }

        [Value("text14")]
        public string? Text14 { get; set; }

        [Value("text15")]
        public string? Text15 { get; set; }

        [Value("text16")]
        public string? Text16 { get; set; }

        [Value("text17")]
        public string? Text17 { get; set; }

        [Value("text18")]
        public string? Text18 { get; set; }

        [Value("text19")]
        public string? Text19 { get; set; }

        [Value("text20")]
        public string? Text20 { get; set; }

        [Value("text21")]
        public string? Text21 { get; set; }

        [Value("text22")]
        public string? Text22 { get; set; }

        [Value("text23")]
        public string? Text23 { get; set; }

        [Value("text24")]
        public string? Text24 { get; set; }

        [Value("text25")]
        public string? Text25 { get; set; }

        [Value("text26")]
        public string? Text26 { get; set; }

        [Value("text27")]
        public string? Text27 { get; set; }

        [Value("text28")]
        public string? Text28 { get; set; }

        [Value("text29")]
        public string? Text29 { get; set; }

        [Value("text30")]
        public string? Text30 { get; set; }

        [Value("text31")]
        public string? Text31 { get; set; }

        [Value("text32")]
        public string? Text32 { get; set; }

        [Value("text33")]
        public string? Text33 { get; set; }

        [Value("text34")]
        public string? Text34 { get; set; }

        [Value("text35")]
        public string? Text35 { get; set; }

        [Value("text36")]
        public string? Text36 { get; set; }

        [Value("text37")]
        public string? Text37 { get; set; }

        [Value("text38")]
        public string? Text38 { get; set; }

        [Value("text39")]
        public string? Text39 { get; set; }

        [Value("text40")]
        public string? Text40 { get; set; }

        [Value("text41")]
        public string? Text41 { get; set; }

        [Value("text42")]
        public string? Text42 { get; set; }

        [Value("owner")]
        public bool? Owner { get; set; }

        [Reference("unlocks")]
        public IEnumerable<ItemReference<Dialogue>> Unlocks { get; set; }

        [Reference("effects")]
        public IEnumerable<ItemReference<DialogAction>> Effects { get; set; }

        [Reference("lines")]
        public IEnumerable<ItemReference<DialogueLine>> Lines { get; set; }

        [Reference("conditions")]
        public IEnumerable<ItemReference<DialogAction>> Conditions { get; set; }

        [Reference("change relations")]
        public IEnumerable<ItemReference<Faction>> ChangeRelations { get; set; }

        [Reference("my race")]
        public IEnumerable<ItemReference<Race>> MyRace { get; set; }

        [Reference("interrupt")]
        public IEnumerable<ItemReference<Dialogue>> Interrupt { get; set; }

        [Reference("unlock but keep me")]
        public IEnumerable<ItemReference<Dialogue>> UnlockButKeepMe { get; set; }

        [Reference("target faction")]
        public IEnumerable<ItemReference<Faction>> TargetFaction { get; set; }

        [Reference("in town of")]
        public IEnumerable<ItemReference<Faction>> InTownOf { get; set; }

        [Reference("target race")]
        public IEnumerable<ItemReference<Race>> TargetRace { get; set; }

        [Reference("world state")]
        public IEnumerable<ItemReference<WorldEventState>> WorldState { get; set; }

        [Reference("change AI")]
        public IEnumerable<ItemReference<AiPackage>> ChangeAi { get; set; }

        [Reference("AI contract")]
        public IEnumerable<ItemReference<AiPackage>> AiContract { get; set; }

        [Reference("target has item type")]
        public IEnumerable<ItemReference<Item>> TargetHasItemType { get; set; }

        [Reference("my faction")]
        public IEnumerable<ItemReference<Faction>> MyFaction { get; set; }

        [Reference("my subrace")]
        public IEnumerable<ItemReference<Race>> MySubrace { get; set; }

        [Reference("crowd trigger")]
        public IEnumerable<ItemReference<Dialogue>> CrowdTrigger { get; set; }

        [Reference("trigger campaign")]
        public IEnumerable<ItemReference<FactionCampaign>> TriggerCampaign { get; set; }

        [Reference("has package")]
        public IEnumerable<ItemReference<DialoguePackage>> HasPackage { get; set; }

        [Reference("is character")]
        public IEnumerable<ItemReference<Character>> IsCharacter { get; set; }

        [Reference("give item")]
        public IEnumerable<ItemReference<Item>> GiveItem { get; set; }

        [Reference("target carrying character")]
        public IEnumerable<ItemReference<Character>> TargetCarryingCharacter { get; set; }

        [Reference("target has item")]
        public IEnumerable<ItemReference<Item>> TargetHasItem { get; set; }

        [Reference("delivery AI package")]
        public IEnumerable<ItemReference<AiPackage>> DeliveryAiPackage { get; set; }

        [Reference("set AI package")]
        public IEnumerable<ItemReference<AiPackage>> SetAiPackage { get; set; }

        [Reference("locks")]
        public IEnumerable<ItemReference<Dialogue>> Locks { get; set; }

        [Reference("lock campaign")]
        public IEnumerable<ItemReference<FactionCampaign>> LockCampaign { get; set; }
    }
}