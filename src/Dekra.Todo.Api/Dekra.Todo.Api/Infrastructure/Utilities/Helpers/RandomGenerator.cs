namespace Dekra.Todo.Api.Infrastructure.Utilities.Helpers
{
    public static class RandomGenerator
    {
        public static Random Random = new Random();

        /// <summary>
        /// Random to choose a gift for current user base on current available gifts.
        /// </summary>
        /// <param name="currentUserId">Current user Id.</param>
        /// <param name="remainingUnreceivedGiftUserIds">List of userids that is pending to receive gifts.</param>
        /// <param name="remainingUserIdsHasAvailableGift">list of userId that has  available gifts to give away.</param>
        /// <returns>Return the user Id that will give the gift to current user.</returns>
        public static string RandomGift(string currentUserId, List<string> remainingUnreceivedGiftUserIds, List<string> remainingUserIdsHasAvailableGift)
        {
            if (remainingUserIdsHasAvailableGift.Count() <= 0)
            {
                throw new ArgumentException(message: "out of gift for user", paramName: nameof(remainingUserIdsHasAvailableGift));
            }

            string selectedGift;

            // Move out gift of current user in the available giveaway gifts
            var selectableGifts = remainingUserIdsHasAvailableGift.Where(gift => gift != currentUserId).ToList();

            if (selectableGifts.Count() <= 0)
            {
                throw new Exception("Out of gift");
            }

            // Suittable gifts for current user to avoid that next users may receive their own gift if current user choose unsuitable gifts.
            var giftsOfPendingUsers = selectableGifts.Where(gift => remainingUnreceivedGiftUserIds.Select(x => x).Contains(gift)).ToList();

            if (giftsOfPendingUsers.Count() > 0)
            {
                selectedGift = giftsOfPendingUsers[Random.Next(giftsOfPendingUsers.Count())];
            }
            else
            {
                selectedGift = selectableGifts[Random.Next(selectableGifts.Count())];
            }

            return selectedGift;
        }

        /// <summary>
        /// Give out yes or no based on percentage input.
        /// </summary>
        /// <param name="percentage">percentage.</param>
        /// <returns>Return yes or no.</returns>
        public static bool PercentageRandom(int percentage)
        {
            if (percentage <= 0 && percentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(percentage));
            }

            return Random.Next(0, 100) <= percentage;
        }
    }
}
