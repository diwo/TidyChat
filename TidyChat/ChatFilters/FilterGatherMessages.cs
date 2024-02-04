using System;
using System.Linq;

namespace TidyChat;

public sealed class FilterGatherMessages
{
    public static bool IsFiltered(string input, Configuration configuration)
    {
        try
        {
            if (
                (configuration.HideGatheringYield &&
                 L10N.Get(ChatStrings.LocationAffects).All(input.Contains) &&
                 L10N.Get(ChatStrings.GatheringYield).All(input.Contains)) ||
                (configuration.HideGatherersBoon &&
                 L10N.Get(ChatStrings.LocationAffects).All(input.Contains) &&
                 L10N.Get(ChatStrings.GatherersBoon).All(input.Contains)) ||
                (configuration.HideGatheringAttempts &&
                 L10N.Get(ChatStrings.LocationAffects).All(input.Contains) &&
                 L10N.Get(ChatStrings.GatheringAttempts).All(input.Contains)) ||
                L10N.Get(ChatRegexStrings.GatheringStartEnd).IsMatch(input)
            )
                return true;
            if (
                (configuration.ShowGatheringSenses && L10N.Get(ChatRegexStrings.SpideySenses).IsMatch(input)) ||
                (configuration.ShowLocationAffects && L10N.Get(ChatStrings.LocationAffects).All(input.Contains)) ||
                (configuration.ShowCaughtFish && L10N.Get(ChatStrings.AddedToFishGuide).All(input.Contains)) ||
                (configuration.ShowCurrentFishingHole &&
                 L10N.Get(ChatRegexStrings.CurrentFishingHole).IsMatch(input)) ||
                (configuration.ShowDiscoveredFishingHole &&
                 L10N.Get(ChatRegexStrings.DiscoveredFishingHole).IsMatch(input)) ||
                L10N.Get(ChatRegexStrings.DiscoveredFishingHoleARR).IsMatch(input) ||
                L10N.Get(ChatRegexStrings.DataOnFishinghole).IsMatch(input) || (configuration.ShowMeasuringIlms &&
                    L10N.Get(ChatStrings.MeasuringIlms).All(input.Contains))
            )
                return false;

            if (L10N.Get(ChatRegexStrings.FishMooch).IsMatch(input)) return false;

            return true;
        }
        catch (Exception e)
        {
            TidyChat.Log.Debug("Encountered error: " + e);
            return true;
        }
    }
}