using System;

namespace TonieCloudApiClient.Models
{
    public class MeModel
    {
        public string EMail { get; set; }
        public Guid Uuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public bool AcceptedTermsOfUse { get; set; }
        public bool Tracking { get; set; }
        public string AuthCode { get; set; }
        public string ProfileImage { get; set; }
        public bool IsVerified { get; set; }
        public string Locale { get; set; }
        public bool IsEduUser { get; set; }
        public int NotificationCount { get; set; }
        public bool RequiresVerificationToUpload { get; set; }

        //        {"email":"batzi.01+tonies@mailbox.org",
        //        "uuid":"152f56d7-fb3f-43fa-a0b3-07648283f1ab",
        //        "firstName":"Marcus",
        //        "lastName":"Radlach",
        //        "sex":"m",
        //        "acceptedTermsOfUse":true,
        //        "tracking":false,
        //        "authCode":"ITC0U",
        //        "profileImage":"igel.svg",
        //        "isVerified":true,
        //        "locale":"de",
        //        "isEduUser":false,
        //        "notificationCount":0,
        //        "requiresVerificationToUpload":true}
        //}
    }
}
