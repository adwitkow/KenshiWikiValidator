﻿using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.WikiTemplates;
using KenshiWikiValidator.Features.WikiTemplates.Creators;

namespace KenshiWikiValidator.Features.ArticleValidation.Weapon.Rules
{
    internal class ContainsWeaponTemplateRule : ContainsTemplateRuleBase
    {
        private readonly IItemRepository itemRepository;

        public ContainsWeaponTemplateRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        protected override WikiTemplate PrepareTemplate(ArticleData data)
        {
            var weaponTemplateCreator = new WeaponWikiTemplateCreator(this.itemRepository, data);

            return weaponTemplateCreator.Generate();
        }
    }
}