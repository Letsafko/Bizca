﻿namespace Bizca.User.Domain
{
    using System;
    using System.Linq;

    public static class ChannelCodeConfirmationGenerator
    {
        private static readonly Random Random = new Random();
        public static string GetCodeConfirmation(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}