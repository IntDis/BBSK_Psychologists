﻿using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.DataLayer.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Extensions
{

    public static class ControllerExtensions
    {
        public static string GetRequestPath(this ControllerBase controller) =>
            $"{controller.Request?.Scheme}://{controller.Request?.Host.Value}{controller.Request?.Path.Value}";


        public static ClaimModel GetClaims(this ControllerBase controller)
        {
            ClaimModel claimModel = new();
            if (controller.User is not null)
            {
                var claims = controller.User.Claims.ToList();
                claimModel.Email = claims[0].Value;
                claimModel.Role = Enum.Parse<Role>(claims[1].Value);
                claimModel.Id = Int32.Parse(claims[2].Value);
            }

            return claimModel;
        }
    }
}