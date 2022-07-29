using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.DataLayer.Entities;

public class Order
{
    public int Id { get; set; } 
    public decimal Cost { get; set; }
    public SessionDuration Duration { get; set; }
    public string Message { get; set; }
    public DateTime SessionDate { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime PayDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public OrderPaymentStatus OrderPaymentStatus { get; set; }
    public bool IsDeleted { get; set; }


    public Client Client { get; set; }
    public Psychologist Psychologist { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is null
                || Id != ((Order)obj).Id
                || Client != ((Order)obj).Client
                || Psychologist != ((Order)obj).Psychologist
                || Cost != ((Order)obj).Cost
                || Duration != ((Order)obj).Duration
                || Message != ((Order)obj).Message
                || SessionDate != ((Order)obj).SessionDate
                || OrderDate != ((Order)obj).OrderDate
                || PayDate != ((Order)obj).PayDate
                || OrderDate != ((Order)obj).OrderDate
                || OrderStatus != ((Order)obj).OrderStatus
                || OrderPaymentStatus != ((Order)obj).OrderPaymentStatus
                || IsDeleted != ((Order)obj).IsDeleted)
            return false;

        return true;
    }
}