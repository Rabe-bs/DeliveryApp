using DeliveryApp.DeliveryApp.Domain.Common;
using DeliveryApp.DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Enums;
using System.Net.NetworkInformation;

namespace DeliveryApp.Domain.Entities
{
    public class DeliveryTrip : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public Guid DeliveryGuyId { get; set; }
        // الإحداثيات   
        public decimal CurrentLatitude { get; set; } // (X)
        public decimal CurrentLongitude { get; set; } //   (Y)
        // الحالة العامة للرحلة
        public TripStatus Status { get; set; } = TripStatus.Pending;
        public bool IsCompleted { get; set; } = false;
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PickedUpAt { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }

        //  لضمان اتساق البيانات
        public void MarkAsPickedUp(decimal latitude, decimal longitude)
        {
            Status = TripStatus.PickedUp;
            PickedUpAt = DateTime.UtcNow;
            CurrentLatitude = latitude;
            CurrentLongitude = longitude;
        }

        public void CompleteTrip(decimal latitude, decimal longitude)

        {
            Status = TripStatus.Completed;
            DeliveredAt = DateTime.UtcNow;
            IsCompleted = true;
            CurrentLatitude = latitude;
            CurrentLongitude = longitude;
        }

        public void CancelTrip()
        {
            Status = TripStatus.Cancelled;
            CancelledAt = DateTime.UtcNow;
            IsCompleted = true;
        }
    }
}