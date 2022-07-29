
using BBSK_Psycho.DataLayer.Enums;
namespace BBSK_Psycho.BusinessLayer
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public override bool Equals(object? obj)
        {
            if(Role == ((ClaimModel)obj).Role)
            {
                return true;
            }

            return false;
        }
    }
}