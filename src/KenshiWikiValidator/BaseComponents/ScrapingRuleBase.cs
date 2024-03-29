﻿// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
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

using System.Diagnostics.CodeAnalysis;

namespace KenshiWikiValidator.BaseComponents
{
    [ExcludeFromCodeCoverage]
    public abstract class ScrapingRuleBase : IValidationRule
    {
        protected abstract string FileName { get; }

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var lines = this.GetLines(title, content, data);

            var directory = Path.Combine("output", "scraping");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var output = Path.Combine(directory, $"{this.FileName}.txt");
            File.AppendAllLines(output, lines);

            return new RuleResult();
        }

        protected abstract string[] GetLines(string title, string content, ArticleData data);
    }
}
