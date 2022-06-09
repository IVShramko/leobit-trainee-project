﻿using Dating.Logic.DTO;
using Dating.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dating.Logic.Repositories
{
    public interface IUserProfileRepository
    {
        public Task<UserProfileFullDTO> GetFullUserDataAsync(string aspNetUserId);

        public Task<UserProfileMainDTO> GetMainUserDataAsync(string aspNetUserId);

        public Task<ICollection<SearchResultDTO>> GetProfilesOnCriteria(CriteriaDTO criteria);

        public void SaveUserData(UserProfile profile);

        public void UpdateUserData(UserProfile profile);
    }
}