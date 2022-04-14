using System.Diagnostics;
using KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue;
using KenshiWikiValidator.Features.DataItemConversion.Builders;
using KenshiWikiValidator.Features.DataItemConversion.Builders.Components;
using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.DataItemConversion
{
    public class ItemBuilder
    {
        private readonly ItemRepository itemRepository;
        private readonly WeaponBuilder weaponBuilder;
        private readonly ArmourBuilder armourBuilder;
        private readonly SquadBuilder squadBuilder;
        private readonly DialogueBuilder dialogueBuilder;
        private readonly Dictionary<ItemType, IItemBuilder> itemBuilders;

        private IItem longestItem;
        private TimeSpan longestTime;

        public ItemBuilder(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            var itemSourcesCreator = new ItemSourcesCreator(itemRepository);
            var blueprintSquadsConverter = new BlueprintSquadsConverter(itemRepository);
            var unlockingResearchConverter = new UnlockingResearchConverter(itemRepository);

            this.weaponBuilder = new WeaponBuilder(
                itemRepository,
                itemSourcesCreator,
                blueprintSquadsConverter,
                unlockingResearchConverter);
            this.armourBuilder = new ArmourBuilder(
                itemRepository,
                itemSourcesCreator,
                blueprintSquadsConverter,
                unlockingResearchConverter);
            this.squadBuilder = new SquadBuilder(itemRepository);
            this.dialogueBuilder = new DialogueBuilder(itemRepository);

            this.itemBuilders = new Dictionary<ItemType, IItemBuilder>()
            {
                { ItemType.Weapon, this.weaponBuilder },
                { ItemType.Armour, this.armourBuilder },
                { ItemType.SquadTemplate, this.squadBuilder },
                { ItemType.DialoguePackage, this.dialogueBuilder },
            };

            this.longestItem = null!;
            this.longestTime = TimeSpan.Zero;
        }

        public IEnumerable<IDataItem> BuildItems()
        {
            var validTypes = this.itemBuilders.Keys.ToArray();
            var items = this.itemRepository.GetDataItemsByTypes(validTypes);

            var results = new List<IDataItem>();

            Parallel.ForEach(items, item =>
            {
                var built = this.BuildItem(item);
                results.Add(built);
            });

            var dialogues = this.itemRepository.GetDataItemsByType(ItemType.Dialogue)
                .Where(dialogue => !this.dialogueBuilder.ContainsDialogue(dialogue.StringId));

            results.AddRange(this.dialogueBuilder.BuildUnparentedDialogues(dialogues));

            Console.WriteLine();
            Console.WriteLine($"The longest item to build was {this.longestItem.Name} and took {this.longestTime}");

            return results;
        }

        private IDataItem BuildItem(IItem item)
        {
            Console.WriteLine($"Building {item.Name}");
            var sw = Stopwatch.StartNew();

            var builder = this.itemBuilders[item.Type];

            var built = builder.Build(item);

            if (built is not IDataItem result)
            {
                throw new InvalidOperationException($"Registered an {nameof(ItemBuilder)} that does not return objects of type {nameof(IDataItem)}");
            }

            Console.WriteLine($"Built {item.Name} in {sw.Elapsed}");

            if (sw.Elapsed > this.longestTime)
            {
                this.longestTime = sw.Elapsed;
                this.longestItem = item;
            }

            return result;
        }
    }
}
