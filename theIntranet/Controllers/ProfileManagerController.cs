using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Diagnostics;
using theIntranet.Helpers;
using theIntranet.Models;

namespace theIntranet.Controllers
{
    public class ProfileManagerController : Controller
    {
        public ProfileManagerController()
        {
            ApiHelper.InitializeClient();
        }

        public IActionResult Index()
        {
            return View();
            //GetProfile();
        }

        public static async Task<ProfileModel> LoadDetails()
        {
            string url = "https://graph.microsoft.com/v1.0/me";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ProfileModel profile = await response.Content.ReadAsAsync<ProfileModel>();

                    return profile;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<ProfileModel> GetProfile()
        {
            var profile = await LoadDetails();
            return profile;
        }
    }
}