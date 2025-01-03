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

using OpenConstructionSet.Data;
using OpenConstructionSet.Mods;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class ItemsCulture : ItemBase
    {
        public ItemsCulture(ModItem item)
            : base(item)
        {
            this.ForbiddenItems = Enumerable.Empty<ItemReference<Item>>();
            this.IllegalBuildings = Enumerable.Empty<ItemReference<Building>>();
            this.IllegalGoods = Enumerable.Empty<ItemReference<Item>>();
            this.TradePrices = Enumerable.Empty<ItemReference<Item>>();
        }

        public override ItemType Type => ItemType.ItemsCulture;

        [Reference("forbidden items")]
        public IEnumerable<ItemReference<Item>> ForbiddenItems { get; set; }

        [Reference("illegal buildings")]
        public IEnumerable<ItemReference<Building>> IllegalBuildings { get; set; }

        [Reference("illegal goods")]
        public IEnumerable<ItemReference<Item>> IllegalGoods { get; set; }

        [Reference("trade prices")]
        public IEnumerable<ItemReference<Item>> TradePrices { get; set; }
    }
}