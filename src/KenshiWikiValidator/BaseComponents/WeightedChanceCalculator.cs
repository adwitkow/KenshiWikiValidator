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
    public class WeightedChanceCalculator
    {
        public double Calculate(int tries, int successes, double chance)
        {
            return GetBinomialCoefficient(tries, successes)
                * Math.Pow(chance, successes)
                * Math.Pow(1 - chance, tries - successes);
        }

        private static long GetBinomialCoefficient(long n, long k)
        {
            // This function gets the total number of unique combinations based upon N and K.
            // N is the total number of items.
            // K is the size of the group.
            // Total number of unique combinations = N! / ( K! (N - K)! ).
            // This function is less efficient, but is more likely to not overflow when N and K are large.
            // Taken from:  http://blog.plover.com/math/choose.html
            long r = 1;
            long d;
            if (k > n)
            {
                return 0;
            }

            for (d = 1; d <= k; d++)
            {
                r *= n--;
                r /= d;
            }

            return r;
        }
    }
}
