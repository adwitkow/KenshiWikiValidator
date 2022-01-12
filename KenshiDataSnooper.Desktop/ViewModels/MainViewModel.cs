using MvvmHelpers;
using OpenConstructionSet;
using OpenConstructionSet.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiDataSnooper.Desktop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<IGrouping<ItemType, Item>> GroupedItems { get; set; }

        public MainViewModel()
        {
            this.GroupedItems = new ObservableCollection<IGrouping<ItemType, Item>>();

            var installations = OcsDiscoveryService.Default.DiscoverAllInstallations();
            var installation = installations.Values.First();
            var baseMods = installation.Data.Mods;

            var items = new HashSet<Item>();
            foreach (var baseModPair in baseMods)
            {
                var baseMod = baseModPair.Value;
                if (!OcsIOService.Default.TryReadDataFile(baseMod.FullName, out var referenceData))
                {
                    Console.Error.WriteLine($"Could not read {baseMod.FullName}");
                    continue;
                }

                foreach (var item in referenceData.Items)
                {
                    items.Add(item);
                }
            }

            var typesToIgnore = new ItemType[]
            {
                ItemType.DialogAction,
                ItemType.Attachment,
                ItemType.FoliageMesh,
                ItemType.DialogueLine,
                ItemType.FoliageLayer,
                ItemType.MapFeatures,
                ItemType.BuildingPart,
                ItemType.AnimalAnimation,
                ItemType.MaterialSpecsClothing,
            };

            var groupedItems = items
                .Where(item => !typesToIgnore.Contains(item.Type))
                .GroupBy(item => item.Type);

            this.GroupedItems = new ObservableCollection<IGrouping<ItemType, Item>>(groupedItems);
            

            //foreach (var group in groupedItems)
            //{
            //    if (typesToIgnore.Contains(group.Key))
            //    {
            //        continue;
            //    }

            //    foreach (var item in group)
            //    {
            //        Console.WriteLine($"--- {group.Key}::{item.Name}");
            //    }
            //}
        }
    }
}
