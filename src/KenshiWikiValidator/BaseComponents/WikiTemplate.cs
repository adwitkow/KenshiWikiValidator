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

namespace KenshiWikiValidator.BaseComponents
{
    public class WikiTemplate
    {
        public WikiTemplate(string name)
            : this(name, new SortedSet<string>(), new SortedList<string, string?>())
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public WikiTemplate(string name, SortedList<string, string?> parameters)
            : this(name, new SortedSet<string>(), parameters)
        {
            this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public WikiTemplate(string name, SortedSet<string> unnamedParameters)
            : this(name, unnamedParameters, new SortedList<string, string?>())
        {
            this.UnnamedParameters = unnamedParameters ?? throw new ArgumentNullException(nameof(unnamedParameters));
        }

        public WikiTemplate(string name, SortedSet<string> unnamedParameters, SortedList<string, string?> parameters)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            this.UnnamedParameters = unnamedParameters ?? throw new ArgumentNullException(nameof(unnamedParameters));
        }

        public string Name { get; set; }

        public SortedSet<string> UnnamedParameters { get; private set; }

        public SortedList<string, string?> Parameters { get; private set; }

        public override bool Equals(object? obj)
        {
            var template = obj as WikiTemplate;

            if (template is null)
            {
                return false;
            }

            if (!template.Name.Equals(this.Name))
            {
                return false;
            }

            if (!AreParametersEqual(this.Parameters, template.Parameters))
            {
                return false;
            }

            if (!AreParametersEqual(template.Parameters, this.Parameters))
            {
                return false;
            }

            if (!this.UnnamedParameters.SequenceEqual(template.UnnamedParameters))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = this.Name.GetHashCode();

            foreach (var parameterPair in this.Parameters)
            {
                if (parameterPair.Value is not null)
                {
                    hash *= parameterPair.Key.GetHashCode();
                    hash *= parameterPair.Value.GetHashCode();
                }
            }

            foreach (var parameter in this.UnnamedParameters)
            {
                hash *= parameter.GetHashCode();
            }

            return hash;
        }

        private static bool AreParametersEqual(SortedList<string, string?> sorted1, SortedList<string, string?> sorted2)
        {
            var nonNullParameters = sorted1.Where(pair => !string.IsNullOrEmpty(pair.Value));
            foreach (var parameterPair in nonNullParameters)
            {
                var otherContainsParam = sorted2.TryGetValue(parameterPair.Key, out var otherValue);

                if (!otherContainsParam)
                {
                    return false;
                }

                if (!parameterPair.Value!.Equals(otherValue))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
